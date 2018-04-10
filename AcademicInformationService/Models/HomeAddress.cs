using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicInformationService.Models
{
    public class HomeAddress : Address
    {
        public byte HomeAddressId { get; set; }

        [Required]
        public virtual Member Member { get; set; }
        // Just to deal with Entity Relation Issues
    }
}