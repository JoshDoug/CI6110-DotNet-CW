﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicInformationService.Models;
using AcademicInformationService.ViewModel;

namespace AcademicInformationService.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public ActionResult Index()
        {
            var events = _context.Events.ToList();
            if (User.IsInRole("Administrator"))
                return View("Index", events);

            return View("ReadOnlyIndex", events);
        }

        // GET: Events/Details/5
        [AllowAnonymous]
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
            var members = _context.Members.ToList();

            if (eventObj == null)
            {
                return HttpNotFound();
            }

            var viewModel = new EventFormViewModel()
            {
                Event = eventObj,
                EventLocation = eventObj.EventLocation,
                AllMembers = members.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.MemberId.ToString()
                })
            };

            return View("EventForm", viewModel);
        }

        public ActionResult Delete(int id)
        {
            var eventLocation =
                _context.EventLocations.SingleOrDefault(e => e.EventId == id); // Should be possible with a cascade?
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
                eventFormViewModel.Event.Members = _context.Members
                    .Where(m => eventFormViewModel.SelectedMembers.Contains(m.MemberId)).ToList();
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

                var updatedEventMembers = _context.Members
                    .Where(m => eventFormViewModel.SelectedMembers.Contains(m.MemberId)).ToList();
                eventInDb.Members
                    .Clear(); // Might be a more efficient way to do this but manually comparing causes a DataReader exception
                eventInDb.Members = updatedEventMembers;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Events");
        }
    }
}