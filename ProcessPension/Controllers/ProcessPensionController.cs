using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProcessPension.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProcessPension.helper;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace ProcessPension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessPensionController : ControllerBase
    {
        PensionerDetailApi _api = new PensionerDetailApi();
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
        public async Task<IActionResult> Inputs(string name, string pan, double aadaharNo, string type)
        {
           
            PensionerDetail pensioner = new PensionerDetail();
            //calling 2 microservice to get details from context
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/PensionerDetailss/{aadaharNo}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                pensioner = JsonConvert.DeserializeObject<PensionerDetail>(result);
                if (pensioner != null)
                {
                    if (pensioner.Name.Equals(name) && pensioner.PAN.Equals(pan) && pensioner.PensionType.Equals(type))
                    {
                        ProcessPensionInput processPensionInput = new ProcessPensionInput();
                        ProcesPensionOutput procesPensionOutput = new ProcesPensionOutput();
                        procesPensionOutput.DOB = pensioner.DOB;
                        procesPensionOutput.Name = pensioner.Name;
                        procesPensionOutput.PAN = pensioner.PAN;
                        procesPensionOutput.PensionType = pensioner.PensionType;
                        processPensionInput.AdhaarNumber = aadaharNo;

                        //Doing calculation
                        if (pensioner.PensionType == "Self")
                            processPensionInput.PensionAmount = pensioner.Salary * 80 / 100;
                        else
                            processPensionInput.PensionAmount = pensioner.Salary * 50 / 100;

                        if (pensioner.BankDetails.BankType == "Public")
                        {
                            processPensionInput.PensionAmount -= 500;
                        }
                        else
                        {
                            processPensionInput.PensionAmount -= 550;
                        }
                        processPensionInput.PensionAmount += pensioner.Allowances;
                        procesPensionOutput.PensionAmount = processPensionInput.PensionAmount;

                        //calling 3 microservice for cross check by postApi of 2nd microservice(processPension)
                        HttpClient client1 = new HttpClient();
                        StringContent content = new StringContent(JsonConvert.SerializeObject(processPensionInput), Encoding.UTF8, "application/json");
                        HttpResponseMessage responseMessage = await client1.PostAsync("https://localhost:44326/api/ProcessPension", content);
                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return Ok(procesPensionOutput);
                        }
                        return NotFound("Calculation mistake");

                    }
                    return BadRequest("Invalid Details");
                }
            }
            return NotFound("Invalid Details");
        }
        [HttpPost]
        public async Task<IActionResult> ProcessPension(ProcessPensionInput processPensionInput)
        {
            //calling 3 microservice for cross check
            HttpClient client = PensionDisbursmentApi.Initial();
            StringContent content = new StringContent(JsonConvert.SerializeObject(processPensionInput), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Disbursement", content);
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                int result = JsonConvert.DeserializeObject<int>(res);
                if (result == 10)
                    return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetByAadhar")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
        public async Task<IActionResult> GetByAadhar(double aadaharNo)
        {
            PensionerDetailApi _api = new PensionerDetailApi();
            PensionerDetail pensioner = new PensionerDetail();
            //calling 2 microservice to get details from context
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/PensionerDetailss/{aadaharNo}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                pensioner = JsonConvert.DeserializeObject<PensionerDetail>(result);
                if (pensioner != null)
                {
                    return Ok(pensioner);
                }
            }
            return NotFound();
        }
    }
}
