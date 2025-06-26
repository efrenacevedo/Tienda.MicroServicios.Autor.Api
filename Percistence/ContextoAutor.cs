using Microsoft.EntityFrameworkCore;

namespace Tienda.MicroServicios.Autor.Api.Percistence
{
    public class ContextoAutor : DbContext
    {
        public ContextoAutor(DbContextOptions<ContextoAutor> options) : base(options)
        {
        }
        public DbSet<Model.AutorLibro> AutorLibros { get; set; }
        public DbSet<Model.GradoAcademico> GradosAcademicos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.AutorLibro>()
                .HasMany(a => a.GradosAcademicos)
                .WithOne(g => g.AutorLibro)
                .HasForeignKey(g => g.AutorLibroId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
