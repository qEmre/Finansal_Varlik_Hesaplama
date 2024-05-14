using indexTable.Models;
using Microsoft.EntityFrameworkCore;

namespace indexTable.DataLayer
{
    public class ProjeDbContext : DbContext
    {
        public ProjeDbContext(DbContextOptions<ProjeDbContext> options) : base(options)
        {

        }
        public DbSet<kurBilgi> kurBilgis { get; set; }
        public DbSet<varlikBilgi> varlikBilgis { get; set; }
        public DbSet<ufeEndeks> tufeEndeks { get; set; }
    }
}