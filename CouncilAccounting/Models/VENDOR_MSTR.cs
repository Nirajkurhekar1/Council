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
    
    public partial class VENDOR_MSTR
    {
        public int Vendor_Id { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PANNo { get; set; }
        public string GISTINNo { get; set; }
        public string VendorMasterCode { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public Nullable<decimal> Status { get; set; }
        public string FunctionCode { get; set; }
        public string ObjectCode { get; set; }
    }
}
