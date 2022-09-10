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
    [RoutePrefix("InventoryProductInfo")]
    public class InventoryProductInfoController : ApiController
    {
        private readonly IInventoryProductInfoManager _IInventoryProductInfoManager;
        public InventoryProductInfoController()
        {

        }
        public InventoryProductInfoController(IInventoryProductInfoManager IInventoryProductInfoManager)
        {
            this._IInventoryProductInfoManager = IInventoryProductInfoManager;
        }
        #region getAllCategory
        [Route("getAllCategory")]
        [HttpGet]
        public List<CategoryVM> getAllCategory()
        {
            return _IInventoryProductInfoManager.getAllCategory();
        }
        #endregion
        #region getAllInventoryLocation
        [Route("getAllInventoryLocation")]
        [HttpGet]
        public List<LocationVM> getAllInventoryLocation()
        {
            return _IInventoryProductInfoManager.getAllInventoryLocation();
        }
        #endregion
        #region getAllInventoryVendors
        [Route("getAllInventoryVendors")]
        [HttpGet]
        public List<VendorVM> getAllInventoryVendors()
        {
            return _IInventoryProductInfoManager.getAllInventoryVendors();
        }
        #endregion
        #region getAllBillOfMaterialsItemName
        [Route("getAllBillOfMaterialsItemName")]
        [HttpGet]
        public List<ProductVM> getAllBillOfMaterialsItemName()
        {
            return _IInventoryProductInfoManager.getAllBillOfMaterialsItemName();
        }
        #endregion
        #region getAllInventoryLastVendorId
        [Route("getAllInventoryLastVendorId")]
        [HttpGet]
        public List<VendorVM> getAllInventoryLastVendorId()
        {
            return _IInventoryProductInfoManager.getAllInventoryLastVendorId();
        }
        #endregion
        #region AddNewProductInfo
        [Route("AddNewProductInfo")]
        [HttpGet]
        public List<InventoryVM> AddNewProductInfo()
        {
            return _IInventoryProductInfoManager.AddNewProductInfo();
        }
        #endregion
        #region SaveInventoryProductInfo
        [Route("SaveInventoryProductInfo")]
        [HttpPost]
        public int SaveInventoryProductInfo(ProductVM ProductModel)
        {
            return _IInventoryProductInfoManager.SaveInventoryProductInfo(ProductModel);
        }
        #endregion
        #region SaveProductVendorsGrid
        [Route("SaveProductVendorsGrid")]
        [HttpPost]
        public int SaveProductVendorsGrid(List<VendorProductAndItemVM> VendorModel)
        {
            return _IInventoryProductInfoManager.SaveProductVendorsGrid(VendorModel);
        }
        #endregion
        #region SaveNewCategoryPopUp
        [Route("SaveNewCategoryPopUp")]
        [HttpPost]
        public int SaveNewCategoryPopUp(CategoryVM CategoryModel)
        {
            return _IInventoryProductInfoManager.SaveNewCategoryPopUp(CategoryModel);
        }
        #endregion
        #region getExistItemName
        [Route("getExistItemName")]
         [HttpGet]
         public int getExistItemName(string ItemName)      
         {
             return _IInventoryProductInfoManager.getExistItemName(ItemName);
         }
        #endregion
        #region getQuantityUnitpriceByItem
        [Route("getQuantityUnitpriceByItem")]
         [HttpGet]
        public string getQuantityUnitpriceByItem(int prodId)
         {
             return _IInventoryProductInfoManager.getQuantityUnitpriceByItem(prodId);
         }
        #endregion
        #region getAllInventoryProductInfo
        [Route("getAllInventoryProductInfo")]
        [HttpGet]
        public List<ProductVM> getAllInventoryProductInfo()
        {
            return _IInventoryProductInfoManager.getAllInventoryProductInfo();
        }
        #endregion
        #region SaveBillOfMaterials
        [Route("SaveBillOfMaterials")]
        [HttpPost]
        public int SaveBillOfMaterials(List<InventoryBillOfMaterialVM> InventoryBillModel)
        {
            return _IInventoryProductInfoManager.SaveBillOfMaterials(InventoryBillModel);
        }
        #endregion
        #region SaveLocationQuantity
        [Route("SaveLocationQuantity")]
        [HttpPost]
        public int SaveLocationQuantity(List<QuantityVM> QuantityModel)
        {
            return _IInventoryProductInfoManager.SaveLocationQuantity(QuantityModel);
        }
        #endregion
    }
}
