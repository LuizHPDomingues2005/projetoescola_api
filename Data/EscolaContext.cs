using Microsoft.EntityFrameworkCore;
using ProjetoAPIEscola.Models;

namespace ProjetoAPIEscola.Data
{
    public class EscolaContext : DbContext
    {
        public EscolaContext(DbContextOptions<EscolaContext> options) : base(options)
        {
        }

        // definimos o contexto de dados: tabelas Aluno e Curso
        public DbSet<Aluno> Aluno { get; set; }
        public DbSet<Curso> Curso {get; set;}
    }
}