using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training_task.Models;
using Training_task.Utility;

namespace Training_task.Controllers
{
    public class ProjectController : Controller
    {
        DBProjectManipulator dbManipulator = new DBProjectManipulator();
        public ActionResult Index()
        {
            return View(dbManipulator.GetProjectsList());
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            try
            {
                dbManipulator.CreateProject(project);

                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Project project)
        {
            try
            {
                dbManipulator.EditProject(project.Id, project);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                dbManipulator.DeleteProject(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}