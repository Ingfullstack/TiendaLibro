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
    public class AlmacenRepositorio : Repositorio<Almacen>, IAlmacenRepositorio
    {
        private readonly ApplicationDbContext _db;

        public AlmacenRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task ActualizarAlmacen(Almacen almacen)
        {
            var obj = await _db.Almacen.FirstOrDefaultAsync(x => x.Id == almacen.Id);
            if (obj != null)
            {
                obj.Nombre = almacen.Nombre;
                obj.Descripcion = almacen.Descripcion;
                obj.Estado = almacen.Estado;
            }
        }

        public async Task<bool> ExisteAlmacen(string nombre)
        {
            return await _db.Almacen.AnyAsync(x => x.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
        }
    }
}
