using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ViewModels;
using WebApplication2.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        Repository _repository = new Repository();
        // GET: Admin/Main
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreatePerson()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePerson(Customers cus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var getcustomerexist = _repository.GetCustomerExist(cus.CustomerName);
                    if (getcustomerexist == null)
                    {
                        _repository.CreatePerson(cus);
                        TempData["Succesfull"] = "Succes! Succesfully Saved";
                        ModelState.Clear();
                        // return View("Thanks", cus);
                        return RedirectToAction("ListPersons");
                    }
                    else
                    {
                        TempData["Existname"] = "This Name You Entered Is Exist System";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
    }
}