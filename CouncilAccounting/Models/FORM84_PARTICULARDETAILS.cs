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
    
    public partial class FORM84_PARTICULARDETAILS
    {
        public int Form84_PDID { get; set; }
        public Nullable<int> Form84_ID { get; set; }
        public string Particulars { get; set; }
        public string Accountcode { get; set; }
        public Nullable<decimal> Actual_last_year { get; set; }
        public Nullable<decimal> Actual_second_last_year { get; set; }
        public Nullable<decimal> Actual_third_last_year { get; set; }
        public Nullable<decimal> Budget_current8_month { get; set; }
        public Nullable<decimal> Remaining4_month { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> Budget_Estimate { get; set; }
        public Nullable<decimal> Remark_variation_difference { get; set; }
        public byte[] Signature_image { get; set; }
        public string MainAccount { get; set; }
        public string SubAccount { get; set; }
        public string AccountCategory { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Session { get; set; }
        public Nullable<decimal> Status { get; set; }
        public Nullable<decimal> Percentage { get; set; }
    }
}
