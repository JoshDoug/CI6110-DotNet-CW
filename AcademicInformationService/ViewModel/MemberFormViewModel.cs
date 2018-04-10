using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademicInformationService.Models;

namespace AcademicInformationService.ViewModel
{
    public class MemberFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public IEnumerable<Gender> Genders { get; set; }
        public Member Member { get; set; }
    }
}