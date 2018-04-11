using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicInformationService.Models
{
    public class Event
    {
        public Event()
        {
            Members = new HashSet<Member>();
        }

        public int EventId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Event Location")]
        public virtual EventLocation EventLocation { get; set; }

        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Event Members")]
        public virtual ICollection<Member> Members { get; set; }
    }
}