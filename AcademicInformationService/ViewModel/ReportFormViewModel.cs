using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicInformationService.Models;

namespace AcademicInformationService.ViewModel
{
    public class ReportFormViewModel
    {
        public Report Report { get; set; }
        public IEnumerable<SelectListItem> AllMembers { get; set; }

        private List<int> _selectedMembers;

        public List<int> SelectedMembers
        {
            get
            {
                if (_selectedMembers == null)
                {
                    _selectedMembers = new List<int>();
                    try
                    {
                        _selectedMembers = Report.Members.Select(m => m.MemberId).ToList();
                    }
                    catch (Exception)
                    {
                        // This should only get hit when using new - try and figure out alternative
                    }
                }

                return _selectedMembers;
            }
            set { _selectedMembers = value; }
        }

        public string Title
        {
            get
            {
                if (Report != null && Report.ReportId != 0)
                    return "Edit Report";

                return "New Report";
            }
        }
    }
}