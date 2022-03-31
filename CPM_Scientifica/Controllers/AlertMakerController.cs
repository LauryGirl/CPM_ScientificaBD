using Data;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App = CPM_Scientifica.Context.AppContext;


namespace CPM_Scientifica.Controllers
{
    public class AlertMakerController : Controller
    {
        App _db = new App();
        // GET: AlertMaker
        public ActionResult Index()
        {
            return View();
        }

        public IQueryable<Inscription> GetInsc() => _db.Inscriptions.AsQueryable();

        public JsonResult CleanerM()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Clean = true } };
            return result;
        }

        #region Maker

        public IQueryable<Maker> GetMakers() => _db.Makers.AsQueryable();

        #region ACC

        #region bool

        public JsonResult ACCBoolAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_5 = Get_ACCBoolAlarmQuery() } };
            return result;
        }

        private bool Get_ACCBoolAlarmQuery()
        {
            var m = GetMakers();

            foreach (var mk in m)
            {
                var now = DateTime.UtcNow;
                var limit = new DateTime(mk.AuthorityOfCommerceCameraDate.Year + mk.LifeTimeYearsACC, mk.AuthorityOfCommerceCameraDate.Month, mk.AuthorityOfCommerceCameraDate.Day);
                var dif = limit - now;
                if (dif.TotalDays <= 30) return true;
            }
            return false;
        }

        #endregion

        #region list

        public JsonResult ACCAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_6 = Get_ACCAlarmQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_ACCAlarmQuery()
        {
            var m = GetMakers();
            var result = new List<List<Tuple<string, string>>>();

            foreach (var mk in m)
            {
                var now = DateTime.UtcNow;
                var limit = new DateTime(mk.AuthorityOfCommerceCameraDate.Year + mk.LifeTimeYearsACC, mk.AuthorityOfCommerceCameraDate.Month, mk.AuthorityOfCommerceCameraDate.Day);
                var dif = limit - now;
                if (dif.TotalDays <= 30)
                {
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Nombre del fabricante: ", mk.Name));
                    result2.Add(new Tuple<string, string>("Autorización de Cámara de Comercio: ", mk.AuthorityOfCommerceCamera));
                    result2.Add(new Tuple<string, string>("Fecha de la Autorización de la Cámara de Comercio: ", mk.AuthorityOfCommerceCameraDate.ToString()));
                    result2.Add(new Tuple<string, string>("Tiempo de Vida de la Autorización de la Cámara de Comercio: ", mk.LifeTimeYearsACC.ToString()));
                    result2.Add(new Tuple<string, string>("Documentación de Buenas Prácticas: ", mk.GoodPracticesDocumentation));
                    result2.Add(new Tuple<string, string>("Fecha de la Documentación de Buenas Prácticas: ", mk.GoodPracticesDocumentationDate.ToString()));
                    result2.Add(new Tuple<string, string>("Tiempo de Vida de la Documentación de Buenas Prácticas: ", mk.LifeTimeYearsGPD.ToString()));
                    result.Add(result2);
                }
            }
            return result.AsQueryable();
        }

        #endregion

        #endregion

        #region GDP

        #region bool

        public JsonResult GDPBoolAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_7 = Get_GDPBoolAlarmQuery() } };
            return result;
        }

        private bool Get_GDPBoolAlarmQuery()
        {
            var m = GetMakers();
            var result = new List<Register>();

            foreach (var mk in m)
            {
                var now = DateTime.UtcNow;
                var limit = new DateTime(mk.AuthorityOfCommerceCameraDate.Year + mk.LifeTimeYearsACC, mk.AuthorityOfCommerceCameraDate.Month, mk.AuthorityOfCommerceCameraDate.Day);
                var dif = limit - now;
                if (dif.TotalDays <= 30) return true;
            }
            return false;
        }

        #endregion

        #region list

        public JsonResult GPDAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_8 = Get_GPDAlarmQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_GPDAlarmQuery()
        {
            var m = GetMakers();
            var result = new List<List<Tuple<string, string>>>();

            foreach (var mk in m)
            {
                var now = DateTime.UtcNow;
                var limit = new DateTime(mk.GoodPracticesDocumentationDate.Year + mk.LifeTimeYearsGPD, mk.GoodPracticesDocumentationDate.Month, mk.GoodPracticesDocumentationDate.Day);
                var dif = limit - now;
                if (dif.TotalDays <= 30)
                {
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Nombre del fabricante: ", mk.Name));
                    result2.Add(new Tuple<string, string>("Autorización de Cámara de Comercio: ", mk.AuthorityOfCommerceCamera));
                    result2.Add(new Tuple<string, string>("Fecha de la Autorización de la Cámara de Comercio: ", mk.AuthorityOfCommerceCameraDate.ToString()));
                    result2.Add(new Tuple<string, string>("Tiempo de Vida de la Autorización de la Cámara de Comercio: ", mk.LifeTimeYearsACC.ToString()));
                    result2.Add(new Tuple<string, string>("Documentación de Buenas Prácticas: ", mk.GoodPracticesDocumentation));
                    result2.Add(new Tuple<string, string>("Fecha de la Documentación de Buenas Prácticas: ", mk.GoodPracticesDocumentationDate.ToString()));
                    result2.Add(new Tuple<string, string>("Tiempo de Vida de la Documentación de Buenas Prácticas: ", mk.LifeTimeYearsGPD.ToString()));
                    result.Add(result2);
                }
            }
            return result.AsQueryable();
        }

        #endregion

        #endregion

        #region FSC

        public IQueryable<Maker> GetForeignMakers() => _db.ForeignMakers.AsQueryable();

        #region bool

        public JsonResult FSCBoolAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_9 = Get_FSCBoolAlarmQuery() } };
            return result;
        }

        private bool Get_FSCBoolAlarmQuery()
        {
            var m = GetForeignMakers();

            foreach (var mk in m)
            {
                var now = DateTime.UtcNow;
                var limit = new DateTime((mk as ForeignMaker).CertificateFreeSaleDate.Year + (mk as ForeignMaker).LifeTimeYearsCFS, (mk as ForeignMaker).CertificateFreeSaleDate.Month, (mk as ForeignMaker).CertificateFreeSaleDate.Day);
                var dif = limit - now;
                if (dif.TotalDays <= 30) return true;
            }
            return false;
        }

        #endregion

        #region list

        public JsonResult FSCAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_10 = Get_FSCAlarmQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_FSCAlarmQuery()
        {
            var m = GetForeignMakers();
            var result = new List<List<Tuple<string, string>>>();

            foreach (var mk in m)
            {
                var now = DateTime.UtcNow;
                var limit = new DateTime((mk as ForeignMaker).CertificateFreeSaleDate.Year + (mk as ForeignMaker).LifeTimeYearsCFS, (mk as ForeignMaker).CertificateFreeSaleDate.Month, (mk as ForeignMaker).CertificateFreeSaleDate.Day);
                var dif = limit - now;
                if (dif.TotalDays <= 30)
                {
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Nombre del fabricante: ", (mk as ForeignMaker).Name));
                    result2.Add(new Tuple<string, string>("Autorización de Cámara de Comercio: ", (mk as ForeignMaker).AuthorityOfCommerceCamera));
                    result2.Add(new Tuple<string, string>("Fecha de la Autorización de la Cámara de Comercio: ", (mk as ForeignMaker).AuthorityOfCommerceCameraDate.ToString()));
                    result2.Add(new Tuple<string, string>("Tiempo de Vida de la Autorización de la Cámara de Comercio: ", (mk as ForeignMaker).LifeTimeYearsACC.ToString()));
                    result2.Add(new Tuple<string, string>("Documentación de Buenas Prácticas: ", (mk as ForeignMaker).GoodPracticesDocumentation));
                    result2.Add(new Tuple<string, string>("Fecha de la Documentación de Buenas Prácticas: ", (mk as ForeignMaker).GoodPracticesDocumentationDate.ToString()));
                    result2.Add(new Tuple<string, string>("Tiempo de Vida de la Documentación de Buenas Prácticas: ", (mk as ForeignMaker).LifeTimeYearsGPD.ToString()));
                    result2.Add(new Tuple<string, string>("Documentación de Buenas Prácticas: ", (mk as ForeignMaker).CertificateFreeSale));
                    result2.Add(new Tuple<string, string>("Fecha de la Documentación de Buenas Prácticas: ", (mk as ForeignMaker).CertificateFreeSaleDate.ToString()));
                    result2.Add(new Tuple<string, string>("Tiempo de Vida de la Documentación de Buenas Prácticas: ", (mk as ForeignMaker).LifeTimeYearsCFS.ToString()));
                    result.Add(result2);
                }
            }
            return result.AsQueryable();
        }

        #endregion

        #endregion

        #endregion
    }
}