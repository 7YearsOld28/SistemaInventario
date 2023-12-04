using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesosDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Utilidades;
using System.Drawing.Text;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin)]
    public class MarcasController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public MarcasController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Marca marca = new Marca();
            if (id == null)
            {
                //Nuevo registro (Insert)

                return View(marca);
            }

            marca = _unidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
            if (marca == null)
            {
                return NotFound();
            }  
            return View(marca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Marca marca)
        {
            if (ModelState.IsValid)
            {
                if (marca.Id == 0)
                {
                    _unidadTrabajo.Marca.Agregar(marca);
                }
                else
                {
                    _unidadTrabajo.Marca.Actualizar(marca);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos() 
        {
            var todos = _unidadTrabajo.Marca.ObtenerTodos();
            return Json(new {data = todos});
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var MarcaDB = _unidadTrabajo.Marca.Obtener(id);
            if (MarcaDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            _unidadTrabajo.Marca.Remover(MarcaDB);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Marca Borrada Exitosamente" });
        }
        #endregion
    }
}
