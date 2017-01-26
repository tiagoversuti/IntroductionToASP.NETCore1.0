using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IntroductionToASP.NETCore1._0.Controllers
{
    public class MariaController : Controller
    {
        public IActionResult Index()
        {
            return View("Scott");
        }
    }

    public class ScottIndexViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


    public class ScottController : Controller
    {
        public IActionResult Index()
        {
            var vm = new ScottIndexViewModel()
            {
                Age = 29,
                Name = "Scott"
            };
            
            return View(vm);
        }
        
    }
}
