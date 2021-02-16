using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PensionDisbursment.helper;
using PensionDisbursment.Models;

namespace PensionDisbursment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisbursementController : ControllerBase
    {
        PensionDetailApi _pensionDetailApi = new PensionDetailApi();
        int ProcessPensionStatusCode = 21;
        [HttpPost]
        public async Task<int> DisburePension(ProcessPensionInput input)
        {
            PensionerDetails pensioner = new PensionerDetails();
            HttpClient client = _pensionDetailApi.Initial();
            HttpResponseMessage res = await client.GetAsync($"api/PensionerDetailss/{input.AdhaarNumber}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                pensioner = JsonConvert.DeserializeObject<PensionerDetails>(result);
                double currentAmount = pensioner.Salary;
                if (pensioner.PensionType=="Self")
                {
                    currentAmount = currentAmount * 80 / 100;
                }
                else
                {
                    currentAmount = currentAmount * 50 / 100;
                }
                currentAmount += pensioner.Allowances;
                if(pensioner.BankDetails.BankType=="Public")
                {
                    currentAmount -= 500;
                }
                else
                {
                    currentAmount -= 550;
                }
                if(currentAmount==input.PensionAmount)
                ProcessPensionStatusCode = 10;
            }
            return ProcessPensionStatusCode;
        }
    }
}
