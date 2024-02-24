using aplicacionExamen.Models;
using Microsoft.EntityFrameworkCore;

namespace aplicacionExamen.Data
{
    public class ElementoDbContext: DbContext
    {
        public ElementoDbContext(DbContextOptions<ElementoDbContext> options) : base(options)
            { 
            }

            public DbSet<Elemento> Elements { get; set; }




    }
}
