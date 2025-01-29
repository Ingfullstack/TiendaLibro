using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaLibro.AccesoDatos.Data;
using TiendaLibro.Modelo.Models;
using TiendaLibroAccesoDatos.Repositorio.IRepositorio;

namespace TiendaLibroAccesoDatos.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task ActualizarCategoria(Categoria categoria)
        {
            var obj = await _db.Categorias.FirstOrDefaultAsync(x => x.Id == categoria.Id);
            if (obj != null)
            {
                obj.Nombre = categoria.Nombre;
                obj.Descripcion = categoria.Descripcion;
                obj.Estado = categoria.Estado;
            }
        }

        public async Task<bool> ExisteCategoria(string nombre)
        {
            return await _db.Categorias.AnyAsync(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());

        }
    }
}
