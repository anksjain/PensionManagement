using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PensionManagement.helper;
using PensionManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PensionManagement.Controllers
{
    public class PensionerController : Controller
    {
        PensionDetailApi _api = new PensionDetailApi();

        public ActionResult Home()
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                return View("PensionerManagableOptions"); 
            }
            else
            {
                TempData["LoginError"] = "Cannot Go back as User logged out";
                return RedirectToAction("Index", "Home");
            }
        }
        // GET: PensionerController

        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                ViewBag.PensionTypeId = new SelectListItem[] {
            new SelectListItem(){ Text="Select",Value=string.Empty},
            new SelectListItem(){ Text="Self",Value="Self"},
            new SelectListItem(){ Text="Family",Value="Family"}
            
            };
                return View();
            }
            else
            {
                TempData["LoginError"] = "Cannot Go back as User logged out";
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<ActionResult> Index(PensionInput pension)
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                Pension pensioner = new Pension();
                HttpClient client = _api.Initial();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                HttpResponseMessage res = await client.GetAsync($"api/ProcessPension/?name={pension.PensionerName}&pan={pension.PensionerPAN}&aadaharNo={pension.Aadhar}&type={pension.PensionType}");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    pensioner = JsonConvert.DeserializeObject<Pension>(result);
                    if (pensioner != null)
                    {
                        ViewBag.Name = pension.Aadhar;
                        return View("Details", pensioner);
                    }

                }
                ViewBag.PensionTypeId = new SelectListItem[] {
            new SelectListItem(){ Text="Select",Value=string.Empty},
            new SelectListItem(){ Text="Self",Value="Self"},
            new SelectListItem(){ Text="Family",Value="Family"}
            };
                ViewBag.ErrorMessage = "Details Not Found.Please Enter Correct Details";
               
                return View();
            }
            else
            {
                TempData["LoginError"] = "Cannot Go back as User logged out";
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult AllDetails()
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                return View();
            }
            else
            {
                TempData["LoginError"] = "Cannot Go back as User logged out";
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<ActionResult> AllDetails(AadharEntry AEntry)
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                PensionDetailApi _api = new PensionDetailApi();
                PensionerDetails Pd = new PensionerDetails();
                HttpClient client = _api.Initial();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                HttpResponseMessage res = await client.GetAsync($"api/ProcessPension/GetByAadhar?aadaharNo={AEntry.AadharNo}");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    Pd = JsonConvert.DeserializeObject<PensionerDetails>(result);
                    if (Pd != null)
                    {
                        ViewBag.Name = Pd.Name;
                        return View("ViewAllDetails", Pd);
                    }

                }
                ModelState.AddModelError("", "Enterd Incorrect Aadhar Number");
                return View();
            }
            else
            {
                TempData["LoginError"] = "Cannot Go back as User logged out";
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<ActionResult> UserDetailsByAdhar()
        {
            if (HttpContext.Session.GetString("User") != null)
            {
                PensionDetailApi _api = new PensionDetailApi();
                PensionerDetails Pd = new PensionerDetails();
                HttpClient client = _api.Initial();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                HttpResponseMessage res = await client.GetAsync($"api/ProcessPension/GetByAadhar?aadaharNo={Convert.ToDouble(HttpContext.Session.GetString("aadhar"))}");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    Pd = JsonConvert.DeserializeObject<PensionerDetails>(result);
                    if (Pd != null)
                    {
                        ViewBag.Name = Pd.Name;
                        return View("ViewAllDetails", Pd);
                    }

                }
                ViewBag.user = $",{HttpContext.Session.GetString("User").ToUpper()}";
                ViewBag.error=$"Pension details for this {Convert.ToDouble(HttpContext.Session.GetString("aadhar"))} not exist in our system";
                return View();
            }
            else
            {
                TempData["LoginError"] = "Cannot Go back as User logged out";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
