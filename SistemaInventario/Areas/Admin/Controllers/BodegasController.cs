using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesosDatos.Repositorio.IRepositorio;
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
