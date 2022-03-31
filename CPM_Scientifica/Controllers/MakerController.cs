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
    public class MakerController : Controller
    {
        App _db = new App();

        // GET: Maker
        public ActionResult Index()
        {
            return View(_db.Makers.ToList());
        }

        // GET: Maker/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var maker = _db.Makers.Find(id);

            if (maker == null)
            {
                return HttpNotFound();
            }

            return View(maker);

        }

        // GET: Maker/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maker/Create
        [HttpPost]
        public ActionResult Create(Maker maker)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _db.Makers.Add(maker);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(maker);
            }
            catch
            {
                return View(maker);
            }
        }

        // GET: Maker/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var maker = _db.Makers.Find(id);

            if (maker == null)
            {
                return HttpNotFound();
            }

            return View(maker);
        }

        // POST: Maker/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Maker maker)
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

                    var m = _db.Makers.Find(id);
                    m.Name = maker.Name;
                    m.GoodPracticesDocumentationDate = maker.GoodPracticesDocumentationDate;
                    m.GoodPracticesDocumentation = maker.GoodPracticesDocumentation;
                    m.LifeTimeYearsGPD = maker.LifeTimeYearsGPD;
                    m.AuthorityOfCommerceCameraDate = maker.AuthorityOfCommerceCameraDate;
                    m.AuthorityOfCommerceCamera = maker.AuthorityOfCommerceCamera;
                    m.LifeTimeYearsACC = maker.LifeTimeYearsACC;

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(maker);
            }
            catch
            {
                return View(maker);
            }

        }

        // GET: Maker/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var maker = _db.Makers.Find(id);

            if (maker == null)
            {
                return HttpNotFound();
            }

            return View(maker);
        }

        // POST: Maker/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Maker maker)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    maker = _db.Makers.Find(id);
                    if (maker == null)
                    {
                        return HttpNotFound();
                    }

                    #region products update

                    //se borran todos sus productos y sus relaciones con las demas entidades
                    foreach (var product in maker.Products.ToList())
                    {
                        var p = _db.Products.Find(product.ProductId);

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

                        _db.Products.Remove(p);
                    }

                    #endregion

                    _db.Makers.Remove(maker);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(maker);
            }
            catch
            {
                return View(maker);
            }
        }
    }
}
