using Femalab.Model.Entities;
using Femalab.Model.ViewModel;
using Femalab.Service.AttentionService;
using Femalab.Service.AttentionService.Interfaces;
using Femalab.Service.MasterService;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Femalab.Controllers
{
    public class AttentionController : Controller
    {
        IAttentionTypeService attentionTypeService;
        IAttentionService attentionService;
        IDoctorService doctorService;
        IProductService productService;
        IPersonaService personaService;
        IPatientService patientService;
        IAttentionDetailsService attentioDetailsService;
        IInvoiceService invoiceService;
        IPaymentService paymentService;

        public AttentionController(IAttentionService attentionService, IDoctorService doctorService, IAttentionTypeService attentionTypeService, IProductService productService, IPersonaService personaService, IPatientService patientService, IAttentionDetailsService attentioDetailsService, IInvoiceService invoiceService, IPaymentService paymentService)
        {
            this.attentionService = attentionService;
            this.doctorService = doctorService;
            this.attentionTypeService = attentionTypeService;
            this.productService = productService;
            this.personaService = personaService;
            this.patientService = patientService;
            this.attentioDetailsService = attentioDetailsService;
            this.invoiceService = invoiceService;
            this.paymentService = paymentService;
        }

        // GET: Attention
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetAll()
        {
            var attentions = (from at in attentionService.GetAll()
                              select new
                              {
                                  at.Id,
                                  at.AttentionCategory.Category,
                                  at.Patient.Document,
                                  at.Patient.FirstName,
                                  at.Patient.LastName,
                                  at.Patient.Gender,
                                  at.Age,
                                  at.Weight,
                                  at.Size,
                                  TypeTag = at.AttentionType.Tag,
                                  at.AttentionType.Type,
                                  CategoryTag = at.AttentionCategory.Tag,
                                  at.AttentionCategory.Action,
                                  at.CreatedDate
                              }).OrderByDescending(x => x.CreatedDate).ToList();                                
            return Json(attentions);
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            var patient = attentionService.GetById(id);
            if (patient == null)
                patient = new Attention();

            return PartialView(patient);
        }

        [HttpGet]
        public ActionResult Attention(int id = 0)
        {
            var attention = attentionService.GetById(id);

            if (attention == null)
            {
                attention = new Attention();
                attention.Patient = new Patient();
                attention.DoctorId = 1;
                attention.AttentionTypeId = 1;
            }

            //Doctores
            var doctors = doctorService
                                .GetAll()
                                .Select(c => new SelectListItem
                                {
                                    Text = c.FullName,
                                    Value = c.Id.ToString()
                                })
                                .ToList();
            doctors.Insert(0, new SelectListItem { Value = 0.ToString(), Text = "MÉDICO", Selected = false });            
            

            //Type
            var attentiontypes = attentionTypeService
                                .GetAll()
                                .Select(c => new SelectListItem
                                {
                                    Text = c.Type,
                                    Value = c.Id.ToString()
                                })
                                .ToList();
            attentiontypes.Insert(0, new SelectListItem { Value = 0.ToString(), Text = "TIPO CONSULTA", Selected = false });

            ViewBag.Doctors = doctors;
            ViewBag.AttentionTypes = attentiontypes;

            return PartialView(attention);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Attention(Attention Model)
        {
            Model.AttentionCategoryId = 1;
            if (ModelState.IsValid)
            {
                if (Model.Id == 0)
                {
                    attentionService.CreateAttention(Model);
                    return Json(new { response = 1});
                }
                else
                {
                    attentionService.UpdateAttention(Model);
                    var attention = attentionService.GetById(Model.Id);
                    return Json(new { response = 2, attention.Id, attention.AttentionCategory.Tag, attention.Patient.Document, attention.Patient.LastName, attention.Patient.FirstName, TypeTag = attention.AttentionType.Tag, attention.AttentionType.Type, attention.CreatedDate });
                }
            }
            else
            {
                return Json(new { response = 1 });
            }            
        }

        [HttpGet]
        public ActionResult Laboratory(int id = 0)
        {
            var attention = attentionService.GetById(id);
            if (attention == null)
            {
                attention = new Attention();
                attention.Patient = new Patient();
                attention.AttentionDetails = new List<AttentionDetails>();
            }

            return PartialView(attention);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Laboratory(Attention Model)
        {
            Model.AttentionCategoryId = 2;
            Model.AttentionTypeId = 1;
            Model.DoctorId = 1;
            if (ModelState.IsValid)
            {
                if (Model.Id == 0)
                {                    
                    attentionService.CreateAttention(Model);
                    return Json(new { response = 1 });
                }
                else
                {
                    var attention = attentionService.GetById(Model.Id);

                    foreach (var item in attention.AttentionDetails.ToList())
                        attentioDetailsService.Delete(item);


                    foreach (var item in Model.AttentionDetails)
                    {
                        item.AttentionId = Model.Id;
                        attentioDetailsService.Create(item);
                        
                    }

                    var patient = patientService.GetById(Model.PatientId);
                    patient.FirstName = Model.Patient.FirstName;
                    patient.LastName = Model.Patient.LastName;
                    patient.Document = Model.Patient.Document;
                    patient.DocumentType = Model.Patient.DocumentType;
                    patient.Email = Model.Patient.Email;
                    patient.Phone = Model.Patient.Phone;
                    patient.Gender = Model.Patient.Gender;
                    Model.Patient = patient; 

                    attention = attentionService.GetById(Model.Id);

                    attention.Age = Model.Age;
                    attention.Height = Model.Height;
                    attention.Weight = Model.Weight;
                    attention.Size = Model.Size;
                    attention.QueryBy = Model.QueryBy;

                    attentionService.UpdateAttention(attention);

                    return Json(new { response = 2, attention.Id, attention.AttentionCategory.Tag, attention.Patient.Document, attention.Patient.LastName, attention.Patient.FirstName, TypeTag = attention.AttentionType.Tag, attention.AttentionType.Type, attention.CreatedDate,attention.Patient.Gender, attention.Age,attention.Weight,attention.Size });
                   
                }
            }
            else
            {
                return Json(new { response = 0 });
            }
        }

        [HttpGet]
        public ActionResult Pharmacy(int id = 0)
        {
            var attention = attentionService.GetById(id);
            if (attention == null)
            {
                attention = new Attention();
                attention.Patient = new Patient();
            }

            return PartialView(attention);
        }

        [HttpGet]
        public ActionResult Invoice(int id = 0)
        {            
            Invoice invoice = invoiceService.GetBy(x => x.AttentionId == id).FirstOrDefault();
            

            if (invoice == null)
            {


                

                var attention = attentionService.GetById(id);
                if (attention != null)
                {
                    var customer = patientService.GetById(attention.PatientId);

                    invoice = new Invoice();

                    invoice.Id = 0;
                    invoice.AttentionId = attention.Id;
                    invoice.Currency = "PE";
                    invoice.Observations = "";
                    invoice.VoucherType = "03";
                    invoice.Series = "B001";
                    invoice.DocumentNumber = 1;

                    var total = attention.AttentionDetails.Sum(x => x.Product.Price);

                    invoice.TotalValue = total;
                    invoice.IGV = 18;
                    invoice.TotalSale = decimal.Round( (total / 1.18M) ,2);
                    invoice.TotalTax = total - invoice.TotalSale;
                    invoice.TotalValue = total;

                    invoice.Customer = new Customer();
                    invoice.Customer.Address = attention.Patient.Address;
                    invoice.Customer.DocumentType = attention.Patient.DocumentType;
                    invoice.Customer.Document = attention.Patient.Document;
                    invoice.Customer.Country = "PE";
                    invoice.Customer.Email = attention.Patient.Email;
                    invoice.Customer.FirstName = attention.Patient.FirstName;
                    invoice.Customer.Phone = attention.Patient.Phone;
                    invoice.Customer.TradeName = attention.Patient.FirstName;

                    invoice.Payment = new Payment();

                    invoice.InvoiceDetails = new List<InvoiceDetails>();
                    InvoiceDetails invoiceDetails;
                    foreach (var item in attention.AttentionDetails)
                    {
                        invoiceDetails = new InvoiceDetails();

                        invoiceDetails.Id = 0;
                        invoiceDetails.InvoiceId = 0;
                        invoiceDetails.Description = item.Description;
                        invoiceDetails.ProductId = item.ProductId;
                        invoiceDetails.Price = item.Price;
                        invoiceDetails.Quantity = item.Quantity;

                        invoice.InvoiceDetails.Add(invoiceDetails);
                    }


                }
            }

            return PartialView(invoice);
        }



        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetProductAll()
        {
            var attentions = (from at in productService.GetAll_Attentention()
                              select new
                              {
                                  at.Id,
                                  at.Code,
                                  at.Description,
                                  at.CategoryId,
                                  Category = at.Category.Description,
                                  at.SpecialtyId,
                                  Specialty = at.Specialty.Description,
                                  at.Price
                              }).OrderByDescending(x => x.Description).ToList();
            return Json(attentions);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetPerson(string Id)
        {
            var atFM = patientService.GetBy(x=> x.Document == Id).FirstOrDefault();
            var at = personaService.GetById(Id);
            if (atFM != null)
            {
                var person = new
                {
                    ID = atFM.Id,
                    DNI = atFM.Document,
                    APE_PATERNO = atFM.LastName,
                    APE_MATERNO ="",
                    NOMBRES = atFM.FirstName,
                    SEXO = (atFM.Gender=="M") ? 1 : 2,
                    FECHA_NACIMIENTO = atFM.BirthDate.Year + atFM.BirthDate.Month.ToString().PadLeft(2,'0') + atFM.BirthDate.Day.ToString().PadLeft(2, '0'),
                    UBIGEO_DIRECCION =  ""
                };
                return Json(person);
            }
            else if (at != null)
            {
                var person = new
                {
                    ID = 0,
                    at.DNI,
                    at.APE_PATERNO,
                    at.APE_MATERNO,
                    at.NOMBRES,
                    at.SEXO,
                    at.FECHA_NACIMIENTO,
                    at.UBIGEO_DIRECCION
                };
                return Json(person);
            }
            else
            {
                return Json(null);
            }            
        }

        [HttpGet]
        public ActionResult InvoiceTest(int id = 0)
        {
            //id = 105;
            var attention = attentionService.GetById(id);
            if (attention == null)
            {
                attention = new Attention();
                attention.Patient = new Patient();
                attention.AttentionDetails = new List<AttentionDetails>();
            }



            return PartialView(attention);
        }
    }
}