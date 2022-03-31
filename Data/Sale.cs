using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [Display(Name = "Centro")]
        public string Center { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Debe introducir {0}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        //llave foranea a Product
        [Display(Name = "Nombre del Producto")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
