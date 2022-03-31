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
    public class QueriesController : Controller
    {
        App _db = new App();

        // GET: Queries
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

        private IQueryable<Product> Get_RegisterNotPresentProdQuery()
        {
            var p = GetProducts();
            var result = new List<Product>();
            foreach (var item in p)
            {
                if (!item.RegisterPresent) result.Add(item);
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

        private IQueryable<Product> Get_RegisterPresentProdQuery()
        {
            var p = GetProducts();
            var result = new List<Product>();
            foreach (var item in p)
            {
                if (item.RegisterPresent) result.Add(item);
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

        private IQueryable<Product> Get_CecmedPresentProdQuery()
        {
            var p = GetProducts();
            var result = new List<Product>();
            foreach (var item in p)
            {
                if (!item.RegisterPresent && item._state == "") result.Add(item);
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

        private IQueryable<Product> Get_CecmedNotPresentProdQuery()
        {
            var p = GetProducts();
            var result = new List<Product>();
            foreach (var item in p)
            {
                if (!item.RegisterPresent && item._state == "") result.Add(item);
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

        private IQueryable<Product> Get_MicrobiologyProdQuery()
        {
            var p = GetProducts();
            var result = new List<Product>();
            foreach (var item in p)
            {
                if (item._type == "") result.Add(item);
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

        private IQueryable<Product> Get_ClinicChemistryProdQuery()
        {
            var p = GetProducts();
            var result = new List<Product>();
            foreach (var item in p)
            {
                if (item._type == "") result.Add(item);
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

        private IQueryable<Product> Get_SystemProdQuery(string system)
        {
            var p = GetProducts();
            var result = p.Where(x => x.System == system).ToList();
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

        private IQueryable<Product> Get_FamilyProdQuery(string family)
        {
            var p = GetProducts();
            var result = p.Where(x => x.Family == family).ToList();
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

        private IQueryable<Product> Get_MakerProdQuery(string maker)
        {
            var p = GetProducts();
            var result = p.Where(x => x.Maker.Name == maker).ToList();
            return result.AsQueryable();
        }

        #endregion


        #endregion

        #region Register

        public IQueryable<Register> GetRegisters() => _db.Registers.AsQueryable();

        #region r.where(r.Date > d1 && r.Date < d2), Date: d1, d2

        public JsonResult TakeRegQuery(DateTime d1, DateTime d2)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_10 = Get_TakeRegQuery(d1, d2) } };
            return result;
        }

        private IQueryable<Register> Get_TakeRegQuery(DateTime d1, DateTime d2)
        {
            var r = GetRegisters();
            var result = r.Where(x => (d1.Year <= x.Date.Year && x.Date.Year <= d2.Year) &&
                                      ((d1.Month < x.Date.Month && x.Date.Month < d2.Month) ||
                                       (d1.Month == x.Date.Month && d2.Month == x.Date.Month && d1.Day <= x.Date.Day && x.Date.Day <= d2.Day) ||
                                       (d1.Month == x.Date.Month && d1.Day <= x.Date.Day) || (x.Date.Day <= d2.Day)));

            return result.AsQueryable();
        }

        #endregion

        #region Registers foreach product

        public JsonResult ProductRegQuery(string product)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_11 = Get_ProductRegQuery(product) } };
            return result;
        }

        private IQueryable<Register> Get_ProductRegQuery(string name)
        {
            var r = GetRegisters();
            var aux = new List<Register>();
            foreach (var item in r)
            {
                if (item is AuthorityTempMarket)
                {
                    if ((item as AuthorityTempMarket).Product.Name == name) aux.Add(item);
                }
                else if ((item as Inscription).Product.Name == name) aux.Add(item);
            }

            return aux.AsQueryable();
        }

        #endregion

        #region GroupBy Product => Count

        public JsonResult ProductGroupByCountRegQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_12 = Get_ProductGroupByCountRegQuery() } };
            return result;
        }

        private IQueryable<Tuple<Product, int>> Get_ProductGroupByCountRegQuery()
        {
            var r = GetRegisters();
            var p = GetProducts();
            var join = p.Join(r, x => x.ProductId, d => d.ProductId, TakeProduct);
            var result = new List<Tuple<Product, int>>();

            foreach (var item in r.GroupBy(x => x.ProductId))
            {
                var y = join.Where(x => x.ProductId == item.Key).ToList();

                //esto no pasa nunk
                if (y.Count > 1)
                    break;

                result.Add(new Tuple<Product, int>(y[0], item.Count()));
            }

            return result.AsQueryable();
        }

        private Product TakeProduct(Product x, object y)
        {
            return x;
        }

        #endregion


        #endregion

        #region Change

        public IQueryable<Change> GetChanges()
        {
            return _db.Changes.AsQueryable();
        }

        #region Changes foreach product

        public JsonResult ProductChangQuery(string product)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_13 = Get_ProductChangQuery(product) } };
            return result;
        }

        private IQueryable<Change> Get_ProductChangQuery(string name)
        {
            var c = GetChanges();
            var aux = c.Where(x => x.Product.Name == name);
            return aux.AsQueryable();
        }


        #endregion

        #region GroupBy Product => Count

        public JsonResult ProductGroupByCountChangQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_14 = Get_ProductGroupByCountChangQuery() } };
            return result;
        }

        private IQueryable<Tuple<Product, int>> Get_ProductGroupByCountChangQuery()
        {
            var r = GetChanges();
            var p = GetProducts();
            var join = p.Join(r, x => x.ProductId, d => d.ProductId, TakeProduct);
            var result = new List<Tuple<Product, int>>();

            foreach (var item in r.GroupBy(x => x.ProductId))
            {
                var y = join.Where(x => x.ProductId == item.Key).ToList();

                //esto no pasa nunk
                if (y.Count > 1)
                    break;

                result.Add(new Tuple<Product, int>(y[0], item.Count()));
            }

            return result.AsQueryable();
        }

        #endregion

        #region >= "x" changes in [d1,d2] 

        public JsonResult XinIntervalChangQuery(int count, DateTime d1, DateTime d2)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_15 = Get_XinIntervalChangQuery(count, d1, d2) } };
            return result;
        }

        private IQueryable<Change> Get_XinIntervalChangQuery(int count, DateTime d1, DateTime d2)
        {
            var pc = Get_ProductGroupByCountChangQuery();
            var pc_aux = pc.Where(x => x.Item2 >= count);

            var c = GetChanges();
            var c_aux = c.Where(x => (d1.Year <= x.Date.Year && x.Date.Year <= d2.Year) &&
                                      ((d1.Month < x.Date.Month && x.Date.Month < d2.Month) ||
                                       (d1.Month == x.Date.Month && d2.Month == x.Date.Month && d1.Day <= x.Date.Day && x.Date.Day <= d2.Day) ||
                                       (d1.Month == x.Date.Month && d1.Day <= x.Date.Day) || (x.Date.Day <= d2.Day)));

            var join = c_aux.Join(pc_aux, x => x.ProductId, d => d.Item1.ProductId, TakeChange).AsQueryable();
            return join;
        }

        private Change TakeChange(Change c, object y)
        {
            return c;
        }

        #endregion

        #endregion

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

        private IQueryable<Inquest> Get_ProductInqQuery(string name)
        {
            var i = GetInquests();
            var aux = i.Where(x => x.Product.Name == name);
            return aux.AsQueryable();
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

        private IQueryable<Tuple<Product, int>> Get_ProductGroupByCountInqQuery()
        {
            var i = GetInquests();
            var p = GetProducts();
            var join = p.Join(i, x => x.ProductId, d => d.ProductId, TakeProduct);
            var result = new List<Tuple<Product, int>>();

            foreach (var item in i.GroupBy(x => x.ProductId))
            {
                var y = join.Where(x => x.ProductId == item.Key).ToList();

                //esto no pasa nunk
                if (y.Count > 1)
                    break;

                result.Add(new Tuple<Product, int>(y[0], item.Count()));
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

        private IQueryable<Inquest> Get_XinIntervalInqQuery(string center, DateTime d1, DateTime d2)
        {
            var i = GetInquests();
            var ic = i.Where(x => x.Center == center);

            var i_interv = ic.Where(x => (d1.Year <= x.Date.Year && x.Date.Year <= d2.Year) &&
                                      ((d1.Month < x.Date.Month && x.Date.Month < d2.Month) ||
                                       (d1.Month == x.Date.Month && d2.Month == x.Date.Month && d1.Day <= x.Date.Day && x.Date.Day <= d2.Day) ||
                                       (d1.Month == x.Date.Month && d1.Day <= x.Date.Day) || (x.Date.Day <= d2.Day)));

            return i_interv.AsQueryable();
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

        private IQueryable<Inquest> Get_ProdInIntervalInqQuery(string product, DateTime d1, DateTime d2)
        {
            var i = Get_ProductInqQuery(product);

            var i_interv = i.Where(x => (d1.Year <= x.Date.Year && x.Date.Year <= d2.Year) &&
                                      ((d1.Month < x.Date.Month && x.Date.Month < d2.Month) ||
                                       (d1.Month == x.Date.Month && d2.Month == x.Date.Month && d1.Day <= x.Date.Day && x.Date.Day <= d2.Day) ||
                                       (d1.Month == x.Date.Month && d1.Day <= x.Date.Day) || (x.Date.Day <= d2.Day)));

            return i_interv.AsQueryable();
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

        private IQueryable<Inquest> Get_InPersonTypeInqQuery()
        {
            var i = GetInquests();
            var result = new List<Inquest>();
            foreach (var item in i)
            {
                if (item._type == "") result.Add(item);
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

        private IQueryable<Inquest> Get_PhoneTypeInqQuery()
        {
            var i = GetInquests();
            var result = new List<Inquest>();
            foreach (var item in i)
            {
                if (item._type == "") result.Add(item);
            }
            return result.AsQueryable();
        }



        #endregion

        #endregion

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

        private IQueryable<Sale> Get_TakeSaleQuery(DateTime d1, DateTime d2)
        {
            var r = GetSales();
            var result = r.Where(x => (d1.Year <= x.Date.Year && x.Date.Year <= d2.Year) &&
                                      ((d1.Month < x.Date.Month && x.Date.Month < d2.Month) ||
                                       (d1.Month == x.Date.Month && d2.Month == x.Date.Month && d1.Day <= x.Date.Day && x.Date.Day <= d2.Day) ||
                                       (d1.Month == x.Date.Month && d1.Day <= x.Date.Day) || (x.Date.Day <= d2.Day)));

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

        private IQueryable<Sale> Get_PriceGreatXSaleQuery(decimal price)
        {
            var i = GetSales();
            var result = i.Where(x => x.Price >= price);
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

        private IQueryable<Sale> Get_PriceLessXSaleQuery(decimal price)
        {
            var i = GetSales();
            var result = i.Where(x => x.Price <= price);
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

        private IQueryable<Sale> Get_PriceEqualsXSaleQuery(decimal price)
        {
            var i = GetSales();
            var result = i.Where(x => x.Price == price);
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

        private IQueryable<Sale> Get_CenterSaleQuery(string name)
        {
            var s = GetSales();
            var aux = s.Where(x => x.Center == name);
            return aux.AsQueryable();
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

        private IQueryable<Tuple<Product, int>> Get_ProductGroupByCountSaleQuery()
        {
            var s = GetSales();
            var p = GetProducts();
            var join = p.Join(s, x => x.ProductId, d => d.ProductId, TakeProduct);
            var result = new List<Tuple<Product, int>>();

            foreach (var item in s.GroupBy(x => x.ProductId))
            {
                var y = join.Where(x => x.ProductId == item.Key).ToList();

                //esto no pasa nunk
                if (y.Count > 1)
                    break;

                result.Add(new Tuple<Product, int>(y[0], item.Count()));
            }

            return result.AsQueryable();
        }

        #endregion

        #region Order Asc Price


        public JsonResult PriceOrderAscSaleQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_28 = Get_PriceOrderAscSaleQuery() } };
            return result;
        }

        private IQueryable<Sale> Get_PriceOrderAscSaleQuery()
        {
            var s = GetSales();
            var aux = s.OrderBy(x => x.Price);
            return aux.AsQueryable();
        }


        #endregion

        #region Order Desc Count of sale product 

        public JsonResult ProductCountOrderDescSaleQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_29 = Get_ProductCountOrderDescSaleQuery() } };
            return result;
        }

        private IQueryable<Tuple<Product, int>> Get_ProductCountOrderDescSaleQuery()
        {
            var s = Get_ProductGroupByCountSaleQuery();
            var result = s.OrderByDescending(x => x.Item2);
            return result;
        }


        #endregion

        #endregion

        #region Wail

        public IQueryable<Wail> GetWails()
        {
            return _db.Wails.AsQueryable();
        }

        #region GroupBy Product => Count

        public JsonResult ProductGroupByCountWailQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_30 = Get_ProductGroupByCountWailQuery() } };
            return result;
        }

        private IQueryable<Tuple<Product, int>> Get_ProductGroupByCountWailQuery()
        {
            var w = GetWails();
            var p = GetProducts();
            var join = p.Join(w, x => x.ProductId, d => d.ProductId, TakeProduct);
            var result = new List<Tuple<Product, int>>();

            foreach (var item in w.GroupBy(x => x.ProductId))
            {
                var y = join.Where(x => x.ProductId == item.Key).ToList();

                //esto no pasa nunk
                if (y.Count > 1)
                    break;

                result.Add(new Tuple<Product, int>(y[0], item.Count()));
            }

            return result.AsQueryable();
        }

        #endregion

        #endregion

        #region Yearly Revision

        public IQueryable<YearlyRevision> GetYearlyRevisions()
        {
            return _db.YearlyRevisions.AsQueryable();
        }

        #region GroupBy Product => Count

        public JsonResult ProductGroupByCountYRevQuery()
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_31 = Get_ProductGroupByCountYRevQuery() } };
            return result;
        }

        private IQueryable<Tuple<Product, int>> Get_ProductGroupByCountYRevQuery()
        {
            var w = GetYearlyRevisions();
            var p = GetProducts();
            var join = p.Join(w, x => x.ProductId, d => d.ProductId, TakeProduct);
            var result = new List<Tuple<Product, int>>();

            foreach (var item in w.GroupBy(x => x.ProductId))
            {
                var y = join.Where(x => x.ProductId == item.Key).ToList();

                //esto no pasa nunk
                if (y.Count > 1)
                    break;

                result.Add(new Tuple<Product, int>(y[0], item.Count()));
            }

            return result.AsQueryable();
        }

        #endregion

        #region yr.where(r.Date > d1 && r.Date < d2), Date: d1, d2

        public JsonResult TakeYRevQuery(DateTime d1, DateTime d2)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_32 = Get_TakeYRevQuery(d1, d2) } };
            return result;
        }

        private IQueryable<YearlyRevision> Get_TakeYRevQuery(DateTime d1, DateTime d2)
        {
            var r = GetYearlyRevisions();
            var result = r.Where(x => (d1.Year <= x.Date.Year && x.Date.Year <= d2.Year) &&
                                      ((d1.Month < x.Date.Month && x.Date.Month < d2.Month) ||
                                       (d1.Month == x.Date.Month && d2.Month == x.Date.Month && d1.Day <= x.Date.Day && x.Date.Day <= d2.Day) ||
                                       (d1.Month == x.Date.Month && d1.Day <= x.Date.Day) || (x.Date.Day <= d2.Day)));

            return result.AsQueryable();
        }

        #endregion

        #region >= "x" yearly rev in [d1,d2] 

        public JsonResult XinIntervalYRevQuery(int count, DateTime d1, DateTime d2)
        {
            if (!Request.IsAjaxRequest())
            {
                return null;
            }

            var result = new JsonResult { Data = new { Q_33 = Get_XinIntervalYRevQuery(count, d1, d2) } };
            return result;
        }

        private IQueryable<YearlyRevision> Get_XinIntervalYRevQuery(int count, DateTime d1, DateTime d2)
        {
            var yr = Get_ProductGroupByCountYRevQuery();
            var yr_aux = yr.Where(x => x.Item2 >= count);

            var c = GetYearlyRevisions();
            var c_aux = c.Where(x => (d1.Year <= x.Date.Year && x.Date.Year <= d2.Year) &&
                                      ((d1.Month < x.Date.Month && x.Date.Month < d2.Month) ||
                                       (d1.Month == x.Date.Month && d2.Month == x.Date.Month && d1.Day <= x.Date.Day && x.Date.Day <= d2.Day) ||
                                       (d1.Month == x.Date.Month && d1.Day <= x.Date.Day) || (x.Date.Day <= d2.Day)));

            var join = c_aux.Join(yr_aux, x => x.ProductId, d => d.Item1.ProductId, TakeYRev).AsQueryable();
            return join;
        }

        private YearlyRevision TakeYRev(YearlyRevision yr, object y)
        {
            return yr;
        }

        #endregion

        #endregion
    }
}
