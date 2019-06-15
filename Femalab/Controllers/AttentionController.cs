using Femalab.Model.Custom.Facturalo;
using Femalab.Model.Entities;
using Femalab.Model.ViewModel;
using Femalab.Service.AttentionService;
using Femalab.Service.AttentionService.Interfaces;
using Femalab.Service.Master.Interfaces;
using Femalab.Service.MasterService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
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
        IUbigeoService ubigeoService;

        public AttentionController(IAttentionService attentionService, IDoctorService doctorService, IAttentionTypeService attentionTypeService, IProductService productService, IPersonaService personaService, IPatientService patientService, IAttentionDetailsService attentioDetailsService, IInvoiceService invoiceService, IPaymentService paymentService, IUbigeoService ubigeoService)
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
            this.ubigeoService = ubigeoService;
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
                                  at.CreatedDate,
                                  Pdf = ((at.Invoice.FirstOrDefault() != null) ? (at.Invoice.FirstOrDefault().SunatPdf != null && at.Invoice.FirstOrDefault().SunatPdf != "") ? at.Invoice.FirstOrDefault().SunatPdf : "#" : "#")
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
                    return Json(new { response = 1 });
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

            if (Model.Patient.Document != "" && Model.Patient.DocumentType == "00")
            {
                Model.Patient.Document = Model.Patient.FirstName.ToUpper().Substring(0, 2) + Model.Patient.LastName.ToUpper().Substring(0, 2) + Model.Patient.BirthDate.Day.ToString().PadLeft(2, '0') + Model.Patient.BirthDate.Month.ToString().PadLeft(2, '0');                
            }

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

                    return Json(new { response = 2, attention.Id, attention.AttentionCategory.Tag, attention.Patient.Document, attention.Patient.LastName, attention.Patient.FirstName, TypeTag = attention.AttentionType.Tag, attention.AttentionType.Type, attention.CreatedDate, attention.Patient.Gender, attention.Age, attention.Weight, attention.Size });

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

            var departments = ubigeoService
                                .GetAll_Departments()
                                .Select(u => new SelectListItem
                                { Text = u.Description.ToString(), Value = u.Code.Substring(0, 2) })
                                .ToList();
            ViewBag.departments = departments;

            Invoice invoice = invoiceService.GetByIdAttention(id);

            if (invoice == null)
            {

                var attention = attentionService.GetById(id);
                if (attention != null)
                {
                    var customer = patientService.GetById(attention.PatientId);

                    invoice = new Invoice();

                    //Invoice
                    invoice.Id = 0;
                    invoice.AttentionId = attention.Id;
                    invoice.Currency = "PEN";
                    invoice.Observations = "";
                    invoice.VoucherType = "03";
                    invoice.Series = "03";
                    invoice.DocumentNumber = 0;
                    var total = attention.AttentionDetails.Sum(x => x.Import);

                    invoice.IGV = 18;
                    invoice.TotalValue = total;
                    invoice.TotalSale = decimal.Round((total / 1.18M), 2);
                    invoice.TotalTax = total - invoice.TotalSale;

                    //Customer
                    invoice.Customer = new Customer();
                    invoice.Customer.Address = (attention.Patient.Address == null | attention.Patient.Address == "") ? "CAL.JORGE Ó CONNOR NRO. 110 URB. JAVIER PRADO LIMA - LIMA - SAN BORJA" : attention.Patient.Address;
                    invoice.Customer.DocumentType = attention.Patient.DocumentType; //"1";
                    invoice.Customer.Document = attention.Patient.Document;
                    invoice.Customer.Country = "PE";
                    invoice.Customer.Email = (attention.Patient.Email == null | attention.Patient.Email == "") ? "administracion@femalab.pe" : attention.Patient.Email;
                    invoice.Customer.FirstName = $"{attention.Patient.FirstName} {attention.Patient.LastName}";
                    invoice.Customer.Phone = (attention.Patient.Phone == null | attention.Patient.Phone == "") ? "000-00000" : attention.Patient.Phone;
                    invoice.Customer.TradeName = $"{attention.Patient.FirstName} {attention.Patient.LastName}";

                    //InvoiceDetails
                    invoice.InvoiceDetails = new List<InvoiceDetails>();
                    InvoiceDetails invoiceDetails;
                    foreach (var item in attention.AttentionDetails)
                    {
                        invoiceDetails = new InvoiceDetails();

                        invoiceDetails.Id = 0;
                        invoiceDetails.InvoiceId = 0;
                        invoiceDetails.Description = item.Product.Description;
                        invoiceDetails.ProductId = item.ProductId;
                        invoiceDetails.Product = item.Product;
                        invoiceDetails.Price = item.Import;
                        invoiceDetails.Quantity = item.Quantity;
                        invoice.InvoiceDetails.Add(invoiceDetails);
                    }

                    //Ubigeo
                    invoice.Customer.Department = "15";
                    var provinces = ubigeoService
                               .GetAll_Province(invoice.Customer.Department)
                               .Select(u => new SelectListItem
                               { Text = u.Description.ToString(), Value = u.Code.Substring(2, 2) })
                               .ToList();
                    ViewBag.provinces = provinces;
                    invoice.Customer.Province = "01";
                    var districts = ubigeoService
                               .GetAll_District(invoice.Customer.Department + invoice.Customer.Province)
                               .Select(u => new SelectListItem
                               { Text = u.Description.ToString(), Value = u.Code.Substring(4) })
                               .ToList();
                    ViewBag.districts = districts;
                    invoice.Customer.District = "23";

                    //Payment
                    invoice.Payments = new List<Payment>();

                }
            }
            else
            {
                //Payments
                invoice.Payments = invoice.Payments.Where(p => p.State == true).ToList();

                //Ubigeo
                invoice.Customer.Department = "15";
                var provinces = ubigeoService
                           .GetAll_Province(invoice.Customer.Department)
                           .Select(u => new SelectListItem
                           { Text = u.Description.ToString(), Value = u.Code.Substring(2, 2) })
                           .ToList();
                ViewBag.provinces = provinces;
                invoice.Customer.Province = "01";
                var districts = ubigeoService
                           .GetAll_District(invoice.Customer.Department + invoice.Customer.Province)
                           .Select(u => new SelectListItem
                           { Text = u.Description.ToString(), Value = u.Code.Substring(4) })
                           .ToList();
                ViewBag.districts = districts;
                invoice.Customer.District = "23";
                    
            }

            ViewBag.Saldo = invoice.InvoiceDetails.Sum(x => x.Price) - invoice.Payments.Sum(x => x.Amount);

            return PartialView(invoice);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Invoice(Invoice Model)
        {
            string response = "0";

            try
            {
                var now = DateTime.UtcNow;
                var code = $"{Model.Customer.Department}{Model.Customer.Province}{Model.Customer.District}";
                var ubigeo = ubigeoService.GetBy(x => x.Code == code).FirstOrDefault();

                Model.IssueDate = now;
                Model.ExpirationDate = now;

                Model.Customer.IdUbigeo = ubigeo.Id;

                if (Model.Customer.DocumentType != "06")
                {
                    var patient = attentionService.GetById(Model.AttentionId).Patient;
                    patient.DocumentType = Model.Customer.DocumentType.Substring(1,1);
                    patient.Document = Model.Customer.Document;
                    patient.Address = Model.Customer.Address;
                    patient.Email = Model.Customer.Email;
                    patient.Phone = Model.Customer.Phone;

                    patientService.Update(patient);
                }


                if (Model.Id == 0)
                {
                    if (Model.Payments == null) Model.Payments = new List<Payment>();
                    invoiceService.Create(Model);
                                        
                    var invoiceSucces = invoiceService.GetByIdAttention(Model.AttentionId);

                    if (invoiceSucces.TotalValue == invoiceSucces.Payments.Where(x=> x.State==true).Sum(x=> x.Amount) )
                    {
                        invoiceSucces.Customer.DocumentType = invoiceSucces.Customer.DocumentType.Substring(1, 1);
                        Facturalo invoice = CreateJson(invoiceSucces);

                        using (var client = new HttpClient())
                        {
                            string url = WebConfigurationManager.AppSettings["urlApi"].ToString();
                            client.BaseAddress = new Uri(url);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            string apiKey = WebConfigurationManager.AppSettings["apiKey"].ToString(); //"hsMqs2uxCwi3LRb9pA6v9DMxl7Gv2LIVihHsFdQSXplazRh9JM";
                            client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", apiKey));

                            var json = JsonConvert.SerializeObject(invoice);
                            var postTask = client.PostAsJsonAsync<Facturalo>("api/documents", invoice);
                            postTask.Wait();

                            var result = postTask.Result;
                            var cont = result.Content.ReadAsAsync<FacturaloResponse>();
                            if (result.IsSuccessStatusCode)
                            {
                                response = "1";
                                invoiceSucces.ApiSuccess = cont.Result.success;
                                invoiceSucces.ApiMessage = cont.Result.message;
                                invoiceSucces.ApiFile = cont.Result.file;
                                invoiceSucces.ApiLine = cont.Result.line;

                                invoiceSucces.SunatNumber = cont.Result.data.number;
                                invoiceSucces.SunatFilename = cont.Result.file;
                                invoiceSucces.SunatExternalId = cont.Result.data.external_id;
                                invoiceSucces.SunatNumberToLetter = cont.Result.data.number_to_letter;
                                invoiceSucces.SunatHash = cont.Result.data.hash;
                                invoiceSucces.SunatQr = cont.Result.data.qr;
                                invoiceSucces.SunatPdf = cont.Result.links.pdf;
                                invoiceSucces.SunatXml = cont.Result.links.xml;
                                invoiceSucces.SunatCdr = cont.Result.links.cdr;

                                invoiceService.UpdateInvoice(invoiceSucces);
                            }
                            else
                            {
                                response = "0";
                                invoiceSucces.ApiSuccess = cont.Result.success;
                                invoiceSucces.ApiMessage = cont.Result.message;
                                invoiceSucces.ApiFile = cont.Result.file;
                                invoiceSucces.ApiLine = cont.Result.line;
                                invoiceService.UpdateInvoice(invoiceSucces);
                            }
                        }
                    }
                    else
                    {
                        response = "1";
                    }
                }
                else
                {
                    response = "2";
                    if (Model.Payments == null) Model.Payments = new List<Payment>();
                    invoiceService.UpdateInvoice(Model);

                    var invoiceSucces = invoiceService.GetByIdAttention(Model.AttentionId);

                    if (invoiceSucces.TotalValue == invoiceSucces.Payments.Where(x => x.State == true).Sum(x => x.Amount) && (invoiceSucces.SunatNumber == "" || invoiceSucces.SunatNumber == null))
                    {
                        invoiceSucces.Customer.DocumentType = invoiceSucces.Customer.DocumentType.Substring(1, 1);
                        Facturalo invoice = CreateJson(invoiceSucces);

                        using (var client = new HttpClient())
                        {
                            string url = WebConfigurationManager.AppSettings["urlApi"].ToString();
                            client.BaseAddress = new Uri(url);
                            client.DefaultRequestHeaders.Accept.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                            string apiKey = WebConfigurationManager.AppSettings["apiKey"].ToString(); //"hsMqs2uxCwi3LRb9pA6v9DMxl7Gv2LIVihHsFdQSXplazRh9JM";
                            client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", apiKey));

                            var json = JsonConvert.SerializeObject(invoice);
                            var postTask = client.PostAsJsonAsync<Facturalo>("api/documents", invoice);
                            postTask.Wait();

                            var result = postTask.Result;
                            var cont = result.Content.ReadAsAsync<FacturaloResponse>();
                            if (result.IsSuccessStatusCode)
                            {
                                response = "1";
                                invoiceSucces.ApiSuccess = cont.Result.success;
                                invoiceSucces.ApiMessage = cont.Result.message;
                                invoiceSucces.ApiFile = cont.Result.file;
                                invoiceSucces.ApiLine = cont.Result.line;

                                invoiceSucces.SunatNumber = cont.Result.data.number;
                                invoiceSucces.SunatFilename = cont.Result.file;
                                invoiceSucces.SunatExternalId = cont.Result.data.external_id;
                                invoiceSucces.SunatNumberToLetter = cont.Result.data.number_to_letter;
                                invoiceSucces.SunatHash = cont.Result.data.hash;
                                invoiceSucces.SunatQr = cont.Result.data.qr;
                                invoiceSucces.SunatPdf = cont.Result.links.pdf;
                                invoiceSucces.SunatXml = cont.Result.links.xml;
                                invoiceSucces.SunatCdr = cont.Result.links.cdr;

                                invoiceService.UpdateInvoice(invoiceSucces);
                            }
                            else
                            {
                                response = "0";
                                invoiceSucces.ApiSuccess = cont.Result.success;
                                invoiceSucces.ApiMessage = cont.Result.message;
                                invoiceSucces.ApiFile = cont.Result.file;
                                invoiceSucces.ApiLine = cont.Result.line;
                                invoiceService.UpdateInvoice(invoiceSucces);
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {
                response = "0";                
            }
            
            return Json(new { response });
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetProvince(string codeDepartment)
        {
            var provinces = ubigeoService
                           .GetAll_Province(codeDepartment)
                           .Select(u => new SelectListItem
                           { Text = u.Description.ToString(), Value = u.Code.Substring(2, 2) })
                           .ToList();

            return Json(provinces);

        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetDistrict(string codeDepartmentProvince)
        {
            var districts = ubigeoService
                           .GetAll_District(codeDepartmentProvince)
                           .Select(u => new SelectListItem
                           { Text = u.Description.ToString(), Value = u.Code.Substring(4) })
                           .ToList();

            return Json(districts);
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
                                  at.Price,
                                  Discount = 0.0M,
                                  Import = at.Price
                              }).OrderByDescending(x => x.Description).ToList();
            return Json(attentions);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetPerson(string Id)
        {
            var atFM = patientService.GetBy(x => x.Document == Id).FirstOrDefault();
            var at = personaService.GetById(Id);
            if (atFM != null)
            {
                var atencion = attentionService.GetBy(x => x.Patient.Document == atFM.Document).LastOrDefault(); ;
                var person = new
                {
                    ID = atFM.Id,
                    DNI = atFM.Document,
                    APE_PATERNO = atFM.LastName,
                    APE_MATERNO = "",
                    NOMBRES = atFM.FirstName,
                    SEXO = (atFM.Gender == "M") ? 1 : 2,
                    FECHA_NACIMIENTO = atFM.BirthDate.Year + atFM.BirthDate.Month.ToString().PadLeft(2, '0') + atFM.BirthDate.Day.ToString().PadLeft(2, '0'),
                    UBIGEO_DIRECCION = "",
                    Address = (atFM.Address == null) ? "" : atFM.Address,
                    Email = (atFM.Email == null) ? "" : atFM.Email,
                    Phone = (atFM.Phone == null) ? "" : atFM.Phone,
                    Weight = (atencion != null) ? atencion.Weight : 0M,
                    Size = (atencion != null) ? atencion.Size : 0M
                };
                return Json(person);
            }
            else if (at != null)
            {
                var atencion = attentionService.GetBy(x => x.Patient.Document == at.DNI).LastOrDefault();
                var person = new
                {
                    ID = 0,
                    at.DNI,
                    at.APE_PATERNO,
                    at.APE_MATERNO,
                    at.NOMBRES,
                    at.SEXO,
                    at.FECHA_NACIMIENTO,
                    at.UBIGEO_DIRECCION,
                    Address = "",
                    Email = "",
                    Phone = "",
                    Weight = (atencion != null) ? atencion.Weight : 0M,
                    Size = (atencion != null) ? atencion.Size : 0M

                
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
            var attention = attentionService.GetById(id);
            if (attention == null)
            {
                attention = new Attention();
                attention.Patient = new Patient();
                attention.AttentionDetails = new List<AttentionDetails>();
            }
            return PartialView(attention);
        }



        [HttpGet]
        public ActionResult Facturalo()
        {            
            return View();
        }

        private Facturalo CreateJson(Invoice Model)
        {
            var facturalo = new Facturalo();

            string serie = "";

            switch (Model.Series)
            {
                case "01":
                    serie = "F001";
                    break;
                case "03":
                    serie = "B001";
                    break;
                case "07":
                    serie = "F001";
                    break;
                case "08":
                    serie = "F001";
                    break;
            }

            facturalo.serie_documento = serie; //"B001"
            facturalo.numero_documento = "#"; 
            facturalo.fecha_de_emision = Model.IssueDate.ToString("yyyy-MM-dd"); // "2018-10-09";
            facturalo.hora_de_emision = Model.IssueDate.ToString("HH:mm:ss"); //"10:11:11";
            facturalo.codigo_tipo_operacion = "0101";
            facturalo.codigo_tipo_documento = Model.VoucherType; //"03";
            facturalo.codigo_tipo_moneda = Model.Currency; //"PEN";
            facturalo.fecha_de_vencimiento = Model.ExpirationDate.ToString("yyyy-MM-dd"); //"2018-10-09";
            facturalo.numero_orden_de_compra = "";

            facturalo.datos_del_emisor = new datos_del_emisor();
            facturalo.datos_del_emisor.codigo_pais = "PE";
            facturalo.datos_del_emisor.ubigeo = "150130";
            facturalo.datos_del_emisor.direccion = "CAL.JORGE Ó CONNOR NRO. 110 URB. JAVIER PRADO LIMA - LIMA - SAN BORJA";
            facturalo.datos_del_emisor.correo_electronico = "administracion@femalab.pe";
            facturalo.datos_del_emisor.telefono = "692 5953";
            facturalo.datos_del_emisor.codigo_del_domicilio_fiscal = "0000";

            facturalo.datos_del_cliente_o_receptor = new datos_del_cliente_o_receptor();
            facturalo.datos_del_cliente_o_receptor.codigo_tipo_documento_identidad = Model.Customer.DocumentType; //"6"
            facturalo.datos_del_cliente_o_receptor.numero_documento = Model.Customer.Document; //"10414711225";
            facturalo.datos_del_cliente_o_receptor.apellidos_y_nombres_o_razon_social = Model.Customer.FirstName; // "EMPRESA XYZ S.A.";
            facturalo.datos_del_cliente_o_receptor.codigo_pais = "PE";
            facturalo.datos_del_cliente_o_receptor.ubigeo = Model.Customer.Ubigeo.Code; //"150101";
            facturalo.datos_del_cliente_o_receptor.direccion = Model.Customer.Address; // "Av. 2 de Mayo";
            facturalo.datos_del_cliente_o_receptor.correo_electronico = Model.Customer.Email; // "demo@gmail.com";
            facturalo.datos_del_cliente_o_receptor.telefono = Model.Customer.Phone; // "427-1148";

            var Total = Model.InvoiceDetails.Sum(x => x.Price);
            var TotalBase = Math.Round((Total / 1.18M), 2);
            var TotalIgv = Math.Round((Total - (Total / 1.18M)), 2);

            facturalo.totales = new totales();
            facturalo.totales.total_exportacion = 0.00M;
            facturalo.totales.total_operaciones_gravadas = TotalBase; //100.00M;
            facturalo.totales.total_operaciones_inafectas = 0.00M;
            facturalo.totales.total_operaciones_exoneradas = 0.00M;
            facturalo.totales.total_operaciones_gratuitas = 0.00M;
            facturalo.totales.total_igv = TotalIgv; //18.00M;
            facturalo.totales.total_impuestos = TotalIgv; //18.00M;
            facturalo.totales.total_valor = TotalBase; // 100.00M;
            facturalo.totales.total_venta = Total; // 118.00M;


            List<items> items = new List<items>();
            items item;

            decimal price;
            decimal priceUnitario;
            decimal import;
            foreach (var detail in Model.InvoiceDetails)
            {
                item = new items();
                priceUnitario = Math.Round(((detail.Price) / 1.18M), 2);
                price = Math.Round(((detail.Price * detail.Quantity) / 1.18M), 2);
                import = Math.Round((detail.Price * detail.Quantity), 2);

                item.codigo_interno = detail.Product.Code; // "P0121";
                item.descripcion = detail.Product.Description; //"Inca Kola 250 ml";
                item.codigo_producto_sunat = detail.Product.SunatCode; //"51121703";
                item.codigo_producto_gsl = detail.Product.SunatCode; //"51121703";
                item.unidad_de_medida = "NIU";
                item.cantidad = detail.Quantity; // 2;
                item.valor_unitario = priceUnitario; // 50;
                item.codigo_tipo_precio = "01";
                item.precio_unitario = detail.Price;
                item.codigo_tipo_afectacion_igv = "10";
                item.total_base_igv = price; //Math.Round((import - price),2);//100.00M;
                item.porcentaje_igv = 18;
                item.total_igv = Math.Round((import - price), 2);
                item.total_impuestos = Math.Round((import - price), 2);
                item.total_valor_item = price;
                item.total_item = import;

                items.Add(item);
            }

            facturalo.items = items;
            facturalo.totales.total_igv = facturalo.items.Sum(x => x.total_igv);
            facturalo.totales.total_impuestos = facturalo.items.Sum(x => x.total_igv);
            facturalo.totales.total_valor = facturalo.items.Sum(x => x.total_valor_item);
            facturalo.totales.total_operaciones_gravadas = facturalo.items.Sum(x => x.total_valor_item);
            facturalo.totales.total_venta = facturalo.items.Sum(x => x.total_item);
            return facturalo;
        }


        [HttpGet]
        public ActionResult FacturaloJson()
        {
            var facturalo = new Facturalo();
            facturalo.serie_documento = "B001";
            facturalo.numero_documento = "#";
            facturalo.fecha_de_emision = "2018-10-09";
            facturalo.hora_de_emision = "10:11:11";
            facturalo.codigo_tipo_operacion = "0101";
            facturalo.codigo_tipo_documento = "03";
            facturalo.codigo_tipo_moneda = "PEN";
            facturalo.fecha_de_vencimiento = "2018-10-09";
            facturalo.numero_orden_de_compra = "0045467898";

            facturalo.datos_del_emisor = new datos_del_emisor();
            facturalo.datos_del_emisor.codigo_pais = "PE";
            facturalo.datos_del_emisor.ubigeo = "150101";
            facturalo.datos_del_emisor.direccion = "Av. 2 de Mayo";
            facturalo.datos_del_emisor.correo_electronico = "vallejoaguilar@gmail.com";
            facturalo.datos_del_emisor.telefono = "427-1148";
            facturalo.datos_del_emisor.codigo_del_domicilio_fiscal = "0000";

            facturalo.datos_del_cliente_o_receptor = new datos_del_cliente_o_receptor();
            facturalo.datos_del_cliente_o_receptor.codigo_tipo_documento_identidad = "6";
            facturalo.datos_del_cliente_o_receptor.numero_documento = "10414711225";
            facturalo.datos_del_cliente_o_receptor.apellidos_y_nombres_o_razon_social = "EMPRESA XYZ S.A.";
            facturalo.datos_del_cliente_o_receptor.codigo_pais = "PE";
            facturalo.datos_del_cliente_o_receptor.ubigeo = "150101";
            facturalo.datos_del_cliente_o_receptor.direccion = "Av. 2 de Mayo";
            facturalo.datos_del_cliente_o_receptor.correo_electronico = "demo@gmail.com";
            facturalo.datos_del_cliente_o_receptor.telefono = "427-1148";

            facturalo.totales = new totales();
            facturalo.totales.total_exportacion = 0.00M;
            facturalo.totales.total_operaciones_gravadas = 100.00M;
            facturalo.totales.total_operaciones_inafectas = 0.00M;
            facturalo.totales.total_operaciones_exoneradas = 0.00M;
            facturalo.totales.total_operaciones_gratuitas = 0.00M;
            facturalo.totales.total_igv = 18.00M;
            facturalo.totales.total_impuestos = 18.00M;
            facturalo.totales.total_valor = 100.00M;
            facturalo.totales.total_venta = 118.00M;

            var items = new List<items>();
            var item = new items();

            item.codigo_interno = "P0121";
            item.descripcion = "Inca Kola 250 ml";
            item.codigo_producto_sunat = "51121703";
            item.codigo_producto_gsl = "51121703";
            item.unidad_de_medida = "NIU";
            item.cantidad = 2;
            item.valor_unitario = 50;
            item.codigo_tipo_precio = "01";
            item.precio_unitario = 59;
            item.codigo_tipo_afectacion_igv = "10";
            item.total_base_igv = 100.00M;
            item.porcentaje_igv = 18;
            item.total_igv = 18;
            item.total_impuestos = 18;
            item.total_valor_item = 100;
            item.total_item = 118;

            items.Add(item);

            facturalo.items = items;

            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://apifacturalo.test:8084/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                const string apiKey = "hsMqs2uxCwi3LRb9pA6v9DMxl7Gv2LIVihHsFdQSXplazRh9JM";
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", apiKey));
               
                var postTask =  client.PostAsJsonAsync<Facturalo>("api/documents", facturalo);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var cont = result.Content.ReadAsAsync<FacturaloResponse>();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                  
                }
            }

            return View();
        }

        // GET: Attention
        public ActionResult Pending()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetAllPending(string filtro, string dateBegin,string dateEnd)
        {

            var lst = attentionService.GetAllPending().ToList();
            var attentions = (from at in lst
                              where (at.CreatedDate.Date >= Convert.ToDateTime(dateBegin).Date && at.CreatedDate.Date <= Convert.ToDateTime(dateEnd).Date)
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
                                  at.CreatedDate,
                                  Pdf = "#",
                                  Total = at.AttentionDetails.Sum(x=>x.Import),
                                  Pay = at.Invoice.Sum(x => (x.Payments.Where(p => p.State == true) == null) ? 0M : x.Payments.Where(p => p.State == true).Sum(s => s.Amount))
                                  
                              }).OrderByDescending(x => x.CreatedDate).ToList();
            switch (filtro)
            {
                case "00":
                    return Json(attentions);
                case "01":
                    return Json(attentions.FindAll(s => s.Total <= s.Pay));
                case "02":
                    return Json(attentions.FindAll(s => s.Total > s.Pay));
                default:
                    return Json(attentions);
            }

        }

        [HttpGet]
        public ActionResult ReportVentas(string dateBegin, string dateEnd)
        {
            var lst = attentionService.GetAllPending().Where(x => x.CreatedDate.Date >= Convert.ToDateTime(dateBegin).Date && x.CreatedDate.Date <= Convert.ToDateTime(dateEnd).Date).ToList();
            ViewBag.dateBegin = dateBegin;
            ViewBag.dateEnd = dateEnd;
            //var attentions = (from at in lst
            //                  where (at.CreatedDate.Date >= Convert.ToDateTime(dateBegin).Date && at.CreatedDate.Date <= Convert.ToDateTime(dateEnd).Date)
            //                  select new ReportVentas
            //                  {
            //                      Order  = new Order {
            //                          Code = at.Id.ToString().PadLeft(10,'0'),
            //                          FullName = $"{at.Patient.LastName}, {at.Patient.FirstName}",
            //                          RegisterDate = at.CreatedDate,
            //                          IsPay = (at.AttentionDetails.Sum(x => x.Import) == at.Invoice.Sum(x => (x.Payments.Where(p => p.State == true) == null) ? 0M : x.Payments.Where(p => p.State == true).Sum(s => s.Amount))),
            //                          Pay = at.Invoice.Sum(x => (x.Payments.Where(p => p.State == true) == null) ? 0M : x.Payments.Where(p => p.State == true).Sum(s => s.Amount))

            //                      },
            //                      OrderDetails = new OrderDetails
            //                      {
            //                          Code = at.Id.ToString().PadLeft(10, '0'),
            //                          Product = at.AttentionDetails
            //                      }

            //                      Code = at.Id,

            //                  }).OrderByDescending(x => x.CreatedDate).ToList();

            return PartialView(lst);
        }

    }
}