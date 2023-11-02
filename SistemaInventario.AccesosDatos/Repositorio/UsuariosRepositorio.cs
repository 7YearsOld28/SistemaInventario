using SistemaInventario.AccesosDatos.Data;
using SistemaInventario.AccesosDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using SistemaInventario.Modelos.ViewMoldels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesosDatos.Repositorio
{
    public class UsuariosRepositorio : Repositorio<UsuarioAplicacion>, IUsuariosRepositorio
    {
        private readonly ApplicationDbContext _db;
        public UsuariosRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(UsuarioAplicacion usuarioAplicacion)
        {
            
        }
    }
}
