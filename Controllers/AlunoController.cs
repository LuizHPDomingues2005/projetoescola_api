using Microsoft.AspNetCore.Mvc;
using ProjetoAPIEscola.Models;
using ProjetoAPIEscola.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ProjetoAPIEscola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AlunoController : ControllerBase
    {
        private EscolaContext _context;
        public AlunoController(EscolaContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<List<Aluno>> GetAll()
        {
            return _context.Aluno.ToList();
        }

        [HttpGet("{AlunoidAluno}")]
        public ActionResult<List<Aluno>> Get(int AlunoidAluno)
        {
            try
            {
                var result = _context.Aluno.Find(AlunoidAluno);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> post(Aluno model)
        {
            try
            {
                _context.Aluno.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    //return Ok();
                    return Created($"/api/aluno/{model.ra}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();

        }

        [HttpDelete("{AlunoidAluno}")]
        public async Task<ActionResult> delete(int AlunoidAluno)
        {
            try
            {
                //verifica se existe aluno a ser excluído
                var aluno = await _context.Aluno.FindAsync(AlunoidAluno);
                if (aluno == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(aluno);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

        [HttpPut("{AlunoidAluno}")]
        public async Task<IActionResult> put(int AlunoidAluno, Aluno dadosAlunoAlt)
        {
            try
            {
                //verifica se existe aluno a ser alterado
                var result = await _context.Aluno.FindAsync(AlunoidAluno);
                if (AlunoidAluno != result.idAluno)
                {
                    return BadRequest();
                }
                result.ra = dadosAlunoAlt.ra;
                result.nomeAluno = dadosAlunoAlt.nomeAluno;
                result.codCurso = dadosAlunoAlt.codCurso;
                await _context.SaveChangesAsync();
                return Created($"/api/aluno/{dadosAlunoAlt.ra}", dadosAlunoAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

    }
}
