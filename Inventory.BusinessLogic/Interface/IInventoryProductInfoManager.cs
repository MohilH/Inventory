using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.CommonViewModels;
using Inventory.DomainModel.DatabaseModel;

namespace Inventory.BusinessLogic.Interface
{
    public interface IInventoryProductInfoManager
    {
        int SaveInventoryProductInfo(ProductVM ProductModel);
        int SaveProductVendorsGrid(List<VendorProductAndItemVM> VendorModel);
        int SaveBillOfMaterials(List<InventoryBillOfMaterialVM> InventoryBillModel);
        int SaveLocationQuantity(List<QuantityVM> QuantityModel);
        int SaveNewCategoryPopUp(CategoryVM CategoryModel);
        List<ProductVM> getAllBillOfMaterialsItemName();
        List<CategoryVM> getAllCategory();
        List<LocationVM> getAllInventoryLocation();
        List<VendorVM> getAllInventoryVendors();
        List<VendorVM> getAllInventoryLastVendorId();
        List<InventoryVM> AddNewProductInfo();
        List<ProductVM> getAllInventoryProductInfo();
        int getExistItemName(string ItemName);

        string getQuantityUnitpriceByItem(int prodId);
    }
}
