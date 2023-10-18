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
        #region API
        [HttpGet]

        public IActionResult ObtenerTodos() 
        {
            var todos = _unidadTrabajo.Bodega.ObtenerTodos();
            return Json(new {data = todos});
        }

        #endregion
    }
}
