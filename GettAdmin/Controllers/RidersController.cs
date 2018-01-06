using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GettAdmin;

namespace GettAdmin.Controllers
{
    public class RidersController : Controller
    {
        private GettEntities db = new GettEntities();

        // GET: /Riders/
        public ActionResult Index()
        {
            return View(db.Riders.ToList());
        }

        // GET: /Riders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Riders riders = db.Riders.Find(id);
            if (riders == null)
            {
                return HttpNotFound();
            }
            return View(riders);
        }

        // GET: /Orders/Create
        public ActionResult Create(int? id, string name)
        {
            TempData["RiderID"] = id;
            TempData["RiderName"] = name;
            return RedirectToAction("Create", "Orders");
        }

        // POST: /Riders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RiderID,Name,Phone,Email")] Riders riders)
        {
            if (ModelState.IsValid)
            {
                db.Riders.Add(riders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(riders);
        }

        // GET: /Riders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Riders riders = db.Riders.Find(id);
            if (riders == null)
            {
                return HttpNotFound();
            }
            return View(riders);
        }

        // POST: /Riders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RiderID,Name,Phone,Email")] Riders riders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(riders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(riders);
        }

        // GET: /Riders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Riders riders = db.Riders.Find(id);
            if (riders == null)
            {
                return HttpNotFound();
            }
            return View(riders);
        }

        // POST: /Riders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Riders riders = db.Riders.Find(id);
            db.Riders.Remove(riders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
