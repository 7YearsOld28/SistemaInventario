using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos.ViewMoldels
{
    public class ProductosVM
    {
        public Producto producto { get; set; }
        public IEnumerable<SelectListItem> CategoriaLista{ get; set; }
        public IEnumerable<SelectListItem> MarcaLista { get; set; }
    }
}
