using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolSystem.WebApplication.Entities;
using SchoolSystem.WebApplication.Models;

namespace SchoolSystem.WebApplication.Controllers
{
    public class SubjectController : Controller
    {
        private readonly List<TimeSpan> _startTimes = new List<TimeSpan>()
        {
            new TimeSpan(7, 15, 00),
            new TimeSpan(8, 00, 00),
            new TimeSpan(9, 00, 00),
            new TimeSpan(9, 45, 00),
            new TimeSpan(10, 45, 00),
            new TimeSpan(11, 30, 00),
            new TimeSpan(12, 30, 00),
            new TimeSpan(13, 15, 00),
            new TimeSpan(14, 15, 00),
            new TimeSpan(15, 00, 00),
            new TimeSpan(16, 00, 00),
            new TimeSpan(16, 45, 00),
            new TimeSpan(17, 45, 00),
            new TimeSpan(18, 30, 00),
        };

        private readonly List<TimeSpan> _endTimes = new List<TimeSpan>()
        {
            new TimeSpan(8,0, 0),
            new TimeSpan(8, 45, 00),
            new TimeSpan(9, 45, 00),
            new TimeSpan(10, 30, 00),
            new TimeSpan(11, 30, 00),
            new TimeSpan(12, 15, 00),
            new TimeSpan(13, 15, 00),
            new TimeSpan(14, 0, 00),
            new TimeSpan(15, 0, 00),
            new TimeSpan(15, 45, 00),
            new TimeSpan(16, 45, 00),
            new TimeSpan(17, 30, 00),
            new TimeSpan(18, 30, 00),
            new TimeSpan(19, 15, 00),
        };
        private readonly SchoolDbContext _dbContext;

        public SubjectController(ILogger<SubjectController> logger, SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var subjects = _dbContext.Subjects.Include(x => x.HeadTeacher).ToList();
          
            return View(subjects);
        }
        
        public IActionResult Delete(int subjectId)
        {
            var subject = _dbContext.Subjects.Find(subjectId);

            if (subject == null)
                return View("DeleteNotFound");
            
            _dbContext.Subjects.Remove(subject);
            _dbContext.SaveChanges();
            return View("Delete");
        }
        
        [HttpGet]   
        public IActionResult Create()
        {
            var teachers = _dbContext.Users.Where(x => x.TypeOfUser=="teacher").ToList();
            ViewBag.Teachers = teachers;
            return View("Create");
        }
        
        [HttpPost]
        public IActionResult Create(SubjectEntity subjectEntity)
        {
            _dbContext.Subjects.Add(subjectEntity);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var subject = _dbContext.Subjects.Find(id);
            var teachers = _dbContext.Users.Where(x => x.TypeOfUser=="teacher").ToList();
            ViewBag.Teachers = teachers;
            return View("Edit", subject);
        }

        [HttpPost]
        public ActionResult Edit(SubjectEntity subject)
        {
            _dbContext.Entry(subject).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}