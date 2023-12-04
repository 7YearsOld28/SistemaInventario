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
    public class CategoriasController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        public CategoriasController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            Categoría categoría = new Categoría();
            if (id == null)
            {
                //Nuevo registro (Insert)

                return View(categoría);
            }

            categoría = _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
            if (categoría == null)
            {
                return NotFound();
            }
            
            return View(categoría);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(Categoría categoría)
        {
            if (ModelState.IsValid)
            {
                if (categoría.Id == 0)
                {
                    _unidadTrabajo.Categoria.Agregar(categoría);
                }
                else
                {
                    _unidadTrabajo.Categoria.Actualizar(categoría);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            return View(categoría);
        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos() 
        {
            var todos = _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new {data = todos});
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var categoriaDB = _unidadTrabajo.Categoria.Obtener(id);
            if (categoriaDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            _unidadTrabajo.Categoria.Remover(categoriaDB);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoría Borrada Exitosamente" });
        }
        #endregion
    }
}
