using DataAccess.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class TechTestDbContext : DbContext
    {
        private readonly IConfiguration config;
        public DbSet<DbLegalMatter> Matter { get; set; }
        public DbSet<DbLawyer> Lawyer { get; set; }


        public TechTestDbContext(IConfiguration config)
        {
            this.config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(config.GetConnectionString("sqlConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbLegalMatter>()
                .HasOne(d => d.Lawyer)
                .WithMany(d => d.LegalMatters)
                .HasPrincipalKey(d => d.Id)
                .HasForeignKey(d => d.LawyerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
