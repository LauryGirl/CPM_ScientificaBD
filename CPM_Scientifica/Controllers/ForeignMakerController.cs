using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Data;
using App = CPM_Scientifica.Context.AppContext;

namespace prove.Controllers
{
    public class ForeignMakerController : Controller
    {
        App _db = new App();

        // GET: ForeignMaker
        public ActionResult Index()
        {
            return View(_db.ForeignMakers.ToList());
        }

        // GET: ForeignMaker/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var fmaker = _db.ForeignMakers.Find(id);

            if (fmaker == null)
            {
                return HttpNotFound();
            }

            return View(fmaker);

        }

        // GET: ForeignMaker/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ForeignMaker/Create
        [HttpPost]
        public ActionResult Create(ForeignMaker fmaker)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    _db.ForeignMakers.Add(fmaker);
                    _db.Makers.Add(fmaker);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(fmaker);
            }
            catch
            {
                return View(fmaker);
            }
        }

        // GET: ForeignMaker/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var foreingm = _db.ForeignMakers.Find(id);

            if (foreingm == null)
            {
                return HttpNotFound();
            }

            return View(foreingm);
        }

        // POST: ForeignMaker/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, ForeignMaker maker)
        {
            try
            {
                if (id == null)
                {
                    //cambiar a vista
                    return HttpNotFound();
                }

                var fm = _db.ForeignMakers.Find(id);
                var m = _db.Makers.Find(id);

                fm.Name = maker.Name;
                fm.GoodPracticesDocumentationDate = maker.GoodPracticesDocumentationDate;
                fm.GoodPracticesDocumentation = maker.GoodPracticesDocumentation;
                fm.LifeTimeYearsGPD = maker.LifeTimeYearsGPD;
                fm.AuthorityOfCommerceCameraDate = maker.AuthorityOfCommerceCameraDate;
                fm.AuthorityOfCommerceCamera = maker.AuthorityOfCommerceCamera;
                fm.LifeTimeYearsACC = maker.LifeTimeYearsACC;
                (fm as ForeignMaker).CertificateFreeSaleDate = (maker as ForeignMaker).CertificateFreeSaleDate;
                (fm as ForeignMaker).CertificateFreeSale = (maker as ForeignMaker).CertificateFreeSale;
                (fm as ForeignMaker).LifeTimeYearsCFS = (maker as ForeignMaker).LifeTimeYearsCFS;
                (fm as ForeignMaker).Country = (maker as ForeignMaker).Country;

                m.GoodPracticesDocumentationDate = maker.GoodPracticesDocumentationDate;
                m.GoodPracticesDocumentation = maker.GoodPracticesDocumentation;
                m.LifeTimeYearsGPD = maker.LifeTimeYearsGPD;
                m.AuthorityOfCommerceCameraDate = maker.AuthorityOfCommerceCameraDate;
                m.AuthorityOfCommerceCamera = maker.AuthorityOfCommerceCamera;
                m.LifeTimeYearsACC = maker.LifeTimeYearsACC;

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ForeignMaker/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var maker = _db.ForeignMakers.Find(id);

            if (maker == null)
            {
                return HttpNotFound();
            }

            return View(maker);
        }

        // POST: ForeignMaker/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, ForeignMaker maker)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    maker = _db.ForeignMakers.Find(id);
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
                    _db.ForeignMakers.Remove(maker);

                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
                
            }
            catch
            {
                return View(maker);
            }

        }
    }
}
