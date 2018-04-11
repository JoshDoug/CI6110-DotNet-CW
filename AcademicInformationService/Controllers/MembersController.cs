using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicInformationService.Models;
using AcademicInformationService.ViewModel;

namespace AcademicInformationService.Controllers
{
    public class MembersController : Controller
    {
        private ApplicationDbContext _context;

        public MembersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Members
        public ViewResult Index()
        {
            var members = _context.Members.Include(c => c.MembershipType).ToList(); // Uses deferred execution
            return View(members);
        }

        // GET: Members/Details/5
        public ActionResult Details(int id)
        {
            var member = _context.Members.Include(m => m.MembershipType).SingleOrDefault(m => m.MemberId == id);

            if (member == null)
                return HttpNotFound();

            return View(member);
        }

        public ViewResult New()
        {
            var genders = _context.Genders.ToList();
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new MemberFormViewModel()
            {
                MembershipTypes = membershipTypes,
                Genders = genders
            };

            return View("MemberForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var member = _context.Members.SingleOrDefault(m => m.MemberId == id);

            if (member == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MemberFormViewModel()
            {
                Member = member,
                Genders = _context.Genders.ToList(),
                MembershipTypes = _context.MembershipTypes.ToList(),
                HomeAddress = member.HomeAddress,
                WorkAddress = member.WorkAddress
            };

            return View("MemberForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(MemberFormViewModel memberFormViewModel)
        {
//            if (!ModelState.IsValid)
//            {
//                var viewModel = new MemberFormViewModel()
//                {
//                    Member = memberFormViewModel.Member,
//                    MembershipTypes = _context.MembershipTypes.ToList(),
//                    Genders = _context.Genders.ToList()
//                };
//
//                return View("MemberForm", viewModel);
//            }

            if (memberFormViewModel.Member.MemberId == 0)
            {
                memberFormViewModel.Member.HomeAddress = memberFormViewModel.HomeAddress;
                memberFormViewModel.Member.WorkAddress = memberFormViewModel.WorkAddress;
                _context.Members.Add(memberFormViewModel.Member);
            }
            else
            {
                var memberInDb = _context.Members.Single(m => m.MemberId == memberFormViewModel.Member.MemberId);
                memberInDb.Name = memberFormViewModel.Member.Name;
                memberInDb.Birthdate = memberFormViewModel.Member.Birthdate;
                memberInDb.Biography = memberFormViewModel.Member.Biography;
                memberInDb.GenderId = memberFormViewModel.Member.GenderId;
                memberInDb.MembershipTypeId = memberFormViewModel.Member.MembershipTypeId;
                memberInDb.Email = memberFormViewModel.Member.Email;
                memberInDb.HomeNumber = memberFormViewModel.Member.HomeNumber;
                memberInDb.WorkNumber = memberFormViewModel.Member.WorkNumber;
                memberInDb.MobileNumber = memberFormViewModel.Member.MobileNumber;
                var homeAddressInDb =
                    _context.HomeAddresses.Single(h =>
                        h.MemberId == memberFormViewModel.Member.MemberId); // Would ideally farm this out to a function, using ViewModel stops TryUpdateModel from working
                homeAddressInDb.Building = memberFormViewModel.HomeAddress.Building;
                homeAddressInDb.Street = memberFormViewModel.HomeAddress.Street;
                homeAddressInDb.TownCity = memberFormViewModel.HomeAddress.TownCity;
                homeAddressInDb.County = memberFormViewModel.HomeAddress.County;
                homeAddressInDb.Postcode = memberFormViewModel.HomeAddress.Postcode;
                var workAddressInDb =
                    _context.WorkAddresses.Single(w => w.MemberId == memberFormViewModel.Member.MemberId);
                workAddressInDb.Building = memberFormViewModel.HomeAddress.Building;
                workAddressInDb.Street = memberFormViewModel.HomeAddress.Street;
                workAddressInDb.TownCity = memberFormViewModel.HomeAddress.TownCity;
                workAddressInDb.County = memberFormViewModel.HomeAddress.County;
                workAddressInDb.Postcode = memberFormViewModel.HomeAddress.Postcode;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Members");
        }

        // POST: Members/Delete/5
        public ActionResult Delete(int id)
        {
            var homeAddress = _context.HomeAddresses.SingleOrDefault(h => h.MemberId == id); // Should be possible with a cascade?
            var workAddress = _context.WorkAddresses.SingleOrDefault(w => w.MemberId == id);
            var member = _context.Members.SingleOrDefault(m => m.MemberId == id);

            if (member == null)
            {
                return HttpNotFound();
            }

            _context.HomeAddresses.Remove(homeAddress);
            _context.WorkAddresses.Remove(workAddress);
            _context.Members.Remove(member);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}