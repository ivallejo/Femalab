using Femalab.Model.Entities;
using Femalab.Service.Master.Interfaces;
using Femalab.Service.MasterService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Femalab.Controllers
{
    public class ProductController : Controller
    {
        IProductService productService;
        ICategoryService categoryService;
        ISpecialtyService specialtyService;

        public ProductController(IProductService productService, ICategoryService categoryService, ISpecialtyService specialtyService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.specialtyService = specialtyService;
        }
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetAll()
        {
            return Json(productService.GetAll());
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            var patient = productService.GetById(id);
            if (patient == null)
                patient = new Product();

            //Category
            var categories = categoryService
                                .GetAll()
                                .Select(c => new SelectListItem
                                {
                                    Text = c.Description,
                                    Value = c.Id.ToString()
                                })
                                .ToList();
            categories.Insert(0, new SelectListItem { Value = 0.ToString(), Text = "CATEGORÍA", Selected = false });

            //Category
            var specialties = specialtyService
                                .GetAll()
                                .Select(c => new SelectListItem
                                {
                                    Text = c.Description,
                                    Value = c.Id.ToString()
                                })
                                .ToList();
            specialties.Insert(0, new SelectListItem { Value = 0.ToString(), Text = "ESPECIALIDAD", Selected = false });

            ViewBag.Category = categories;
            ViewBag.Specialty = specialties;

            return PartialView(patient);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult AddOrEdit(Product Model)
        {
            if (ModelState.IsValid)
            {
                if (Model.Id == 0)
                    productService.Create(Model);
                else
                    productService.Update(Model);
            }
            return Json(1);
        }
    }
}