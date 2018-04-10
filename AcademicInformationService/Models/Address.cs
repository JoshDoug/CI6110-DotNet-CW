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

        // https://en.wikipedia.org/wiki/Postcodes_in_the_United_Kingdom#Validation
        // Using the UK Gov Postal Code Regex Standard available at: https://assets.publishing.service.gov.uk/government/uploads/system/uploads/attachment_data/file/488478/Bulk_Data_Transfer_-_additional_validation_valid_from_12_November_2015.pdf
        //[RegularExpression("^ ([Gg][Ii][Rr] 0[Aa]{2})|((([A - Za - z][0 - 9]{1,2})|(([A - Za - z][A - Ha - hJ - Yj - y][0 - 9]{1,2})|(([A - Za - z][0 - 9][A - Za - z])|([A - Za - z][A - Ha - hJ - Yj - y][0 - 9]?[A - Za - z])))) [0-9] [A-Za-z]{2})$")]
        [Required]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
    }
}