using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicInformationService.Models
{
    // UK Only Address - could be enhanced to work with other countries
    public class Address
    {
        [Required]
        public string Building { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Town or City")]
        public string TownCity { get; set; }

        [Required]
        [Display(Name = "County (optional)")] // Following UK Gov Standards
        public string County { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
    }
}