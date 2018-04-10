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
        public HomeAddress HomeAddress { get; set; }
        public WorkAddress WorkAddress { get; set; }

        public string Title
        {
            get
            {
                if (Member != null && Member.MemberId != 0)
                    return "Edit Member";

                return "New Member";
            }
        }

    }
}