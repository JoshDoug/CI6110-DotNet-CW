using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AcademicInformationService.Models
{
    public class Gender
    {
        public byte Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}