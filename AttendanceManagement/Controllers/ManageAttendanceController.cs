using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttendanceManagement.Models;
using System.Data.Entity;
using System.Collections;

namespace AttendanceManagement.Controllers
{
    public class ManageAttendanceController : Controller
    {
        private AttendanceManagementDBEntities1 db = new AttendanceManagementDBEntities1();

        // GET: ManageAttendance/Index
        public ActionResult Index()
        {

            // new SelectList(db.students, "field1", "field2")
            // field1 denotes which field from db has to be selected
            // field2 denotes which attribute corresponding to field1 has to be passed to controller URL

            ViewBag.Department_DID = new SelectList(db.Departments, "DID", "Name");
            ViewBag.Section = new SelectList(db.Students, "Section", "Section");
            ViewBag.Sem = new SelectList(db.Students, "Sem", "Sem");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Student student)
        {
            return RedirectToAction("AddAttendance", "ManageAttendance", new { departmentID = student.Department_DID, Semester = student.Sem, section = student.Section, slot = student.Slot, date = student.Date });
        }

        public ActionResult AddAttendance(string departmentID, int Semester, string section, string slot)
        {
            var getStudent = db.Students.Find(departmentID);
            var students = db.Students.Where(s => s.Department_DID == (departmentID)).Where(s => s.Sem == (Semester)).Where(s => s.Section == (section)).ToList();
            return View(students);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAttendance([Bind(Include = "Student_USN,Teacher_TID,Subject_SubCode,Date,Slot,Status")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                db.Attendances.Add(attendance);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attendance);
        }

        public ActionResult Invalid()
        {
            return View();
        }

    }
}