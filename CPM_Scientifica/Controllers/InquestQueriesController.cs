using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App = CPM_Scientifica.Context.AppContext;

namespace CPM_Scientifica.Controllers
{
    public class InquestQueriesController : Controller
    {
        App _db = new App();

        // GET: InquestQueries
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

        public IQueryable<Product> GetProducts()
        {
            return _db.Products.AsQueryable();
        }

        private Product TakeProduct(Product x, object y)
        {
            return x;
        }

        #region Inquest

        public IQueryable<Inquest> GetInquests()
        {
            return _db.Inquests.AsQueryable();
        }

        #region Inquests foreach product

        public JsonResult ProductInqQuery(string product)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_16 = Get_ProductInqQuery(product) } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_ProductInqQuery(string name)
        {
            var inq = GetInquests();
            var aux = inq.Where(x => x.Product.Name == name);
            var result = new List<List<Tuple<string, string>>>();
            var aux2 = new List<int>();
            foreach (var item in aux)
            {
                aux2.Add(item.ProductId);
                var result2 = new List<Tuple<string, string>>();
                result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                result2.Add(new Tuple<string, string>("Centro: ", item.Center));
                result2.Add(new Tuple<string, string>("Atiende: ", item.Receiver));
                result2.Add(new Tuple<string, string>("Recomendación: ", item.Recommendation));
                result2.Add(new Tuple<string, string>("Fecha: ", item.Date.ToString()));
                result.Add(result2);
            }
            for (int i = 0; i < aux2.Count; i++)
            {
                var p = _db.Products.Find(aux2[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Producto: ", p.Name));
            }
            return result.AsQueryable();
        }

        #endregion

        #region GroupBy Product => Count

        public JsonResult ProductGroupByCountInqQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_17 = Get_ProductGroupByCountInqQuery() } };
            return result;
        }

        private IQueryable<Tuple<List<Tuple<string, string>>, int>> Get_ProductGroupByCountInqQuery()
        {
            var r = GetInquests();
            var result = new List<Tuple<List<Tuple<string, string>>, int>>();
            var aux = new List<Tuple<int, int>>();
            var mids = new List<int>();
            var ids = new List<int>();
            foreach (var item in r)
            {
                if (ids.Contains(item.ProductId))
                {
                    var temp1 = 0;
                    var temp2 = new Tuple<int, int>(0, 0);
                    foreach (var item2 in aux)
                    {
                        if (item2.Item1 == item.ProductId)
                        {
                            temp1 = item2.Item2;
                            temp2 = item2;
                        }
                    }
                    aux.Remove(temp2);
                    temp1++;
                    aux.Add(new Tuple<int, int>(item.ProductId, temp1));
                }
                else
                {
                    ids.Add(item.ProductId);
                    aux.Add(new Tuple<int, int>(item.ProductId, 1));
                }

            }
            for (int i = 0; i < aux.Count; i++)
            {
                var p1 = _db.Products.Find(aux[i].Item1);
                var result2 = new List<Tuple<string, string>>();
                mids.Add(p1.MakerId);
                result2.Add(new Tuple<string, string>("Nombre del Producto: ", p1.Name));
                result2.Add(new Tuple<string, string>("Tipo: ", p1._type));
                result2.Add(new Tuple<string, string>("Estado: ", p1._state));
                result2.Add(new Tuple<string, string>("Ref: ", p1.Ref));
                result2.Add(new Tuple<string, string>("Aplicación: ", p1.Application));
                result2.Add(new Tuple<string, string>("Presentación: ", p1.Presentation));
                result2.Add(new Tuple<string, string>("Familia: ", p1.Family));
                result2.Add(new Tuple<string, string>("Sistema: ", p1.System));
                result2.Add(new Tuple<string, string>("Registro Presentado: ", p1.RegisterPresent.ToString()));

                result.Add(new Tuple<List<Tuple<string, string>>, int>(result2, aux[i].Item2));
            }
            for (int i = 0; i < mids.Count; i++)
            {
                var m = _db.Makers.Find(mids[i]);
                result[i].Item1.Add(new Tuple<string, string>("Nombre del Fabricante: ", m.Name));
            }
            return result.AsQueryable();
        }

        #endregion

        #region inquest in "x" center in [d1,d2] 

        public JsonResult XinIntervalInqQuery(string center, DateTime d1, DateTime d2)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_18 = Get_XinIntervalInqQuery(center, d1, d2) } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_XinIntervalInqQuery(string center, DateTime d1, DateTime d2)
        {
            var inq = GetInquests();
            var list = new List<Inquest>();
            foreach(var item in inq)
            {
                int comp1 = item.Date.CompareTo(d1);
                int comp2 = item.Date.CompareTo(d2);

                if (comp1 >= 0 && comp2<= 0)
                {
                    list.Add(item);
                }
            }
            var result = new List<List<Tuple<string, string>>>();
            var aux2 = new List<int>();
            foreach (var item in list)
            {
                if(item.Center == center)
                {
                    aux2.Add(item.ProductId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                    result2.Add(new Tuple<string, string>("Centro: ", item.Center));
                    result2.Add(new Tuple<string, string>("Atiende: ", item.Receiver));
                    result2.Add(new Tuple<string, string>("Recomendación: ", item.Recommendation));
                    result2.Add(new Tuple<string, string>("Fecha: ", item.Date.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux2.Count; i++)
            {
                var p = _db.Products.Find(aux2[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Producto: ", p.Name));
            }
            return result.AsQueryable();
        }

        #endregion

        #region inquest to "x" product in [d1,d2] 

        public JsonResult ProdInIntervalInqQuery(string product, DateTime d1, DateTime d2)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_19 = Get_ProdInIntervalInqQuery(product, d1, d2) } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_ProdInIntervalInqQuery(string product, DateTime d1, DateTime d2)
        {
            var inq = GetInquests();
            var list = new List<Inquest>();
            foreach (var item in inq)
            {
                int comp1 = item.Date.CompareTo(d1);
                int comp2 = item.Date.CompareTo(d2);

                if (comp1 >= 0 && comp2 <= 0)
                {
                    list.Add(item);
                }
            }
            var result = new List<List<Tuple<string, string>>>();
            var aux2 = new List<int>();
            foreach (var item in list)
            {
                    aux2.Add(item.ProductId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                    result2.Add(new Tuple<string, string>("Centro: ", item.Center));
                    result2.Add(new Tuple<string, string>("Atiende: ", item.Receiver));
                    result2.Add(new Tuple<string, string>("Recomendación: ", item.Recommendation));
                    result2.Add(new Tuple<string, string>("Fecha: ", item.Date.ToString()));
                    result.Add(result2);
            }
            var result3 = new List<List<Tuple<string, string>>>();
            for (int i = 0; i < aux2.Count; i++)
            {
                var p = _db.Products.Find(aux2[i]);
                if(p.Name == product)
                {
                result[i].Add(new Tuple<string, string>("Nombre del Producto: ", p.Name));
                    result3.Add(result[i]);
                }
            }
            return result3.AsQueryable();
        }

        #endregion

        #region InPerson Type

        public JsonResult InPersonTypeInqQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_20 = Get_InPersonTypeInqQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_InPersonTypeInqQuery()
        {
            var inq = GetInquests();
            var result = new List<List<Tuple<string, string>>>();
            var aux2 = new List<int>();
            foreach (var item in inq)
            {
                if (item._type == "Presencial") {
                    aux2.Add(item.ProductId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                    result2.Add(new Tuple<string, string>("Centro: ", item.Center));
                    result2.Add(new Tuple<string, string>("Atiende: ", item.Receiver));
                    result2.Add(new Tuple<string, string>("Recomendación: ", item.Recommendation));
                    result2.Add(new Tuple<string, string>("Fecha: ", item.Date.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux2.Count; i++)
            {
                var p = _db.Products.Find(aux2[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Producto: ", p.Name));
            }
            return result.AsQueryable();
        }



        #endregion

        #region Telephonic Type

        public JsonResult PhoneTypeInqQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_21 = Get_PhoneTypeInqQuery() } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_PhoneTypeInqQuery()
        {
            var inq = GetInquests();
            var result = new List<List<Tuple<string, string>>>();
            var aux2 = new List<int>();
            foreach (var item in inq)
            {
                if (item._type == "Telefónica")
                {
                    aux2.Add(item.ProductId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Tipo: ", item._type));
                    result2.Add(new Tuple<string, string>("Centro: ", item.Center));
                    result2.Add(new Tuple<string, string>("Atiende: ", item.Receiver));
                    result2.Add(new Tuple<string, string>("Recomendación: ", item.Recommendation));
                    result2.Add(new Tuple<string, string>("Fecha: ", item.Date.ToString()));
                    result.Add(result2);
                }
            }
            for (int i = 0; i < aux2.Count; i++)
            {
                var p = _db.Products.Find(aux2[i]);
                result[i].Add(new Tuple<string, string>("Nombre del Producto: ", p.Name));
            }
            return result.AsQueryable();

        }



        #endregion

        #endregion
    }
}