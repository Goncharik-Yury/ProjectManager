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
    public class ProjectController : Controller
    {
        DBProjectManipulator dbManipulator = new DBProjectManipulator();
        public ActionResult Index()
        {
            return View(ViewModelConverter.ProjectDTOtoViewModel(dbManipulator.GetProjectsList()));
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
        public ActionResult Create(ProjectDTO project)
        {
            try
            {
                dbManipulator.CreateProject(project);
            }
            catch
            {
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Edit(int id)
        {
            return View(ViewModelConverter.ProjectDTOtoViewModel(dbManipulator.GetProjectById(id))[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjectDTO project)
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
                return View(nameof(Index));
            }
        }
    }
}