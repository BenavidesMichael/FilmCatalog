using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.API.DAL
{
    public class FilmCatalogContext : DbContext
    {
        public FilmCatalogContext(DbContextOptions opt)
            : base(opt)
        {
        }

        public DbSet<Genre> Genres { get; set; }
    }
}
