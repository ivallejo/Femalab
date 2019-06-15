using Femalab.Model.Custom.Facturalo;
using Femalab.Service.AttentionService.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Femalab.Controllers
{
    public class DocumentController : Controller
    {

        IInvoiceService invoiceService;

        public DocumentController(IInvoiceService invoiceService)
        {
            
            this.invoiceService = invoiceService;
           
        }

        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetAll(string dateBegin, string dateEnd)
        {

            var lst = invoiceService.GetAll();
            var attentions = (from at in lst
                              where (at.CreatedDate.Date >= Convert.ToDateTime(dateBegin).Date && at.CreatedDate.Date <= Convert.ToDateTime(dateEnd).Date)
                              select new
                              {
                                  at.Id,
                                  at.SunatNumber,
                                  at.Customer.FirstName,
                                  at.TotalValue,
                                  at.CreatedDate,
                                  PaidDate = at.Payments.Where(p => p.State == true) == null ? DateTime.Now : at.Payments.Where(p => p.State == true).LastOrDefault().CreatedDate,
                                  at.SunatPdf ,
                                  at.SunatState
                              }).OrderByDescending(x => x.CreatedDate).ToList();
            
            return Json(attentions);               

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Send(int Id)
        {

            var invoice = invoiceService.GetById(Id);

            using (var client = new HttpClient())
            {
                string url = WebConfigurationManager.AppSettings["urlApi"].ToString();
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string apiKey = WebConfigurationManager.AppSettings["apiKey"].ToString(); //"hsMqs2uxCwi3LRb9pA6v9DMxl7Gv2LIVihHsFdQSXplazRh9JM";
                client.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", apiKey));

                FacturaloSend oSend = new FacturaloSend();
                oSend.external_id = invoice.SunatExternalId;

                //var postTask = client.PostAsJsonAsync<FacturaloSend>("api/send", oSend);

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>(invoice.SunatExternalId, "external_id")
                });

                var postTask =  client.PostAsync("api/send", content);
                postTask.Wait();

                var result = postTask.Result;
                //var cont = result.Content.ReadAsAsync<FacturaloResponse>();
                var resultContent =  result.Content.ReadAsStringAsync();
                if (result.IsSuccessStatusCode)
                {
                    //response = "1";
                    //invoiceSucces.ApiSuccess = cont.Result.success;
                    //invoiceSucces.ApiMessage = cont.Result.message;
                    //invoiceSucces.ApiFile = cont.Result.file;
                    //invoiceSucces.ApiLine = cont.Result.line;

                    //invoiceSucces.SunatNumber = cont.Result.data.number;
                    //invoiceSucces.SunatFilename = cont.Result.file;
                    //invoiceSucces.SunatExternalId = cont.Result.data.external_id;
                    //invoiceSucces.SunatNumberToLetter = cont.Result.data.number_to_letter;
                    //invoiceSucces.SunatHash = cont.Result.data.hash;
                    //invoiceSucces.SunatQr = cont.Result.data.qr;
                    //invoiceSucces.SunatPdf = cont.Result.links.pdf;
                    //invoiceSucces.SunatXml = cont.Result.links.xml;
                    //invoiceSucces.SunatCdr = cont.Result.links.cdr;
                    invoice.SunatState = 1;
                    invoiceService.UpdateInvoice(invoice);
                }
                else
                {
                    //response = "0";
                    //invoiceSucces.ApiSuccess = cont.Result.success;
                    //invoiceSucces.ApiMessage = cont.Result.message;
                    //invoiceSucces.ApiFile = cont.Result.file;
                    //invoiceSucces.ApiLine = cont.Result.line;
                    //invoiceService.UpdateInvoice(invoiceSucces);
                    invoice.SunatState = 2;
                    invoiceService.UpdateInvoice(invoice);
                }
            }

            return Json("Ok");
        }
    }
}