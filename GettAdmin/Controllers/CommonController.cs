using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GettAdmin;
using GettAdmin.Models;

namespace GettAdmin.Controllers
{
    public class CommonController : Controller
    {
        private GettEntities db = new GettEntities();

        // GET: /Common/
        //public ActionResult Index()
        //{
        //    return View(db.Drivers.ToList());
        //}

        public ActionResult Index()
        {
            CommonViewModel model = new CommonViewModel();
            model.DriverModel = new Drivers();
            model.RiderModel = new Riders();
            model.OrderModel = new Orders();

            return View(model);
        }
        // GET: /Common/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drivers drivers = db.Drivers.Find(id);
            if (drivers == null)
            {
                return HttpNotFound();
            }
            return View(drivers);
        }

        // GET: /Common/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Common/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="DriverID,Name,Phone,Email")] Drivers drivers)
        {
            if (ModelState.IsValid)
            {
                db.Drivers.Add(drivers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drivers);
        }

        // GET: /Common/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drivers drivers = db.Drivers.Find(id);
            if (drivers == null)
            {
                return HttpNotFound();
            }
            return View(drivers);
        }

        // POST: /Common/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DriverID,Name,Phone,Email")] Drivers drivers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drivers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drivers);
        }

        // GET: /Common/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drivers drivers = db.Drivers.Find(id);
            if (drivers == null)
            {
                return HttpNotFound();
            }
            return View(drivers);
        }

        // POST: /Common/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Drivers drivers = db.Drivers.Find(id);
            db.Drivers.Remove(drivers);
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
