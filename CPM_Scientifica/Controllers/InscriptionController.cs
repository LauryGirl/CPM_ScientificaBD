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
    public class InscriptionController : Controller
    {
        App _db = new App();

        // GET: Inscription
        public ActionResult Index()
        {
            return View(_db.Inscriptions.ToList());
        }

        // GET: Inscription/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ins = _db.Inscriptions.Find(id);

            if (ins == null)
            {
                return HttpNotFound();
            }

            return View(ins);
        }

        // GET: Inscription/Create
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(_db.Products, "ProductId", "Name");
            return View();
        }

        // POST: Inscription/Create
        [HttpPost]
        public ActionResult Create(Inscription ins)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    ins.NewRegister = new DateTime(ins.Date.Year + 5, ins.Date.Month, ins.Date.Day);
                    
                    #region product update

                    //product update
                    var p1 = _db.Products.Find(ins.ProductId);

                    //creo que esto no pasa, no puedes toamr un product q no existe
                    if (p1 == null)
                    {
                        return HttpNotFound();
                    }

                    ins.Product = p1;
                    p1.Registers.Add(ins);

                    _db.Inscriptions.Add(ins);
                    _db.Registers.Add(ins);

                   

                    #endregion

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(ins);
            }
            catch
            {
                return View(ins);
            }
        }

        // GET: Inscription/Edit/5
        public ActionResult Edit(int? id)
        {
            var x = _db.Inscriptions.Find(id);
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

            var ins = _db.Inscriptions.Find(id);

            if (ins == null)
            {
                return HttpNotFound();
            }

            return View(ins);
        }

        // POST: Inscription/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, Inscription inscription)
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

                    var a = _db.Inscriptions.Find(id);
                    var b = _db.Registers.Find(id);

                    var p1 = _db.Products.Find(a.ProductId);
                    p1.Registers.Remove(a);
                    
                    #region product update

                    //product update
                    a.Date = inscription.Date;
                    a.NewRegister = new DateTime(a.Date.Year + 5, a.Date.Month, a.Date.Day);
                    a.Validity = inscription.Validity;
                    a.ProductId = inscription.ProductId;
                    var p2 = _db.Products.Find(inscription.ProductId);
                    a.Product = p2;
                    p2.Registers.Add(a);

                    #endregion

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(inscription);
            }
            catch
            {
                return View(inscription);
            }

        }

        // GET: Inscription/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //en este caso crear vista
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var inscription = _db.Inscriptions.Find(id);

            if (inscription == null)
            {
                return HttpNotFound();
            }

            return View(inscription);
        }

        // POST: Inscription/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Inscription inscription)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    inscription = _db.Inscriptions.Find(id);
                    if (inscription == null)
                    {
                        return HttpNotFound();
                    }

                    //product update
                    var p1 = _db.Products.Find(inscription.ProductId);
                    p1.Registers.Remove(inscription);

                    _db.Inscriptions.Remove(inscription);
                    _db.Registers.Remove(inscription);


                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(inscription);
            }
            catch
            {
                return View(inscription);
            }
        }
    }
}
