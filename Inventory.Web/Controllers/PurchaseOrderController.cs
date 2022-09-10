using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Net;
using Inventory.CommonViewModels;
using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using Inventory.Web.Common.Classes;
using Newtonsoft.Json;
using System.Web.UI;
using System.Web.Script.Serialization;
using System.IO;


namespace Inventory.Web.Controllers
{
    public class PurchaseOrderController : Controller
    {
        #region Global Variabal
        /// <summary>
        /// 
        /// </summary>

        CommunicationManager communicationManager = new CommunicationManager();
        string projectURL = ConfigurationManager.AppSettings["ProjectURL"];

        #endregion


        #region Add New PurchaseOrder
        /// <summary>
        /// GET: /PurchaseOrder/ This Method show PurchaseOrder page.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult AddPurchaseOrder()
        {
            PurchaseOrderVM PurchaseOrder = new PurchaseOrderVM();

            string urlGetVendorList = projectURL + "getAllVendors";
            string urlGetAllTaxingScheme = projectURL + "getAllTaxing";
            string urlGetAllPaymentTerms = projectURL + "getAllPaymentTerms";
            string urlGetAllCurrency = projectURL + "getAllCurrency";
            string urlGetAllLocation = projectURL + "getAllLocation";

            PurchaseOrder.VendorTaxingSchemeList = communicationManager.Get<List<TaxingSchemeVM>>(urlGetAllTaxingScheme);
            PurchaseOrder.VendorPaymentTermList = communicationManager.Get<List<VendorPaymentTermVM>>(urlGetAllPaymentTerms);
            PurchaseOrder.VendorCurrencyList = communicationManager.Get<List<CurrencyVM>>(urlGetAllCurrency);
            PurchaseOrder._VendorList = communicationManager.Get<List<VendorVM>>(urlGetVendorList);
            PurchaseOrder.VendorLocationList = communicationManager.Get<List<LocationVM>>(urlGetAllLocation);

            string OrderNumber = Request.QueryString["PurchaseOrderNumber"];

            if (OrderNumber != null)
            {
                string urlPurchaseOrderDetails = projectURL + "getPurchaseOrderByID?OrderNumber=" + OrderNumber;
                PurchaseOrder.PurchaseOrderDetails = communicationManager.Get<PurchaseOrderVM>(urlPurchaseOrderDetails);

                string urlPurchaseOrderItemList = projectURL + "getPurchaseOrderItemByOrderNumber?OrderNumber=" + OrderNumber;

                PurchaseOrder.PurchaseOrderItem = communicationManager.Get<List<PurchaseOrderLineVM>>(urlPurchaseOrderItemList);

                int PurchaseOrderId = PurchaseOrder.PurchaseOrderDetails.PurchaseOrderId;
                if (PurchaseOrderId > 0)
                {
                    string apiURLGetPurchaseOrderReceiveItem = projectURL + "getPurchaseOrderReceiveByPurchaseOrderId?PurchaseOrderId=" + PurchaseOrderId;
                    PurchaseOrder.purchaseOrderReciveItem = communicationManager.Get<List<PurchaseOrderReceiveLineVM>>(apiURLGetPurchaseOrderReceiveItem);


                    string apiURLGetPurchaseOrderReturnItem = projectURL + "getPurchaseOrderReturnByPurchaseOrderId?PurchaseOrderId=" + PurchaseOrderId;
                    PurchaseOrder.purchaseOrderReturnItem = communicationManager.Get<List<PurchaseOrderReturnVM>>(apiURLGetPurchaseOrderReturnItem);

                    string apiURLGetPurchaseOrderUnstockItem = projectURL + "getPurchaseOrderUnstockByPurchaseOrderId?PurchaseOrderId=" + PurchaseOrderId;
                    PurchaseOrder.purchaseOrderUnstockItem = communicationManager.Get<List<PurchaseOrderUnstockVM>>(apiURLGetPurchaseOrderUnstockItem);

                }

            }


            return View(PurchaseOrder);

        }


        /// <summary>
        /// POST: /PurchaseOrder/ This Method Post PurchaseOrder data into database.
        /// </summary>
        /// <returns></returns>

        [HttpPost]
        public ActionResult AddPurchaseOrder(PurchaseOrderFormAllData FormData)
        {
            PurchaseOrderVM PurchaseOrder = FormData.PurchaseOrder;


            var PurchaseOrderId = 0;
            string purchaseOrderFalseId = "";
            if (PurchaseOrder.PuchaseOrderFormId != null)
            {
                if (PurchaseOrder.PuchaseOrderFormId == "purchase")
                {
                    List<PurchaseOrderLineVM> PurchaseOrderFormGridDataModel = FormData.PurchaseOrderItem;
                    if (PurchaseOrderFormGridDataModel != null)
                    {
                        string apiURLAddPurchaseOrder = projectURL + "addPurchaseOrder";

                        PurchaseOrderId = communicationManager.Post<PurchaseOrderVM, int>(apiURLAddPurchaseOrder, PurchaseOrder);
                        if (PurchaseOrderId > 0)
                        { 
                            foreach (var item in PurchaseOrderFormGridDataModel)
                            {
                                item.PurchaseOrderId = PurchaseOrderId;
                               
                            } 
                            string urlAddPurchaseOrderItem = projectURL + "addPurchaseOrderItem";
                            var OP_Id = communicationManager.Post<List<PurchaseOrderLineVM>, int>(urlAddPurchaseOrderItem, PurchaseOrderFormGridDataModel);

                            return Json(new { success = true, purchaseOrderId = "purchase" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, purchaseOrderId = PurchaseOrderId }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    else
                    {
                        return Json(new { success = false, purchaseOrderFalseId = "purchaseFalse" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (PurchaseOrder.PuchaseOrderFormId == "receive")
                {
                    List<PurchaseOrderReceiveLineVM> PurchaseOrderReceiveFormGridDataModel = FormData.PurchaseOrderReceiveItem;
                    if (PurchaseOrderReceiveFormGridDataModel != null)
                    {
                        string apiURLAddPurchaseOrder = projectURL + "addPurchaseOrder";

                        PurchaseOrderId = communicationManager.Post<PurchaseOrderVM, int>(apiURLAddPurchaseOrder, PurchaseOrder);

                        if (PurchaseOrderId > 0)
                        {
 
                            foreach (var item in PurchaseOrderReceiveFormGridDataModel)
                            {
                                item.PurchaseOrderId = PurchaseOrderId;
                            }

                            string urlAddPurchaseOrderReceiveItem = projectURL + "addPurchaseOrderReceiveItem";
                            var OP_Id = communicationManager.Post<List<PurchaseOrderReceiveLineVM>, int>(urlAddPurchaseOrderReceiveItem, PurchaseOrderReceiveFormGridDataModel);
                                                       

                            return Json(new { success = true, purchaseOrderId = "receive" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, purchaseOrderId = PurchaseOrderId }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, purchaseOrderId = "receiveFalse" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (PurchaseOrder.PuchaseOrderFormId == "return")
                {
                    List<PurchaseOrderReturnVM> PurchaseOrderReturnFormGridDataModel = FormData.PurchaseOrderReturnItem;
                    if (PurchaseOrderReturnFormGridDataModel != null)
                    {
                        string apiURLAddPurchaseOrder = projectURL + "addPurchaseOrder";

                        PurchaseOrderId = communicationManager.Post<PurchaseOrderVM, int>(apiURLAddPurchaseOrder, PurchaseOrder);

                        if (PurchaseOrderId > 0)
                        {
                            string urlAddPurchaseOrderReturnItem = projectURL + "addPurchaseOrderReturnItem";

                            foreach (var item in PurchaseOrderReturnFormGridDataModel)
                            {
                                item.PurchaseOrderId = PurchaseOrderId;
                            }
                            var OP_Id = communicationManager.Post<List<PurchaseOrderReturnVM>, int>(urlAddPurchaseOrderReturnItem, PurchaseOrderReturnFormGridDataModel);

                            return Json(new { success = true, purchaseOrderId = "return" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, purchaseOrderId = PurchaseOrderId }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, purchaseOrderFalseId = "returnFalse" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (PurchaseOrder.PuchaseOrderFormId == "unstock")
                {
                    List<PurchaseOrderUnstockVM> PurchaseOrderUnstockFormGridDataModel = FormData.PurchaseOrderUnstockItem;
                    if (PurchaseOrderUnstockFormGridDataModel != null)
                    {
                        string apiURLAddPurchaseOrder = projectURL + "addPurchaseOrder";

                        PurchaseOrderId = communicationManager.Post<PurchaseOrderVM, int>(apiURLAddPurchaseOrder, PurchaseOrder);

                        if (PurchaseOrderId > 0)
                        {
                            string urlAddPurchaseOrderUnstockItem = projectURL + "addPurchaseOrderUnstockItem";

                            foreach (var item in PurchaseOrderUnstockFormGridDataModel)
                            {
                                item.PurchaseOrderId = PurchaseOrderId;
                            }
                            var OP_Id = communicationManager.Post<List<PurchaseOrderUnstockVM>, int>(urlAddPurchaseOrderUnstockItem, PurchaseOrderUnstockFormGridDataModel);

                            return Json(new { success = true, purchaseOrderId = "unstock" }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new { success = false, purchaseOrderId = PurchaseOrderId }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {
                        return Json(new { success = false, purchaseOrderFalseId = "unstockFalse" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { success = false, purchaseOrderId = PurchaseOrderId }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, purchaseOrderId = PurchaseOrderId }, JsonRequestBehavior.AllowGet);
            }
        }
         
        #endregion


        #region Show PurchaseOrder List For Update
        /// <summary>
        ///  This will Show PurchaseOrder List For Update
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpGet]
        public ActionResult PurchaseOrdeList()
        {
            string urlGetAllPurchaseOrder = projectURL + "getAllPurchaseOrder";

            List<ListPurchaseOrderVM> PurchaseOrder = new List<ListPurchaseOrderVM>();
            PurchaseOrder = communicationManager.Get<List<ListPurchaseOrderVM>>(urlGetAllPurchaseOrder);
            return View(PurchaseOrder);

        }

        #endregion



        #region Get All Product List
        /// <summary>
        ///  Get All Product List for dropdown list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllProductList()
        {



            string urlGetexistProductList = projectURL + "getExistProductList";
            List<ProductVM> ExistProductList = communicationManager.Get<List<ProductVM>>(urlGetexistProductList);



            if (ExistProductList != null)
            {
                List<SelectListItem> ProductList = new List<SelectListItem>();
                foreach (var item in ExistProductList)
                {
                    ProductList.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ProdId.ToString()
                    });
                }


                return Json(ProductList, JsonRequestBehavior.AllowGet);


            }


            return Json(new { success = false, responseText = "" }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Get All Location List for Grid DropDown
        /// <summary>
        ///  Get All Location List for dropdown list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetAllLocationList()
        {



            string urlGetAllLocation = projectURL + "getAllLocation";
            List<LocationVM> getLocationList = communicationManager.Get<List<LocationVM>>(urlGetAllLocation);



            if (getLocationList != null)
            {
                List<SelectListItem> LocationList = new List<SelectListItem>();
                foreach (var item in getLocationList)
                {
                    LocationList.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.LocationId.ToString()
                    });
                }


                return Json(LocationList, JsonRequestBehavior.AllowGet);


            }


            return Json(new { success = false, responseText = "" }, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #region  Auto Fill Data Into Receive Line
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult autoFillDataIntoReceiveLine(PurchaseOrderReceiveLineVM purchaseOrderReceiveLineModle)
        {
            int purchaseOrderId = purchaseOrderReceiveLineModle.PurchaseOrderId;
            PurchaseOrderVM purchaseOrder = new PurchaseOrderVM();

            if (purchaseOrderId > 0)
            {
                string urlAddPOReceiveautoFillItem = projectURL + "addPOReceiveAutoFillItem";

                var OP_Id = communicationManager.Post<PurchaseOrderReceiveLineVM, int>(urlAddPOReceiveautoFillItem , purchaseOrderReceiveLineModle);
                 

                return Json(new { success = true, purchaseOrderReceiveDatd = purchaseOrderReceiveLineModle }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, purchaseOrderReceive = 0 }, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region delete Received

        [HttpPost]
        public ActionResult receivedDataRowRemove(PurchaseOrderReceiveLineVM purchaseOrderReceive)
        {

            PurchaseOrderVM PurchaseOrder = new PurchaseOrderVM();

            var vendorItemCode = purchaseOrderReceive.VendorItemCode;

            if (vendorItemCode != null)
            {
                string apiURLReceivedDataRowRemove = projectURL + "receivedDataRowRemove";
                var test = communicationManager.Post<PurchaseOrderReceiveLineVM, int>(apiURLReceivedDataRowRemove, purchaseOrderReceive);

                PurchaseOrderLineVM purchaseOrderLine = new PurchaseOrderLineVM();
                if (purchaseOrderReceive.PurchaseOrderLineId > 0)
                {
                    purchaseOrderLine.PurchaseOrderId = purchaseOrderReceive.PurchaseOrderId;
                    purchaseOrderLine.VendorItemCode = purchaseOrderReceive.VendorItemCode;
                    purchaseOrderLine.PurchaseOrderLineId = purchaseOrderReceive.PurchaseOrderLineId;
                    purchaseOrderLine.PurchaseOrderReceiveLineId = Convert.ToInt32(null);
                    purchaseOrderLine.OrderStatus = false;
                }


                string urlUpdateSinglePurchaseOrderItem = projectURL + "updateSinglePurchaseOrderItem";
                var OP_Id = communicationManager.Post<PurchaseOrderLineVM, int>(urlUpdateSinglePurchaseOrderItem, purchaseOrderLine);

                string OrderNumber = purchaseOrderReceive.UrlParameter;

                if (OrderNumber != null)
                {
                    string urlPurchaseOrderItemList = projectURL + "getPurchaseOrderItemByOrderNumber?OrderNumber=" + OrderNumber;

                    PurchaseOrder.PurchaseOrderItem = communicationManager.Get<List<PurchaseOrderLineVM>>(urlPurchaseOrderItemList);
                }

                 return Json(new { success = true }, JsonRequestBehavior.AllowGet);
               // return PartialView("_PurchaseOrderItemGrid", PurchaseOrder);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion


        #region Add Received Ordered Product(Row)

        [HttpPost]
        public ActionResult addReceivedOrderedProduct(PurchaseOrderReceiveLineVM purchaseOrderReceive)
        {

            PurchaseOrderVM PurchaseOrder = new PurchaseOrderVM();
            if (purchaseOrderReceive != null)
            {
                string apiURLAddReceivedOrderedProduct = projectURL + "addReceivedOrderedProduct";
                var getNewPurchaseOrderReceiveId = communicationManager.Post<PurchaseOrderReceiveLineVM, int>(apiURLAddReceivedOrderedProduct, purchaseOrderReceive);
                 
                PurchaseOrderLineVM purchaseOrderLine = new PurchaseOrderLineVM();
                if (purchaseOrderReceive.PurchaseOrderLineId > 0)
                {
                    purchaseOrderLine.PurchaseOrderId = purchaseOrderReceive.PurchaseOrderId;
                    purchaseOrderLine.VendorItemCode = purchaseOrderReceive.VendorItemCode;
                    purchaseOrderLine.PurchaseOrderLineId = purchaseOrderReceive.PurchaseOrderLineId;
                    purchaseOrderLine.PurchaseOrderReceiveLineId = getNewPurchaseOrderReceiveId;
                    purchaseOrderLine.OrderStatus = true;
                }


                string urlUpdateSinglePurchaseOrderItem = projectURL + "updateSinglePurchaseOrderItem";
                var OP_Id = communicationManager.Post<PurchaseOrderLineVM, int>(urlUpdateSinglePurchaseOrderItem, purchaseOrderLine);

                string OrderNumber = purchaseOrderReceive.UrlParameter;
                if (OrderNumber != null)
                {
                    string urlPurchaseOrderItemList = projectURL + "getPurchaseOrderItemByOrderNumber?OrderNumber=" + OrderNumber;

                    PurchaseOrder.PurchaseOrderItem = communicationManager.Get<List<PurchaseOrderLineVM>>(urlPurchaseOrderItemList);
                }

                 return Json(new { success = true }, JsonRequestBehavior.AllowGet);
              //  return PartialView("_PurchaseOrderItemGrid", PurchaseOrder);

              
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion


        #region Load tab data on tab click

        [HttpPost]
        public ActionResult loadTabData(string getLoadTabData)
        {
            PurchaseOrderVM PurchaseOrder = new PurchaseOrderVM();

            string tabName = "";
           int PurchaseOrderId = 0;
           string UrlParameter = "";

            var resolveRequest = HttpContext.Request;           
            resolveRequest.InputStream.Seek(0, SeekOrigin.Begin);
            string jsonString = new StreamReader(resolveRequest.InputStream).ReadToEnd();
            if (jsonString != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();      

                Dictionary<string, object> deserializedDictionary = serializer.Deserialize<Dictionary<string, object>>(jsonString);
                 tabName = Convert.ToString(deserializedDictionary["tabName"]);
                 UrlParameter = Convert.ToString(deserializedDictionary["getUrlid"]);
                 PurchaseOrderId = Convert.ToInt32(deserializedDictionary["PurchaseOrderId"]);

            }


            if (tabName == "purchase")
            {

                string OrderNumber = UrlParameter;
                if (OrderNumber != null)
                {
                    string urlPurchaseOrderItemList = projectURL + "getPurchaseOrderItemByOrderNumber?OrderNumber=" + OrderNumber;

                    PurchaseOrder.PurchaseOrderItem = communicationManager.Get<List<PurchaseOrderLineVM>>(urlPurchaseOrderItemList);
                }
                    return PartialView("_PurchaseOrderItemGrid", PurchaseOrder);
              //  return Json(new { success = true }, JsonRequestBehavior.AllowGet);

            }
            else if (tabName == "receive")
            {

                string OrderNumber = UrlParameter;
                if (OrderNumber != null)
                {

                    string apiURLGetPurchaseOrderReceiveItem = projectURL + "getPurchaseOrderReceiveByPurchaseOrderId?PurchaseOrderId=" + PurchaseOrderId;
                    PurchaseOrder.purchaseOrderReciveItem = communicationManager.Get<List<PurchaseOrderReceiveLineVM>>(apiURLGetPurchaseOrderReceiveItem);


                }
                return PartialView("_PurchaseOrderReceiveGrid", PurchaseOrder);
            }
            else if (tabName == "return")
            {

                string OrderNumber = UrlParameter;
                if (OrderNumber != null)
                {
                    string apiURLGetPurchaseOrderReturnItem = projectURL + "getPurchaseOrderReturnByPurchaseOrderId?PurchaseOrderId=" + PurchaseOrderId;
                    PurchaseOrder.purchaseOrderReturnItem = communicationManager.Get<List<PurchaseOrderReturnVM>>(apiURLGetPurchaseOrderReturnItem);
                }
                return PartialView("_PurchaseOrderReturnGrid", PurchaseOrder);
            }
            else if (tabName == "unstock")
            {

                string OrderNumber = UrlParameter;
                if (OrderNumber != null)
                {

                    string apiURLGetPurchaseOrderUnstockItem = projectURL + "getPurchaseOrderUnstockByPurchaseOrderId?PurchaseOrderId=" + PurchaseOrderId;
                    PurchaseOrder.purchaseOrderUnstockItem = communicationManager.Get<List<PurchaseOrderUnstockVM>>(apiURLGetPurchaseOrderUnstockItem);
                }
                return PartialView("_PurchaseOrderUnstockGrid", PurchaseOrder);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}