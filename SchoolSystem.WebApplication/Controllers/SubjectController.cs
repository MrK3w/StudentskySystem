using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolSystem.WebApplication.Entities;
using SchoolSystem.WebApplication.Models;

namespace SchoolSystem.WebApplication.Controllers
{
    public class SubjectController : Controller
    {

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
            var lesson = _dbContext.Lessons.FirstOrDefault(x => x.SubjectId == subjectId);
            if (lesson == null)
            {
                var subject = _dbContext.Subjects.Find(subjectId);
                if (subject == null)
                    return View("DeleteNotFound");
                _dbContext.Subjects.Remove(subject);
                _dbContext.SaveChanges();
                return View("Delete");
            }
            return View("DeleteNotPossible");
        }
        
        [HttpGet]   
        public IActionResult Create()
        {
            if (!ModelState.IsValid) return Create();
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
            if (!ModelState.IsValid) return Edit(subject.Id);
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