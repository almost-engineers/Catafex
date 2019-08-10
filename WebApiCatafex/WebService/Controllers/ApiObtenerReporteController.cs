using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebService.Controllers
{
    public class ApiObtenerReporteController : Controller
    {
        // GET: ApiObtenerReporte
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApiObtenerReporte/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApiObtenerReporte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApiObtenerReporte/Create
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

        // GET: ApiObtenerReporte/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApiObtenerReporte/Edit/5
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

        // GET: ApiObtenerReporte/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApiObtenerReporte/Delete/5
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
