using ConsidTechnicalBackend.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsidTechnicalBackend.Database
{
    public class ConsidContext : DbContext
    {
        //public ConsidContext(DbContextOptions<ConsidContext> options) : base(options)
        //{

        //}
        public ConsidContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("name=ConnectionStrings:DefaultConnection");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                     .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                     .AddJsonFile("appsettings.json")
                     .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbEmployees>()
                .HasIndex(e => e.IsCEO)
                .IsUnique()
                .HasFilter("[IsCEO] = 1");

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

            SeedDatabase(modelBuilder);
        }

        public DbSet<DbLibraryItem> LibraryItems { get; set; }
        public DbSet<DbCategory> Categories { get; set; }
        public DbSet<DbEmployees> Employees { get; set; }

        private static void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbCategory>().HasData(
                 new DbCategory
                 {
                     Id = 1,
                     CategoryName = "Action"
                 },
                 new DbCategory
                 {
                     Id = 2,
                     CategoryName = "Comedy"
                 },
                 new DbCategory
                 {
                     Id = 3,
                     CategoryName = "Sci-Fi"
                 }
            );

            modelBuilder.Entity<DbLibraryItem>().HasData(
                new DbLibraryItem
                {
                    Id = 1,
                    CategoryId = 1,
                    Title = "The Short Man",
                    Author = "John Doe",
                    IsBorrowable = true,
                    Pages = 50,
                    Type = "Book"
                },
                new DbLibraryItem
                {
                    Id = 2,
                    CategoryId = 1,
                    Title = "The Idiot",
                    IsBorrowable = true,
                    RunTimeMinutes = 120,
                    Type = "DVD"
                },
                 new DbLibraryItem
                 {
                     Id = 3,
                     CategoryId = 2,
                     Title = "The Shark",
                     IsBorrowable = true,
                     RunTimeMinutes = 100,
                     Type = "Audio Book"
                 },
                 new DbLibraryItem
                 {
                     Id = 4,
                     CategoryId = 3,
                     Title = "The cooking book",
                     Author = "Jane Doe",
                     IsBorrowable = false,
                     Pages = 200,
                     Type = "Reference Book"
                 }

            );

            modelBuilder.Entity<DbEmployees>().HasData(
                new DbEmployees
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    IsCEO = true,
                    IsManager = false,
                    Salary = 50,
                },
                new DbEmployees
                {
                    Id = 2,
                    FirstName = "Alex",
                    LastName = "Fernandez",
                    IsCEO = false,
                    IsManager = true,
                    Salary = 25,
                    ManagerId = 1,
                },
                 new DbEmployees
                 {
                     Id = 3,
                     FirstName = "Jane",
                     LastName = "Doe",
                     IsCEO = false,
                     IsManager = true,
                     Salary = 20,
                     ManagerId = 2,
                 },
                 new DbEmployees
                 {
                     Id = 4,
                     FirstName = "Harry",
                     LastName = "Andersson",
                     IsCEO = false,
                     IsManager = false,
                     Salary = 12,
                     ManagerId = 2,
                 },
                 new DbEmployees
                 {
                     Id = 5,
                     FirstName = "Lucas",
                     LastName = "Swan",
                     IsCEO = false,
                     IsManager = false,
                     Salary = 15,
                     ManagerId = 2,
                 }
            );
        }
    }
}
