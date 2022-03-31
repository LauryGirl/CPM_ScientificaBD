using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace Data
{
    public class AuthorityTempMarket : Register
    {
        [Display(Name = "Documentación")]
        public string Documentation { get; set; }

        //llave foranea a Product
        public virtual Product Product { get; set; }

        [Display(Name = "Tiempo de vida en años")]
        public int _montoYear { get; set; }
    }
}
