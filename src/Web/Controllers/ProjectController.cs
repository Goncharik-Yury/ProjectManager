using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainingTask.Web.Models;
using TrainingTask.ApplicationCore.DBManipulators;
using TrainingTask.ApplicationCore.DTO;
using TrainingTask.Web.Functional;
using TrainingTask.ApplicationCore.Validators;
using TrainingTask.Web.Converters;

namespace TrainingTask.Controllers
{
    public class ProjectController : Controller
    {
        readonly DBProjectManipulator dbManipulator = new DBProjectManipulator();
        public ActionResult Index()
        {
            return View(ProjectConverter.ProjectDTOtoViewModel(dbManipulator.GetProjectsList()));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel project)
        {
            ProjectValidate(project);
            if (ModelState.IsValid)
            {
                try
                {
                    ProjectDTO projectDTO = ProjectConverter.ProjectViewModelToDTO(project);
                    dbManipulator.CreateProject(projectDTO);
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        public ActionResult Edit(int id)
        {
            ProjectViewModel model = ProjectConverter.ProjectDTOtoViewModel(dbManipulator.GetProjectById(id))[0];
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjectViewModel project)
        {
            ProjectValidate(project);
            if (ModelState.IsValid)
            {
                try
                {
                    ProjectDTO projectDTO = ProjectConverter.ProjectViewModelToDTO(project);
                    dbManipulator.EditProject(project.Id, projectDTO);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    throw;
                }
            }
            return View(project);
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

        private void ProjectValidate(ProjectViewModel project)
        {
            const string TooLongString = "Too long";
            const string InvalidValue = "Invalid value";
            const int MaxLength = 50;

            if (!Validator.NameIsValid(project.Name))
            {
                ModelState.AddModelError("Name", InvalidValue);
            }
            if (!Validator.LengthIsValid(project.Name, MaxLength))
            {
                ModelState.AddModelError("Name", TooLongString);
            }
            if (!Validator.NameIsValid(project.ShortName))
            {
                ModelState.AddModelError("ShortName", InvalidValue);
            }
        }
    }
}