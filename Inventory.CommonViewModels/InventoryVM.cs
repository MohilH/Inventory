using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Inventory.CommonViewModels
{
    public class InventoryVM
    {
        public ProductVM InventoryProductDetails { get; set; }
        public List<LocationVM> InventoryLocationList { get; set; }
        public List<QuantityVM> InventoryQuantityList { get; set; }
        public List<VendorVM> InventoryLastVendorIdList { get; set; } 
        public List<CategoryVM> InventoryCategoryList { get; set; }
        public List<PricingSchemeVM> InventoryPricingSchemeList { get; set; }
        public List<VendorProductAndItemVM> InventoryProductItemList { get; set; }
        public List<InventoryLocationAndQuantityVM> InventoryLocationAndQuantityList { get; set; }
        public List<VendorVM> InventoryVendorList { get; set; }
        public List<ItemPriceVM> InventoryItemPriceList { get; set; }
        public List<ProductVM> InventoryProductList { get; set; }
        public List<InventoryBillOfMaterialVM> InventoryBillOfMaterialList { get; set; }
      
    }
    public class LocationVM
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
      
    }

    public class CategoryVM
    {
        public int CategoryId { get; set; }
        //public Nullable<int> ParentCategoryId { get; set; }
        public string Name { get; set; }
    }
    public class PricingSchemeVM
    {
        public int PricingSchemeId { get; set; }
        public string Name { get; set; }
        public Nullable<int> LastModUserId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CurrencyId { get; set; }
    }
    public class InventoryLocationAndQuantityVM
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
        public int QuantityId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> ProdId { get; set; }
    }

    public class InventoryBillOfMaterialVM
    {
        public int BillOfMaterialId { get; set; }
        public Nullable<int> ProdId { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
    }
  
}
