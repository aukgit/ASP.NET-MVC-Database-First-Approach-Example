using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models.EntityModel;

namespace WebApplication3.Controllers
{
    public class NewTablesController : Controller
    {
        private ASPDatabaseFirstEntities db = new ASPDatabaseFirstEntities();

        // GET: NewTables
        public ActionResult Index()
        {
            var newTables = db.TestEmployeesViews("david");
            return View(newTables.ToList());
        }

        // GET: NewTables/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewTable newTable = db.NewTables.Find(id);
            if (newTable == null)
            {
                return HttpNotFound();
            }
            return View(newTable);
        }

        // GET: NewTables/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID");
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "FirstName");
            return View();
        }

        // POST: NewTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NewTableID,PersonID,EmployeeID")] NewTable newTable)
        {
            if (ModelState.IsValid)
            {
                db.NewTables.Add(newTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", newTable.EmployeeID);
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "FirstName", newTable.PersonID);
            return View(newTable);
        }

        // GET: NewTables/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewTable newTable = db.NewTables.Find(id);
            if (newTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", newTable.EmployeeID);
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "FirstName", newTable.PersonID);
            return View(newTable);
        }

        // POST: NewTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NewTableID,PersonID,EmployeeID")] NewTable newTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", newTable.EmployeeID);
            ViewBag.PersonID = new SelectList(db.People, "PersonID", "FirstName", newTable.PersonID);
            return View(newTable);
        }

        // GET: NewTables/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewTable newTable = db.NewTables.Find(id);
            if (newTable == null)
            {
                return HttpNotFound();
            }
            return View(newTable);
        }

        // POST: NewTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            NewTable newTable = db.NewTables.Find(id);
            db.NewTables.Remove(newTable);
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
