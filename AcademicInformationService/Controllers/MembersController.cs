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

            var viewModel = new MemberFormViewModel()
            {
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
                Genders = _context.Genders.ToList()
            };

            return View("MemberForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Member member)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MemberFormViewModel()
                {
                    Member = member,
                    Genders = _context.Genders.ToList()
                };

                return View("MemberForm", viewModel);
            }

            if (member.Id == 0)
            {
                _context.Members.Add(member);
            }
            else
            {
                var memberInDb = _context.Members.Single(m => m.Id == member.Id);
                memberInDb.Name = member.Name;
                memberInDb.Birthdate = member.Birthdate;
                memberInDb.Biography = member.Biography;
                memberInDb.GenderId = member.GenderId;
                memberInDb.MembershipTypeId = member.MembershipTypeId;
                memberInDb.Email = member.Email;
                memberInDb.HomeNumber = member.HomeNumber;
                memberInDb.WorkNumber = member.WorkNumber;
                memberInDb.MobileNumber = member.MobileNumber;
                memberInDb.HomeAddressId = member.HomeAddressId;
                memberInDb.WorkAddressId = member.WorkAddressId;
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
                return View();
            }
        }
    }
}
