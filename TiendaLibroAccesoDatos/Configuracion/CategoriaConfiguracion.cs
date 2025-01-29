using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaLibro.Modelo.Models;

namespace TiendaLibroAccesoDatos.Configuracion
{
    public class CategoriaConfiguracion : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.Property(x => x.Id)
                   .IsRequired();

            builder.Property(x => x.Nombre)
                   .IsRequired()
                   .HasMaxLength(60);

            builder.Property(x => x.Descripcion)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(x => x.Estado)
                   .IsRequired();
        }
    }
}
