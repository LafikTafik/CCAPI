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
        public DbSet<Vehicle> Vehicles { get; set; } = null!;
        public DbSet<Driver> Drivers { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Только если таблицы называются не так, как классы
            modelBuilder.Entity<Client>().ToTable("Clients");
            modelBuilder.Entity<Orders>().ToTable("Order");
            modelBuilder.Entity<Cargos>().ToTable("Cargo");
            modelBuilder.Entity<Transportation>().ToTable("Transportations");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
            modelBuilder.Entity<Driver>().ToTable("Drivers");

            // Связи
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.IDClient);

            modelBuilder.Entity<Cargos>()
                .HasOne(c => c.Order)
                .WithMany(o => o.Cargos)
                .HasForeignKey(c => c.OrderId);


            modelBuilder.Entity<Transportation>()
                .HasOne<Cargos>(t => t.Load)
                .WithMany()
                .HasForeignKey(t => t.CargoID);

            modelBuilder.Entity<Transportation>()
                .HasOne<Vehicle>(t => t.Vehicle)
                .WithMany()
                .HasForeignKey(t => t.VehicleId);


            base.OnModelCreating(modelBuilder);
        }

    }
    
}