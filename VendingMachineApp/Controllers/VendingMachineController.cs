using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VendingMachineApp.Controllers
{
    public class VendingMachineController : Controller
    {
        // GET: VendingMachine
        public ActionResult VendingMachineDisplayView()
        {
            return View("VendingMachineDisplayView");
        }

        // GET: VendingMachine/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VendingMachine/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendingMachine/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: VendingMachine/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VendingMachine/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: VendingMachine/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VendingMachine/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
