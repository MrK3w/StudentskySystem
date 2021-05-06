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
    public class HomeController : Controller
    {
        private readonly SchoolDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var students = _dbContext.Students.ToList();
            return View(students);
        }
        
        public IActionResult Delete(int studentId)
        {
            var student = _dbContext.Students.Find(studentId);

            if (student == null)
                return View("DeleteNotFound");
            
            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();
            return View("Delete");
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        
        [HttpPost]
        public IActionResult Create(StudentEntity studentEntity)
        {
            _dbContext.Students.Add(studentEntity);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var student = _dbContext.Students.Find(id);
            return View("Edit", student);
        }

        [HttpPost]
        public ActionResult Edit(StudentEntity student)
        {
            _dbContext.Entry(student).State = EntityState.Modified;
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