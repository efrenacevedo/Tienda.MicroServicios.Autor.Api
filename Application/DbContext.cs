using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Tienda.MicroServicios.Autor.Api.Application
{
    public class WriteDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public WriteDBContext(DbContextOptions<WriteDBContext> options) : base(options) { }
        public DbSet<Model.AutorLibro>AutorLibros { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.AutorLibro>().ToTable("AutorLibro");
            modelBuilder.Entity<Model.AutorLibro>().HasKey(a => a.AutorLibroGuid);
            modelBuilder.Entity<Model.AutorLibro>().Property(a => a.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Model.AutorLibro>().Property(a => a.Apellido).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Model.AutorLibro>().Property(a => a.FechaNacimiento).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }

    public class ReadDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbConnection CurrentConnection => this.Database.GetDbConnection();
        public ReadDBContext(DbContextOptions<ReadDBContext> options) : base(options) {
            var conn = this.Database.GetDbConnection();
            Console.WriteLine($"[Read DB] conectado a {conn.DataSource}, DB: {conn.Database}");
        }
        public DbSet<Model.AutorLibro> AutorLibros { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.AutorLibro>().ToTable("AutorLibro");
            modelBuilder.Entity<Model.AutorLibro>().HasKey(a=>a.AutorLibroGuid);
            modelBuilder.Entity<Model.AutorLibro>().Property(a => a.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Model.AutorLibro>().Property(a => a.Apellido).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Model.AutorLibro>().Property(a => a.FechaNacimiento).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
