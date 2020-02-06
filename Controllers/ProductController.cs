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
            List<Product> products = new List<Product>();
            //read data from TempData
            int catRowId = Convert.ToInt32(TempData["CategoryRowId"]);
            if (catRowId != 0)
            {
                products = (from p in await repository.GetAync()
                            where p.CategoryRowId == catRowId
                            select p).ToList();
            }
            else
            {
                products = repository.GetAync().Result.ToList();
            }
            ViewBag.CategoryRowId = new SelectList(await catRepository.GetAync(), "CategoryRowId", "CategoryName");
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            // define a ViewBag that will pass the Category List to Create View	            // define a ViewBag that will pass the Category List to Create View
            // so that it will be rendered in DropDown aka <select>	            // so that it will be rendered in DropDown aka <select>
            // use the 'SelectList' class that will carry the data	            // use the 'SelectList' class that will carry the data
            // Note: The Key of ViewBag must match with the Property from Model class bind to View	            // Note: The Key of ViewBag must match with the Property from Model class bind to View
            // so that it can be used for Form Post	            // so that it can be used for Form Post
            // ViewBag compiled as ViewDataDiectionary at Runtime	            // ViewBag compiled as ViewDataDiectionary at Runtime
            // ViewBag will be expired after the method completes it execution	            // ViewBag will be expired after the method completes it execution
            // IMP**, if a View Accept/uses a ViewBag then all action methods	            // IMP**, if a View Accept/uses a ViewBag then all action methods
            // returning the same view must pass ViewBag to View else View will crash

            var res = new Product();
            ViewBag.CategoryRowId = new SelectList(await catRepository.GetAync(), "CategoryRowId", "CategoryName");
            return View(res); // return the create view
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
