using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public HomeController(ILogger<HomeController> logger,SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Login(UserEntity objUser)   
        {
            var obj = _dbContext.Users.FirstOrDefault(a => a.Login.Equals(objUser.Login) && a.Password.Equals(objUser.Password));  
                    if (obj != null)
                    {
                        HttpContext.Session.SetString("login",obj.Login);
                        HttpContext.Session.SetString("type",obj.TypeOfUser);
                        return RedirectToAction("Index", "Student");
                    }
                    return View(objUser);  
        }  
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}