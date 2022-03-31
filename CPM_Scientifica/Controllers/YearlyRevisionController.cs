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
    public class YearlyRevisionController : Controller
    {
        App _db = new App();

        // GET: YearlyRevision
        public ActionResult Index()
        {
            return View(_db.YearlyRevisions.ToList());
        }

        // GET: YearlyRevision/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var yearlyRevision = _db.YearlyRevisions.Find(id);

            if (yearlyRevision == null)
            {
                return HttpNotFound();
            }

            return View(yearlyRevision);

        }

        // GET: YearlyRevision/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View();
        }

        // POST: YearlyRevision/Create
        [HttpPost]
        public ActionResult Create(YearlyRevision yearlyRevision)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(yearlyRevision.ProductId);
                    
                    if (p1 == null)
                    {
                        return HttpNotFound();
                    }

                    p1.YearlyRevisions.Add(yearlyRevision);

                    #endregion

                    _db.YearlyRevisions.Add(yearlyRevision);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(yearlyRevision);
            }
            catch
            {
                return View(yearlyRevision);
            }
        }

        // GET: YearlyRevision/Edit/5
        public ActionResult Edit(int? id)
        {
            var x = _db.YearlyRevisions.Find(id);
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

            var yearlyRevision = _db.YearlyRevisions.Find(id);

            if (yearlyRevision == null)
            {
                return HttpNotFound();
            }

            return View(yearlyRevision);
        }

        // POST: YearlyRevision/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, YearlyRevision yearlyRevision)
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

                    var m = _db.YearlyRevisions.Find(id);

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(m.ProductId);
                    p1.YearlyRevisions.Remove(m);

                    m.Date = yearlyRevision.Date;
                    m.LimitRev = yearlyRevision.LimitRev;
                    m.ProductId = yearlyRevision.ProductId;
                    var p2 = _db.Products.Find(yearlyRevision.ProductId);
                    m.Product = p2;
                    p2.YearlyRevisions.Add(m);

                    #endregion

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(yearlyRevision);
            }
            catch
            {
                return View(yearlyRevision);
            }

        }

        // GET: YearlyRevision/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var yearlyRevision = _db.YearlyRevisions.Find(id);

            if (yearlyRevision == null)
            {
                return HttpNotFound();
            }

            return View(yearlyRevision);
        }

        // POST: YearlyRevision/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, YearlyRevision yearlyRevision)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    yearlyRevision = _db.YearlyRevisions.Find(id);
                    if (yearlyRevision == null)
                    {
                        return HttpNotFound();
                    }
                    
                    #region product update

                    //product update
                    var p1 = _db.Products.Find(yearlyRevision.ProductId);
                    p1.YearlyRevisions.Remove(yearlyRevision);

                    #endregion

                    _db.YearlyRevisions.Remove(yearlyRevision);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(yearlyRevision);
            }
            catch
            {
                return View(yearlyRevision);
            }
        }
    }
}
