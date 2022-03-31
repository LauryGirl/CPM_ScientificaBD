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
    public class WailController : Controller
    {
        App _db = new App();
        // GET: Wail
        public ActionResult Index()
        {
            return View(_db.Wails.ToList());
        }

        // GET: Wail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var wail = _db.Wails.Find(id);

            if (wail == null)
            {
                return HttpNotFound();
            }

            return View(wail);

        }

        // GET: Wail/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View();
        }

        // POST: Wail/Create
        [HttpPost]
        public ActionResult Create(Wail wail)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(wail.ProductId);

                    //creo que esto no pasa, no puedes toamr un product q no existe
                    if (p1 == null)
                    {
                        return HttpNotFound();
                    }

                    p1.Wails.Add(wail);

                    #endregion

                    _db.Wails.Add(wail);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(wail);
            }
            catch
            {
                return View(wail);
            }
        }

        // GET: Wail/Edit/5
        public ActionResult Edit(int? id)
        {
            var x = _db.Wails.Find(id);
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

            var wail = _db.Wails.Find(id);

            if (wail == null)
            {
                return HttpNotFound();
            }

            return View(wail);
        }

        // POST: Wail/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Wail wail)
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

                    var m = _db.Wails.Find(id);

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(m.ProductId);
                    p1.Wails.Remove(m);

                    m.CecmedInfo = wail.CecmedInfo;
                    m.MakerInfo = wail.MakerInfo;
                    m.Closure = wail.Closure;
                    m._state = wail._state;
                    m.ProductId = wail.ProductId;
                    var p2 = _db.Products.Find(wail.ProductId);
                    m.Product = p2;
                    p2.Wails.Add(m);

                    #endregion

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(wail);
            }
            catch
            {
                return View(wail);
            }

        }

        // GET: Wail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var wail = _db.Wails.Find(id);

            if (wail == null)
            {
                return HttpNotFound();
            }

            return View(wail);
        }

        // POST: Wail/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Wail wail)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    wail = _db.Wails.Find(id);
                    if (wail == null)
                    {
                        return HttpNotFound();
                    }

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(wail.ProductId);
                    p1.Wails.Remove(wail);

                    #endregion

                    _db.Wails.Remove(wail);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(wail);
            }
            catch
            {
                return View(wail);
            }
        }
    }
}
