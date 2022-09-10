//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Inventory.DomainModel.DatabaseModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class SO_SalesOrder_Version
    {
        public int SalesOrderVersionId { get; set; }
        public int SalesOrderId { get; set; }
        public Nullable<int> Version { get; set; }
        public string OrderNumber { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string SalesRep { get; set; }
        public string PONumber { get; set; }
        public Nullable<System.DateTime> RequestShipDate { get; set; }
        public Nullable<int> PaymentTermsId { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public Nullable<int> PricingSchemeId { get; set; }
        public string OrderRemarks { get; set; }
        public Nullable<decimal> OrderSubTotal { get; set; }
        public Nullable<decimal> OrderTax1 { get; set; }
        public Nullable<decimal> OrderTax2 { get; set; }
        public Nullable<decimal> OrderExtra { get; set; }
        public Nullable<decimal> OrderTotal { get; set; }
        public Nullable<int> TaxingSchemeId { get; set; }
        public Nullable<decimal> Tax1Rate { get; set; }
        public Nullable<decimal> Tax2Rate { get; set; }
        public Nullable<bool> CalculateTax2OnTax1 { get; set; }
        public string Tax1Name { get; set; }
        public string Tax2Name { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> PickedDate { get; set; }
        public string PickingRemarks { get; set; }
        public Nullable<System.DateTime> PackedDate { get; set; }
        public string PackingRemarks { get; set; }
        public string ShippingRemarks { get; set; }
        public Nullable<System.DateTime> InvoicedDate { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string ReturnRemarks { get; set; }
        public Nullable<decimal> ReturnSubTotal { get; set; }
        public Nullable<decimal> ReturnTax1 { get; set; }
        public Nullable<decimal> ReturnTax2 { get; set; }
        public Nullable<decimal> ReturnExtra { get; set; }
        public Nullable<decimal> ReturnTotal { get; set; }
        public Nullable<decimal> ReturnFee { get; set; }
        public string RestockRemarks { get; set; }
        public string ContactName { get; set; }
        public string Phone { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingCountry { get; set; }
        public string BillingPostalCode { get; set; }
        public string BillingAddressRemarks { get; set; }
        public Nullable<int> BillingAddressType { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingAddress2 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingPostalCode { get; set; }
        public string ShippingAddressRemarks { get; set; }
        public Nullable<int> ShippingAddressType { get; set; }
        public string Custom1 { get; set; }
        public string Custom2 { get; set; }
        public string Custom3 { get; set; }
        public string Custom4 { get; set; }
        public string Custom5 { get; set; }
        public Nullable<int> LastModUserId { get; set; }
        public Nullable<System.DateTime> LastModDttm { get; set; }
        public byte[] Timestamp { get; set; }
        public Nullable<int> ParentSalesOrderId { get; set; }
        public Nullable<int> SplitPartNumber { get; set; }
        public Nullable<bool> Tax1OnShipping { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<bool> ShowShipping { get; set; }
        public string ShipToCompanyName { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<bool> Tax2OnShipping { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> PaymentStatus { get; set; }
        public Nullable<int> InventoryStatus { get; set; }
        public Nullable<bool> IsCancelled { get; set; }
        public string SummaryLinePermutation { get; set; }
        public Nullable<decimal> NonCustomerCost { get; set; }
        public Nullable<bool> NonCustomerCostIsPercent { get; set; }
        public Nullable<bool> IsQuote { get; set; }
        public Nullable<bool> IsInvoiced { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public Nullable<bool> SameBillingAndShipping { get; set; }
    }
}
