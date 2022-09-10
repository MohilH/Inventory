
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.CommonViewModels;
using Inventory.DomainModel.DatabaseModel;


namespace Inventory.BusinessLogic.Interface
{
    public interface IVendorManager
    {
      // List<VendorVM> getVendorList();

        int saveNewVendorDetails(VendorVM VendorModel);
        List<CurrencyVM> getAllCurrency();
        List<VendorPaymentTermVM> getAllPaymentTerms();
        List<TaxingSchemeVM> getAllTaxing();
        
       List<VendorVM> getAllVendors();

        List<LocationVM> getAllLocation();

        List<ProductVM> existProductList();

       // int  addVendorProductItem(List<VendorProductItemVM> VendorProductItemModel);

      //  List<VendorProductItemVM> getVendorProductItemList();

       // List<VendorVM> getAllVendors(int? venderID);
        List<PurchaseOrderVM> getVendorOrderStatus(int vendorID);
        //List<PaymentHistoryVM> getVendorPaymentHistory(int vendorID);
        //List<PurchaseOrderVM> getVendorPaymentHistory(int vendorID);
        int SaveVendorProducts(VendorProductAndItemVM vendorProductModel);

        List<VendorProductAndItemVM> getAllVendorProductItem(int vendorID);
        VendorVM getVendorByID(int venderID);
        int getExistVendorName(string VendorName);    ///////17052017(D)////////
        int getExistVendorItemName(string vendorItemName);    ///////17052017(D)////////

        PurchaseOrderVM getLatestPaymentRecord(int vendorId);  ///////31052017(D)////////
        List<PurchaseOrderVM> getPaymentHistoryByDate(string StartDate, string EndDate, int VendorId); ///////01062017(D)////////
    }
}
