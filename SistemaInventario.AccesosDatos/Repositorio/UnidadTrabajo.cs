﻿using SistemaInventario.AccesosDatos.Data;
using SistemaInventario.AccesosDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesosDatos.Repositorio
{
    public class UnidadTrabajo :IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;
        public IBodegaRepositorio Bodega {  get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Bodega = new BodegaRepositorio(_db);
        }
        public void Guardar() 
        {
            _db.SaveChanges();
        }

        public void Dispose() 
        {
            _db.Dispose();
        }
    }
}
