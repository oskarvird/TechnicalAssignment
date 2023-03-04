using ConsidTechnicalBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsidTechnicalBackend.Database
{
    //TODO: RENAME?
    public class ConsidContext : DbContext
    {
        public ConsidContext(DbContextOptions<ConsidContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbEmployees>()
                .HasIndex(e => e.IsCEO)
                .IsUnique();

            modelBuilder.Entity<DbCategory>()
                .HasIndex(e => e.CategoryName)
                .IsUnique();

            modelBuilder.Entity<DbLibraryItem>()
            .Property(e => e.BorrowDate)
            .HasColumnType("date")
            .HasConversion(
                v => v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Local) : default(DateTime?)
            ); ;

            //TODO: check if enum is a good idea
            //modelBuilder
            //    .Entity<DbLibraryItem>()
            //    .Property(d => d.Type)
            //    .HasConversion(new EnumToStringConverter<Types>());
        }



        public DbSet<DbLibraryItem> LibraryItems { get; set; }
        public DbSet<DbCategory> Categories { get; set; }
        public DbSet<DbEmployees> Employees { get; set; }
    }
}
