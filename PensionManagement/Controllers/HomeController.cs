using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PensionManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PensionManagement.helper;
using System.Xml;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace PensionManagement.Controllers
{
    public class HomeController : Controller
    {
        PensionDetailApi _api = new PensionDetailApi();
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString("token") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Home", "Pensioner");
            }
        }
        [HttpPost]
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public async Task<ActionResult> Index(LoginModel user)
        {
            HttpClient client = _api.Initial();
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync($"api/Auth/login",content);
            if (res.IsSuccessStatusCode)
            {
                var token = res.Content.ReadAsStringAsync().Result;
                if (token != null)
                {
                    HttpContext.Session.SetString("token", token);
                    HttpContext.Session.SetString("aadhar",user.Aadhar.ToString());
                    HttpContext.Session.SetString("User", user.Username);
                    return RedirectToAction("Home", "Pensioner");
                }
            }         
                ViewBag.LoginError = "Invalid Login Credentials";
                return View();
            
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            RegisterModel user = new RegisterModel();
            user.Aadhar = model.Aadhar;
            user.Email = model.Email;
            user.Password = model.Password;
            user.Username = model.Username;
            HttpClient client = _api.Initial();
            StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync($"api/Auth/register", content);
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                if (result.Equals("User created successfully!"))
                { 
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.LoginError = "User Already Exist";
            return View();

        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}
