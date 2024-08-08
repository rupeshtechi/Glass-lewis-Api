using GlassLewis.Company.Api.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace GlassLewis.Company.Api.Repository.Context
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Companies> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
