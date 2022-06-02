using System.ComponentModel.DataAnnotations;

namespace ProjetoAPIEscola.Models
{
    public class Curso
    {

        // declaramos as variáveis da classe Curso

        [Key]

        public int idCurso {get; set;}
        public int? codigo {get;set;}
        public string? nomeCurso {get;set;}
        

    }
}