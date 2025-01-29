using Microsoft.AspNetCore.Mvc;
using TiendaLibro.Modelo.Models;
using TiendaLibroAccesoDatos.Repositorio.IRepositorio;

namespace TiendaLibro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public CategoriasController(IUnidadTrabajo unidadTrabajo)
        {
            this.unidadTrabajo = unidadTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Agregar()
        {
            Categoria categoria = new Categoria()
            {
                Estado = true
            };
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(Categoria categoria)
        {
            var existeCategoria = await unidadTrabajo.Categoria.ExisteCategoria(categoria.Nombre);
            if (existeCategoria)
            {
                ModelState.AddModelError("nombre", "La categoria ya existe");
                TempData["error"] = "Categoria Existe";
            }

            if (ModelState.IsValid)
            {
                await unidadTrabajo.Categoria.Agregar(categoria);
                await unidadTrabajo.Guardar();
                TempData["success"] = "Categoria Creada";
                return RedirectToAction("Index","Categorias");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var categoria = await unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
            if (categoria is null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                await unidadTrabajo.Categoria.ActualizarCategoria(categoria);
                await unidadTrabajo.Guardar();
                TempData["success"] = "Categoria Actualizada";
                return RedirectToAction("Index", "Categorias");
            }
            return View();
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id is null || id == 0)
            {
                return Json(new { success = false, message = " No existe la categoria" });
            }
            var categoria = await unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());

            if (categoria is null)
            {
                return Json(new { success = false, message = " Error al eliminar" });
            }
            unidadTrabajo.Categoria.Eliminar(categoria);
            await unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoria Eliminada"});
        }
        #endregion
    }
}
