using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CBNFormQ.Models
{
    public class OneGov
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }

    public class CbnFormQViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Bank Verification Number")]
        public string Bvn { get; set; }

        [Required]
        [Display(Name = "Mobile Number")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Annual Turnover")]
        public string AnnualTurnover { get; set; }

        [Required]
        [Display(Name = "Number of Employees")]
        public string NumberOfEmployees { get; set; }

        [Required]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Item of Import")]
        public string ItemOfImport { get; set; }

        [Required]
        [Display(Name = "Beneficiary's Name")]
        public string BeneficiaryName { get; set; }

        [Required]
        [Display(Name = "Beneficiary's Bank Name")]
        public string BeneficiaryBankName { get; set; }

        [Required]
        [Display(Name = "Beneficiary's Bank Address")]
        public string BeneficiaryBankAdress { get; set; }

        [Required]
        [Display(Name = "IBAN")]
        public string Iban { get; set; }

        [Required]
        [Display(Name = "Swift Code")]
        public string SwiftCode { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public string Amount { get; set; }

        [Required]
        [Display(Name = "Amount in Words")]
        public string AmountInWords { get; set; }

        [Required]
        [Display(Name = "Purpose of Remitance/Transfer")]
        public string Purpose { get; set; }

        public string TransactionRef { get; set; }

        public string SubmissionDate { get; set; }
    }

    public class OneGovDbContext : DbContext
    {
        public DbSet<CbnFormQViewModel> CbnFormQViewModels { get; set; }
        public DbSet<OneGov> OneGovs { get; set; }
    }


}