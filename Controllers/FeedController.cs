using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class FeedController : Controller
    {
        // GET: Feed
        [Authorize]
        [HttpGet]
        public ActionResult Welcome()
        {
            return View();

        }

    }
}