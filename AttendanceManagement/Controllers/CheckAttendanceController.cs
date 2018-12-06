using AttendanceManagement.Models;
using AttendanceManagement.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AttendanceManagement.Controllers
{
    public class CheckAttendanceController : Controller
    {

        private AttendanceManagementDBEntities1 db = new AttendanceManagementDBEntities1();

        private static AttendanceViewModel attendanceViewModel = new AttendanceViewModel();


        // GET: CheckAttendance
        public ActionResult Index()
        {
            ViewBag.Department_DID = new SelectList(db.Departments, "DID", "Name");
            ViewBag.Section = new SelectList(db.Students, "Section", "Section");
            ViewBag.Sem = new SelectList(db.Students, "Sem", "Sem");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CheckAttendanceModel classViewModel)
        {
            return RedirectToAction("GetAttendance", "CheckAttendance", new { departmentID = classViewModel.Department_DID, Semester = classViewModel.Sem, section = classViewModel.Section, slot = classViewModel.Slot, date = classViewModel.Date });
        }



        [HttpGet]
        public ActionResult GetAttendance(string departmentID, int Semester, string section, string slot, DateTime date)
        {


            AttendanceViewModel attendanceViewModel = new AttendanceViewModel();


            AttendanceManagementDBEntities1 db = new AttendanceManagementDBEntities1();
            var customer = db.AspNetUsers.FirstOrDefault(u => u.Email == User.Identity.Name);

            var teacher = db.Teachers.FirstOrDefault(u => u.REFID == customer.Id);
            var students = db.Students.Where(s => s.Department_DID == (departmentID)).Where(s => s.Sem == (Semester)).Where(s => s.Section == (section)).ToList();

            //var subject = db.Subjects.FirstOrDefault(u => u.Department_DID == teacher.Department_DID);
            if (students.Count == 0)
                return RedirectToAction("Index", "TeachersPortal");
            string usn = students[0].USN;
            
            var tts = db.Teacher_Teaches_Student.Where(u => u.Teacher_TID == teacher.TID).FirstOrDefault(u => u.Student_USN == usn);


            string dt = date.ToShortDateString();

            var attendances = db.Attendances.Where(s => s.Teacher_TID == teacher.TID).Where(s => s.Subject_SubCode == tts.Subject_SubCode).Where(s => s.Date == dt).Where(s => s.Slot == slot).ToList();

            

            for (int i = 0; i < attendances.Count; i++)
            {
                var studentusn = attendances[i].Student_USN;
                int totalClasses = db.Attendances.Where(s => s.Teacher_TID == teacher.TID).Where(s => s.Subject_SubCode == tts.Subject_SubCode).Where(s => s.Student_USN == studentusn).ToList().Count;
                int presentClasses = db.Attendances.Where(s => s.Teacher_TID == teacher.TID).Where(s => s.Subject_SubCode == tts.Subject_SubCode).Where(s => s.Student_USN == studentusn).Where(s => s.IsPresent == 1).ToList().Count;
                double percentage = ((presentClasses * 1.0) / totalClasses) * 100.0;
                attendanceViewModel.presentClasses.Add(presentClasses);
                attendanceViewModel.totalClass = totalClasses;
                attendanceViewModel.percentageOfAttendance.Add(percentage);


            }
            



            attendanceViewModel.Attds = attendances;
            attendanceViewModel.Date = dt;
            attendanceViewModel.Slot = slot;
            attendanceViewModel.TeacherId = teacher.TID;
            attendanceViewModel.SubjectCode = tts.Subject_SubCode;
            attendanceViewModel.Students = students;


            return View(attendanceViewModel);


        }



    }
}