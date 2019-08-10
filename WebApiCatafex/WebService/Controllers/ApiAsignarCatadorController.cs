using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebService.Controllers
{
    public class ApiAsignarCatadorController : Controller
    {
        // GET: ApiAsignarCatador
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApiAsignarCatador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApiAsignarCatador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApiAsignarCatador/Create
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

        // GET: ApiAsignarCatador/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApiAsignarCatador/Edit/5
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

        // GET: ApiAsignarCatador/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApiAsignarCatador/Delete/5
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
