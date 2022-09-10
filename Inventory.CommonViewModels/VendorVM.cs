using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.CommonViewModels
{
    // [MetadataType(typeof(VendorVMMetaData))]
    #region VendorVM
    public class VendorVM
    {
        public int VendorId { get; set; }
        public Nullable<int> Version { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> DefaultPaymentTermsId { get; set; }
        public Nullable<int> TaxingSchemeId { get; set; }
        public string DefaultCarrier { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string AddressRemarks { get; set; }
        public Nullable<int> AddressType { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public Nullable<int> LastModUserId { get; set; }
        public Nullable<System.DateTime> LastModDttm { get; set; }
        public byte[] Timestamp { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string Website { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> Credit { get; set; }
        public string PaymentTerms { get; set; }
        public string Currency { get; set; }
        public string TaxingScheme { get; set; }
        public Nullable<bool> PriceIncludeTaxes { get; set; }

        public int VendorItemId { get; set; }
        public string VendorItemCode { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public int? ProdId { get; set; }
        public VendorVM VendorDetails { get; set; }
        public List<VendorPaymentTermVM> VendorPaymentTermList { get; set; }
        public List<TaxingSchemeVM> VendorTaxingSchemeList { get; set; }
        public List<CurrencyVM> VendorCurrencyList { get; set; }
        public List<PurchaseOrderVM> VendorOrderHistoryList { get; set; }
        //public List<PaymentHistoryVM> VendorPaymentHistoryList { get; set; }
        public List<VendorProductAndItemVM> VendorProductItemList { get; set; }
       


    }
    #endregion


    #region VendorPaymentTermVM
    public class VendorPaymentTermVM
    {
        public int PaymentTermsId { get; set; }
        public string Name { get; set; }
    }
    #endregion
    #region TaxingSchemeVM
    public class TaxingSchemeVM
    {
        public int TaxingSchemeId { get; set; }
        public string Name { get; set; }

    }
    #endregion
    #region CurrencyVM
    public class CurrencyVM
    {
        public int CurrencyId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

    }

    #endregion

    #region LocationVM
    //public class LocationVM
    //{

    //    public int LocationId { get; set; }
    //    public string Name { get; set; }
    //    //public Nullable<int> LastModUserId { get; set; }

    //}
    #endregion
    public class ProductVM
    {
        public Nullable<int> Version { get; set; }
        public Nullable<int> ItemType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public string BarCode { get; set; }
        public Nullable<decimal> ProductCost { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string DefaultLocationId { get; set; }
        public string DefaultSublocation { get; set; }
        public Nullable<decimal> ReorderPoint { get; set; }
        public Nullable<decimal> ReorderQuantity { get; set; }
        public string Uom { get; set; }
        public Nullable<decimal> MasterPackQty { get; set; }
        public Nullable<decimal> InnerPackQty { get; set; }
        public Nullable<decimal> CaseLength { get; set; }
        public Nullable<decimal> CaseWidth { get; set; }
        public Nullable<decimal> CaseHeight { get; set; }
        public Nullable<decimal> CaseWeight { get; set; }
        public Nullable<decimal> ProductLength { get; set; }
        public Nullable<decimal> ProductWidth { get; set; }
        public Nullable<decimal> ProductHeight { get; set; }
        public Nullable<decimal> ProductWeight { get; set; }
        public Nullable<int> LastVendorId { get; set; }
        public Nullable<bool> IsSellable { get; set; }
        public Nullable<bool> IsPurchaseable { get; set; }
        public Nullable<System.DateTime> DateIntroduced { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public Nullable<int> LastModUserId { get; set; }
        public Nullable<System.DateTime> LastModDttm { get; set; }
        public byte[] Timestamp { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string PictureFileAttachmentId { get; set; }
        public string SoUomName { get; set; }
        public Nullable<decimal> SoUomRatioStd { get; set; }
        public Nullable<decimal> SoUomRatio { get; set; }
        public string PoUomName { get; set; }
        public Nullable<decimal> PoUomRatioStd { get; set; }
        public Nullable<decimal> PoUomRatio { get; set; }
        public int ProdId { get; set; }
        public Nullable<bool> TrackSerials { get; set; }
        public int QuantityId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> LocationId { get; set; }

        public int ItemPriceId { get; set; }
        public int PricingSchemeId { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> NormalPrice { get; set; }

        public string CategoryName { get; set; }

    }

 


    public class VendorItemVM
    {
        public int VendorItemId { get; set; }
        public int VendorId { get; set; }
        public string VendorItemCode { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> LastModUserId { get; set; }
        public Nullable<System.DateTime> LastModDttm { get; set; }
        public byte[] Timestamp { get; set; }
        public Nullable<int> ProdId { get; set; }

    }

    public class QuantityVM
    {
        public int QuantityId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<int> ProdId { get; set; }

    }

  


    public class ItemPriceVM
    {
        public int ItemPriceId { get; set; }
        public int PricingSchemeId { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<int> LastModUserId { get; set; }
        public Nullable<System.DateTime> LastModDttm { get; set; }
        public Nullable<int> ProdId { get; set; }

        public Nullable<decimal> NormalPrice { get; set; }
    }

    public class VendorProductItemVM
    {
        public int VendorItemId { get; set; }
        public int QuantityId { get; set; }

        public int ItemPriceId { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public int VendorId { get; set; }
        public string VendorItemCode { get; set; }

        public Nullable<int> ProdId { get; set; }

        public Nullable<int> Quantity { get; set; }

        public Nullable<int> LocationId { get; set; }

        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> Discount { get; set; }

        public string OrderNumber { get; set; }
    }
    #region VendorProductAndItemVM
    public class VendorProductAndItemVM
    {
        public int VendorItemId { get; set; }
        public int VendorId { get; set; }
        public string VendorItemCode { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public int? ProdId { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> Discount { get; set; }

        public string OrderNumber { get; set; }

    }
    #endregion
  

   

//    #region PaymentHistoryVM
//    public class PaymentHistoryVM
//    {
//        public int PaymentHistoryId { get; set; }
//        public Nullable<System.DateTime> Date { get; set; }
//        public Nullable<System.DateTime> DueDate { get; set; }
//        public Nullable<decimal> Amount { get; set; }
//        public Nullable<decimal> CreditBalance { get; set; }
//        public Nullable<decimal> Balance { get; set; }
//        public Nullable<decimal> Transactions { get; set; }
//    }
//    #endregion
}
