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
    public class InquestController : Controller
    {
        App _db = new App();

        // GET: Inquest
        public ActionResult Index()
        {
            return View(_db.Inquests.ToList());
        }

        // GET: Inquest/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var inquest = _db.Inquests.Find(id);

            if (inquest == null)
            {
                return HttpNotFound();
            }

            return View(inquest);
        }

        // GET: Inquest/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View();
        }

        // POST: Inquest/Create
        [HttpPost]
        public ActionResult Create(Inquest inquest)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(inquest.ProductId);
                    
                    if (p1 == null)
                    {
                        return HttpNotFound();
                    }

                    inquest.Product = p1;
                    p1.Inquests.Add(inquest);

                    #endregion
                    _db.Inquests.Add(inquest);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(inquest);
            }
            catch
            {
                return View(inquest);
            }
        }

        // GET: Inquest/Edit/5
        public ActionResult Edit(int? id)
        {
            var x = _db.Inquests.Find(id);
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

            var inquest = _db.Inquests.Find(id);

            if (inquest == null)
            {
                return HttpNotFound();
            }

            return View(inquest);
        }

        // POST: Inquest/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Inquest inquest)
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

                    var i = _db.Inquests.Find(id);

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(i.ProductId);
                    p1.Inquests.Remove(i);

                    i.Date = inquest.Date;
                    i.Receiver = inquest.Receiver;
                    i.Recommendation = inquest.Recommendation;
                    i.Center = inquest.Center;
                    i._type = inquest._type;
                    i.ProductId = inquest.ProductId;
                    var p2 = _db.Products.Find(inquest.ProductId);
                    i.Product = p2;
                    p2.Inquests.Add(i);

                    #endregion

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(inquest);
            }
            catch
            {
                return View(inquest);
            }
        }

        // GET: Inquest/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var inquest = _db.Inquests.Find(id);

            if (inquest == null)
            {
                return HttpNotFound();
            }

            return View(inquest);
        }

        // POST: Inquest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Inquest inquest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    inquest = _db.Inquests.Find(id);
                    if (inquest == null)
                    {
                        return HttpNotFound();
                    }
                    
                    #region product update

                    //product update
                    var p1 = _db.Products.Find(inquest.ProductId);
                    p1.Inquests.Remove(inquest);

                    #endregion

                    _db.Inquests.Remove(inquest);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(inquest);
            }
            catch
            {
                return View(inquest);
            }
        }
    }
}
