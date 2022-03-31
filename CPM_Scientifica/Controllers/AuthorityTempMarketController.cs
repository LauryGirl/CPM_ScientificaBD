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
    public class AuthorityTempMarketController : Controller
    {
        App _db = new App();
        // GET: AuthorityTemporalMarket
        public ActionResult Index()
        {
            return View(_db.AuthorityTemporalMarkets.ToList());
        }

        // GET: AuthorityTemporalMarket/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var auth = _db.AuthorityTemporalMarkets.Find(id);

            if (auth == null)
            {
                return HttpNotFound();
            }

            return View(auth);

        }

        // GET: AuthorityTemporalMarket/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View();
        }

        // POST: AuthorityTemporalMarket/Create
        [HttpPost]
        public ActionResult Create(AuthorityTempMarket authority)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    authority.NewRegister = new DateTime(authority.Date.Year + authority._montoYear, authority.Date.Month, authority.Date.Day);
                    
                    #region product update

                    //product update
                    var p1 = _db.Products.Find(authority.ProductId);

                    //creo que esto no pasa, no puedes toamr un product q no existe
                    if (p1 == null)
                    {
                        return HttpNotFound();
                    }

                    authority.Product = p1;
                    p1.Registers.Add(authority);

                    #endregion

                    _db.AuthorityTemporalMarkets.Add(authority);
                    _db.Registers.Add(authority);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(authority);
            }
            catch
            {
                return View(authority);
            }
        }

        // GET: AuthorityTemporalMarket/Edit/5
        public ActionResult Edit(int? id)
        {
            var x = _db.AuthorityTemporalMarkets.Find(id);
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

            var auth = _db.AuthorityTemporalMarkets.Find(id);

            if (auth == null)
            {
                return HttpNotFound();
            }

            return View(auth);

        }

        // POST: AuthorityTemporalMarket/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, AuthorityTempMarket auth)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (id == null)
                    {
                        //biar a vista
                        return HttpNotFound();
                    }

                    var a = _db.AuthorityTemporalMarkets.Find(id);
                    var b = _db.Registers.Find(id);

                    var p1 = _db.Products.Find(a.ProductId);
                    p1.Registers.Remove(a);


                    #region product update

                    //product update

                    a.Date = auth.Date;
                    a.Documentation = auth.Documentation;
                    a._montoYear = auth._montoYear;
                    a.NewRegister = new DateTime(a.Date.Year + a._montoYear, a.Date.Month, a.Date.Day);
                    a.ProductId = auth.ProductId;
                    var p2 = _db.Products.Find(auth.ProductId);
                    a.Product = p2;
                    p2.Registers.Add(a);

                    #endregion

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(auth);
            }
            catch
            {
                return View(auth);
            }

        }

        // GET: AuthorityTemporalMarket/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var auth = _db.AuthorityTemporalMarkets.Find(id);

            if (auth == null)
            {
                return HttpNotFound();
            }

            return View(auth);

        }

        // POST: AuthorityTemporalMarket/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, AuthorityTempMarket auth)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    auth = _db.AuthorityTemporalMarkets.Find(id);
                    if (auth == null)
                    {
                        return HttpNotFound();
                    }

                    #region product update

                    //product update
                    var p1 = _db.Products.Find(auth.ProductId);
                    p1.Registers.Remove(auth);

                    #endregion

                    _db.AuthorityTemporalMarkets.Remove(auth);
                    _db.Registers.Remove(auth);
                    
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(auth);
            }
            catch
            {
                return View(auth);
            }

        }
    }
}
