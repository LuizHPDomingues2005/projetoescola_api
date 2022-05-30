using System.ComponentModel.DataAnnotations;

namespace ProjetoAPIEscola.Models
{
    public class Aluno
    {
        [Key]
        public int idAluno { get; set; }
        public string? ra { get; set; }
        public string? nomeAluno { get; set; }
        public int codCurso { get; set; }
    }
}