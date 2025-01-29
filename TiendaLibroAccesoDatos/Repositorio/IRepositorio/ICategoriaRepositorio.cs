using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaLibro.Modelo.Models;

namespace TiendaLibroAccesoDatos.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio : IRepositorio<Categoria>
    {
        Task ActualizarCategoria(Categoria categoria);
        Task<bool> ExisteCategoria(string nombre);
    }
}
