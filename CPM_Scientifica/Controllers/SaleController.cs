using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App = CPM_Scientifica.Context.AppContext;

namespace CPM_Scientifica.Controllers
{
    public class SaleController : Controller
    {
        App _db = new App();
        // GET: Sale
        public ActionResult Index()
        {
            return View(_db.Sales.ToList());
        }

        // GET: Sale/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sale = _db.Sales.Find(id);

            if (sale == null)
            {
                return HttpNotFound();
            }

            return View(sale);

        }

        // GET: Sale/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View();
        }

        // POST: Sale/Create
        [HttpPost]
        public ActionResult Create(Sale sale)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(sale.ProductId);

                    //creo que esto no pasa, no puedes toamr un product q no existe
                    if (p1 == null)
                    {
                        return HttpNotFound();
                    }

                    p1.Sales.Add(sale);

                    #endregion

                    _db.Sales.Add(sale);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sale);
            }
            catch
            {
                return View(sale);
            }
        }

        // GET: Sale/Edit/5
        public ActionResult Edit(int? id)
        {
            var x = _db.Sales.Find(id);
            var p = _db.Products.Find(x.ProductId);
            List<Product> list = new List<Product>();
            list.Add(p);
            foreach (var item in _db.Products)
            {
                if (!list.Contains(item)) list.Add(item);
            }
            ViewBag.ProductId = new SelectList(list, "ProductId", "Name");
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sale = _db.Sales.Find(id);

            if (sale == null)
            {
                return HttpNotFound();
            }

            return View(sale);
        }

        // POST: Sales/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Sale sale)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (id == null)
                    {
                        //cambiar a vista
                        return HttpNotFound();
                    }

                    var s = _db.Sales.Find(id);

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(s.ProductId);
                    p1.Sales.Remove(s);

                    s.Date = sale.Date;
                    s.Center = sale.Center;
                    s.Price = sale.Price;
                    s.ProductId = sale.ProductId;
                    var p2 = _db.Products.Find(sale.ProductId);
                    s.Product = p2;
                    p2.Sales.Add(s);

                    #endregion


                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sale);
            }
            catch
            {
                return View(sale);
            }

        }

        // GET: Sale/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sale = _db.Sales.Find(id);

            if (sale == null)
            {
                return HttpNotFound();
            }

            return View(sale);
        }

        // POST: Sale/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Sale sale)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    sale = _db.Sales.Find(id);
                    if (sale == null)
                    {
                        return HttpNotFound();
                    }

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(sale.ProductId);
                    p1.Sales.Remove(sale);

                    #endregion

                    _db.Sales.Remove(sale);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sale);
            }
            catch
            {
                return View(sale);
            }
        }
    }
}
