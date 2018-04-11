﻿using System.Linq;
using System.Web.Mvc;
using AcademicInformationService.Models;
using AcademicInformationService.ViewModel;

namespace AcademicInformationService.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext _context;

        public ReportsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Reports
        public ActionResult Index()
        {
            var reports = _context.Reports.ToList();
            return View(reports);
        }

        // GET: Reports/Details/5
        public ActionResult Details(int id)
        {
            var report = _context.Reports.SingleOrDefault(r => r.ReportId == id);

            if (report == null)
                return HttpNotFound();

            return View(report);
        }

        public ActionResult Edit(int id)
        {
            var report = _context.Reports.SingleOrDefault(r => r.ReportId == id);
            var members = _context.Members.ToList();

            if (report == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ReportFormViewModel()
            {
                Report = report,
                AllMembers = members.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.MemberId.ToString()
                })
            };

            return View("ReportForm", viewModel);
        }

        public ActionResult Delete(int id)
        {
            var report = _context.Reports.SingleOrDefault(r => r.ReportId == id);

            if (report == null)
            {
                return HttpNotFound();
            }

            _context.Reports.Remove(report);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ViewResult New()
        {
            var members = _context.Members.ToList();

            var viewModel = new ReportFormViewModel
            {
                AllMembers = members.Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.MemberId.ToString()
                })
            };

            return View("ReportForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(ReportFormViewModel reportFormViewModel)
        {
            if (reportFormViewModel.Report.ReportId == 0)
            {
                reportFormViewModel.Report.Members = _context.Members.Where(m => reportFormViewModel.SelectedMembers.Contains(m.MemberId)).ToList();
                _context.Reports.Add(reportFormViewModel.Report);
            }
            else
            {
                var reportInDb = _context.Reports.Single(e => e.ReportId == reportFormViewModel.Report.ReportId);
                reportInDb.Name = reportFormViewModel.Report.Name;
                reportInDb.ReportDate = reportFormViewModel.Report.ReportDate;
                reportInDb.Abstract = reportFormViewModel.Report.Abstract;
                reportInDb.ReportText = reportFormViewModel.Report.ReportText;

                var updatedReportMembers = _context.Members.Where(m => reportFormViewModel.SelectedMembers.Contains(m.MemberId)).ToList();
                reportInDb.Members.Clear(); // Might be a more efficient way to do this but manually comparing causes a DataReader exception
                reportInDb.Members = updatedReportMembers;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Reports");
        }
    }
}