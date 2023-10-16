using SistemaInventario.AccesosDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesosDatos.Repositorio
{
    public interface IBodegaRepositorio :IRepositorio<Bodega>
    {
        void Actualizar(Bodega bodega);
    }
}
