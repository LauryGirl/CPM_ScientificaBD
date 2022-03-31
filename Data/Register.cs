using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Register : IRegister
    {
        [Key]
        public int RegisterId { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Debe introducir {0}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Fecha del Nuevo Registro")]
        [Required(ErrorMessage = "Debe introducir {0}")]
        [DataType(DataType.Date)]
        public DateTime NewRegister { get; set; }

        //llave foranea a Product
        [Display(Name = "Nombre del Producto")]
        public int ProductId { get; set; }

    }
}
