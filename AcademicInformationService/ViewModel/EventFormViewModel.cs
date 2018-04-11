using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicInformationService.Models;

namespace AcademicInformationService.ViewModel
{
    public class EventFormViewModel
    {
        public Event Event { get; set; }
        public EventLocation EventLocation { get; set; }
        public IEnumerable<SelectListItem> AllMembers { get; set; }

        private List<int> _selectedMembers;

        public List<int> SelectedMembers
        {
            get
            {
                if (_selectedMembers == null) // Try with a check for Event being null?
                {
                    _selectedMembers = new List<int>();
//                    try
//                    {
//                        _selectedMembers = Event.Members.Select(m => m.MemberId).ToList();
//                    }
//                    catch (Exception)
//                    {
//                        // This should only get hit when using new - try and figure out alternative
//                    }
                }

                return _selectedMembers;
            }
            set { _selectedMembers = value; }
        }

        public string Title
        {
            get
            {
                if (Event != null && Event.EventId != 0)
                    return "Edit Event";

                return "New Event";
            }
        }
    }
}