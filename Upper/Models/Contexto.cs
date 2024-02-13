using Microsoft.EntityFrameworkCore;

namespace Upper.Models
{
    public class Contexto : DbContext
    {
        public Contexto( DbContextOptions<Contexto> options) : base(options)
        {
            
        }

        public DbSet<Livro> Livro { get; set; }
    }
}
