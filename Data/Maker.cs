using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Maker
    {
        [Key]
        public int MakerId { get; set; }

        [Display(Name = "Nombre del Fabricante")]
        public string Name { get; set; }

        [Display(Name = "Fecha de la Autorización de la Cámara de Comercio")]
        [DataType(DataType.Date)]
        public DateTime AuthorityOfCommerceCameraDate { get; set; }

        [Display(Name = "Tiempo de Vida de la Autorización de la Cámara de Comercio")]
        public int LifeTimeYearsACC { get; set; }

        [Display(Name = "Autorización de Cámara de Comercio")]
        public string AuthorityOfCommerceCamera { get; set; }

        [Display(Name = "Fecha de la Documentación de Buenas Prácticas")]
        [DataType(DataType.Date)]
        public DateTime GoodPracticesDocumentationDate { get; set; }

        [Display(Name = "Tiempo de Vida de la Documentación de Buenas Prácticas")]
        public int LifeTimeYearsGPD { get; set; }

        [Display(Name = "Documentación de Buenas Prácticas")]
        public string GoodPracticesDocumentation { get; set; }

        //relacion de uno a muchos
        public virtual ICollection<Product> Products { get; set; }
    }

    public class ForeignMaker : Maker
    {
        [Display(Name = "País")]
        public string Country { get; set; }

        [Display(Name = "Fecha del Certificado de Libre Venta")]
        [DataType(DataType.Date)]
        public DateTime CertificateFreeSaleDate { get; set; }

        [Display(Name = "Certificado de Libre Venta")]
        public string CertificateFreeSale { get; set; }

        [Display(Name = "Tiempo de Vida del Certificado de Libre Venta")]
        public int LifeTimeYearsCFS { get; set; }

        //relacion de uno a muchos
        public override ICollection<Product> Products { get; set; }
    }
}
