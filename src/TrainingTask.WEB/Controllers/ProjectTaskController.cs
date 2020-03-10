using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.WEB.Models;
using TrainingTask.BLL.Functional;
using TrainingTask.BLL.DTO;
using TrainingTask.WEB.Functional;

namespace TrainingTask.Controllers
{
    public class ProjectTaskController : Controller
    {
        DBProjectTaskManipulator dbManipulator = new DBProjectTaskManipulator();
        public ActionResult Index()
        {
            return View(ViewModelConverter.ProjectTaskDTOtoViewModel(dbManipulator.GetProjectTasksList()));
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
        public ActionResult Create(ProjectTaskDTO projectTask)
        {
            try
            {
                dbManipulator.CreateProjectTask(projectTask);
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
        public ActionResult Edit(int id, ProjectTaskDTO task)
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
                return View(nameof(Index));
            }
        }
    }
}