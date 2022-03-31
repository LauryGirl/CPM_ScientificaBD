using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Inquest
    {
        [Key]
        public int InquestId { get; set; }
        
        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "Debe introducir {0}")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Atiende")]
        public string Receiver { get; set; }

        [Display(Name = "Centro")]
        public string Center { get; set; }

        [Display(Name = "Recomendación")]
        public string Recommendation { get; set; }

        [Display(Name = "Tipo")]
        public string _type { get; set; }

        //llave foranea a Product
        [Display(Name = "Nombre de producto")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
