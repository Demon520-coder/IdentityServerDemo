using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KerberosMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;

namespace KerberosMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        protected IList<Student> students;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            this.students = new List<Student>();

            for (int i = 0; i < 10000000; i++)
            {
                students.Add(new Student
                {
                    Name = $"zzl_{i}",
                    Age = i,
                    Gender = i % 2 == 0 ? '男' : '女'
                });
            }
        }


        public IActionResult About()
        {
            return View();
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Produces("application/proto")]
        public IEnumerable<Student> ProtobTest()
        {
            var feature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (feature != null)
            {
                feature.AllowSynchronousIO = true;
            }

            return this.students;
        }


        [HttpGet]
        [Produces("application/json")]
        public IEnumerable<Student> JsonTest()
        {
            return this.students;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
