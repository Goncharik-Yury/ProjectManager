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
        readonly DBProjectManipulator dbManipulator = new DBProjectManipulator();
        public ActionResult Index()
        {
            return View(ConverterViewModel.ProjectDTOtoViewModel(dbManipulator.GetProjectsList()));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProjectDTO projectDTO = ConverterViewModel.ProjectViewModelToDTO(project);
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
            return View(ConverterViewModel.ProjectDTOtoViewModel(dbManipulator.GetProjectById(id))[0]);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProjectDTO projectDTO = ConverterViewModel.ProjectViewModelToDTO(project);
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
    }
}