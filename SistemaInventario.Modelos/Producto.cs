using System;
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
        public int id { get; set; }

        [Required]
        [MaxLength(30)]
        [Display(Name ="Número de serie")]
        public string NumeroSerie { get; set; }

        [Required]
        [MaxLength(60)]
        [Display(Name ="Descripción")]
        public int Descripcion { get; set; }

        [Required]
        [Range(1,10000)]
        [Display(Name ="Precio")]
        public string Precio { get; set; }

        [Required]
        [Range(1,10000)]
        [Display (Name ="Costo")]
        public string Costo { get; set; }

        public string ImagenUrl { get; set; }

        //Foreign Keys
        [Required]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public Categoría Categoria { get; set;}

        [Required]
        public int MarcaId { get; set; }

        [ForeignKey("MarcaId")]
        public Marca Marca { get; set;}

        //Recursividad

        public int? PadreId { get; set; }
        public virtual Producto Padre{ get; set; }
    }
}
