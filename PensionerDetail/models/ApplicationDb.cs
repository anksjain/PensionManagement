using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetail.Models
{
    public class ApplicationDb : DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options)
        {

        }
        public DbSet<PensionerDetails> PensionerDetails { get; set; }
        public DbSet<BankDetails> BankDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<PensionerDetails>().HasKey(c => c.PensionerId);
            modelBuilder.Entity<BankDetails>().HasKey(c => c.BankDetailId);
            //modelBuilder.Entity<PensionerDetails>().HasOne(c => c.BankDetailId).
        }
        public void SeedValues()
        {
            BankDetails.Add(
                new BankDetails
                {
                    BankDetailId = 1,
                    BankName = "State Bank Of India",
                    AccountNumber = 121233453245,
                    BankType = "Public"
                });
            BankDetails.Add(
                new BankDetails
                {
                    BankDetailId = 2,
                    BankName = "HDFC Bank",
                    AccountNumber = 334478928435,
                    BankType = "Private"
                });
            PensionerDetails.Add(
                new PensionerDetails
                {
                    PensionerId = 1,
                    PAN = "ABDFG1234C",
                    Aadhar = 334555677899,
                    DOB = DateTime.Parse("11/05/1997"),
                    Name = "Ajay",
                    Salary = 80000,
                    Allowances = 2000,
                    PensionType = "Self",
                    BankDetailId = 1
                });
            PensionerDetails.Add(
                new PensionerDetails
                {
                    PensionerId = 2,
                    PAN = "BNDFG1234N",
                    Aadhar = 567844322348,
                    DOB = DateTime.Parse("11/05/1997"),
                    Name = "Anks",
                    Salary = 100000,
                    Allowances = 2200,
                    PensionType = "Self",
                    BankDetailId = 2
                });
        }
    }
}
