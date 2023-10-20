using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesosDatos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio :IRepositorio<Producto>
    {
        void Actualizar (Producto producto);
    }
}
