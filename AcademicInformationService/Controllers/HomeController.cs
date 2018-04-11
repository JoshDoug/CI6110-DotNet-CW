using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AcademicInformationService.Models;

namespace AcademicInformationService.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            var members = _context.Members.Where(m => m.MembershipType.Name == "Chair" || m.MembershipType.Name == "Co-Chair").Include(m => m.MembershipType).ToList();
            return View(members);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Academic Information Service About Page.";

            return View();
        }
    }
}