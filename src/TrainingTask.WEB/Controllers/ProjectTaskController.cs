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
        readonly DBProjectTaskManipulator dbManipulator = new DBProjectTaskManipulator();
        public ActionResult Index()
        {
            return View(ConverterViewModel.ProjectTaskDTOtoViewModel(dbManipulator.GetProjectTasksList()));
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
        public ActionResult Create(ProjectTaskViewModel projectTask)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProjectTaskDTO projectTaskDTO = ConverterViewModel.ProjectTaskViewModelToDTO(projectTask);
                    DBProjectTaskManipulator.CreateProjectTask(projectTaskDTO);
                }
                catch
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(projectTask);
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjectTaskViewModel projectTask)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProjectTaskDTO projectTaskDTO = ConverterViewModel.ProjectTaskViewModelToDTO(projectTask);
                    DBProjectTaskManipulator.EditProjectTask(projectTask.Id, projectTaskDTO);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    throw;
                }
            }
            return View(projectTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                DBProjectTaskManipulator.DeleteProjectTask(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(nameof(Index));
            }
        }
    }
}