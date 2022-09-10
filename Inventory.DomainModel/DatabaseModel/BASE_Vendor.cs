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
    
    public partial class BASE_Vendor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BASE_Vendor()
        {
            this.tblPaymentHistories = new HashSet<tblPaymentHistory>();
        }
    
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
    
        public virtual BASE_PaymentTerms BASE_PaymentTerms { get; set; }
        public virtual BASE_User BASE_User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPaymentHistory> tblPaymentHistories { get; set; }
        public virtual GLOBAL_Currency GLOBAL_Currency { get; set; }
    }
}
