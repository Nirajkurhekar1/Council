using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouncilAccounting.ViewModel
{
    public class BillINWARDs
    {
        public int Bill_Id { get; set; }
        public Nullable<int> Form_id { get; set; }
        public Nullable<decimal> NetAmountPayble { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public byte[] Attachment { get; set; }
        public string VendorName { get; set; }
        public string DeptName { get; set; }
        public string PSNNO { get; set; }
        public string Work_Order_NO { get; set; }
        public Nullable<decimal> GrossAmount { get; set; }
        public Nullable<decimal> TotalTaxAmount { get; set; }
        public Nullable<decimal> Status { get; set; }
    }

    public class BillInwardsParticularDetail
    {
        public int Bill_PD_Id { get; set; }
        public int Bill_Id { get; set; }
        public string AccountCode { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Quanity { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public string Unit { get; set; }
        public Nullable<decimal> Percentage { get; set; }
        public string Description { get; set; }
    }
    public class AccountingPosting
    {
        public int Accountposting_Id { get; set; }
        public int Form_id { get; set; }
        public int Acc_id { get; set; }
        public Nullable<int> RefDocDetails { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public string Accountposting1 { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string ACC_Code { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string Description { get; set; }

    }

    public class AccountingPostingID
    {
        public int Accountposting_Id { get; set; }       

    }
    public class FormDetails
    {
        public int Form_Id { get; set; }
        public string Form_Name { get; set; }
        public Nullable<int> DocTypeId { get; set; }
    }


    public class CombineBillInwards
    {
        public BillINWARDs billINWARDs { get; set; }
        public List<BillInwardsParticularDetail> billInwardsParticularDetails { get; set; }
        public List<AccountingPosting> accountingPostings { get; set; }
    }
    public class WorkOrderRegister
    {
        public int Form12_Id { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string ProjectName { get; set; }
        public string Work_Order_No { get; set; }
        public string Ref_WIP_Register { get; set; }
        public Nullable<decimal> Estimated_Cost { get; set; }
        public Nullable<decimal> Tender_Cost { get; set; }
        public Nullable<int> VendorNameId { get; set; }
        public Nullable<decimal> Sanctioned_Tender_Rates { get; set; }
        public string Sanctioned_Year { get; set; }
        public Nullable<System.DateTime> Planned_completion_date { get; set; }
        public string Total_Work_value { get; set; }
        public string EMD_Receipt_No { get; set; }
        public Nullable<decimal> EMD_Amount { get; set; }
        public string SD_Receipt_No { get; set; }
        public Nullable<System.DateTime> Actual_date_completion { get; set; }
        public string Completion_certificate_No { get; set; }
        public string Remark { get; set; }
        public byte[] Signature { get; set; }
        public Nullable<decimal> SD_Amount { get; set; }
        public string VendorName { get; set; }
    }
    public class WorkOrderRegisterList
    {       
        public string Work_Order_No { get; set; }
       
    }

    public class VendorMaster
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
        public string FunctionCode { get; set; }
        public string ObjectCode { get; set; }
        public ChartOfAccount chartOfAccount { get; set; }
    }

    public class ChartOfAccount
    {
        public int Acc_Id { get; set; }
        public string ACC_Code { get; set; }
        public string Narration { get; set; }
        public string AccountType { get; set; }
        public Nullable<int> AccTypeId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string subgroup { get; set; }
        public string GroupName { get; set; }
        public Nullable<int> Acc_subtype_Id { get; set; }
        public string FunctionCode { get; set; }
        public string ObjectCode { get; set; }
        public string FunctionCodeDescription { get; set; }
        public string ObjectCodeDescription { get; set; }
    }



    public class AccountType
    {
        public int AccTypeId { get; set; }
        public string AccountType1 { get; set; }
    }
    public class UserMSTR
    {
        public int User_Id { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<int> Branch_Id { get; set; }
        public Nullable<int> Role_Id { get; set; }
    }

    public class Form84ParticularDetails
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
    public class Form84Details
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
    public class CombineForm84
    {
        public Form84ParticularDetails form84ParticularDetails { get; set; }
        public Form84Details form84Details { get; set; }
    }

    public class FORM64PAYMENTVOUCHER
    {
        public int Form64_Id { get; set; }
        public Nullable<int> Form_id { get; set; }
        public Nullable<int> Bill_Id { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Bill_No { get; set; }
        public string Bill_Details { get; set; }
        public string NamePayeeVendorContractor { get; set; }
        public Nullable<int> Form12_Id { get; set; }
        public Nullable<decimal> First_Advance { get; set; }
        public Nullable<decimal> Running_Amount { get; set; }
        public Nullable<decimal> Final_Amount { get; set; }
        public Nullable<decimal> GrossAmtPayable { get; set; }
        public Nullable<decimal> TotalDeduction { get; set; }
        public Nullable<decimal> Net_AmtPayable { get; set; }
        public string Amt_inwords { get; set; }
        public string Budget_provision { get; set; }
        public string Budget_code { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> BillPaid_tillnow { get; set; }
        public string Measurementbookreference { get; set; }
        public string No_of_stock { get; set; }
        public string current_billamt { get; set; }
        public string Book_No { get; set; }
        public Nullable<decimal> passed_paymentamt { get; set; }
        public string payment_inwords { get; set; }
        public string Work_Order_No { get; set; }
        public string Bank { get; set; }
        public string ChequeNo { get; set; }
        public string PSN { get; set; }
    }

    public class FORM64PARTICULARDETAILS
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
    }
    public class FORM64Deduction
    {
        public int FORM64_DEDUCTIONID { get; set; }
        public Nullable<int> Form64_Id { get; set; }
        public string Narration { get; set; }
        public string Accountcode { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Description { get; set; }
    }

    public class CombinePaymentVoucher
    {
        public FORM64PAYMENTVOUCHER form64PAYMENTVOUCHER { get; set; }
        public List<FORM64PARTICULARDETAILS> form64PARTICULARDETAILs { get; set; }
        public List<AccountingPosting> accountingPostings { get; set; }
        public List<FORM64Deduction> fORM64Deductions { get; set; }
     }

    public class SelectPaymentVoucher
    {
        public int Form64_Id { get; set; }
        public Nullable<int> Form_id { get; set; }
        public Nullable<int> Bill_Id { get; set; }
        public string DepartmentName { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Bill_No { get; set; }
        public string Bill_Details { get; set; }
        public string NamePayeeVendorContractor { get; set; }
        public Nullable<int> Form12_Id { get; set; }
        public Nullable<decimal> First_Advance { get; set; }
        public Nullable<decimal> Running_Amount { get; set; }
        public Nullable<decimal> Final_Amount { get; set; }
        public Nullable<decimal> GrossAmtPayable { get; set; }
        public Nullable<decimal> TotalDeduction { get; set; }
        public Nullable<decimal> Net_AmtPayable { get; set; }
        public string Amt_inwords { get; set; }
        public string Budget_provision { get; set; }
        public string Budget_code { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public Nullable<decimal> BillPaid_tillnow { get; set; }
        public string Measurementbookreference { get; set; }
        public string No_of_stock { get; set; }
        public string current_billamt { get; set; }
        public string Book_No { get; set; }
        public Nullable<decimal> passed_paymentamt { get; set; }
        public string payment_inwords { get; set; }
        public string Work_Order_No { get; set; }
        public string Bank { get; set; }
        public string ChequeNo { get; set; }
        public string PSN { get; set; }

        public List<FORM64PARTICULARDETAILS> fORM64PARTICULARDETAILs = new List<FORM64PARTICULARDETAILS>();
        public List<FORM64Deduction> fORM64Deductions { get; set; }
    }

    public class AccountSubtype
    {
        public int Acc_subtype_Id { get; set; }
        public int AccTypeId { get; set; }
        public string NameSubType { get; set; }
    }

    public class SelectBillInward
    {
        public int Bill_Id { get; set; }
        public Nullable<int> Form_id { get; set; }
        public Nullable<decimal> NetAmountPayble { get; set; }
        public Nullable<System.DateTime> BillDate { get; set; }
        public byte[] Attachment { get; set; }
        public string VendorName { get; set; }
        public string DeptName { get; set; }
        public string PSNNO { get; set; }
        public string Work_Order_NO { get; set; }
        public Nullable<decimal> GrossAmount { get; set; }
        public Nullable<decimal> TotalTaxAmount { get; set; }

        public List<BillInwardsParticularDetail> billInwardsParticulardetails { get; set; }
    }
    public class JournalVoucher
    {
        public int JV_Id { get; set; }
        public string AccountCode { get; set; }
        public Nullable<System.DateTime> JVDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string AccountType { get; set; }
        public string Narration { get; set; }
        public Nullable<int> Acc_subtype_Id { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
    }
    public class CombineJournalVoucher
    {
        public JournalVoucher journalVoucher { get; set; }
        public List<AccountingPosting> accountingPostings { get; set; }
    }

    public class GETJournalVoucher
    {
        public int JV_Id { get; set; }
        public string AccountCode { get; set; }
        public Nullable<System.DateTime> JVDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string AccountType { get; set; }
        public string Narration { get; set; }
        public Nullable<int> Acc_subtype_Id { get; set; }
        public string VoucherNo { get; set; }
        public string Description { get; set; }
        public List<AccountingPosting> accountPostings { get; set; }
    }

    public class TDSREPORT
    {
        public int Accountposting_Id { get; set; }
        public int Form_id { get; set; }
        public int Acc_id { get; set; }
        public Nullable<int> RefDocDetails { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public string Accountposting1 { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string ACC_Code { get; set; }
        public string Narration { get; set; }
        public int Form64_Id { get; set; }
        public string NamePayeeVendorContractor { get; set; }
    }
    public class AccountingPosting1
    {
        public int Accountposting_Id { get; set; }
        public int Form_id { get; set; }
        public int Acc_id { get; set; }
        public Nullable<int> RefDocDetails { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public string Accountposting1 { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string ACC_Code { get; set; }
        public string Narration { get; set; }

        public string Description { get; set; }
    }

    public class AMOUNT
    {
        public Nullable<decimal> Amount1 { get; set; }
        public Nullable<decimal> Amount2 { get; set; }
        public string Accountposting1 { get; set; }
        public string ACC_Code { get; set; }
        public string Narration { get; set; }
    }
    public class PostingCalculation
    {
        public string ACC_Code { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public string POSTING { get; set; }

    }
    public class addAmount
    {
        public Nullable<decimal> AmountCR { get; set; }
        public Nullable<decimal> AmountDR { get; set; }
        public List<PostingCalculation> postingCalculations { get; set; }
        public List<AccountingPosting1> accountingPosting1s { get; set; }
        public List<AMOUNT> aMOUNTs { get; set; }
    }
    public class BILLIDINWARDS
    {
        public List<BillInwardsParticularDetail> billInwardsParticularDetails { get; set; }
        public Nullable<decimal> NetAmountPayble { get; set; }
    }

    public class SelectPaymentVoucher11
    {
        public int Form64_Id { get; set; }

        public Nullable<System.DateTime> Date { get; set; }

        public string NamePayeeVendorContractor { get; set; }


        public Nullable<decimal> GrossAmtPayable { get; set; }
        public Nullable<decimal> TotalDeduction { get; set; }
        public Nullable<decimal> Net_AmtPayable { get; set; }

        public int Form64_ParticularDetailID { get; set; }
        public string Narration { get; set; }
        public string Accountcode { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Description { get; set; }
        public string GSTINNO { get; set; }
        public Nullable<int> PSNNO { get; set; }
        public Nullable<decimal> TDSAmount { get; set; }
        public Nullable<decimal> SGSTAmount { get; set; }
        public Nullable<decimal> CGSTAmount { get; set; }
        public Nullable<decimal> IGSTAmount { get; set; }
        public Nullable<decimal> SDAmount { get; set; }
        public Nullable<decimal> BIMAAmount { get; set; }
        public Nullable<decimal> ROYALTYAmount { get; set; }
    }
    public class sesseionmstr
    {
        public int YEARID { get; set; }
        public string Session { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    }
    public class ACCOUNTPOSTINGFORCASHBOOK
    {
        public int Accountposting_Id { get; set; }
        public int Form_id { get; set; }
        public int Acc_id { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public string Accountposting1 { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> RefDocDetails { get; set; }
        public Nullable<int> Acc_subtype_Id { get; set; }
        public string Particular { get; set; }
        public string AccountCode { get; set; }
        public string VendorName { get; set; }
        public Nullable<int> PSNNO { get; set; }
        public string ChequeNo { get; set; }
        public string Description { get; set; }

    }

    public class CompanyMSTR
    {
        public int CompanyId { get; set; }
        public string Comp_code { get; set; }
        public string Company_Name { get; set; }
        public string Address { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string States { get; set; }
        public string EMail { get; set; }
        public string Mobile_number { get; set; }
        public string Pancard { get; set; }
        public string GST_Number { get; set; }
        public string Company_Type { get; set; }
        public Nullable<System.DateTime> Company_Formation_Date { get; set; }
        public string CIN_Number { get; set; }
        public Nullable<System.DateTime> ValidTo { get; set; }
        public Nullable<System.DateTime> Validfrom { get; set; }
        public Nullable<int> No_User_Allowed { get; set; }
        public string Status { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role_assign { get; set; }
        public byte[] Image { get; set; }
    }
    public class AccountingPostingDR
    {
        public Nullable<decimal> AmountDR { get; set; }

    }
    public class AccountingPostingCR
    {
        public Nullable<decimal> AmountCR { get; set; }

    }
    public class AccountingPostingsReport
    {
        public string accountposting { get; set; }
        public Nullable<decimal> Balance { get; set; }
        public List<AccountingPosting> accountingPostings { get; set; }
    }
    public class AccountingPostingOpeningbalance
    {
        public int OPBID { get; set; }
        public string YearId { get; set; }
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string Remarks { get; set; }
        public Nullable<decimal> TotalDr { get; set; }
        public Nullable<decimal> TotalCr { get; set; }
        public List<AccountingPosting> accountingPostings { get; set;}
    }

    public class OpeningBalance
    {
        public int OPBID { get; set; }
       
        public Nullable<decimal> TotalDr { get; set; }
        public Nullable<decimal> TotalCr { get; set; }
    }

    public class AccountpostingReport
    {
        public int Accountposting_Id { get; set; }
        public int Form_id { get; set; }
        public int Acc_id { get; set; }
        public Nullable<int> RefDocDetails { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public string Accountposting1 { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string ACC_Code { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> Balance { get; set; }

    }

    public class Accounts
    {      

        public List<AccountingPosting> accountingPostings { get; set;}
        public List<AccountingPosting> OpeningBalance { get; set; }
    }
    public class AccountingPostingLedgerReport
    {
       
        public Nullable<decimal> Amount1 { get; set; }
        public Nullable<decimal> Amount2 { get; set; }
        public int Accountposting_Id { get; set; }
        public int Form_id { get; set; }
        public int Acc_id { get; set; }
        public Nullable<int> RefDocDetails { get; set; }
        public Nullable<System.DateTime> PostingDate { get; set; }
        public string Accountposting1 { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string ACC_Code { get; set; }
        public string Narration { get; set; }
        public Nullable<decimal> Balance { get; set; }

    }

    public class UserMstr
    {
        public int User_Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ContactNo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleAssign { get; set; }
        public string Status { get; set; }
        public byte[] Image { get; set; }
        public string Token { get; set; }
    }
    public class Departmentmaster
    {
        public int DepId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string DepartmentName { get; set; }
        public string AuthorizePersonName { get; set; }
        public Nullable<decimal> Status { get; set; }
    }

}