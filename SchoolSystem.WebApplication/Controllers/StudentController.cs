using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolSystem.WebApplication.Entities;
using SchoolSystem.WebApplication.Models;

namespace SchoolSystem.WebApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly SchoolDbContext _dbContext;

        public StudentController(ILogger<StudentController> logger, SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("type") == null)
            {
                return RedirectToAction("Login","Home");
            }

            var students = _dbContext.Users.Where(x => x.TypeOfUser=="student").ToList();
            ViewBag.Type = HttpContext.Session.GetString("type");;
            return View(students);
        }
        
        public IActionResult Delete(int studentId)
        {
            var student = _dbContext.Users.Find(studentId);

            if (student == null)
                return View("DeleteNotFound");
            
            _dbContext.Users.Remove(student);
            _dbContext.SaveChanges();
            return View("Delete");
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(UserEntity userEntity)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Users.Add(userEntity);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return Create();
        }
    
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var student = _dbContext.Users.Find(id);
            return View("Edit", student);
        }

        [HttpPost]
        public ActionResult Edit(UserEntity user)
        {
            if (!ModelState.IsValid) return Edit(user.Id);
            _dbContext.Entry(user).State = EntityState.Modified;
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