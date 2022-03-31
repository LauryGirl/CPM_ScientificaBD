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
    public class ProductController : Controller
    {
        App _db = new App();

        // GET: Product
        public ActionResult Index()
        {
            return View(_db.Products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);

        }

        // GET: Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MakerId = new SelectList(_db.Makers, "MakerId", "Name");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {

            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    
                    _db.Products.Add(product);

                    #region maker update

                    //product update
                    var m = _db.Makers.Find(product.MakerId);

                    //creo que esto no pasa, no puedes toamr un product q no existe
                    if (m == null)
                    {
                        return HttpNotFound();
                    }

                    m.Products.Add(product);

                    #endregion

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            var p = _db.Products.Find(id);
            var m = _db.Makers.Find(p.MakerId);
            List<Maker> list = new List<Maker>();
            list.Add(m);
            foreach(var item in _db.Makers)
            {
                if (!list.Contains(item)) list.Add(item);
            }
            ViewBag.MakerId = new SelectList(list, "MakerId", "Name");
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Product product)
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

                    var p = _db.Products.Find(id);

                    #region maker update

                    //maker update
                    var m = _db.Makers.Find(p.MakerId);
                    var new_m = _db.Makers.Find(product.MakerId);
                    m.Products.Remove(p);

                    p.Name = product.Name;
                    p.Application = product.Application;
                    p.Family = product.Family;
                    p.Maker = new_m;
                    p.MakerId = product.MakerId;
                    p.Presentation = product.Presentation;
                    p.Ref = product.Ref;
                    p.RegisterPresent = product.RegisterPresent;
                    p.System = product.System;
                    p._state = product._state;
                    p._type = product._type;

                    m.Products.Add(p);

                    #endregion

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch
            {
                return View(product);
            }

        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, Product p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    p = _db.Products.Find(id);
                    if (p == null)
                    {
                        return HttpNotFound();
                    }


                    #region maker update

                    //maker update
                    var m = _db.Makers.Find(p.MakerId);
                    m.Products.Remove(p);

                    #endregion

                    #region update
                    
                    foreach (var item in p.Changes.ToList())
                    {
                        var i = _db.Changes.Find(item.ChangeId);
                        _db.Changes.Remove(i);
                    }
                    foreach (var item in p.Inquests.ToList())
                    {
                        var i = _db.Inquests.Find(item.InquestId);
                        _db.Inquests.Remove(i);
                    }
                    foreach (var item in p.Registers.ToList())
                    {
                        var i = _db.Registers.Find(item.RegisterId);

                        if (i is AuthorityTempMarket)
                            _db.AuthorityTemporalMarkets.Remove(i as AuthorityTempMarket);
                        else
                            _db.Inscriptions.Remove(i as Inscription);

                        _db.Registers.Remove(i);
                    }
                    foreach (var item in p.Sales.ToList())
                    {
                        var i = _db.Sales.Find(item.SaleId);
                        _db.Sales.Remove(i);
                    }
                    foreach (var item in p.Wails.ToList())
                    {
                        var i = _db.Wails.Find(item.WailId);
                        _db.Wails.Remove(i);
                    }
                    foreach (var item in p.YearlyRevisions.ToList())
                    {
                        var i = _db.YearlyRevisions.Find(item.YearlyRevisionId);
                        _db.YearlyRevisions.Remove(i);
                    }

                    #endregion

                    _db.Products.Remove(p);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                return View(p);
            }
            catch
            {
                return View(p);
            }
        }
    }
}
