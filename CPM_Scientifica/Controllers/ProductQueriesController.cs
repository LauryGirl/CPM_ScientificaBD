using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App = CPM_Scientifica.Context.AppContext;

namespace CPM_Scientifica.Controllers
{
    public class ProductQueriesController : Controller
    {
        App _db = new App();
        // GET: ProductQueries
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CleanerQueries()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Clean = true } };
            return result;
        }

        #region Products

        public IQueryable<Product> GetProducts()
        {
            return _db.Products.AsQueryable();
        }

        #region !p.RegisterPresent

        public JsonResult RegisterNotPresentProdQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_1 = Get_RegisterNotPresentProdQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_RegisterNotPresentProdQuery()
        {
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();
            foreach (var item in _db.Products)
            {
                if (!item.RegisterPresent) {
                    aux.Add(item.MakerId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Nombre del Producto: ", item.Name));
                    result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                    result2.Add(new Tuple<string, string>("Estado: ", item._state));
                    result2.Add(new Tuple<string, string>("Ref: ", item.Ref));
                    result2.Add(new Tuple<string, string>("Aplicación: ", item.Application));
                    result2.Add(new Tuple<string, string>("Presentación: ", item.Presentation));
                    result2.Add(new Tuple<string, string>("Familia: ", item.Family));
                    result2.Add(new Tuple<string, string>("Sistema: ", item.System));
                    result2.Add(new Tuple<string, string>("Registro Presentado: ", item.RegisterPresent.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var m = _db.Makers.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Fabricante: ", m.Name));
            }
            return result.AsQueryable();
        }


        #endregion

        #region p.RegisterPresent

        public JsonResult RegisterPresentProdQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_2 = Get_RegisterPresentProdQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_RegisterPresentProdQuery()
        {
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();
            foreach (var item in _db.Products)
            {
                if (item.RegisterPresent)
                {
                    aux.Add(item.MakerId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Nombre del Producto: ", item.Name));
                    result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                    result2.Add(new Tuple<string, string>("Estado: ", item._state));
                    result2.Add(new Tuple<string, string>("Ref: ", item.Ref));
                    result2.Add(new Tuple<string, string>("Aplicación: ", item.Application));
                    result2.Add(new Tuple<string, string>("Presentación: ", item.Presentation));
                    result2.Add(new Tuple<string, string>("Familia: ", item.Family));
                    result2.Add(new Tuple<string, string>("Sistema: ", item.System));
                    result2.Add(new Tuple<string, string>("Registro Presentado: ", item.RegisterPresent.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var m = _db.Makers.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Fabricante: ", m.Name));
            }
            return result.AsQueryable();
        }

        #endregion

        #region !p.RegisterPresent && p.State = CecmedPresent

        public JsonResult CecmedPresentProdQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_3 = Get_CecmedPresentProdQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_CecmedPresentProdQuery()
        {
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();
            foreach (var item in _db.Products)
            {
                if (!item.RegisterPresent && item._state == "Cecmed")
                {
                    aux.Add(item.MakerId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Nombre del Producto: ", item.Name));
                    result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                    result2.Add(new Tuple<string, string>("Estado: ", item._state));
                    result2.Add(new Tuple<string, string>("Ref: ", item.Ref));
                    result2.Add(new Tuple<string, string>("Aplicación: ", item.Application));
                    result2.Add(new Tuple<string, string>("Presentación: ", item.Presentation));
                    result2.Add(new Tuple<string, string>("Familia: ", item.Family));
                    result2.Add(new Tuple<string, string>("Sistema: ", item.System));
                    result2.Add(new Tuple<string, string>("Registro Presentado: ", item.RegisterPresent.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var m = _db.Makers.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Fabricante: ", m.Name));
            }
            return result.AsQueryable();

        }


        #endregion

        #region !p.RegisterPresent && p.State = NoPresent

        public JsonResult CecmedNotPresentProdQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_4 = Get_CecmedNotPresentProdQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_CecmedNotPresentProdQuery()
        {
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();
            foreach (var item in _db.Products)
            {
                if (!item.RegisterPresent && item._state == "No Presentado")
                {
                    aux.Add(item.MakerId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Nombre del Producto: ", item.Name));
                    result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                    result2.Add(new Tuple<string, string>("Estado: ", item._state));
                    result2.Add(new Tuple<string, string>("Ref: ", item.Ref));
                    result2.Add(new Tuple<string, string>("Aplicación: ", item.Application));
                    result2.Add(new Tuple<string, string>("Presentación: ", item.Presentation));
                    result2.Add(new Tuple<string, string>("Familia: ", item.Family));
                    result2.Add(new Tuple<string, string>("Sistema: ", item.System));
                    result2.Add(new Tuple<string, string>("Registro Presentado: ", item.RegisterPresent.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var m = _db.Makers.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Fabricante: ", m.Name));
            }
            return result.AsQueryable();

        }


        #endregion

        #region p.Type = Microbiology

        public JsonResult MicrobiologyProdQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_5 = Get_MicrobiologyProdQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_MicrobiologyProdQuery()
        {
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();
            foreach (var item in _db.Products)
            {
                if (item._type == "Microbiologia")
                {
                    aux.Add(item.MakerId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Nombre del Producto: ", item.Name));
                    result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                    result2.Add(new Tuple<string, string>("Estado: ", item._state));
                    result2.Add(new Tuple<string, string>("Ref: ", item.Ref));
                    result2.Add(new Tuple<string, string>("Aplicación: ", item.Application));
                    result2.Add(new Tuple<string, string>("Presentación: ", item.Presentation));
                    result2.Add(new Tuple<string, string>("Familia: ", item.Family));
                    result2.Add(new Tuple<string, string>("Sistema: ", item.System));
                    result2.Add(new Tuple<string, string>("Registro Presentado: ", item.RegisterPresent.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var m = _db.Makers.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Fabricante: ", m.Name));
            }
            return result.AsQueryable();
        }


        #endregion

        #region p.Type = ClinicChemistry

        public JsonResult ClinicChemistryProdQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_6 = Get_ClinicChemistryProdQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_ClinicChemistryProdQuery()
        {
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();
            foreach (var item in _db.Products)
            {
                if (item._type == "Quimica Clinica")
                {
                    aux.Add(item.MakerId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Nombre del Producto: ", item.Name));
                    result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                    result2.Add(new Tuple<string, string>("Estado: ", item._state));
                    result2.Add(new Tuple<string, string>("Ref: ", item.Ref));
                    result2.Add(new Tuple<string, string>("Aplicación: ", item.Application));
                    result2.Add(new Tuple<string, string>("Presentación: ", item.Presentation));
                    result2.Add(new Tuple<string, string>("Familia: ", item.Family));
                    result2.Add(new Tuple<string, string>("Sistema: ", item.System));
                    result2.Add(new Tuple<string, string>("Registro Presentado: ", item.RegisterPresent.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var m = _db.Makers.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Fabricante: ", m.Name));
            }
            return result.AsQueryable();
        }


        #endregion

        #region System 

        public JsonResult SystemProdQuery(string system)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_7 = Get_SystemProdQuery(system) } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_SystemProdQuery(string system)
        {
            var p = GetProducts();
            var r = p.Where(x => x.System == system).ToList();
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();
            foreach (var item in r)
            {
                aux.Add(item.MakerId);
                var result2 = new List<Tuple<string, string>>();
                result2.Add(new Tuple<string, string>("Nombre del Producto: ", item.Name));
                result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                result2.Add(new Tuple<string, string>("Estado: ", item._state));
                result2.Add(new Tuple<string, string>("Ref: ", item.Ref));
                result2.Add(new Tuple<string, string>("Aplicación: ", item.Application));
                result2.Add(new Tuple<string, string>("Presentación: ", item.Presentation));
                result2.Add(new Tuple<string, string>("Familia: ", item.Family));
                result2.Add(new Tuple<string, string>("Sistema: ", item.System));
                result2.Add(new Tuple<string, string>("Registro Presentado: ", item.RegisterPresent.ToString()));
                result.Add(result2);
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var m = _db.Makers.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Fabricante: ", m.Name));
            }
            return result.AsQueryable();
        }


        #endregion

        #region Family

        public JsonResult FamilyProdQuery(string family)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_8 = Get_FamilyProdQuery(family) } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_FamilyProdQuery(string family)
        {
            var p = GetProducts();
            var r = p.Where(x => x.Family == family).ToList();
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();
            foreach (var item in r)
            {
                aux.Add(item.MakerId);
                var result2 = new List<Tuple<string, string>>();
                result2.Add(new Tuple<string, string>("Nombre del Producto: ", item.Name));
                result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                result2.Add(new Tuple<string, string>("Estado: ", item._state));
                result2.Add(new Tuple<string, string>("Ref: ", item.Ref));
                result2.Add(new Tuple<string, string>("Aplicación: ", item.Application));
                result2.Add(new Tuple<string, string>("Presentación: ", item.Presentation));
                result2.Add(new Tuple<string, string>("Familia: ", item.Family));
                result2.Add(new Tuple<string, string>("Sistema: ", item.System));
                result2.Add(new Tuple<string, string>("Registro Presentado: ", item.RegisterPresent.ToString()));
                result.Add(result2);
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var m = _db.Makers.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Fabricante: ", m.Name));
            }
            return result.AsQueryable();
        }


        #endregion

        #region Maker

        public JsonResult MakerProdQuery(string maker)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_9 = Get_MakerProdQuery(maker) } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_MakerProdQuery(string maker)
        {
            var p = GetProducts();
            var r = p.Where(x => x.Maker.Name == maker).ToList();
            var result = new List<List<Tuple<string, string>>>();
            var aux = new List<int>();
            foreach (var item in r)
            {
                aux.Add(item.MakerId);
                var result2 = new List<Tuple<string, string>>();
                result2.Add(new Tuple<string, string>("Nombre del Producto: ", item.Name));
                result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                result2.Add(new Tuple<string, string>("Estado: ", item._state));
                result2.Add(new Tuple<string, string>("Ref: ", item.Ref));
                result2.Add(new Tuple<string, string>("Aplicación: ", item.Application));
                result2.Add(new Tuple<string, string>("Presentación: ", item.Presentation));
                result2.Add(new Tuple<string, string>("Familia: ", item.Family));
                result2.Add(new Tuple<string, string>("Sistema: ", item.System));
                result2.Add(new Tuple<string, string>("Registro Presentado: ", item.RegisterPresent.ToString()));
                result.Add(result2);
            }
            for (int i = 0; i < aux.Count; i++)
            {
                var m = _db.Makers.Find(aux[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Fabricante: ", m.Name));
            }
            return result.AsQueryable();
        }

        #endregion


        #endregion
    }
}