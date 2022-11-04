using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pyfa.busdev.Controllers
{
    public class MasterCrudController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
