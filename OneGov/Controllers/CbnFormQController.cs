using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CBNFormQ.Models;
using Microsoft.AspNet.Identity;
using PayStack.Net;
using Rotativa;
using Rotativa.Options;

namespace CBNFormQ.Controllers
{
    public class CbnFormQController : BaseController
    {
        private static PayStackApi _api;
        private readonly OneGovDbContext db = new OneGovDbContext();

        // GET: CbnFormQ
        public ActionResult Index()
        {
            return View(db.CbnFormQViewModels.ToList());
        }

        [HttpPost]
        public JsonResult Paystack(object request, string Bvn)
        {
            _api = new PayStackApi("sk_live_27a5ba903be8e7f5a862eaf39d26b916a28eac0d");

            //request = _api.ResolveCardBin("539941");

            request = _api.ResolveBvn(Bvn);

            (request as IPreparable)?.Prepare();

            //var bin = JsonConvert.SerializeObject(request, Formatting.Indented, PayStackApi.SerializerSettings);

            return Json(request, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListBanks(object request)
        {
            _api = new PayStackApi("sk_test_760c409e1e8e60e037b6e6d7b68ab5f51d3c2f90");

            request = _api.SubAccounts.GetBanks();

            (request as IPreparable)?.Prepare();

            //var bin = JsonConvert.SerializeObject(request, Formatting.Indented, PayStackApi.SerializerSettings);

            return Json(request, JsonRequestBehavior.AllowGet);
        }

        // GET: CbnFormQ/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cbnFormQViewModel = db.CbnFormQViewModels.Find(id);
            if (cbnFormQViewModel == null)
                return HttpNotFound();
            return View(cbnFormQViewModel);
        }


        // GET: CbnFormQ/Generated/5
        public ActionResult Generated(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cbnFormQViewModel = db.CbnFormQViewModels.Find(id);
            if (cbnFormQViewModel == null)
                return HttpNotFound();
            return View(cbnFormQViewModel);
        }

        [AllowAnonymous]
        public ActionResult GeneratedPdf(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cbnFormQViewModel = db.CbnFormQViewModels.Find(id);
            if (cbnFormQViewModel == null)
                return HttpNotFound();
            return new ViewAsPdf("Generated", cbnFormQViewModel)
            {
                //FileName = cbnFormQViewModel.FullName + "CBN FormQ" + ".pdf",
                PageSize = Size.A4,
                PageOrientation = Orientation.Portrait
            };
        }

        [AllowAnonymous]
        public ActionResult PaymentConfirmation()
        {
            return View();
        }

        // GET: CbnFormQ/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CbnFormQ/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CbnFormQViewModel cbnFormQViewModel, object request, string Bvn, string BankName,
            string TransactionRef)
        {
            if (ModelState.IsValid)
            {
                cbnFormQViewModel.Email = User.Identity.GetUserName();

                cbnFormQViewModel.SubmissionDate = DateTime.Now.ToLongDateString();

                _api = new PayStackApi("sk_test_760c409e1e8e60e037b6e6d7b68ab5f51d3c2f90");

                var verifyResponse = _api.Transactions.Verify(TransactionRef); // auto or supplied when initializing;
                Debug.WriteLine(TransactionRef);
                db.CbnFormQViewModels.Add(cbnFormQViewModel);
                db.SaveChanges();
                RedirectToAction("Dashboard", "Home");
            }

            return View(cbnFormQViewModel);
        }

        // GET: CbnFormQ/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cbnFormQViewModel = db.CbnFormQViewModels.Find(id);
            if (cbnFormQViewModel == null)
                return HttpNotFound();
            return View(cbnFormQViewModel);
        }

        // POST: CbnFormQ/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include =
                "Id,Bvn,Mobile,Email,AnnualTurnover,NumberOfEmployees,BankName,AccountNumber,ItemOfImport,BeneficiaryName,BeneficiaryBankName,BeneficiaryBankAdress,Iban,SwiftCode,Amount,AmountInWords,Purpose")]
            CbnFormQViewModel cbnFormQViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cbnFormQViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cbnFormQViewModel);
        }


        // GET: CbnFormQ/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var cbnFormQViewModel = db.CbnFormQViewModels.Find(id);
            if (cbnFormQViewModel == null)
                return HttpNotFound();
            return View(cbnFormQViewModel);
        }

        // POST: CbnFormQ/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cbnFormQViewModel = db.CbnFormQViewModels.Find(id);
            db.CbnFormQViewModels.Remove(cbnFormQViewModel);
            db.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}