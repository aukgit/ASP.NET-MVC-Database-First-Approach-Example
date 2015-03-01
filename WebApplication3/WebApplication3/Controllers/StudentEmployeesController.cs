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
    public class StudentEmployeesController : Controller
    {
        private ASPDatabaseFirstEntities db = new ASPDatabaseFirstEntities();

        // GET: StudentEmployees
        public ActionResult Index()
        {
            var studentEmployees = db.StudentEmployees.Include(s => s.Employee).Include(s => s.Student);
            return View(studentEmployees.ToList());
        }

        // GET: StudentEmployees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentEmployee studentEmployee = db.StudentEmployees.Find(id);
            if (studentEmployee == null)
            {
                return HttpNotFound();
            }
            return View(studentEmployee);
        }

        // GET: StudentEmployees/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentID");
            return View();
        }

        // POST: StudentEmployees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentEmployeeID,StudentID,EmployeeID")] StudentEmployee studentEmployee)
        {
            if (ModelState.IsValid)
            {
                db.StudentEmployees.Add(studentEmployee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", studentEmployee.EmployeeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentID", studentEmployee.StudentID);
            return View(studentEmployee);
        }

        // GET: StudentEmployees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentEmployee studentEmployee = db.StudentEmployees.Find(id);
            if (studentEmployee == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", studentEmployee.EmployeeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentID", studentEmployee.StudentID);
            return View(studentEmployee);
        }

        // POST: StudentEmployees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentEmployeeID,StudentID,EmployeeID")] StudentEmployee studentEmployee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "EmployeeID", studentEmployee.EmployeeID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentID", studentEmployee.StudentID);
            return View(studentEmployee);
        }

        // GET: StudentEmployees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentEmployee studentEmployee = db.StudentEmployees.Find(id);
            if (studentEmployee == null)
            {
                return HttpNotFound();
            }
            return View(studentEmployee);
        }

        // POST: StudentEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentEmployee studentEmployee = db.StudentEmployees.Find(id);
            db.StudentEmployees.Remove(studentEmployee);
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
