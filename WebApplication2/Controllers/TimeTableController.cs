using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ViewModels;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class TimeTableController : Controller
    {
        Repository _repository = new Repository();
        // GET: TimeTable
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyTimeTable()
        {
            var gettime = _repository.ListTimeTable();

            return View(gettime);
        }

        public ActionResult SaveTimeTable(TimeTable time)
        {
            if (ModelState.IsValid)
            {

                _repository.CreateTimeTable(time);
                TempData["succes"] = "!Succesfully Saved Time";
                ModelState.Clear();
                return RedirectToAction("MyTimeTable");

            }
            return View();
        }
    }
}