using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class YearlyRevision
    {
        [Key]
        public int YearlyRevisionId { get; set; }

        [Required(ErrorMessage = "Debe introducir {0}")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }

        [Display(Name = "Límite del Tiempo de Vida")]
        [Required(ErrorMessage = "Debe introducir {0}")]
        [DataType(DataType.Date)]
        public DateTime LimitRev { get; set; }

        //llave foranea a Product
        [Display(Name = "Nombre del Producto")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
