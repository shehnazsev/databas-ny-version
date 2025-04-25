using Microsoft.EntityFrameworkCore;

namespace Data.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Kund> Kunder { get; set; }
        public DbSet<Tjanst> Tjanster { get; set; }
        public DbSet<Projekt> Projekt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Projekt>()
                .HasKey(p => p.Projektnummer);

            modelBuilder.Entity<Projekt>()
                .HasOne(p => p.Tjanst)
                .WithMany(t => t.Projekt)
                .HasForeignKey(p => p.TjanstId);

            modelBuilder.Entity<Kund>()
                .HasKey(k => k.Kundnummer);

            //ChatGPT - fyll databas med kund och tjänst då vi inte behöver göra det i frontend
            modelBuilder.Entity<Kund>().HasData(
               new Kund
               {
                   Kundnummer = 1,
                   Namn = "Kalle",
                   Telefonnummer = "0732758912"
               },
               new Kund
               {
                   Kundnummer = 2,
                   Namn = "Anna",
                   Telefonnummer = "0708190298"
               },
               new Kund
               {
                   Kundnummer = 3,
                   Namn = "Pelle",
                   Telefonnummer = "08-1272489"
               });

            modelBuilder.Entity<Tjanst>().HasData(
                new Tjanst
                {
                    TjanstId = 1,
                    Namn = "IT-konsult",
                    PrisPerTimme = 1000
                },
                new Tjanst
                {
                    TjanstId = 2,
                    Namn = "HR-konsult",
                    PrisPerTimme = 2000
                });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ProjektSystemDB;Trusted_Connection=True;");
            }
        }
    }
}
