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
    public class LessonController : Controller
    {
        private readonly SchoolDbContext _dbContext;

       
        public LessonController(ILogger<LessonController> logger, SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var lessons = _dbContext.Lessons.Include(x => x.Subject).ToList();
            return View(lessons);
        }
        
        public IActionResult Delete(int lessonId)
        {
            var lesson = _dbContext.Lessons.Find(lessonId);

            if (lesson == null)
                return View("DeleteNotFound");
            
            _dbContext.Lessons.Remove(lesson);
            _dbContext.SaveChanges();
            return View("Delete");
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var subject = _dbContext.Subjects.ToList();
            ViewBag.Subjects = subject;
            return View("Create");
        }
        
        [HttpPost]
        public IActionResult Create(LessonEntity lessonEntity)
        {
            _dbContext.Lessons.Add(lessonEntity);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var lesson = _dbContext.Lessons.Find(id);
            return View("Edit", lesson);
        }

        [HttpPost]
        public ActionResult Edit(LessonEntity lesson)
        {
            _dbContext.Entry(lesson).State = EntityState.Modified;
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