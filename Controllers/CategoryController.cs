using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core_WebApp.Models;
using Core_WebApp.Services;

namespace Core_WebApp.Controllers
{
    public class CategoryController : Controller
    {
        /// <summary>
        /// Dependency Inject the repository classes
        /// Controller is base class for MVC controllers has following 
        /// IACtionFilter = Action filer interfaces and IDisposal = memory management
        /// All action method of controller class get httpget
        /// </summary>
        /// <returns></returns>
        /// 

        private readonly IRepository<Category, int> repository;
        public CategoryController(IRepository<Category, int> repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var res = await repository.GetAync();
            return View(res);
        }
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            //check for validation
            if (ModelState.IsValid)
            {
                var res = await repository.CreateAsync(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var res = await repository.GetAsync(id);
            return View(res);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            //check for validation
            if (ModelState.IsValid)
            {
                var res = await repository.UpdateAsync(id, category);
                return RedirectToAction("Index");
            }
            return View(category); // stay on edit view
        }


        // [HttpDelete] we are not posting the page so not required
        public async Task<IActionResult> Delete(int id)
        {
            var res = await repository.DeleteAsync(id);
            if (res) // is deleted
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        public IActionResult ShowProducts(int id)
        {
            TempData["CategoryRowId"] = id;
            return RedirectToAction("Index", "Product"); // redirect to indexx of product controller
        }

    }
}