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
    
    public partial class SO_SalesOrderPick_Line_Version
    {
        public int SalesOrderPickVersionLineId { get; set; }
        public int SalesOrderPickLineId { get; set; }
        public int SalesOrderId { get; set; }
        public Nullable<int> Version { get; set; }
        public Nullable<int> LineNum { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string Sublocation { get; set; }
        public byte[] Timestamp { get; set; }
        public string QuantityUom { get; set; }
        public Nullable<decimal> QuantityDisplay { get; set; }
        public Nullable<int> ProdId { get; set; }
        public string Serials { get; set; }
    }
}
