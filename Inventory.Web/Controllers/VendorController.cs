using System;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.CommonViewModels;
using Inventory.Web.Common.Classes;
using System.Configuration;

namespace Inventory.Web.Controllers
{
    public class VendorController : Controller
    {
        CommunicationManager communicationManager = new CommunicationManager();
        Logger logger = LogManager.GetCurrentClassLogger();
        // public static string APIURI = ConfigurationManager.AppSettings["ApiUri"];

        string APIURI = ConfigurationManager.AppSettings["ProjectURL"];
        // GET: Vendor
        #region Vendor Info
        /// <summary>
        /// Get the vendor infomartion
        /// </summary>
        [HttpGet]
        public ActionResult NewVendorInfo(int? venderID)
        {
            Session["VendorID"] = venderID;
            VendorVM vendorModel = new VendorVM();

            string urlGetAllTaxingScheme = APIURI + "getAllTaxing";
            string urlGetAllPaymentTerms = APIURI + "getAllPaymentTerms";
            string urlGetAllCurrency = APIURI + "getAllCurrency";
            string urlGetAllPurchaseOrderHistory = APIURI + "getVendorOrderStatus?vendorID="+ venderID;
            string urlGetAllPaymentHistory = APIURI + "getVendorPaymentHistory?vendorID=" + venderID;
            string urlGetAllVendorItemProduct = APIURI + "getAllVendorProductItem?vendorID=" + venderID;

            //string urlGetAllVendors = APIURI + "getAllVendors";
            //List<VendorVM> getAllVendors = communicationManager.Get<List<VendorVM>>(urlGetAllVendors);
            if (venderID > 0)
            {
                vendorModel.VendorOrderHistoryList = communicationManager.Get<List<PurchaseOrderVM>>(urlGetAllPurchaseOrderHistory);
                vendorModel.VendorProductItemList = communicationManager.Get<List<VendorProductAndItemVM>>(urlGetAllVendorItemProduct);
                //vendorModel.VendorPaymentHistoryList = communicationManager.Get<List<PaymentHistoryVM>>(urlGetAllPaymentHistory);
                //vendorModel.VendorOrderHistoryList = communicationManager.Get<List<PurchaseOrderVM>>(urlGetAllPaymentHistory);
            }
            vendorModel.VendorTaxingSchemeList = communicationManager.Get<List<TaxingSchemeVM>>(urlGetAllTaxingScheme);
            vendorModel.VendorPaymentTermList = communicationManager.Get<List<VendorPaymentTermVM>>(urlGetAllPaymentTerms);
            vendorModel.VendorCurrencyList = communicationManager.Get<List<CurrencyVM>>(urlGetAllCurrency);

            if (venderID > 0)
            {
                string urlVendorDetails = APIURI + "getVendorByID?venderID=" + venderID;
                vendorModel.VendorDetails = communicationManager.Get<VendorVM>(urlVendorDetails);
            }
            return View(vendorModel);
        }


        /// <summary>
        /// Save the vendor infomartion
        /// </summary>
        [HttpPost]
        public ActionResult SaveNewVendorDetails(VendorVM VendorModel)
        {
            VendorVM vendorModel = new VendorVM();
            string urlNewVendorDetails = APIURI + "SaveNewVendorDetails";
            var vendorID = communicationManager.Post<VendorVM, int>(urlNewVendorDetails, VendorModel);
            Session["VendorID"] = vendorID;
        
            return Json(new { success = true, vendorid= vendorID }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Get the vendor infomartion list and can edit the information 
        /// </summary>
        [HttpGet]
        public ActionResult VendorList(int? venderID)
        {
            VendorVM vendorModel = new VendorVM();
            string urlGetAllVendors = APIURI + "getAllVendors?venderID=" + venderID;
            List<VendorVM> getAllVendors = communicationManager.Get<List<VendorVM>>(urlGetAllVendors);
            return View(getAllVendors);
        }
        /// <summary>
        /// Unique name valid method for New Vendor 
        /// </summary>
        [HttpGet]
        public ActionResult IsVendorNameExists(string VendorName)   ///////17052017(D)////////
        {

            string urlGetExistVendorName = APIURI + "getExistVendorName?VendorName=" + VendorName;
            int response = communicationManager.Get<int>(urlGetExistVendorName);
            //obj.VendorDetails = communicationManager.Get<VendorVM>(urlGetExistVendorName);
            return Json(new { usercheck = response }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Save Vendor Product and Item
        /// <summary>
        /// ave the vendor product and item
        /// </summary>
        [HttpPost]
        public ActionResult SaveVendorProducts(VendorProductAndItemVM vendorProductModel)
        {
            int vendorID = Convert.ToInt32(Session["VendorID"]);
            vendorProductModel.VendorId = vendorID;
            string urlVendorProductsDetails = APIURI + "SaveVendorProducts";
            var vendorProductID = communicationManager.Post<VendorProductAndItemVM, int>(urlVendorProductsDetails, vendorProductModel);
            return Json(new { success = "true",vendorID }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Unique name valid method for New Vendor 
        /// </summary>
        [HttpGet]
        public ActionResult IsVendorItemNameExists(string vendorItemName)   ///////17052017(D)////////
        {

            string urlGetExistVendorItemName = APIURI + "getExistVendorItemName?vendorItemName=" + vendorItemName;
            int response = communicationManager.Get<int>(urlGetExistVendorItemName);
            return Json(new { usercheck = response }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// get the latest payment record 
        /// </summary>
        [HttpGet]
        public ActionResult getLatestPaymentRecord(int vendorId)   ///////31052017(D)////////
        {
            PurchaseOrderVM obj = new PurchaseOrderVM();
            string urlgetLatestPaymentRecord = APIURI + "getLatestPaymentRecord?vendorId=" + vendorId;
            obj = communicationManager.Get<PurchaseOrderVM>(urlgetLatestPaymentRecord);
            //obj = communicationManager.Get<PurchaseOrderVM>(urlgetLatestPaymentRecord);
            //return View(VendorModel);
            return Json(new { Balance = obj.Balance, CurrentBal = obj.CurrentBalance }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getPaymentHistoryByDate(string StartDate, string EndDate, int VendorId)   ///////01062017(D)////////
        {
            if (StartDate != "" && EndDate != "")
            {
                List<PurchaseOrderVM> obj = new List<PurchaseOrderVM>();
                string urlgetPaymentHistoryByDate = APIURI + "getPaymentHistoryByDate?StartDate=" + StartDate + "&EndDate=" + EndDate + "&VendorId=" + VendorId;
                obj = communicationManager.Get<List<PurchaseOrderVM>>(urlgetPaymentHistoryByDate);
                return Json(new { data = obj }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { data = false }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}