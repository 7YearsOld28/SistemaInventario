﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string NumeroSerie { get; set; }

        [Required]
        [MaxLength(60)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [Range(0, 10000)]
        [Display(Name = "Precio")]
        public int Precio { get; set; }

        [Required]
        [Range(0, 10000)]
        [Display(Name = "Costo")]
        public int Costo { get; set; }

        
        public string ImagenUrl { get; set; }

        //Foreign Keys
        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoría Categoria { get; set; }

        [Required]
        public int MarcaId { get; set; }
        [ForeignKey("MarcaId")]
        public Marca Marca { get; set; }

        //Recursividad

        public int? PadreId { get; set; }
        public virtual Producto Padre { get; set; }
    }
}
