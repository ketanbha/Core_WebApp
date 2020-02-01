using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core_WebApp.Models;
using Core_WebApp.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core_WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IRepository<Product, int> repository;
        private readonly IRepository<Category, int> catRepository;
        public ProductController(IRepository<Product, int> repository, IRepository<Category, int> catRepository)
        {
            this.repository = repository;
            this.catRepository = catRepository;
        }
        public async Task<IActionResult> Index()
        {
            var res = await repository.GetAync();
            ViewBag.CategoryRowId = new SelectList(await catRepository.GetAync(), "CategoryRowId", "CategoryName");
            return View(res);
        }

        public async Task<IActionResult> Create()
        {
            var res = new Product();
            ViewBag.CategoryRowId = new SelectList(await catRepository.GetAync(), "CategoryRowId", "CategoryName");
            return View(res);
        }

        //public IActionResult Create()
        //{
        //    return View(new Product());
        //}

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            //check for validation
            if (ModelState.IsValid)
            {
                var res = await repository.CreateAsync(product);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryRowId = new SelectList(await catRepository.GetAync(), "CategoryRowId", "CategoryName");
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var res = await repository.GetAsync(id);
            ViewBag.CategoryRowId = new SelectList(await catRepository.GetAync(), "CategoryRowId", "CategoryName", res.CategoryRowId);
            return View(res);
        }

        [HttpPut]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            //check for validation
            if (ModelState.IsValid)
            {
                var res = await repository.UpdateAsync(id, product);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryRowId = new SelectList(await catRepository.GetAync(), "CategoryRowId", "CategoryName");
            return View(product); // stay on edit view
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
    }
}
