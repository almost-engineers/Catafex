using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebService.Controllers
{
    public class ApiRegistrarCatadorController : Controller
    {
        // GET: ApiRegistrarCatador
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApiRegistrarCatador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApiRegistrarCatador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApiRegistrarCatador/Create
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

        // GET: ApiRegistrarCatador/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApiRegistrarCatador/Edit/5
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

        // GET: ApiRegistrarCatador/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApiRegistrarCatador/Delete/5
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
