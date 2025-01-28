using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaLibro.Modelo.Models;

namespace TiendaLibroAccesoDatos.Repositorio.IRepositorio
{
    public interface IAlmacenRepositorio : IRepositorio<Almacen>
    {
        Task ActualizarAlmacen(Almacen almacen);
        Task<bool> ExisteAlmacen(string nombre);
    }
}
