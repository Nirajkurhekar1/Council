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
    
    public partial class ACCOUNTPOSTING
    {
        public int Accountposting_Id { get; set; }
        public int Form_id { get; set; }
        public int Acc_id { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public string Accountposting1 { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> RefDocDetails { get; set; }
        public Nullable<int> Acc_subtype_Id { get; set; }
        public Nullable<int> RefParticularDetail { get; set; }
        public string Description { get; set; }
    
        public virtual CHARTOFACCOUNT CHARTOFACCOUNT { get; set; }
        public virtual FORM_DETAILS FORM_DETAILS { get; set; }
    }
}
