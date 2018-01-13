﻿using System;
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
    public class OrdersController : Controller
    {
        private GettEntities db = new GettEntities();

        // GET: /Orders/
        public ActionResult Index()
        {
            List<Orders> activeOrdersList = new List<Orders>();
            foreach (var activeOrder in db.Orders.Where(o => o.IsActive == true).Include(o => o.Drivers).Include(o => o.Riders))
            {
                Orders temp = new Orders
                {
                    Destination = activeOrder.Destination,
                    Origin = activeOrder.Origin,
                    RiderName = activeOrder.Riders.Name,
                    DriverName = activeOrder.Drivers.Name

                };
                activeOrdersList.Add(temp);
            }
            return Json(activeOrdersList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InactiveOrders() {
            List<Orders> inactiveOrdersList = new List<Orders>();
            foreach (var inactiveOrder in db.Orders.Where(o => o.IsActive == false).Include(o => o.Drivers).Include(o => o.Riders))
            {
                Orders temp = new Orders
                {
                    Destination = inactiveOrder.Destination,
                    Origin = inactiveOrder.Origin,
                    RiderName = inactiveOrder.Riders.Name,
                    OrderID = inactiveOrder.OrderID
                };
                inactiveOrdersList.Add(temp);
            }
            return Json(inactiveOrdersList, JsonRequestBehavior.AllowGet);
        }

        // GET: /Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: /Orders/Create
        public ActionResult Create(int? id, string name)
        {
            TempData["RiderID"] = id;
            TempData["RiderName"] = name;
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "Name");
            ViewBag.RiderID = new SelectList(db.Riders, "RiderID", "Name");
            return PartialView();
        }

        // POST: /Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Destination,Origin")] Orders orders)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orders.RiderID = int.Parse(TempData["RiderID"].ToString());
                    orders.RiderName = TempData["RiderName"].ToString();
                    db.Orders.Add(orders);
                    db.SaveChanges();
                    return PartialView(orders);
                }

                ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "Name", orders.DriverID);
                ViewBag.RiderID = new SelectList(db.Riders, "RiderID", "Name", orders.RiderID);
                return null;

            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        // GET: /Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "Name", orders.DriverID);
            ViewBag.RiderID = new SelectList(db.Riders, "RiderID", "Name", orders.RiderID);
            return View(orders);
        }

        // POST: /Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="OrderID,Destination,Origin,RiderID,DriverID,IsActive")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverID = new SelectList(db.Drivers, "DriverID", "Name", orders.DriverID);
            ViewBag.RiderID = new SelectList(db.Riders, "RiderID", "Name", orders.RiderID);
            return View(orders);
        }

        [HttpPost]
        // POST: /Orders/Assign/5
        public ActionResult Assign(int? id, int? SelectedDriverID)
        {
            if (SelectedDriverID == null || id == null) {
                return null;
            }
            try
            {
                int selectedOrderID = (int)id;
                Drivers assignedDriver = db.Drivers.Where(d => d.DriverID == SelectedDriverID).FirstOrDefault();
                Orders selectedOrder = db.Orders.Where(o => o.OrderID == selectedOrderID).FirstOrDefault();

                selectedOrder.DriverID = assignedDriver.DriverID;
                selectedOrder.DriverName = assignedDriver.Name;
                selectedOrder.IsActive = true;
                db.Entry(selectedOrder).State = EntityState.Modified;
                db.SaveChanges();

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // GET: /Orders/Assign/5
        public ActionResult Assign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Orders> orders = db.Orders.ToList();
            List<int?> ordersDriversIds = orders.Where(o => o.DriverID != null).Select(o => o.DriverID).ToList();
            List<Drivers> availableDrivers = db.Drivers.Where(d => !ordersDriversIds.Contains(d.DriverID)).ToList();

            IEnumerable<SelectListItem> items =

                from value in availableDrivers

                select new SelectListItem

                {

                    Text = value.Name,

                    Value = value.DriverID.ToString()

                };

            ViewBag.AvailableDrivers = new SelectList(items, "Value", "Text");
            return PartialView("Assign");
        }

        // GET: /Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: /Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
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
