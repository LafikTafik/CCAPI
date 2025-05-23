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
            // Связь Orders <-> Client
            modelBuilder.Entity<Orders>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.IDClient);

            // Связь Cargos <-> Orders
            modelBuilder.Entity<Cargos>()
                .HasOne(c => c.Order)
                .WithMany(o => o.Cargos)
                .HasForeignKey(c => c.OrderId);

            // 🔥 Связь Transportation -> Cargos
            modelBuilder.Entity<Transportation>()
                .HasOne<Vehicle>(t => t.Vehicle)       // Навигация к транспорту
                .WithMany()                             // Без обратной коллекции
                .HasForeignKey(t => t.VehicleID);      // Через поле VehicleID

            // Связь Transportation -> Cargos
            modelBuilder.Entity<Transportation>()
                .HasOne<Cargos>(t => t.Load)            // Навигация к грузу
                .WithMany()                              // Без обратной связи
                .HasForeignKey(t => t.CargoID);

            base.OnModelCreating(modelBuilder);
        }

    }
    
}