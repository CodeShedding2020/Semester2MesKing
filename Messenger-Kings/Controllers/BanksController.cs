﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Messenger_Kings.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace Messenger_Kings.Controllers
{
    [Authorize(Roles = "Client")]
    public class BanksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Banks
        public ActionResult Index()
        {
            var banks = db.Banks.Include(b => b.AccountCategory).Include(b => b.BankCategory).Include(b => b.Client);
            return View(banks.ToList());
        }

        // GET: Banks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bank bank = db.Banks.Find(id);
            if (bank == null)
            {
                return HttpNotFound();
            }
            return View(bank);
        }

        // GET: Banks/Create
        public ActionResult Create()
        {
            
            ViewBag.AccountCat_ID = new SelectList(db.AccountCategories, "AccountCat_ID", "Account_Type");
            ViewBag.BankCat_ID = new SelectList(db.BankCategories, "BankCat_ID", "Bank_Name");
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_IDNo");
         
            return View();
        }

        // POST: Banks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Bank_ID,Bank_Account_Holder,Bank_Account_Number,Debit_Date,Client_ID,BankCat_ID,AccountCat_ID")] Bank bank)
        {
            if (ModelState.IsValid)
            {
                var uid = User.Identity.GetUserId();
                bank.Client_ID = uid;
                var holder = (from h in db.Clients
                              where h.Client_ID == uid
                              select h.Client_Name).FirstOrDefault();
                bank.Bank_Account_Holder = holder;
                db.Banks.Add(bank);
                db.SaveChanges();

                return RedirectToAction("Create", "Documents", new { id = uid });
            }

            ViewBag.AccountCat_ID = new SelectList(db.AccountCategories, "AccountCat_ID", "Account_Type", bank.AccountCat_ID);
            ViewBag.BankCat_ID = new SelectList(db.BankCategories, "BankCat_ID", "Bank_Name", bank.BankCat_ID);
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_IDNo", bank.Client_ID);
            return View(bank);
        }

        // GET: Banks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bank bank = db.Banks.Find(id);
            if (bank == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountCat_ID = new SelectList(db.AccountCategories, "AccountCat_ID", "Account_Type", bank.AccountCat_ID);
            ViewBag.BankCat_ID = new SelectList(db.BankCategories, "BankCat_ID", "Bank_Name", bank.BankCat_ID);
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_IDNo", bank.Client_ID);
            return View(bank);
        }

        // POST: Banks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Bank_ID,Bank_Account_Holder,Bank_Account_Number,Debit_Date,Client_ID,BankCat_ID,AccountCat_ID")] Bank bank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountCat_ID = new SelectList(db.AccountCategories, "AccountCat_ID", "Account_Type", bank.AccountCat_ID);
            ViewBag.BankCat_ID = new SelectList(db.BankCategories, "BankCat_ID", "Bank_Name", bank.BankCat_ID);
            ViewBag.Client_ID = new SelectList(db.Clients, "Client_ID", "Client_IDNo", bank.Client_ID);
            return View(bank);
        }

        // GET: Banks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bank bank = db.Banks.Find(id);
            if (bank == null)
            {
                return HttpNotFound();
            }
            return View(bank);
        }

        // POST: Banks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bank bank = db.Banks.Find(id);
            db.Banks.Remove(bank);
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
