using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App = CPM_Scientifica.Context.AppContext;

namespace CPM_Scientifica.Controllers
{
    public class SaleQueriesController : Controller
    {
        App _db = new App();

        // GET: SaleQueries
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

        #region Sale

        public IQueryable<Sale> GetSales()
        {
            return _db.Sales.AsQueryable();
        }

        #region s.where(r.Date > d1 && r.Date < d2), Date: d1, d2

        public JsonResult TakeSaleQuery(DateTime d1, DateTime d2)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_22 = Get_TakeSaleQuery(d1, d2) } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_TakeSaleQuery(DateTime d1, DateTime d2)
        {
            var r = GetSales();
            var list = new List<Sale>();
            foreach (var item in r)
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
                result2.Add(new Tuple<string, string>("Precio: ", item.Price.ToString()));
                result2.Add(new Tuple<string, string>("Centro: ", item.Center.ToString()));
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

        #region Price >= "x"

        public JsonResult PriceGreatXSaleQuery(decimal price)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_23 = Get_PriceGreatXSaleQuery(price) } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_PriceGreatXSaleQuery(decimal price)
        {
            var s = GetSales();
            var result = new List<List<Tuple<string, string>>>();
            var aux2 = new List<int>();
            foreach (var item in s)
            {
                if (item.Price >= price)
                {
                    aux2.Add(item.ProductId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Precio: ", item.Price.ToString()));
                    result2.Add(new Tuple<string, string>("Centro: ", item.Center));
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

        #region Price <= "x"

        public JsonResult PriceLessXSaleQuery(decimal price)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_24 = Get_PriceLessXSaleQuery(price) } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_PriceLessXSaleQuery(decimal price)
        {
            var s = GetSales();
            var result = new List<List<Tuple<string, string>>>();
            var aux2 = new List<int>();
            foreach (var item in s)
            {
                if (item.Price <= price)
                {
                    aux2.Add(item.ProductId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Precio: ", item.Price.ToString()));
                    result2.Add(new Tuple<string, string>("Centro: ", item.Center));
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

        #region Price == "x"

        public JsonResult PriceEqualsXSaleQuery(decimal price)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_25 = Get_PriceEqualsXSaleQuery(price) } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_PriceEqualsXSaleQuery(decimal price)
        {
            var s = GetSales();
            var result = new List<List<Tuple<string, string>>>();
            var aux2 = new List<int>();
            foreach (var item in s)
            {
                if (item.Price == price)
                {
                    aux2.Add(item.ProductId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Precio: ", item.Price.ToString()));
                    result2.Add(new Tuple<string, string>("Centro: ", item.Center));
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

        #region on center "x"

        public JsonResult CenterSaleQuery(string center)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_26 = Get_CenterSaleQuery(center) } };
            return result;
        }

        private IQueryable<List<Tuple<string, string>>> Get_CenterSaleQuery(string name)
        {
            var s = GetSales();
            var result = new List<List<Tuple<string, string>>>();
            var aux2 = new List<int>();
            foreach (var item in s)
            {
                if (item.Center == name)
                {
                    aux2.Add(item.ProductId);
                    var result2 = new List<Tuple<string, string>>();
                    result2.Add(new Tuple<string, string>("Precio: ", item.Price.ToString()));
                    result2.Add(new Tuple<string, string>("Centro: ", item.Center));
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

        #region GroupBy Product => Count

        public JsonResult ProductGroupByCountSaleQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_27 = Get_ProductGroupByCountSaleQuery() } };
            return result;
        }

        private IQueryable<Tuple<List<Tuple<string, string>>, int>> Get_ProductGroupByCountSaleQuery()
        {
            var s = GetSales();
            var result = new List<Tuple<List<Tuple<string, string>>, int>>();
            var aux = new List<Tuple<int, int>>();
            var mids = new List<int>();
            var ids = new List<int>();
            foreach (var item in s)
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

       
        
        #endregion
    }
}