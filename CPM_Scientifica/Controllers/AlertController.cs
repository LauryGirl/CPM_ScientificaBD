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
    public class AlertController : Controller
    {
        App _db = new App();
        // GET: Alert
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Cleaner()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Clean = true } };
            return result;
        }
        #region Product

        #region Register

        public IQueryable<Inscription> GetInsc() => _db.Inscriptions.AsQueryable();

        #region bool

        public JsonResult RegisterBoolAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_1 = Get_RegisterBoolAlarmQuery() } };
            return result;
        }

        private bool Get_RegisterBoolAlarmQuery()
        {
            var r = GetInsc();
            var result = new List<Register>();

            foreach (var reg in r)
            {
                var dif = reg.NewRegister - DateTime.UtcNow;
                if (dif.TotalDays <= 120) return true;
            }
            return false;
        }

        #endregion

        #region list

        public JsonResult RegisterAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_2 = Get_RegisterAlarmQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_RegisterAlarmQuery()
        {
            var r = GetInsc();
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();

            foreach (var item in r)
            {
                var dif = item.NewRegister - DateTime.UtcNow;
                if (dif.TotalDays <= 120) {
                    aux.Add(item.ProductId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Vigencia: ", item.Validity));
                    result2.Add(new Tuple<string, string>("Fecha: ", item.Date.ToString()));
                    result2.Add(new Tuple<string, string>("Nuevo Registro: ", item.NewRegister.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var p = _db.Products.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Producto: ", p.Name));
            }
            return result.AsQueryable();
        }

        #endregion

        public IQueryable<AuthorityTempMarket> GetAuths() => _db.AuthorityTemporalMarkets.AsQueryable();

        #region bool

        public JsonResult AuthBoolAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_11 = Get_AuthBoolAlarmQuery() } };
            return result;
        }

        private bool Get_AuthBoolAlarmQuery()
        {
            var r = GetAuths();
            var result = new List<Register>();

            foreach (var reg in r)
            {
                var dif = reg.NewRegister - DateTime.UtcNow;
                if (dif.TotalDays <= 120) return true;
            }
            return false;
        }

        #endregion

        #region list

        public JsonResult AuthAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_12 = Get_AuthAlarmQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_AuthAlarmQuery()
        {
            var r = GetAuths();
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();

            foreach (var item in r)
            {
                var dif = item.NewRegister - DateTime.UtcNow;
                if (dif.TotalDays <= 120) {
                    aux.Add(item.ProductId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Documentación: ", item.Documentation));
                    result2.Add(new Tuple<string, string>("Tiempo de vida en años: ", item._montoYear.ToString()));
                    result2.Add(new Tuple<string, string>("Fecha: ", item.Date.ToString()));
                    result2.Add(new Tuple<string, string>("Nuevo Registro: ", item.NewRegister.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var p = _db.Products.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Producto: ", p.Name));
            }
            return result.AsQueryable();
        }

        #endregion

        #endregion

        #region Yearly Revisions

        public IQueryable<YearlyRevision> GetYearlyRevisions()
        {
            return _db.YearlyRevisions.AsQueryable();
        }

        #region bool

        public JsonResult YRevBoolAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_3 = Get_YRevBoolAlarmQuery() } };
            return result;
        }

        private bool Get_YRevBoolAlarmQuery()
        {
            var yr = GetYearlyRevisions();
            var result = new List<Register>();

            foreach (var yrev in yr)
            {
                var dif = yrev.LimitRev - DateTime.UtcNow;
                if (dif.TotalDays <= 30) return true;
            }
            return false;
        }

        #endregion

        #region list

        public JsonResult YRevAlarmQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { A_4 = Get_YRevAlarmQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_YRevAlarmQuery()
        {
            var yr = GetYearlyRevisions();
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();

            foreach (var item in yr)
            {
                var dif = item.LimitRev - DateTime.UtcNow;
                if (dif.TotalDays <= 30) {
                    aux.Add(item.ProductId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Fecha: ", item.Date.ToString()));
                    result2.Add(new Tuple<string, string>("Límite del Tiempo de Vida: ", item.LimitRev.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var p = _db.Products.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Producto: ", p.Name));
            }
            return result.AsQueryable();
        }

        #endregion

        #endregion

        #endregion

    }
}
