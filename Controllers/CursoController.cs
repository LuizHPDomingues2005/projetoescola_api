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
    
    public class CursoController : ControllerBase
    {
        private EscolaContext _context; // definimos o contexo que usaremos nesse controlador
        public CursoController(EscolaContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<List<Curso>> GetAll()
        {
            return _context.Curso.ToList(); // dados serão resgatados em formato de lista
        }

        [HttpGet("{CursoidCurso}")]
        public ActionResult<List<Curso>> Get(int CursoidCurso)
        {

            try
            {
                // criamos a variável result, que retornará o idCurso,
                // caso result seja nulo, ou seja, idCurso é nulo então não foi encontrado
                var result = _context.Curso.Find(CursoidCurso);
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
        public async Task<ActionResult> post(Curso model)
        {
            try
            {
                _context.Curso.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    //return Ok();
                    return Created($"/api/Curso/{model.codigo}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            // retorna BadRequest se não conseguiu incluir
            return BadRequest();

        }

        [HttpDelete("{CursoidCurso}")]
        public async Task<ActionResult> delete(int CursoidCurso)
        {
            try
            {
                //verifica se existe Curso a ser excluído
                var Curso = await _context.Curso.FindAsync(CursoidCurso);
                if (Curso == null)
                {
                    //método do EF
                    return NotFound();
                }
                _context.Remove(Curso);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falhano acesso ao banco de dados.");
            }
        }

        [HttpPut("{CursoidCurso}")]
        public async Task<IActionResult> put(int CursoidCurso, Curso dadosCursoAlt)
        {
            try
            {
                //verifica se existe Curso a ser altecodigodo
                var result = await _context.Curso.FindAsync(CursoidCurso);
                if (CursoidCurso != result.idCurso)
                {
                    return BadRequest();
                }
                result.codigo = dadosCursoAlt.codigo;
                result.nomeCurso = dadosCursoAlt.nomeCurso;
                await _context.SaveChangesAsync();
                return Created($"/api/Curso/{dadosCursoAlt.codigo}", dadosCursoAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }

    }
}
