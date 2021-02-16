using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PensionerDetail.Models;

namespace PensionerDetail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionerDetailssController : ControllerBase
    {
        private readonly ApplicationDb _context;

        public PensionerDetailssController(ApplicationDb context)
        {
            _context = context;
            if (!_context.BankDetails.Any() && !_context.PensionerDetails.Any())
            {
                _context.SeedValues();
                _context.SaveChanges();
            }
        }

        [HttpGet("{aadhar}")]
        public async Task<ActionResult<PensionerDetails>> GetPensionerDetails(double aadhar)
        {
            var pensionerDetails = await _context.PensionerDetails.Include("BankDetails").Where(x=>x.Aadhar == aadhar).FirstOrDefaultAsync();


            if (pensionerDetails == null)
            {
                return NotFound();
            }
            //pensionerDetails.BankDetails=await _context.BankDetails.Where(x=>x.BankDetailId==pe)
            return pensionerDetails;
        }
    }
}
