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
            var member = _context.Members.Include(m => m.MembershipType).SingleOrDefault(m => m.Id == id);

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
            var member = _context.Members.SingleOrDefault(m => m.Id == id);

            if (member == null)
            {
                return HttpNotFound();
            }

            var viewModel = new MemberFormViewModel()
            {
                Member = member,
                Genders = _context.Genders.ToList(),
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("MemberForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(MemberFormViewModel memberFormViewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    var viewModel = new MemberFormViewModel()
            //    {
            //        Member = memberFormViewModel.Member,
            //        MembershipTypes = _context.MembershipTypes.ToList(),
            //        Genders = _context.Genders.ToList()
            //    };

            //    return View("MemberForm", viewModel);
            //}

            if (memberFormViewModel.Member.Id == 0)
            {
                memberFormViewModel.HomeAddress.Member = memberFormViewModel.Member;
                memberFormViewModel.WorkAddress.Member = memberFormViewModel.Member;
                _context.Members.Add(memberFormViewModel.Member);
                _context.HomeAddresses.Add(memberFormViewModel.HomeAddress);
                _context.WorkAddresses.Add(memberFormViewModel.WorkAddress);
            }
            else
            {
                var memberInDb = _context.Members.Single(m => m.Id == memberFormViewModel.Member.Id);
                memberInDb.Name = memberFormViewModel.Member.Name;
                memberInDb.Birthdate = memberFormViewModel.Member.Birthdate;
                memberInDb.Biography = memberFormViewModel.Member.Biography;
                memberInDb.GenderId = memberFormViewModel.Member.GenderId;
                memberInDb.MembershipTypeId = memberFormViewModel.Member.MembershipTypeId;
                memberInDb.Email = memberFormViewModel.Member.Email;
                memberInDb.HomeNumber = memberFormViewModel.Member.HomeNumber;
                memberInDb.WorkNumber = memberFormViewModel.Member.WorkNumber;
                memberInDb.MobileNumber = memberFormViewModel.Member.MobileNumber;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Members");
        }

        // POST: Member/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
