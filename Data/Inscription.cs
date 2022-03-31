using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Inscription : Register
    {
        [Display(Name = "Vigencia")]
        public string Validity { get; set; }

        //llave foranea a Product
        public virtual Product Product { get; set; }
    }
}
