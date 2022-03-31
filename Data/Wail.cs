using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Wail
    {
        [Key]
        public int WailId { get; set; }

        [Display(Name = "Información del Cecmed")]
        public string CecmedInfo { get; set; }

        [Display(Name = "Información del Fabricante")]
        public string MakerInfo { get; set; }

        [Display(Name = "Conclusiones de la Información")]
        public string Closure { get; set; }

        [Display(Name = "Estado")]
        public string _state { get; set; }

        [Display(Name = "Nombre del Producto")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
