using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicInformationService.Models
{
    public class Member
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; } // DoB is optional

        public string Gender { get; set; }

        // Contact Details
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Home Number")]
        public string HomeNumber { get; set; }

        [Phone]
        [Display(Name = "Work Number")]
        public string WorkNumber { get; set; } // Don't need to be nullable as type is string

        [Phone]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; } // Don't need to be nullable as type is string

        [Required]
        public Address HomeAddress { get; set; }

        [Required]
        public Address WorkAddress { get; set; }

        public MembershipType MembershipType { get; set; } // Normal, Chair, Co-Chair - can easily be extended and enhanced

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; } // Entity recognises this convention and treats it as a foreign key

        [Display(Name = "Home Address")]
        public byte HomeAddressId { get; set; }

        [Display(Name = "Work Address")]
        public byte WorkAddressId { get; set; }
    }
}