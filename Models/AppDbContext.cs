using Microsoft.EntityFrameworkCore;

namespace CCAPI.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Orders> Order { get; set; } = null!;
        public DbSet<Cargos> Cargo { get; set; } = null!;
        public DbSet<Transportation> Transportations { get; set; } = null!;
        public DbSet<Vehicle> Vehicle { get; set; } = null!;
        public DbSet<Driver> Drivers { get; set; } = null!;
        public DbSet<TransComp> TransComp { get; set; } = null!;
        public DbSet<CargoOrders> CargoOrders { get; set; } = null!;
        public DbSet<TransportationCompany> TransportationCompany { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Orders>().ToTable("Order");
            modelBuilder.Entity<Cargos>().ToTable("Cargo");
            modelBuilder.Entity<Transportation>().ToTable("Transportations");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
            modelBuilder.Entity<Driver>().ToTable("Drivers");
            modelBuilder.Entity<TransportationCompany>().ToTable("TransportationCompany");

            // --- Связь: Client → Orders
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.IDClient)
                .OnDelete(DeleteBehavior.Restrict);

            // --- Связь: Order → Transportation 
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Transportation)
                .WithOne()
               .HasForeignKey<Orders>(o => o.TransId)
               .OnDelete(DeleteBehavior.Restrict);

            // --- Связь: Cargo ↔ Order 
            modelBuilder.Entity<CargoOrders>()
                .HasKey(co => new { co.CargoID, co.OrderID });

            modelBuilder.Entity<CargoOrders>()
                .HasOne(co => co.Cargo)
                .WithMany(c => c.Orders)
                .HasForeignKey(co => co.CargoID);

            modelBuilder.Entity<CargoOrders>()
                .HasOne(co => co.Order)
                .WithMany(o => o.Cargos)
                .HasForeignKey(co => co.OrderID);

            // --- Связь: Transportation ↔ TransportationCompany через TransComp
            modelBuilder.Entity<TransComp>()
                .HasKey(tc => new { tc.TransportationID, tc.CompanyID });

            modelBuilder.Entity<TransComp>()
                .HasOne(tc => tc.Transportation)
                .WithMany(t => t.TransComp)
                .HasForeignKey(tc => tc.TransportationID);

            modelBuilder.Entity<TransComp>()
                .HasOne(tc => tc.Company)
                .WithMany(c => c.TransComp)
                .HasForeignKey(tc => tc.CompanyID);

            // --- Связь: Vehicle → TransportationCompany
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Company)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(v => v.TransportationCompanyId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}