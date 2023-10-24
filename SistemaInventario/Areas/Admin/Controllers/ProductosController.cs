using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaInventario.AccesosDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Modelos.ViewMoldels;

namespace SistemaInventario.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductosController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment hostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            ProductosVM productoVM = new ProductosVM()
            {
                producto = new Producto(),
                CategoriaLista = _unidadTrabajo.Categoria.ObtenerTodos().Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                }),
                MarcaLista = _unidadTrabajo.Marca.ObtenerTodos().Select(m => new SelectListItem
                {
                    Text = m.Nombre,
                    Value = m.Id.ToString()
                }),
            };
            if (id == null)
            {
                //Nuevo registro (Insert)

                return View(productoVM);
            }

            productoVM.producto = _unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());
            if (productoVM.producto == null)
            {
                return NotFound();
            }
            return View(productoVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Upsert(ProductosVM productoVM)
        {
            if (ModelState.IsValid)
            {
                // Cargar Imágenes
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"imagenes/productos");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (productoVM.producto.ImagenUrl != null)
                    {
                        var imagenPath = Path.Combine(webRootPath, productoVM.producto.ImagenUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagenPath))
                        {
                            System.IO.File.Delete(imagenPath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }
                    productoVM.producto.ImagenUrl = @"\imagenes\productos" + filename + extension;
                }
                else
                {
                    if (productoVM.producto.Id != 0)
                    {
                        Producto productoDB = _unidadTrabajo.Producto.Obtener(productoVM.producto.Id);
                        productoVM.producto.ImagenUrl = productoDB.ImagenUrl;
                    }
                }

                if (productoVM.producto.Id == 0)
                {
                    _unidadTrabajo.Producto.Agregar(productoVM.producto);
                }
                else
                {
                    _unidadTrabajo.Producto.Actualizar(productoVM.producto);
                }
                _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productoVM.CategoriaLista = _unidadTrabajo.Categoria.ObtenerTodos().Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.Id.ToString()
                });
                productoVM.MarcaLista = _unidadTrabajo.Marca.ObtenerTodos().Select(m => new SelectListItem
                {
                    Text = m.Nombre,
                    Value = m.Id.ToString()
                });

                if (productoVM.producto.Id != 0)
                {
                    productoVM.producto = _unidadTrabajo.Producto.Obtener(productoVM.producto.Id);
                }
            }
            return View(productoVM.producto);
        }

        #region API
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var todos = _unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades: "Categoria, Marca");
            return Json(new { data = todos });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var ProductoDB = _unidadTrabajo.Producto.Obtener(id);
            if (ProductoDB == null)
            {
                return Json(new { success = false, message = "Error al Borrar" });
            }
            _unidadTrabajo.Producto.Remover(ProductoDB);
            _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "ProductosBorrada Exitosamente" });
        }
        #endregion
    }
}
