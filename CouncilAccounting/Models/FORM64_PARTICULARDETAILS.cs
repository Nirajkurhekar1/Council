//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CouncilAccounting.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FORM64_PARTICULARDETAILS
    {
        public int Form64_ParticularDetailID { get; set; }
        public Nullable<int> Form64_Id { get; set; }
        public Nullable<int> Bill_PD_Id { get; set; }
        public string Narration { get; set; }
        public string Accountcode { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Percentage { get; set; }
        public string Description { get; set; }
    
        public virtual FORM64_PAYMENTVOUCHER FORM64_PAYMENTVOUCHER { get; set; }
    }
}
