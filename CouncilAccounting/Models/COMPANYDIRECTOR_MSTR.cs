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
    
    public partial class COMPANYDIRECTOR_MSTR
    {
        public int Director_Id { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string Director_Propiertor { get; set; }
        public string PAN_DIR { get; set; }
        public string DIN_Number { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
    }
}
