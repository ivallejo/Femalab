using Femalab.Model;
using Femalab.Model.Entities;
using Femalab.Service;
using Femalab.Service.MasterService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Femalab.Controllers
{
    public class PatientController : Controller
    {
        IPatientService patientService;

        public PatientController(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetAll()
        {            
            return Json(patientService.GetAll());
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            var patient = patientService.GetById(id);
            if (patient == null)
                patient = new Patient();

            return PartialView(patient);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult Remove(int id = 0)
        {
            var patient = patientService.GetById(id);
            patientService.Delete(patient);

            return Json(1);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult AddOrEdit(Patient Model)
        {
            if (ModelState.IsValid)
            {
                if (Model.Id == 0)
                    patientService.Create(Model);
                else
                    patientService.Update(Model);
            }
            return Json(1);
        }
    }
}