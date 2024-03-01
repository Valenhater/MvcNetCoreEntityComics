using Microsoft.EntityFrameworkCore;
using MvcNetCoreEntityComics.Models;

namespace MvcNetCoreEntityComics.data
{
    public class ComicContext : DbContext
    {
        public ComicContext(DbContextOptions<ComicContext> options) : base(options) { }

        public DbSet <ComicView> ComicsView { get; set; }

    }
}
