using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesosDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System.Drawing.Text;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BodegasController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public BodegasController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Bodega bodega = new Bodega();
            if (id == null)
            {
                //Nuevo registro (Insert)

                return View(bodega);
            }

            bodega = _unidadTrabajo.Bodega.Obtener(id.GetValueOrDefault());
            if (bodega == null)
            {
                return NotFound();
            }
            
            return View(bodega);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                if (bodega.Id == 0)
                {
                    _unidadTrabajo.Bodega.Agregar(bodega);
                }
                else
                {
                    _unidadTrabajo.Bodega.Actualizar(bodega);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(bodega);
        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos() 
        {
            var todos = _unidadTrabajo.Bodega.ObtenerTodos();
            return Json(new {data = todos});
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var bodegaDB = _unidadTrabajo.Bodega.Obtener(id);
            if (bodegaDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            _unidadTrabajo.Bodega.Remover(bodegaDB);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Bodega Borrada Exitosamente" });
        }
        #endregion
    }
}
