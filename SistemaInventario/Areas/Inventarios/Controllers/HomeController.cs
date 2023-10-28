using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesosDatos.Repositorio;
using SistemaInventario.AccesosDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Modelos.ViewMoldels;
using System.Diagnostics;

namespace SistemaInventario.Areas.Inventarios.Controllers
{
    [Area("Inventarios")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;

       public HomeController(ILogger<HomeController> logger, IUnidadTrabajo unidadTrabajo)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            IEnumerable<Producto> productoLista = _unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades: "Categoria,Marca");
            return View(productoLista);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}