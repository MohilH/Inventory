using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.CommonViewModels;
using Inventory.Web.Common.Classes;
using System.Configuration;
using NLog;
using System.Net.Mail;
using System.Web.Script.Serialization;


namespace Inventory.Web.Controllers
{
    public class InventoryProductInfoController : Controller
    {
        CommunicationManager communicationManager = new CommunicationManager();
        Logger logger = LogManager.GetCurrentClassLogger();
        string APIURI = ConfigurationManager.AppSettings["ProjectURL"];

        #region InventoryNewProduct
        /// <summary>
        /// Get the Inventory Information
        /// </summary>
        [HttpGet]
        public ActionResult AddNewProductInfo()
        {
            InventoryVM inventoryModel = new InventoryVM();
            string urlGetAllCategory = APIURI + "InventoryProductInfo/getAllCategory";
            string urlGetAllInventoryLocation = APIURI + "InventoryProductInfo/getAllInventoryLocation";
            string urlGetAllInventoryLastVendorId = APIURI + "InventoryProductInfo/getAllInventoryLastVendorId";
            string urlGetAllVendorsList = APIURI + "InventoryProductInfo/getAllInventoryVendors";
            string urlGetGetBillOfMaterialsItemList = APIURI + "InventoryProductInfo/getAllBillOfMaterialsItemName";
            inventoryModel.InventoryProductList = communicationManager.Get<List<ProductVM>>(urlGetGetBillOfMaterialsItemList);
            inventoryModel.InventoryVendorList = communicationManager.Get<List<VendorVM>>(urlGetAllVendorsList);
            inventoryModel.InventoryLocationList = communicationManager.Get<List<LocationVM>>(urlGetAllInventoryLocation);
            inventoryModel.InventoryCategoryList = communicationManager.Get<List<CategoryVM>>(urlGetAllCategory);
            inventoryModel.InventoryLastVendorIdList = communicationManager.Get<List<VendorVM>>(urlGetAllInventoryLastVendorId);
            return View(inventoryModel);
        }
        /// <summary>
        /// use for get the Location list
        /// </summary>
        [HttpGet]
        public ActionResult GetAllLocationList()
        {

            string urlGetAllInventoryLocation = APIURI + "InventoryProductInfo/getAllInventoryLocation";
            List<LocationVM> getLocationList = communicationManager.Get<List<LocationVM>>(urlGetAllInventoryLocation);
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

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// use for get the Vendors List
        /// </summary>
        [HttpGet]
        public ActionResult GetAllVendorsList()
        {

            string urlGetAllVendorsList = APIURI + "InventoryProductInfo/getAllInventoryVendors";
            List<VendorVM> getVendorList = communicationManager.Get<List<VendorVM>>(urlGetAllVendorsList);
            if (getVendorList != null)
            {
                List<SelectListItem> VendorList = new List<SelectListItem>();
                foreach (var item in getVendorList)
                {
                    VendorList.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.VendorId.ToString()
                    });
                }
                return Json(VendorList, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// use for save the Product info and extra info information
        /// </summary>
        [HttpPost]
        public ActionResult SaveInventoryProductInfo(ProductVM ProductModel)
        {
            Guid guidImageName = Guid.NewGuid();
            if (Request.Files.Count > 0)
            {
                string imageName = "";
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    imageName = System.IO.Path.GetFileName(guidImageName + "-" + file.FileName);
                    var physicalPath = Server.MapPath("~/Images/" + file.FileName);
                    file.SaveAs(physicalPath);
                }
                ProductModel.PictureFileAttachmentId = imageName;
            }

            string urlInventoryProductDetails = APIURI + "InventoryProductInfo/SaveInventoryProductInfo";
            var productID = communicationManager.Post<ProductVM, int>(urlInventoryProductDetails, ProductModel);
            //ProductVM ProductModel = new ProductVM();
            var model = Request.Form["inventoryGridModel"];
            var serializer = new JavaScriptSerializer();
            List<QuantityVM> QuantityModel = new List<QuantityVM>();
            if (model != "[]")
            {
                if (productID > 0)
                {
                    var deserializedResult = serializer.Deserialize<List<QuantityVM>>(model);
                    for (int i = 0; i < deserializedResult.Count; i++)
                    {
                        QuantityVM quantity = new QuantityVM();
                        quantity.Quantity = deserializedResult[i].Quantity;
                        quantity.LocationId = deserializedResult[i].LocationId;
                        quantity.ProdId = productID;
                        QuantityModel.Add(quantity);
                    }
                }
            }
            string urlInventoryLocationQuantity = APIURI + "InventoryProductInfo/SaveLocationQuantity";
            var quantityID = communicationManager.Post<List<QuantityVM>, int>(urlInventoryLocationQuantity, QuantityModel);
            return Json(new { success = true, prodid = 0 }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// use for get the Item list in Bill of materials grid
        /// </summary>
        [HttpGet]
        public ActionResult GetBillOfMaterialsItemList()
        {

            string urlGetGetBillOfMaterialsItemList = APIURI + "InventoryProductInfo/getAllBillOfMaterialsItemName";
            List<ProductVM> getVendorList = communicationManager.Get<List<ProductVM>>(urlGetGetBillOfMaterialsItemList);
            if (getVendorList != null)
            {
                List<SelectListItem> VendorList = new List<SelectListItem>();
                foreach (var item in getVendorList)
                {
                    VendorList.Add(new SelectListItem()
                    {
                        Text = item.Name,
                        Value = item.ProdId.ToString()
                    });
                }
                return Json(VendorList, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// use for get the Product Quantity and Cost from Bill of Materials grid
        /// </summary>
        [HttpGet]
        public ActionResult getQuantityUnitpriceByItem(int prodId)
        {
            string urlgetQuantityUnitpriceByItem = APIURI + "InventoryProductInfo/getQuantityUnitpriceByItem?prodId=" + prodId;
            string unitAndQuantity = communicationManager.Get<string>(urlgetQuantityUnitpriceByItem);
            return Json(new { UnitAndQuantity = unitAndQuantity }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// use for save the data of Products Vendor Grid
        /// </summary>
        [HttpPost]
        public ActionResult SaveProductVendorsGrid(List<VendorProductAndItemVM> VendorModel)
        {
            string urlInventoryProductDetails = APIURI + "InventoryProductInfo/SaveProductVendorsGrid";
            var vendorID = communicationManager.Post<List<VendorProductAndItemVM>, int>(urlInventoryProductDetails, VendorModel);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// use for save the data of Bill Of Materials
        /// </summary>
        [HttpPost]
        public ActionResult SaveBillOfMaterials(List<InventoryBillOfMaterialVM> InventoryBillModel)
        {
            string urlInventoryBillOfMaterials = APIURI + "InventoryProductInfo/SaveBillOfMaterials";
            var billID = communicationManager.Post<List<InventoryBillOfMaterialVM>, int>(urlInventoryBillOfMaterials, InventoryBillModel);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// use for get Partial View for Add New Catagory Pop Up
        /// </summary>
        [HttpGet]
        public ActionResult AddNewCategoryPopUp()
        {
            return PartialView("~/Views/InventoryProductInfo/PartialView/_AddNewCategoryPopUp.cshtml");
        }
        /// <summary>
        /// use for save the add new catagory in Catagory Dropdown
        /// </summary>
        [HttpPost]
        public ActionResult SaveNewCategoryPopUp(CategoryVM CategoryModel)
        {
            string urlSaveNewCategory = APIURI + "InventoryProductInfo/SaveNewCategoryPopUp";
            var categoryID = communicationManager.Post<CategoryVM, int>(urlSaveNewCategory, CategoryModel);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// use for check the Item name is Exists or not
        /// </summary>
        [HttpGet]
        public ActionResult IsItemNameExists(string ItemName)
        {
            string urlGetExistItemName = APIURI + "InventoryProductInfo/getExistItemName?ItemName=" + ItemName;
            int response = communicationManager.Get<int>(urlGetExistItemName);
            return Json(new { usercheck = response }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Get the ProductInformation List
        /// </summary>
        [HttpGet]
        public ActionResult ProductInfoList()
        {
            InventoryVM inventoryModel = new InventoryVM();
            string urlGetAllVendorsList = APIURI + "InventoryProductInfo/getAllInventoryProductInfo";
            List<ProductVM> getAllInventoryProductInfo = communicationManager.Get<List<ProductVM>>(urlGetAllVendorsList);
            inventoryModel.InventoryProductList = getAllInventoryProductInfo;
            return View(inventoryModel);
        }

        #endregion
    }

}

