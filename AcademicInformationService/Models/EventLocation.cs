using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AcademicInformationService.Models
{
    public class EventLocation : Address
    {
        [Key]
        [ForeignKey("Event")]
        public int EventId { get; set; }

        [Required]
        public virtual Event Event { get; set; }
        // Just to deal with Entity Relation Issues
    }
}