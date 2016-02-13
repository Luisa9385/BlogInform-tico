using Articulos.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Articulos.DAC
{
    public class ArticuloDbContext : DbContext
    {
        public DbSet<Entities.Articulo> Articulos { get; set; }
        public DbSet<Entities.Comentario> Comentarios { get; set; }
        public ArticuloDbContext()
            : base("Articulos")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Comentario>().ToTable("Comentario").HasKey(c => c.Id);
            modelBuilder.Entity<Articulo>().ToTable("Articulo").HasKey(c => c.Id).HasMany(c => c.Comentarios).WithOptional(c => c.Articulo).HasForeignKey(c => c.Articulo_id);
            
        }
    }
}
