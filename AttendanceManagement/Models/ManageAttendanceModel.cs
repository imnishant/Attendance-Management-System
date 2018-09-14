using AttendanceManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AttendanceManagement.Models
{
    public class ManageAttendanceModel
    {


        public List<string> Branch { get; set; }

        public List<string> Semester { get; set; }

        public List<string> Section { get; set; }

        public List<string> Departments { get; set; }

        public List<string> Slot { get; set; }

        public DateTime Date { get; set; }



        // [Required]
        //  [Display(Name = "Date")]
        // public DateTime Date { get; set; }



        //   [Required]
        //   [Display(Name = "Slot")]
        //   public int Slot { get; set; }

    }
}