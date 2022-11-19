//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmpanadaReviewCS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserEmpanada
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserEmpanada()
        {
            this.Reviews1 = new HashSet<Review>();
        }
    
        public int idUser { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string bio { get; set; }
        public System.DateTime createdAt { get; set; }
        public Nullable<System.DateTime> updatedAt { get; set; }
        public string imageSrc { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public Nullable<int> reviews { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews1 { get; set; }
    }
}
