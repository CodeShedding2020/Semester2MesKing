using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Messenger_Kings.Models;
using Microsoft.AspNet.Identity;

namespace Messenger_Kings.Controllers
{
    public class PointsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Points
        public ActionResult Index()
        {
            var uid = User.Identity.GetUserId();
            var points = db.Points.Include(p => p.Client).Where(p=>p.Client_ID == uid);
           
            return View(points.ToList());
        }

        [ChildActionOnly]
        public ActionResult TotalPoints()
        {
            var uid = User.Identity.GetUserId();
            var points = db.Points.Include(p => p.Client).Where(p => p.Client_ID == uid).Count();
            ViewBag.Count = points;
            return PartialView();
        }
        // GET: Points/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Points points = db.Points.Find(id);
            if (points == null)
            {
                return HttpNotFound();
            }
            return View(points);
        }

        // GET: Points/Create
        public ActionResult Create()
        {
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_IDNo");
            return View();
        }

        // POST: Points/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Points_ID,ClientPoints,Client_ID")] Points points)
        {
            if (ModelState.IsValid)
            {
                db.Points.Add(points);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_IDNo", points.Client_ID);
            return View(points);
        }

        // GET: Points/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Points points = db.Points.Find(id);
            if (points == null)
            {
                return HttpNotFound();
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_IDNo", points.Client_ID);
            return View(points);
        }

        // POST: Points/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Points_ID,ClientPoints,Client_ID")] Points points)
        {
            if (ModelState.IsValid)
            {
                db.Entry(points).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_IDNo", points.Client_ID);
            return View(points);
        }

        // GET: Points/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Points points = db.Points.Find(id);
            if (points == null)
            {
                return HttpNotFound();
            }
            return View(points);
        }

        // POST: Points/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Points points = db.Points.Find(id);
            db.Points.Remove(points);
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
