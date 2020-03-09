using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training_task.Utility;
using Task = Training_task.Models.Task;

namespace Training_task.Controllers
{
    public class TaskController : Controller
    {
        DBTaskManipulator dbManipulator = new DBTaskManipulator();
        public ActionResult Index()
        {
            return View(dbManipulator.GetTasksList());
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
        public ActionResult Create(Task task)
        {
            try
            {
                dbManipulator.CreateTask(task);

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
        public ActionResult Edit(int id, Task task)
        {
            try
            {
                dbManipulator.EditTask(task.Id, task);

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
                dbManipulator.DeleteTask(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}