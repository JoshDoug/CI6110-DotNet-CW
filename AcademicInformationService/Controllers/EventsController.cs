using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicInformationService.Models;
using AcademicInformationService.ViewModel;

namespace AcademicInformationService.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Events
        public ActionResult Index()
        {
            var events = _context.Events.ToList();
            return View(events);
        }

        // GET: Events/Details/5
        public ActionResult Details(int id)
        {
            var eventObj = _context.Events.SingleOrDefault(e => e.EventId == id);

            if (eventObj == null)
                return HttpNotFound();

            return View(eventObj);
        }

        public ActionResult Edit(int id)
        {
            var eventObj = _context.Events.SingleOrDefault(e => e.EventId == id);

            if (eventObj == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EventFormViewModel()
            {
                Event = eventObj,
                EventLocation = eventObj.EventLocation
            };

            return View("EventForm", viewModel);
        }
        
        public ActionResult Delete(int id)
        {
            var eventLocation = _context.EventLocations.SingleOrDefault(e => e.EventId == id); // Should be possible with a cascade?
            var eventObj = _context.Events.SingleOrDefault(e => e.EventId == id);

            if (eventObj == null)
            {
                return HttpNotFound();
            }

            _context.EventLocations.Remove(eventLocation);
            _context.Events.Remove(eventObj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ViewResult New()
        {
            var members = _context.Members.ToList();

            var viewModel = new EventFormViewModel
            {
                AllMembers = members.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.MemberId.ToString()
                })
            };

            return View("EventForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(EventFormViewModel eventFormViewModel)
        {
            if (eventFormViewModel.Event.EventId == 0)
            {
                eventFormViewModel.Event.EventLocation = eventFormViewModel.EventLocation;
                _context.Events.Add(eventFormViewModel.Event);
            }
            else
            {
                var eventInDb = _context.Events.Single(e => e.EventId == eventFormViewModel.Event.EventId);
                eventInDb.Name = eventFormViewModel.Event.Name;
                eventInDb.EventDate = eventFormViewModel.Event.EventDate;
                eventInDb.Description = eventFormViewModel.Event.Description;
               
                var eventLocationInDb =
                    _context.EventLocations.Single(e => e.EventId == eventFormViewModel.Event.EventId);
                eventLocationInDb.Building = eventFormViewModel.EventLocation.Building;
                eventLocationInDb.Street = eventFormViewModel.EventLocation.Street;
                eventLocationInDb.TownCity = eventFormViewModel.EventLocation.TownCity;
                eventLocationInDb.County = eventFormViewModel.EventLocation.County;
                eventLocationInDb.Postcode = eventFormViewModel.EventLocation.Postcode;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Events");
        }
    }
}
