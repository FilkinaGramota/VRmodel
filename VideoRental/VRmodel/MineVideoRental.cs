using System.Data.Entity;

namespace VideoRental.VRmodel
{
    public class MineVideoRental : DbContext
    {
        public DbSet<Cassette> Cassettes { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }

        public MineVideoRental() : base("MineVideoRental")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // для Name, чтоб столбцы назывались красивенько
            modelBuilder.Entity<Client>().Property(client => client.Name.FirstName)
                .HasColumnName("FirstName");
            modelBuilder.Entity<Client>().Property(client => client.Name.LastName)
                .HasColumnName("LastName");
        }
    }
}
