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
    
    public partial class BASE_Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BASE_Category()
        {
            this.BASE_Category1 = new HashSet<BASE_Category>();
        }
    
        public int CategoryId { get; set; }
        public Nullable<int> ParentCategoryId { get; set; }
        public string Name { get; set; }
        public byte[] Timestamp { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BASE_Category> BASE_Category1 { get; set; }
        public virtual BASE_Category BASE_Category2 { get; set; }
    }
}
