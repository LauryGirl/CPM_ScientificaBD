using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Nombre del Producto")]
        public string Name { get; set; }

        [Display(Name = "Registro Presentado")]
        public bool RegisterPresent { get; set; }

        [Display(Name = "Sistema")]
        public string System { get; set; }

        [Display(Name = "Familia")]
        public string Family { get; set; }

        [Display(Name = "Aplicación")]
        public string Application { get; set; }

        [Display(Name = "Presentación")]
        public string Presentation { get; set; }

        [Display(Name = "Ref")]
        public string Ref { get; set; }

        //en edit de product ver el change, poder actualizar el change

        [Display(Name = "Tipo")]
        public string _type { get; set; }

        [Display(Name = "Estado")]
        public string _state { get; set; }

        //llave foranea a Maker, primero crear fabricante
        [Display(Name = "Nombre del Fabricante")]
        public int MakerId { get; set; }
        public virtual Maker Maker { get; set; }

        //llave foranea a Change
        [Display(Name = "Modificaciones")]
        public virtual ICollection<Change> Changes { get; set; }

        //llave foranea a Inquest
        [Display(Name = "Encuestas")]
        public virtual ICollection<Inquest> Inquests { get; set; }

        //llave foranea a Wail
        [Display(Name = "Quejas")]
        public virtual ICollection<Wail> Wails { get; set; }

        //llave foranea a Register
        [Display(Name = "Registros")]
        public virtual ICollection<Register> Registers { get; set; }

        //llave foranea a Sale
        [Display(Name = "Ventas")]
        public virtual ICollection<Sale> Sales { get; set; }

        //llave foranea a YearlyRevision
        [Display(Name = "Revisiones Anuales")]
        public virtual ICollection<YearlyRevision> YearlyRevisions { get; set; }
    }
}
