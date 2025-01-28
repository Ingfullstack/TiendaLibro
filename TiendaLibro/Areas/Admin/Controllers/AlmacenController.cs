using Microsoft.AspNetCore.Mvc;
using TiendaLibro.Modelo.Models;
using TiendaLibroAccesoDatos.Repositorio.IRepositorio;

namespace TiendaLibro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AlmacenController : Controller
    {
        private readonly IUnidadTrabajo unidadTrabajo;

        public AlmacenController(IUnidadTrabajo unidadTrabajo)
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
            Almacen almacen = new Almacen()
            {
                Estado = true
            };
            return View(almacen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(Almacen almacen)
        {
            var existeAlmacen = await unidadTrabajo.Almacen.ExisteAlmacen(almacen.Nombre);
            if (existeAlmacen)
            {
                ModelState.AddModelError("nombre", "El almacen ya existe");
                TempData["error"] = "Almacen Existe";
            }

            if (ModelState.IsValid)
            {
                await unidadTrabajo.Almacen.Agregar(almacen);
                await unidadTrabajo.Guardar();
                TempData["success"] = "Almacen Creado";
                return RedirectToAction("Index","Almacen");
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
            var almacen = await unidadTrabajo.Almacen.Obtener(id.GetValueOrDefault());
            if (almacen is null)
            {
                return NotFound();
            }
            return View(almacen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Almacen almacen)
        {
            var existeAlmacen = await unidadTrabajo.Almacen.ExisteAlmacen(almacen.Nombre);
            if (existeAlmacen)
            {
                ModelState.AddModelError("nombre", "El almacen ya existe");
                TempData["error"] = "Almacen Existe";
            }

            if (ModelState.IsValid)
            {
                await unidadTrabajo.Almacen.ActualizarAlmacen(almacen);
                await unidadTrabajo.Guardar();
                TempData["success"] = "Almacen Actualizado";
                return RedirectToAction("Index", "Almacen");
            }
            return View();
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await unidadTrabajo.Almacen.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id is null || id == 0)
            {
                return Json(new { success = false, message = " No existe el almacen" });
            }
            var almacen = await unidadTrabajo.Almacen.Obtener(id.GetValueOrDefault());

            if (almacen is null)
            {
                return Json(new { success = false, message = " Error al eliminar" });
            }
            unidadTrabajo.Almacen.Eliminar(almacen);
            await unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Almacen Eliminado"});
        }
        #endregion
    }
}
