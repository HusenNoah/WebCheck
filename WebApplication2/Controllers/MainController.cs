using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.ViewModels;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class MainController : Controller
    {
        Repository _repository = new Repository();

        // GET: Main
        [WebApplication2.Helpers.PermisionRequired(Login.Permissions.Add)]
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
                    if (getcustomerexist==null)
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
         [WebApplication2.Helpers.PermisionRequired(Login.Permissions.View)]
        public ActionResult ListPersons()
        {
            var getListPersons = _repository.ListPersons();
            return View(getListPersons);
        }

        public ActionResult UpdatePerson(string Id)
        {
            var getupdate = _repository.GetUpdate(Id);
            return View(getupdate);
        }
        [HttpPost]
        public ActionResult UpdateXuseen(Customers cus)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.UpdatePersons(cus);
                    TempData["SuccesfullyUpdated"] = "Succes! Succesfully Updated";
                    ModelState.Clear();
                    return RedirectToAction("ListPersons");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }

        public ActionResult DeletePerson(string Id)
        {
            var getDeletePerson = _repository.GetUpdate(id:Id);
            return View(getDeletePerson);

        }
        [HttpPost]
        public ActionResult DeletePerson(Customers cus)
        {
            _repository.DeletePerson(cus);
            TempData["DeleteSuc"] = "Succes! Succesfully Deleted";
            return RedirectToAction("ListPersons") ;
        }
        public ActionResult DetailPerson(string Id)
        {
            var getdetail = _repository.GetUpdate(id:Id);
            return View(getdetail);

        }
    }
}