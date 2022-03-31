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
    public class ChangeController : Controller
    {
        App _db = new App();

        // GET: Change
        public ActionResult Index()
        {
            return View(_db.Changes.ToList());
        }

        // GET: Change/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var change = _db.Changes.Find(id);

            if (change == null)
            {
                return HttpNotFound();
            }

            return View(change);

        }

        // GET: Change/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View();
        }

        // POST: Change/Create
        [HttpPost]
        public ActionResult Create(Change change)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    #region product update

                    //product update
                    var p1 = _db.Products.Find(change.ProductId);
                    
                    if (p1 == null)
                    {
                        return HttpNotFound();
                    }

                    p1.Changes.Add(change);

                    #endregion

                    _db.Changes.Add(change);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(change);
            }
            catch
            {
                return View(change);
            }
        }

        // GET: Change/Edit/5
        public ActionResult Edit(int? id)
        {
            var x = _db.Changes.Find(id);
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

            var change = _db.Changes.Find(id);

            if (change == null)
            {
                return HttpNotFound();
            }

            return View(change);
        }

        // POST: Change/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Change change)
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

                    var c = _db.Changes.Find(id);

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(c.ProductId);
                    p1.Changes.Remove(c);

                    c.Date = change.Date;
                    c.Reason = change.Reason;
                    c.ProductId = change.ProductId;
                    var p2 = _db.Products.Find(change.ProductId);
                    c.Product = p2;
                    p2.Changes.Add(c);

                    #endregion

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(change);
            }
            catch
            {
                return View(change);
            }

        }

        // GET: Change/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var change = _db.Changes.Find(id);

            if (change == null)
            {
                return HttpNotFound();
            }

            return View(change);
        }

        // POST: Change/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Change change)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    change = _db.Changes.Find(id);
                    if (change == null)
                    {
                        return HttpNotFound();
                    }

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(change.ProductId);
                    p1.Changes.Remove(change);

                    #endregion
                    _db.Changes.Remove(change);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(change);
            }
            catch
            {
                return View(change);
            }
        }
    }
}
