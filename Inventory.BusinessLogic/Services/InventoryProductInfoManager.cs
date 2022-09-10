using Inventory.BusinessLogic.Interface;
using Inventory.CommonViewModels;
using Inventory.Repositories.Repository;
using Inventory.DomainModel.DatabaseModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Repositories.Interface;
using NLog;
namespace Inventory.BusinessLogic.Services
{
    public class InventoryProductInfoManager : IInventoryProductInfoManager
    {
        Logger logger = LogManager.GetCurrentClassLogger();
        private ICategory _ICategory;
        private ILocation _Ilocation;
        private IProduct _IProduct;
        private IQuantity _IQuantity;
        private IVendor _IVendor;
        private IVendorItem _IVendorItem;
        private IItemPrice _IItemPrice;
        private IBillOfMaterial _IBillOfMaterial;
        public InventoryProductInfoManager()
        {

        }
        public InventoryProductInfoManager(ICategory ICategory, IProduct IProduct, ILocation Ilocation,
            IQuantity IQuantity, IVendor IVendor,
            IVendorItem IVendorItem, IItemPrice IItemPrice,
            IBillOfMaterial IBillOfMaterial
            )
        {
            this._ICategory = ICategory;
            this._IProduct = IProduct;
            this._Ilocation = Ilocation;
            this._IQuantity = IQuantity;
            this._IVendor = IVendor;
            this._IVendorItem = IVendorItem;
            this._IItemPrice = IItemPrice;
            this._IBillOfMaterial = IBillOfMaterial;
        }
        /// <summary>
        /// Get the dropdown list of Category
        /// </summary>
        public List<CategoryVM> getAllCategory()
        {
            List<CategoryVM> getCategory = new List<CategoryVM>();
            List<BASE_Category> categoryList = _ICategory.GetAll().ToList();
            foreach (var category in categoryList)
            {
                CategoryVM objCategoryVM = new CategoryVM
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name
                };
                getCategory.Add(objCategoryVM);

            }
            return getCategory;
        }

        /// <summary>
        /// Get the dropdown list of Location
        /// </summary>
        public List<LocationVM> getAllInventoryLocation()
        {
            List<LocationVM> getLocation = new List<LocationVM>();
            List<BASE_Location> locationList = _Ilocation.GetAll().ToList();
            foreach (var location in locationList)
            {
                LocationVM objLocationVM = new LocationVM
                {
                    LocationId = location.LocationId,
                    Name = location.Name,
                };
                getLocation.Add(objLocationVM);

            }
            return getLocation;
        }

        /// <summary>
        /// Get the dropdown list of Vendor Name 
        /// </summary>
        public List<VendorVM> getAllInventoryVendors()
        {
            List<VendorVM> getVendors = new List<VendorVM>();
            List<BASE_Vendor> vendorsList = _IVendor.GetAll().ToList();
            foreach (var vendor in vendorsList)
            {
                VendorVM objVendorVM = new VendorVM
                {
                    VendorId = vendor.VendorId,
                    Name = vendor.Name,
                };
                getVendors.Add(objVendorVM);

            }
            return getVendors;
        }
        /// <summary>
        /// Get the dropdown list for Item Name 
        /// </summary>
        public List<ProductVM> getAllBillOfMaterialsItemName()
        {
            List<ProductVM> getProductsItem = new List<ProductVM>();
            List<BASE_Product> productItemList = _IProduct.GetAll().ToList();
            foreach (var productItem in productItemList)
            {
                ProductVM objProductVM = new ProductVM
                {
                    ProdId = productItem.ProdId,
                    Name = productItem.Name,
                };
                getProductsItem.Add(objProductVM);

            }
            return getProductsItem;
        }

        /// <summary>
        /// Get Quantity and cost according to item name in bill of materials
        /// </summary>
        public string getQuantityUnitpriceByItem(int prodId)
        {
            string unitPrice = Convert.ToString(_IItemPrice.FindBy(x => x.ProdId == prodId).Select(x => x.UnitPrice).FirstOrDefault());
            string unitQuantity = Convert.ToString(_IQuantity.FindBy(x => x.ProdId == prodId).Select(x => x.Quantity).FirstOrDefault());


            //obj.CurrentBalance = objBalance.Balance;

            return unitPrice + "||" + unitQuantity;
        }
        /// <summary>
        /// Get the dropdown list of Vendor Name 
        /// </summary>
        public List<VendorVM> getAllInventoryLastVendorId()
        {
            List<VendorVM> getLastVendorId = new List<VendorVM>();
            List<BASE_Vendor> vendorList = _IVendor.GetAll().ToList();
            foreach (var lastvendorId in vendorList)
            {
                VendorVM objVendorVM = new VendorVM
                {
                    VendorId = lastvendorId.VendorId,
                    Name = lastvendorId.Name,
                };
                getLastVendorId.Add(objVendorVM);

            }
            return getLastVendorId;
        }
        public List<InventoryVM> AddNewProductInfo()
        {
            List<InventoryVM> inventoryVM = new List<InventoryVM>();
            return inventoryVM;

        }

        /// <summary>
        /// use for save the data of Inventory Product Info
        /// </summary>
        public int SaveInventoryProductInfo(ProductVM ProductModel)
        {
            BASE_Product objProduct = new BASE_Product();
            //Base_Quantity objQuantity = new Base_Quantity();
            BASE_ItemPrice objItemPrice = new BASE_ItemPrice();
            try
            {
                if (ProductModel.ProdId > 0 && ProductModel.QuantityId > 0 && ProductModel.ItemPriceId > 0)
                {

                    objProduct = _IProduct.FindBy(x => x.ProdId == ProductModel.ProdId).FirstOrDefault();
                    //objQuantity = _IQuantity.FindBy(x => x.QuantityId == ProductModel.QuantityId).FirstOrDefault();
                    objItemPrice = _IItemPrice.FindBy(x => x.ItemPriceId == ProductModel.ItemPriceId).FirstOrDefault();
                    objProduct.Name = ProductModel.Name;
                    objProduct.CategoryId = ProductModel.CategoryId;
                    objProduct.ItemType = ProductModel.ItemType;
                    objProduct.PictureFileAttachmentId = ProductModel.PictureFileAttachmentId;
                    //objProduct.ProductCost = ProductModel.ProductCost;
                    //objProduct.NormalPrice = ProductModel.NormalPrice;
                    objProduct.BarCode = ProductModel.BarCode;
                    objProduct.ReorderPoint = ProductModel.ReorderPoint;
                    objProduct.ReorderQuantity = ProductModel.ReorderQuantity;
                    objProduct.DefaultLocationId = ProductModel.DefaultLocationId;
                    objProduct.LastVendorId = ProductModel.LastVendorId;
                    objProduct.ProductLength = ProductModel.ProductLength;
                    objProduct.ProductWidth = ProductModel.ProductWidth;
                    objProduct.ProductHeight = ProductModel.ProductHeight;
                    objProduct.ProductWeight = ProductModel.ProductWeight;
                    objProduct.Remarks = ProductModel.Remarks;
                    //objQuantity.Quantity = ProductModel.Quantity;
                    //objQuantity.LocationId = ProductModel.LocationId;
                    objItemPrice.UnitPrice = ProductModel.UnitPrice;
                    objItemPrice.NormalPrice = ProductModel.NormalPrice;
                    _IProduct.Edit(objProduct);
                    //_IQuantity.Edit(objQuantity);
                    _IItemPrice.Edit(objItemPrice);
                }
                else
                {
                    objProduct.Name = ProductModel.Name;
                    objProduct.CategoryId = ProductModel.CategoryId;
                    objProduct.ItemType = ProductModel.ItemType;
                    objProduct.PictureFileAttachmentId = ProductModel.PictureFileAttachmentId;
                    //objProduct.ProductCost = ProductModel.ProductCost;
                    //objProduct.NormalPrice = ProductModel.NormalPrice;
                    objProduct.BarCode = ProductModel.BarCode;
                    objProduct.ReorderPoint = ProductModel.ReorderPoint;
                    objProduct.ReorderQuantity = ProductModel.ReorderQuantity;
                    objProduct.DefaultLocationId = ProductModel.DefaultLocationId;
                    objProduct.LastVendorId = ProductModel.LastVendorId;
                    objProduct.ProductLength = ProductModel.ProductLength;
                    objProduct.ProductWidth = ProductModel.ProductWidth;
                    objProduct.ProductHeight = ProductModel.ProductHeight;
                    objProduct.ProductWeight = ProductModel.ProductWeight;
                    objProduct.Remarks = ProductModel.Remarks;
                    _IProduct.Add(objProduct);
                    //var productid = objProduct.ProdId;
                    //objQuantity.Quantity = ProductModel.Quantity;
                    //objQuantity.LocationId = ProductModel.LocationId;
                    //objQuantity.ProdId = productid;
                    //_IQuantity.Add(objQuantity);
                    var ItemPriceProdid = objProduct.ProdId;
                    objItemPrice.UnitPrice = ProductModel.UnitPrice;
                    objItemPrice.NormalPrice = ProductModel.NormalPrice;
                    objItemPrice.ProdId = ItemPriceProdid;
                    _IItemPrice.Add(objItemPrice);

                }
                int productID = objProduct.ProdId;
                return productID;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in InventoryProductInfo  manager SaveInventoryProductInfo method ", ex);
                int productID = objProduct.ProdId;
                return productID;
            }
            //return 1;

        }


        /// <summary>
        /// use for save the data of Inventory Grid for Location and Quantity
        /// </summary>
        public int SaveLocationQuantity(List<QuantityVM> QuantityModel)
        {
            Base_Quantity objQuantity = new Base_Quantity();
       
            try
            {
                foreach (var item in QuantityModel)
                {
                    if (item.QuantityId > 0)
                    {
                        //objBillOfMaterial = _IBillOfMaterial.FindBy(x => x.BillOfMaterialId == InventoryBillModel.BillOfMaterialId).FirstOrDefault();
                        objQuantity.Quantity = item.Quantity;
                        objQuantity.LocationId = item.LocationId;
                        objQuantity.ProdId = item.ProdId;
                        _IQuantity.Edit(objQuantity);
                    }
                    else
                    {
                        objQuantity.Quantity = item.Quantity;
                        objQuantity.LocationId = item.LocationId;
                        objQuantity.ProdId = item.ProdId;
                        _IQuantity.Add(objQuantity);
                    }
                }
                int QuantityId = objQuantity.QuantityId;
                return QuantityId;
            }

            catch (Exception ex)
            {
                logger.ErrorException("Error occured in InventoryProductInfo  manager SaveBillOfMaterials method ", ex);
                int QuantityId = objQuantity.QuantityId;
                return QuantityId;
            }
            //return 1;
        }





        /// <summary>
        /// use for save the data of Product Vendors Grid
        /// </summary>
        public int SaveProductVendorsGrid(List<VendorProductAndItemVM> VendorModel)
        {
            BASE_Product objProduct = new BASE_Product();
            BASE_VendorItem objVendorItem = new BASE_VendorItem();
            try
            {
                foreach (var item in VendorModel)
                {
                    if (item.VendorItemId > 0 && item.ProdId > 0)
                    {
                        //objProduct = _IProduct.FindBy(x => x.ProdId == VendorModel.ProdId).FirstOrDefault();
                        //objVendorItem = _IVendorItem.FindBy(x => x.VendorItemId == VendorModel.VendorItemId).FirstOrDefault();
                        objVendorItem.VendorId = item.VendorId;
                        objVendorItem.VendorItemCode = item.VendorItemCode;
                        objVendorItem.Cost = item.Cost;
                        objVendorItem.ProdId = item.ProdId;
                        // _IVendor.Edit(objVendor);
                        _IVendorItem.Edit(objVendorItem);
                    }
                    else
                    {
                        //objVendor.Name = VendorModel.Name;
                        objVendorItem.VendorId = item.VendorId;
                        objVendorItem.VendorItemCode = item.VendorItemCode;
                        objVendorItem.Cost = item.Cost;
                        objVendorItem.ProdId = item.ProdId;
                        //objVendorItem.ProdId = VendorModel.ProdId;
                        //_IVendor.Add(objVendor);
                        //var productid = objProduct.ProdId;
                        //objVendorItem.ProdId = productid;
                        _IVendorItem.Add(objVendorItem);
                    }
                }
                int VendorItemId = objVendorItem.VendorItemId;
                return VendorItemId;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in InventoryProductInfo  manager SaveProductVendorsGrid method ", ex);
                int VendorItemId = objVendorItem.VendorItemId;
                return VendorItemId;
            }
            //return 1;
        }



        /// <summary>
        /// use for save the data of Bill Of Materials
        /// </summary>
        public int SaveBillOfMaterials(List<InventoryBillOfMaterialVM> InventoryBillModel)
        {
            Base_BillOfMaterials objBillOfMaterial = new Base_BillOfMaterials();
            try
            {
                foreach (var item in InventoryBillModel)
                {
                    if (item.BillOfMaterialId > 0)
                    {
                        //objBillOfMaterial = _IBillOfMaterial.FindBy(x => x.BillOfMaterialId == InventoryBillModel.BillOfMaterialId).FirstOrDefault();
                        objBillOfMaterial.ProdId = item.ProdId;
                        objBillOfMaterial.Quantity = item.Quantity;
                        objBillOfMaterial.UnitPrice = item.UnitPrice;
                        objBillOfMaterial.TotalCost = item.TotalCost;
                        _IBillOfMaterial.Edit(objBillOfMaterial);
                    }
                    else
                    {
                        objBillOfMaterial.ProdId = item.ProdId;
                        objBillOfMaterial.Quantity = item.Quantity;
                        objBillOfMaterial.UnitPrice = item.UnitPrice;
                        objBillOfMaterial.TotalCost = item.TotalCost;
                        _IBillOfMaterial.Add(objBillOfMaterial);
                    }
                }
                int BillOfMaterialId = objBillOfMaterial.BillOfMaterialId;
                return BillOfMaterialId;
            }

            catch (Exception ex)
            {
                logger.ErrorException("Error occured in InventoryProductInfo  manager SaveBillOfMaterials method ", ex);
                int BillOfMaterialId = objBillOfMaterial.BillOfMaterialId;
                return BillOfMaterialId;
            }
            //return 1;
        }


        /// <summary>
        /// use for save the data of Add New Catagory in Product Info
        /// </summary>
        public int SaveNewCategoryPopUp(CategoryVM CategoryModel)
        {
            int categoryID = 0;
            try
            {
                BASE_Category objCategory = new BASE_Category()
                {
                    Name = CategoryModel.Name,

                };
                if (CategoryModel.CategoryId > 0)
                {
                    objCategory.CategoryId = CategoryModel.CategoryId;
                    _ICategory.Edit(objCategory);
                }
                else
                {
                    _ICategory.Add(objCategory);
                }
                categoryID = objCategory.CategoryId;

            }
            catch (Exception ex)
            {
                logger.ErrorException("Error occured in InventoryProductInfo manager SaveNewCategoryPopUp", ex);
            }
            return categoryID;
        }
        /// <summary>
        /// use for check Item Name exists or not
        /// </summary>
        public int getExistItemName(string ItemName)
        {
            BASE_Product obj = new BASE_Product();
            obj.Name = _IProduct.FindBy(x => x.Name == ItemName).Select(x => x.Name).FirstOrDefault();
            int checkUser = 0;
            if (obj.Name != null)
            {
                checkUser = 1;
            }
            else
            {
                checkUser = 0;
            }
            return checkUser;
        }


        /// <summary>
        /// Use for get the Vendor List
        /// </summary>
        public List<ProductVM> getAllInventoryProductInfo()
        {
            List<ProductVM> getAllInventoryProductInfo = new List<ProductVM>();
            List<BASE_Product> objProductInfoList = _IProduct.GetAll().ToList();
            List<BASE_Category> objCategoryList = _ICategory.GetAll().ToList();
            List<BASE_ItemPrice> objItemPriceList = _IItemPrice.GetAll().ToList();
            var getProductItemList = from category in objCategoryList
                                     join product in objProductInfoList
                                     on category.CategoryId equals product.CategoryId
                                     //join itemPrice in objItemPriceList
                                     //on product.ProdId equals itemPrice.ProdId
                                     select new
                                     {
                                         categoryName = category.Name,
                                         //itemUnitPrice=itemPrice.UnitPrice,
                                         //itemNormalPrice = itemPrice.NormalPrice,
                                         product.CategoryId,
                                         product.ItemType,
                                         product.PictureFileAttachmentId,
                                         product.BarCode,
                                         product.ReorderPoint,
                                         product.ReorderQuantity,
                                         product.DefaultLocationId,
                                         product.LastVendorId,
                                         product.ProductLength,
                                         product.ProductWidth,
                                         product.ProductHeight,
                                         product.ProductWeight,
                                         product.Remarks,
                                         product.Name,
                                     };
            foreach (var product in getProductItemList)
            {
                ProductVM objProduct = new ProductVM
                {
                    Name = product.Name,
                    CategoryId = product.CategoryId,
                    ItemType = product.ItemType,
                    PictureFileAttachmentId = product.PictureFileAttachmentId,
                    BarCode = product.BarCode,
                    ReorderPoint = product.ReorderPoint,
                    ReorderQuantity = product.ReorderQuantity,
                    DefaultLocationId = product.DefaultLocationId,
                    LastVendorId = product.LastVendorId,
                    ProductLength = product.ProductLength,
                    ProductWidth = product.ProductWidth,
                    ProductHeight = product.ProductHeight,
                    ProductWeight = product.ProductWeight,
                    CategoryName = product.categoryName,
                    //UnitPrice=product.itemUnitPrice,
                    //NormalPrice=product.itemNormalPrice,
                    Remarks = product.Remarks,

                };
                getAllInventoryProductInfo.Add(objProduct);
            }

            return getAllInventoryProductInfo;
        }

    }
}
