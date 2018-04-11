using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicInformationService.Models
{
    public class Report
    {
        public Report()
        {
            Members = new HashSet<Member>();
        }

        public int ReportId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } // Name/Title
        
        [Display(Name = "Date Published")]
        public DateTime ReportDate { get; set; }

        [Display(Name = "Abstract")]
        public string Abstract { get; set; }

        [Display(Name = "Report")]
        public string ReportText { get; set; } // This can just be dummy text or lorem ipsum

        [Display(Name = "Report Authors and Collaborators")]
        public virtual ICollection<Member> Members { get; set; }
    }
}