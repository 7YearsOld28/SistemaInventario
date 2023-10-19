using SistemaInventario.AccesosDatos.Data;
using SistemaInventario.AccesosDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesosDatos.Repositorio
{
    public class CategoriaRepositorio : Repositorio<Categoría>, ICategoriaRepositorio
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepositorio(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }
        
        public void Actualizar(Categoría categoría)
        {
            var categoriaDb = _db.Categorías.FirstOrDefault(b => b.Id == categoría.Id);
            if (categoriaDb != null)
            {
                categoriaDb.Nombre = categoría.Nombre;
                categoriaDb.Estado = categoría.Estado;
            }
        }
    }
}
