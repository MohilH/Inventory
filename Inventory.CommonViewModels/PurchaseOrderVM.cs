using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;

using System.Web;

using System.Web.Mvc;
using System.ComponentModel;


namespace Inventory.CommonViewModels
{


    public partial class PurchaseOrderVM
    {
        public IList<VendorVM> _VendorList { get; set; }
        public List<VendorPaymentTermVM> VendorPaymentTermList { get; set; }
        public List<TaxingSchemeVM> VendorTaxingSchemeList { get; set; }
        public List<CurrencyVM> VendorCurrencyList { get; set; }
        public List<LocationVM> VendorLocationList { get; set; }
        public List<ProductVM> ExistProductList { get; set; }
        public List<PurchaseOrderLineVM> PurchaseOrderItem { get; set; }

        public List<PurchaseOrderReceiveLineVM> purchaseOrderReciveItem { get; set; }

        public List<PurchaseOrderReturnVM> purchaseOrderReturnItem { get; set; }

        public List<PurchaseOrderUnstockVM> purchaseOrderUnstockItem { get; set; }

        public int PurchaseOrderId { get; set; }        
        public string OrderNumber { get; set; }
        public string VendorOrderNumber { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> VendorId { get; set; }
        public Nullable<int> PaymentTermsId { get; set; }
        public string Carrier { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string VendorAddress1 { get; set; }
        public string ShipToAddress1 { get; set; }
  
        public Nullable<decimal> Freight { get; set; }
        public Nullable<System.DateTime> RequestShipDate { get; set; }
        public Nullable<decimal> OrderSubTotal { get; set; }
        public Nullable<decimal> OrderTotal { get; set; }

        public Nullable<decimal> ReturnSubTotal { get; set; }

        public Nullable<decimal> ReturnTotal { get; set; }

        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }


        public Nullable<decimal> Balance { get; set; }
        public string InventoryPaymentStatusName { get; set; }

        public Nullable<int> TaxingSchemeId { get; set; }
        public string OrderRemarks { get; set; }
        public Nullable<int> PaymentStatus { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
         
        public Nullable<int> LocationId { get; set; }
      
        public Nullable<int> CurrencyId { get; set; }
        public string ReceiveRemarks { get; set; }

        public string ReturnRemarks { get; set; }

        public string UnstockRemarks { get; set; }

        public Nullable<int> ProdId { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public string VendorItemCode { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<System.DateTime> ReceiveDate { get; set; }
        public Nullable<System.DateTime> ReturnDate { get; set; }
        public Nullable<System.DateTime> UnstockDate { get; set; }
        public PurchaseOrderVM PurchaseOrderDetails { get; set; }
        public string PuchaseOrderFormId { get; set; }

        public Nullable<decimal> CurrentBalance { get; set; }
        

        
    }


    public  class VendorItemCodeVM
    {
        public int VendorItemCodeId { get; set; }
        public string CodeName { get; set; }
        public Nullable<int> PurchaseOrderId { get; set; }
        public Nullable<int> PurchaseOrderLineId { get; set; }
        public Nullable<int> PurchaseOrderReceiveLineId { get; set; }
    }
    public class ListPurchaseOrderVM
    {
        public int PurchaseOrderId { get; set; }

        public string OrderNumber { get; set; }
         [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> InventoryStatus { get; set; }
        public Nullable<int> PaymentStatus { get; set; }
       
        public string VendorOrderNumber { get; set; }

        public string VendorName { get; set; }
        public Nullable<int> VendorId { get; set; }


        public string LocationName { get; set; }
         [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> DueDate { get; set; }

      //  [DisplayFormat(DataFormatString = "{0:n2}")]
         public Nullable<decimal> OrderTotal { get; set; }

        //  [DisplayFormat(DataFormatString = "{0:n2}")]
        public Nullable<decimal> Balance { get; set; }

        //  [DisplayFormat(DataFormatString = "{0:n2}")]
        public Nullable<decimal> AmountPaid { get; set; }
         [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> RequestShipDate { get; set; }


    }


    public class PurchaseOrderFormAllData
    {
        public List<PurchaseOrderLineVM> PurchaseOrderItem { get; set; }

        public PurchaseOrderVM PurchaseOrder { get; set; }

        public List<PurchaseOrderReceiveLineVM> PurchaseOrderReceiveItem { get; set; }

        public List<PurchaseOrderReturnVM> PurchaseOrderReturnItem { get; set; }
        public List<PurchaseOrderUnstockVM> PurchaseOrderUnstockItem { get; set; }
         
    }


    public class PurchaseOrderLineVM
    {
        public int PurchaseOrderLineId { get; set; }
        public int PurchaseOrderId { get; set; }
        public Nullable<int> Version { get; set; }
        public string Description { get; set; }
        public string VendorItemCode { get; set; }

       // [DisplayFormat(DataFormatString = "{0:n0}")]
        public int ? Quantity { get; set; }

     //   [DisplayFormat(DataFormatString = "{0:n2}")]
        public Nullable<decimal> UnitPrice { get; set; }

       // [DisplayFormat(DataFormatString = "{0:n2}")]
        public Nullable<decimal> SubTotal { get; set; }
        public byte[] Timestamp { get; set; }
        public string QuantityUom { get; set; }
        public Nullable<decimal> QuantityDisplay { get; set; }

       // [DisplayFormat(DataFormatString = "{0:n2}")]
        public Nullable<decimal> Discount { get; set; }
        public Nullable<bool> DiscountIsPercent { get; set; }
        public Nullable<int> ProdId { get; set; }
        public Nullable<int> LineNum { get; set; }
        public Nullable<int> TaxCodeId { get; set; }
        public Nullable<decimal> Tax1Rate { get; set; }
        public Nullable<decimal> Tax2Rate { get; set; }
        public string Serials { get; set; }
        public Nullable<bool> ServiceCompleted { get; set; }

        public Nullable<bool> OrderStatus { get; set; }
        
        public string productName { get; set; }

        public int PurchaseOrderReceiveLineId { get; set; }
        
        
    }

    public class PurchaseOrderReceiveLineVM
    {
        public int PurchaseOrderReceiveLineId { get; set; }
        public int PurchaseOrderId { get; set; }
        public Nullable<int> Version { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> ReceiveDate { get; set; }
        public string Description { get; set; }
        public string VendorItemCode { get; set; }

       //  [DisplayFormat(DataFormatString = "{0:n0}")]
        public int ? Quantity { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string Sublocation { get; set; }
        public byte[] Timestamp { get; set; }
        public string QuantityUom { get; set; }
        public Nullable<decimal> QuantityDisplay { get; set; }
        public Nullable<int> ProdId { get; set; }
        public Nullable<int> LineNum { get; set; }
        public string Serials { get; set; }

        public string productName { get; set; }

        public string LocationName { get; set; }

        public int PurchaseOrderLineId { get; set; }

        public string UrlParameter { get; set; }

        public Nullable<int> VendorItemCodeId { get; set; }
       

        public string VendorItemCodeName { get; set; }

        //public virtual BASE_Location BASE_Location { get; set; }
        //public virtual BASE_Product BASE_Product { get; set; }
        //public virtual PO_PurchaseOrder PO_PurchaseOrder { get; set; }


    }

    public class PurchaseOrderReturnVM
    {
        public int PurchaseOrderReturnLineId { get; set; }
        public int PurchaseOrderId { get; set; }
        public Nullable<int> Version { get; set; }
        public string Description { get; set; }
        public string VendorItemCode { get; set; }
      //   [DisplayFormat(DataFormatString = "{0:n0}")]
        public int ? Quantity { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<decimal> SubTotal { get; set; }
        public byte[] Timestamp { get; set; }
        public string QuantityUom { get; set; }
        public Nullable<decimal> QuantityDisplay { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public Nullable<bool> DiscountIsPercent { get; set; }
        public Nullable<int> ProdId { get; set; }
        public Nullable<int> LineNum { get; set; }
        public Nullable<int> TaxCodeId { get; set; }
        public Nullable<decimal> Tax1Rate { get; set; }
        public Nullable<decimal> Tax2Rate { get; set; }
        public string Serials { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> ReturnDate { get; set; }

        public string productName { get; set; }

    }

    public class PurchaseOrderUnstockVM
    {
        public int PurchaseOrderUnstockLineId { get; set; }
        public int PurchaseOrderId { get; set; }
        public Nullable<int> Version { get; set; }
        public string Description { get; set; }
      //   [DisplayFormat(DataFormatString = "{0:n0}")]
        public int ? Quantity { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string Sublocation { get; set; }
        public byte[] Timestamp { get; set; }
        public string QuantityUom { get; set; }
        public Nullable<decimal> QuantityDisplay { get; set; }
        public Nullable<int> ProdId { get; set; }
        public Nullable<int> LineNum { get; set; }
        public string Serials { get; set; }
        public string VendorItemCode { get; set; }

         [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> UnstockDate { get; set; }

        public string productName { get; set; }
        public string LocationName { get; set; }


    }



}
