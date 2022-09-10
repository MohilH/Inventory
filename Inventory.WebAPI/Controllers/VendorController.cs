using Inventory.BusinessLogic.Interface;
using Inventory.CommonViewModels;
using Inventory.DomainModel.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Inventory.WebAPI.Controllers
{
    public class VendorController : ApiController
    {
        public readonly IVendorManager _IVendorManager;
        public VendorController()
        {

        }
        public VendorController(IVendorManager IVendorManager)
        {
            _IVendorManager = IVendorManager;
        }

        

        #region getAllVendors
        [Route("getAllVendors")]
        [HttpGet]
        public List<VendorVM> getAllVendors( )
        {
            return _IVendorManager.getAllVendors();
        }
        #endregion

        [Route("getAllLocation")]
        [HttpGet]
        public List<LocationVM> getAllLocation()
        {
            return _IVendorManager.getAllLocation();
        }
        [Route("getAllTaxing")]
        [HttpGet]
        public List<TaxingSchemeVM> getAllTaxing()
        {
            return _IVendorManager.getAllTaxing();
        }

        [Route("getAllPaymentTerms")]
        [HttpGet]
        public List<VendorPaymentTermVM> getAllPaymentTerms()
        {
            return _IVendorManager.getAllPaymentTerms();
        }

        [Route("getAllCurrency")]
        [HttpGet]
        public List<CurrencyVM> getAllCurrency()
        {
            return _IVendorManager.getAllCurrency();
        }

        [Route("SaveNewVendorDetails")]
        [HttpPost]
        public int SaveNewVendorDetails(VendorVM VendorModel)
        {
            return _IVendorManager.saveNewVendorDetails(VendorModel);
        }

        [Route("getExistProductList")]
        [HttpGet]
        public List<ProductVM> existProductList()
        {

            return _IVendorManager.existProductList();

        }

        //[Route("addVendorProductItem")]
        //public int addVendorProductItem(List<VendorProductItemVM> VendorProductItemModel)
        //{
        //   return   _IVendorManager.addVendorProductItem(VendorProductItemModel);
        //}

        
        //[Route("getVendorProductItemList")]
        //[HttpGet]
        //public List<VendorProductItemVM> getVendorProductItemList()
        //{
        //    return _IVendorManager.getVendorProductItemList();
        //}

        #region getVendorByID
        [Route("getVendorByID")]
        [HttpGet]
        public VendorVM getVendorByID(int venderID)
        {
            return _IVendorManager.getVendorByID(venderID);
        }
        #endregion
        #region getVendorOrderStatus
        [Route("getVendorOrderStatus")]
        [HttpGet]
        public List<PurchaseOrderVM> getVendorOrderStatus(int vendorID)
        {
            return _IVendorManager.getVendorOrderStatus(vendorID);
        }
        #endregion
        #region getVendorPaymentHistory
        //[Route("getVendorPaymentHistory")]
        //[HttpGet]
        //public List<PurchaseOrderVM> getVendorPaymentHistory(int vendorID)
        //{
        //    return _IVendorManager.getVendorPaymentHistory(vendorID);
        //}
        #endregion
        #region UpdateVendorProducts
        [Route("SaveVendorProducts")]
        [HttpPost]
        public int SaveVendorProducts(VendorProductAndItemVM vendorProductModel)
        {
            return _IVendorManager.SaveVendorProducts(vendorProductModel);
        }

        #endregion
        [Route("getExistVendorName")]
        [HttpGet]
        public int getExistVendorName(string VendorName)      ///////17052017(D)////////
        {
            return _IVendorManager.getExistVendorName(VendorName);
        }
        [Route("getExistVendorItemName")]
        [HttpGet]
        public int getExistVendorItemName(string vendorItemName)      ///////17052017(D)////////
        {
            return _IVendorManager.getExistVendorItemName(vendorItemName);
        }
        [Route("getLatestPaymentRecord")]
        [HttpGet]
        public PurchaseOrderVM getLatestPaymentRecord(int vendorId)      ///////31052017(D)////////
        {
            return _IVendorManager.getLatestPaymentRecord(vendorId);
        }
        [Route("getPaymentHistoryByDate")]
        [HttpGet]
        public List<PurchaseOrderVM> getPaymentHistoryByDate(string StartDate,string EndDate,int VendorId)      ///////31052017(D)////////
        {
            return _IVendorManager.getPaymentHistoryByDate(StartDate,EndDate,VendorId);
        }
        #region getAllVendorProductItem
        [Route("getAllVendorProductItem")]
        [HttpGet]
        public List<VendorProductAndItemVM> getAllVendorProductItem(int vendorID)
        {
            return _IVendorManager.getAllVendorProductItem(vendorID);
        }
        #endregion
    }
}
