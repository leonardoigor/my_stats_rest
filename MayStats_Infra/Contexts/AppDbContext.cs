using Microsoft.EntityFrameworkCore;
using MyStats_Rest.Models;

namespace MayStats_Infra.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Stats> Stats { get; set; }
        public AppDbContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configuração para MySQL
            var connectionString = "server=localhost;port=33600;database=myStats;user=root;password=root;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}
