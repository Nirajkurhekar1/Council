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
    
    public partial class FORM84_DETAIL_ESTIMATE
    {
        public int Form84_ID { get; set; }
        public string Form84_Year { get; set; }
        public Nullable<decimal> TotalActual_last_year { get; set; }
        public Nullable<decimal> TotalActual_second_last_year { get; set; }
        public Nullable<decimal> TotalActual_third_last_year { get; set; }
        public Nullable<decimal> TotalBudget_current8_month { get; set; }
        public Nullable<decimal> TotalRemaining4_month { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> TotalBudget_Estimate { get; set; }
        public Nullable<decimal> Remark_variation_difference { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    }
}
