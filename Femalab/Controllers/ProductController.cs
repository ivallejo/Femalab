using Femalab.Model.Entities;
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

        public ProductController(IProductService productService)
        {
            this.productService = productService;
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