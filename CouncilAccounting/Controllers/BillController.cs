﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CouncilAccounting.ViewModel;
using CouncilAccounting.Models;
using System.Web.Http.Cors;
using System.Timers;
using System.Globalization;

namespace CouncilAccounting.Controllers
{
    
  //[BasicAuthentication]
   //  [EnableCors(origins: "https://parseoni.councilaccounting.in", headers: "*", methods: "*")]
     //[EnableCors(origins: "https://warora.councilaccounting.in", headers: "*", methods: "*")]

   // [EnableCors(origins: "https://councilaccounting.in", headers: "*", methods: "*")]
    //  [EnableCors(origins: "https://wanadongri.councilaccounting.in", headers: "*", methods: "*")]
    [EnableCors(origins: "https://warora.councilaccounting.in", headers: "*", methods: "*")]
  //  [EnableCors(origins: "https://parseoni1.councilaccounting.in", headers: "*", methods: "*")]

    public class BillController : ApiController
    {
        //CouncilAccountingEntities db = new CouncilAccountingEntities();
        // AccountingDBEntities db = new AccountingDBEntities();
         waroraEntities db = new waroraEntities();
        // municipalcouncil3Entities db = new municipalcouncil3Entities();
        // municipalcouncil2Entities db = new municipalcouncil2Entities();
        // wanadongriEntities db = new wanadongriEntities();
        // parseoniEntities db = new parseoniEntities();
        // municipalcouncilEntities db = new municipalcouncilEntities();
       // ParseoniDBEntities db = new ParseoniDBEntities();
       // localhostCAEntities db = new localhostCAEntities();
        //----------------------------FOR-LOGIN----------------------//



        //[System.Web.Http.HttpPost]
        ////[BasicAuthentication]
        //public object Login(string username,string Status,string Token)
        //{
        //    try
        //    {
        //        var usermstr = db.USER_MSTR.FirstOrDefault(a => a.Username == username);
        //        var date = DateTime.Now;
        //        date = date.AddYears(-1);
        //        var sessionmstr = db.SESSION_MSTR.FirstOrDefault(a =>a.StartDate >= date );
        //        if (usermstr == null)
        //        {
        //            return "Invalid Username or Password";
        //        }
        //        else
        //        {
        //            var model = new UserMstr
        //            {
        //                User_Id = usermstr.User_Id,
        //                Address = usermstr.Address,
        //                Name = usermstr.Name,
        //                Email = usermstr.Email,
        //                State = usermstr.State,
        //                City = usermstr.City,
        //                ContactNo = usermstr.ContactNo,
        //                Username = usermstr.Username,
        //                Password = usermstr.Password,
        //                RoleAssign = usermstr.RoleAssign,
        //                Status = usermstr.Status,
        //                Image = usermstr.Image,
        //                Session = sessionmstr.Session,
        //            };
        //            return model;
        //        }
        //        // return "Login Succsesfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
        //    }
        //}


        //----------------------------FOR-SessionDrodown----------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public object SessionmasterList()
        {
            try
            {
                var Sessionmaster = db.SESSION_MSTR.Select(a => a.Session).ToList();
                return Sessionmaster;
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        //----------------------------Save-Update-Delete-for VendorMaster----------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<OBJECTCODE> OBJECTCODEslistForVendor()
        {
            try
            {
                List<OBJECTCODE> oBJECTCODEslist = new List<OBJECTCODE>();
                var ObjectCode = db.OBJECTCODEs.Where(a => a.Accounttype == "Liability" && a.Objectcode1 == "3600");
                foreach (var i in ObjectCode)
                {
                    var model = new OBJECTCODE
                    {
                        Objectcode1 = i.Objectcode1,
                        ObjectDescription = i.ObjectDescription,
                    };
                    oBJECTCODEslist.Add(model);
                }
                return oBJECTCODEslist;
            }
            catch (Exception ex)
            {

                return new List<OBJECTCODE>();
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string saveVendorMaster(VendorMaster model)
        {
            try
            {
                //var currentDate = new DateTime();
                //try
                //{
                //    Convert.ToDateTime("31-01-2019");
                //    currentDate = Convert.ToDateTime(model.Date);

                //}
                //catch (Exception ex)
                //{

                //    throw;
                //}
                if (model.Vendor_Id <= 0)
                {
                    var chartofaccount1 = db.CHARTOFACCOUNTs.Where(a=>a.Narration == model.VendorName).ToList();
                    //var chartofaccount1 = chartofaccount.Where(a => a.Narration == model.Narration).ToList();
                    if (chartofaccount1.Count > 0)
                    {
                        return "Vendor Name Already exist";
                    }

                    var savevendor = new VENDOR_MSTR
                    {
                        Vendor_Id = model.Vendor_Id,
                        VendorName = model.VendorName,
                        VendorAddress = model.VendorAddress,
                        City = model.City,
                        State = model.State,
                        PANNo = model.PANNo,
                        GISTINNo = model.GISTINNo,
                        VendorMasterCode = model.VendorMasterCode,
                        Date = model.Date,
                        Email = model.Email,
                        ContactNo = model.ContactNo,
                        Status = 0,
                        ObjectCode = model.ObjectCode,
                        FunctionCode = model.FunctionCode,
                    };
                    db.VENDOR_MSTR.Add(savevendor);
                    db.SaveChanges();
                    
                    var accountcode = model.FunctionCode + model.ObjectCode;
                    var chartofaccount = db.CHARTOFACCOUNTs.Where(a => a.FunctionCode == model.FunctionCode && a.ObjectCode == model.ObjectCode).ToList();
                    var chartofaccountacccode1 = "";
                    if (chartofaccount.Count > 0)
                    {
                        var chartofaccountacccode = chartofaccount.Max(a => a.ACC_Code);
                        decimal? acccode = 0;
                        acccode = Convert.ToDecimal(chartofaccountacccode);
                        acccode = acccode + 1;
                        chartofaccountacccode1 = Convert.ToString(acccode);
                        var number = (int)(Math.Log10((double)acccode) + 1);

                        if (number < 11)
                        {
                            number = 11 - number;
                            if (number == 1)
                            {
                                chartofaccountacccode1 = "0" + chartofaccountacccode1;
                            }
                            if (number == 2)
                            {
                                chartofaccountacccode1 = "0" + chartofaccountacccode1;
                            }
                        }
                    }
                    else
                    {
                        chartofaccountacccode1 = model.FunctionCode + model.ObjectCode + "0001";
                    }

                    var FunctionCode = db.FUNCTIONCODEs.FirstOrDefault(a => a.FunctionCode1 == model.FunctionCode);
                    var ObjecctCode = db.OBJECTCODEs.FirstOrDefault(a => a.Objectcode1 == model.ObjectCode);
                    var savechartofaccount = new CHARTOFACCOUNT
                    {
                        ACC_Code = chartofaccountacccode1,
                        Narration = model.VendorName,
                        AccountType = "Liability",
                        AccTypeId = 2,
                        Date = model.Date,
                        ObjectCode = model.ObjectCode,
                        FunctionCode = model.FunctionCode,
                        FunctionCodeDescription = FunctionCode.Description,
                        GroupName = ObjecctCode.Majorhead,
                        subgroup = ObjecctCode.Minorhead,
                        ObjectCodeDescription = ObjecctCode.ObjectDescription,
                        Acc_subtype_Id = savevendor.Vendor_Id,
                    };
                    db.CHARTOFACCOUNTs.Add(savechartofaccount);
                    db.SaveChanges();
                    return "save";
                }
                else
                {
                    var vendormasters = db.VENDOR_MSTR.FirstOrDefault(a => a.Vendor_Id == model.Vendor_Id);
                   
                    if (vendormasters != null)
                    {
                       
                            //if (vendormasters.Status == 0)
                            //{
                            //    vendormasters.VendorName = model.VendorName;
                            //    vendormasters.FunctionCode = model.FunctionCode;
                            //    vendormasters.ObjectCode = model.ObjectCode;
                            //}
                       
                       

                        vendormasters.VendorAddress = model.VendorAddress;
                        vendormasters.City = model.City;
                        vendormasters.State = model.State;
                        vendormasters.PANNo = model.PANNo;
                        vendormasters.GISTINNo = model.GISTINNo;
                        vendormasters.VendorMasterCode = model.VendorMasterCode;
                        vendormasters.Date = model.Date;
                        vendormasters.Email = model.Email;
                        vendormasters.ContactNo = model.ContactNo;
                        db.SaveChanges();
                        return "Done";
                        
                    }
                    return "error";

                }
                return "error";
            }
            catch (Exception ex)
            {
                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }
        [System.Web.Http.HttpPost]
        ////[BasicAuthentication]
        public List<VendorMaster> VendorList()
        {
            try
            {
                List<VendorMaster> vendorMastersList = new List<VendorMaster>();
                var vendormaster = db.VENDOR_MSTR.ToList();
                foreach (var i in vendormaster)
                {
                    var model = new VendorMaster
                    {
                        Vendor_Id = i.Vendor_Id,
                        VendorName = i.VendorName,
                        VendorAddress = i.VendorAddress,
                        City = i.City,
                        State = i.State,
                        PANNo = i.PANNo,
                        GISTINNo = i.GISTINNo,
                        VendorMasterCode = i.VendorMasterCode,
                        Date = i.Date,
                        Email = i.Email,
                        ContactNo = i.ContactNo,
                        ObjectCode = i.ObjectCode,
                        FunctionCode = i.FunctionCode,
                    };
                    vendorMastersList.Add(model);
                }
                return vendorMastersList;
            }
            catch (Exception ex)
            {

                return new List<VendorMaster>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<VendorMaster> VendorListBySession(string Session)
        {
            try
            {
                List<VendorMaster> vendorMastersList = new List<VendorMaster>();
                var Sessionmaster = db.SESSION_MSTR.FirstOrDefault(a => a.Session == Session);
                var vendormaster = db.VENDOR_MSTR.Where(a=>a.Date >= Sessionmaster.StartDate && a.Date <= Sessionmaster.EndDate).ToList();
                foreach (var i in vendormaster)
                {
                    var model = new VendorMaster
                    {
                        Vendor_Id = i.Vendor_Id,
                        VendorName = i.VendorName,
                        VendorAddress = i.VendorAddress,
                        City = i.City,
                        State = i.State,
                        PANNo = i.PANNo,
                        GISTINNo = i.GISTINNo,
                        VendorMasterCode = i.VendorMasterCode,
                        Date = i.Date,
                        Email = i.Email,
                        ContactNo = i.ContactNo,
                        ObjectCode = i.ObjectCode,
                        FunctionCode = i.FunctionCode,
                    };
                    vendorMastersList.Add(model);
                }
                return vendorMastersList;
            }
            catch (Exception ex)
            {

                return new List<VendorMaster>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<VendorMaster> SelectVendorList(int VendorId)
        {
            try
            {
                List<VendorMaster> VendorList = new List<VendorMaster>();
                var VendorMSTR = db.VENDOR_MSTR.FirstOrDefault(a => a.Vendor_Id == VendorId);
                if (VendorMSTR != null)
                {
                    var model = new VendorMaster
                    {
                        Vendor_Id = VendorMSTR.Vendor_Id,
                        VendorName = VendorMSTR.VendorName,
                        VendorAddress = VendorMSTR.VendorAddress,
                        City = VendorMSTR.City,
                        State = VendorMSTR.State,
                        PANNo = VendorMSTR.PANNo,
                        GISTINNo = VendorMSTR.GISTINNo,
                        VendorMasterCode = VendorMSTR.VendorMasterCode,
                        Date = VendorMSTR.Date,
                        Email = VendorMSTR.Email,
                        ContactNo = VendorMSTR.ContactNo,
                        FunctionCode = VendorMSTR.FunctionCode,
                        ObjectCode = VendorMSTR.ObjectCode,
                    };
                    VendorList.Add(model);
                }
                return VendorList;
            }
            catch (Exception ex)
            {

                return new List<VendorMaster>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string DeleteVendorMSTR(int VendorId)
        {
            try
            {
                var VendorMSTR = db.VENDOR_MSTR.FirstOrDefault(a => a.Vendor_Id == VendorId);
                var Chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_subtype_Id == VendorMSTR.Vendor_Id);
                
                if (VendorMSTR != null)
                {
                    if (VendorMSTR.Status == 0)
                    {
                        if(Chartofaccount != null)
                        {
                            db.CHARTOFACCOUNTs.Remove(Chartofaccount);
                            db.SaveChanges();
                        }
                        db.VENDOR_MSTR.Remove(VendorMSTR);
                        db.SaveChanges();
                        return "Deleted SuccessFully";
                    }
                    return "Vendor is in used";
                }
                return "Not Deleted";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }



        //----------------------------Save-Update-Delete-for WORKORDERREGISTER----------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]

        public string SaveWorkOrderRegister(WorkOrderRegister model)
        {
            try
            {
                //var completion_date = new DateTime();
                //var date_completion = new DateTime();
                //var sanctioned_Year = new DateTime();
                //try
                //{
                //    Convert.ToDateTime("31-01-2019");
                //    completion_date = Convert.ToDateTime(model.Planned_completion_date);
                //    date_completion = Convert.ToDateTime(model.Actual_date_completion);
                //    sanctioned_Year = Convert.ToDateTime(model.Sanctioned_Year);
                //}
                //catch (Exception ex)
                //{

                //    throw;
                //}
                if (model.Form12_Id <= 0)
                {
                    var workorder = db.FORM12_WO_REGISTER.FirstOrDefault(a => a.Work_Order_No == model.Work_Order_No);
                    if(workorder != null)
                    {
                        return "Workorder no already created";
                    }
                    var saveworkorderregister = new Models.FORM12_WO_REGISTER
                    {
                        Form12_Id = model.Form12_Id,
                        Date = model.Date,
                        ProjectName = model.ProjectName,
                        Work_Order_No = model.Work_Order_No,
                        Ref_WIP_Register = model.Ref_WIP_Register,
                        Estimated_Cost = model.Estimated_Cost,
                        Tender_Cost = model.Tender_Cost,
                        VendorNameId = model.VendorNameId,
                        Sanctioned_Tender_Rates = model.Sanctioned_Tender_Rates,
                        Sanctioned_Year = model.Sanctioned_Year,
                        Planned_completion_date = model.Planned_completion_date,
                        Total_Work_value = model.Total_Work_value,
                        EMD_Receipt_No = model.EMD_Receipt_No,
                        EMD_Amount = model.EMD_Amount,
                        SD_Receipt_No = model.SD_Receipt_No,
                        Actual_date_completion = model.Actual_date_completion,
                        Completion_certificate_No = model.Completion_certificate_No,
                        Remark = model.Remark,
                        Signature = model.Signature,
                        SD_Amount = model.SD_Amount,
                        Status = 0,
                    };
                    db.FORM12_WO_REGISTER.Add(saveworkorderregister);
                    db.SaveChanges();
                    var vendorMaster = db.VENDOR_MSTR.FirstOrDefault(a => a.Vendor_Id == model.VendorNameId);
                    if(vendorMaster != null)
                    {
                        vendorMaster.Status = vendorMaster.Status + 1;
                        db.SaveChanges();
                    }
                    return "Save";
                }
                else
                {
                    var OldWorkOrder = db.FORM12_WO_REGISTER.FirstOrDefault(a => a.Form12_Id == model.Form12_Id);
                    
                    if (OldWorkOrder != null)
                    {
                       
                        if(OldWorkOrder.Status == 0)
                        {
                            var workorderlist = db.FORM12_WO_REGISTER.Where(a => a.Form12_Id != model.Form12_Id).ToList();
                            var workorders = workorderlist.FirstOrDefault(a => a.Work_Order_No == model.Work_Order_No);
                            if(workorders != null)
                            {
                                return "Workorder no already created";
                            }
                            OldWorkOrder.ProjectName = model.ProjectName;
                            OldWorkOrder.Work_Order_No = model.Work_Order_No;
                            OldWorkOrder.Ref_WIP_Register = model.Ref_WIP_Register;
                            OldWorkOrder.Estimated_Cost = model.Estimated_Cost;
                            OldWorkOrder.Tender_Cost = model.Tender_Cost;
                            OldWorkOrder.VendorNameId = model.VendorNameId;
                            OldWorkOrder.Sanctioned_Tender_Rates = model.Sanctioned_Tender_Rates;
                            OldWorkOrder.Sanctioned_Year = model.Sanctioned_Year;
                            OldWorkOrder.Planned_completion_date = model.Planned_completion_date;
                            OldWorkOrder.Total_Work_value = model.Total_Work_value;
                            OldWorkOrder.EMD_Receipt_No = model.EMD_Receipt_No;
                            OldWorkOrder.EMD_Amount = model.EMD_Amount;
                            OldWorkOrder.SD_Receipt_No = model.SD_Receipt_No;
                            OldWorkOrder.Actual_date_completion = model.Actual_date_completion;
                            OldWorkOrder.Completion_certificate_No = model.Completion_certificate_No;
                            OldWorkOrder.Remark = model.Remark;
                            OldWorkOrder.Signature = model.Signature;
                            OldWorkOrder.SD_Amount = model.SD_Amount;
                            var OLDvendormaster = db.VENDOR_MSTR.FirstOrDefault(a => a.Vendor_Id == OldWorkOrder.VendorNameId);
                            var NEWvendormaster = db.VENDOR_MSTR.FirstOrDefault(a => a.Vendor_Id == model.VendorNameId);
                            if (OLDvendormaster != null)
                            {
                                if(OLDvendormaster.Status > 0)
                                {
                                    OLDvendormaster.Status = OLDvendormaster.Status - 1;
                                    db.SaveChanges();
                                }
                               
                            }
                            if (NEWvendormaster != null)
                            {                               
                                 NEWvendormaster.Status = NEWvendormaster.Status + 1;
                                 db.SaveChanges();  
                            }
                            db.SaveChanges();
                            return "Done";
                        }
                        else
                        {
                            return "Vendor Name is used in another form";
                        }
                    }
                }
                return "Error";

            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message; 
            }

        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<WorkOrderRegister> WorkorderRegisterList()
        {
            try
            {
                List<WorkOrderRegister> workOrderRegistersList = new List<WorkOrderRegister>();
                var workOrder = db.FORM12_WO_REGISTER.ToList();
                foreach (var i in workOrder)
                {

                    var vendorMstr = db.VENDOR_MSTR.FirstOrDefault(a => a.Vendor_Id == i.VendorNameId);
                    var model = new WorkOrderRegister
                    {
                        Form12_Id = i.Form12_Id,
                        Date = i.Date,
                        ProjectName = i.ProjectName,
                        Work_Order_No = i.Work_Order_No,
                        Ref_WIP_Register = i.Ref_WIP_Register,
                        Estimated_Cost = i.Estimated_Cost,
                        Tender_Cost = i.Tender_Cost,
                        VendorNameId = i.VendorNameId,
                        Sanctioned_Tender_Rates = i.Sanctioned_Tender_Rates,
                        Sanctioned_Year = i.Sanctioned_Year,
                        Planned_completion_date = i.Planned_completion_date,
                        Total_Work_value = i.Total_Work_value,
                        EMD_Receipt_No = i.EMD_Receipt_No,
                        SD_Receipt_No = i.SD_Receipt_No,
                        Actual_date_completion = i.Actual_date_completion,
                        Completion_certificate_No = i.Completion_certificate_No,
                        Remark = i.Remark,
                        Signature = i.Signature,
                        EMD_Amount = i.EMD_Amount,
                        SD_Amount = i.SD_Amount,
                        VendorName = vendorMstr.VendorName,
                    };
                    workOrderRegistersList.Add(model);
                }
                return workOrderRegistersList;
            }
            catch (Exception ex)
            {

                return new List<WorkOrderRegister>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<WorkOrderRegister> WorkorderRegisterListBysession(string Session)
        {
            try
            {
                List<WorkOrderRegister> workOrderRegistersList = new List<WorkOrderRegister>();
                var sessionmaster = db.SESSION_MSTR.FirstOrDefault(a => a.Session == Session);
                var workOrder = db.FORM12_WO_REGISTER.Where(a=>a.Date >= sessionmaster.StartDate && a.Date <= sessionmaster.EndDate).ToList();
                if(workOrder!= null)
                {
                    foreach (var i in workOrder)
                    {

                        var vendorMstr = db.VENDOR_MSTR.FirstOrDefault(a => a.Vendor_Id == i.VendorNameId);
                        var VendorName = "";
                        if(vendorMstr != null)
                        {
                            VendorName = vendorMstr.VendorName;
                        }
                        var model = new WorkOrderRegister
                        {
                            Form12_Id = i.Form12_Id,
                            Date = i.Date,
                            ProjectName = i.ProjectName,
                            Work_Order_No = i.Work_Order_No,
                            Ref_WIP_Register = i.Ref_WIP_Register,
                            Estimated_Cost = i.Estimated_Cost,
                            Tender_Cost = i.Tender_Cost,
                            VendorNameId = i.VendorNameId,
                            Sanctioned_Tender_Rates = i.Sanctioned_Tender_Rates,
                            Sanctioned_Year = i.Sanctioned_Year,
                            Planned_completion_date = i.Planned_completion_date,
                            Total_Work_value = i.Total_Work_value,
                            EMD_Receipt_No = i.EMD_Receipt_No,
                            SD_Receipt_No = i.SD_Receipt_No,
                            Actual_date_completion = i.Actual_date_completion,
                            Completion_certificate_No = i.Completion_certificate_No,
                            Remark = i.Remark,
                            Signature = i.Signature,
                            EMD_Amount = i.EMD_Amount,
                            SD_Amount = i.SD_Amount,
                            VendorName = VendorName,
                        };
                        workOrderRegistersList.Add(model);
                    }
                   
                }
                return workOrderRegistersList;
            }
            catch (Exception ex)
            {

                return new List<WorkOrderRegister>();
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string DeleteWorkOrderRegister(int Form12Id)
        {
            try
            {
                var WorkOrderRegister = db.FORM12_WO_REGISTER.FirstOrDefault(a => a.Form12_Id == Form12Id);

                if (WorkOrderRegister != null)
                {
                    if(WorkOrderRegister.Status == 0)
                    { 
                    db.FORM12_WO_REGISTER.Remove(WorkOrderRegister);
                    db.SaveChanges();
                        var VendorMstr = db.VENDOR_MSTR.FirstOrDefault(a => a.Vendor_Id == WorkOrderRegister.VendorNameId);
                        if(VendorMstr != null)
                        {
                            if (VendorMstr.Status > 0)
                            {
                                VendorMstr.Status = VendorMstr.Status - 1;
                                db.SaveChanges();
                            }
                        }
                    return "Deleted SuccessFully";
                    }
                    else
                    {
                        return "WorkOrder Number is use in BILL OR Payment";
                    }
                }
                return "Not Deleted";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        public List<WorkOrderRegister> SelectWorkorderRegister(int Form12Id)
        {
            try
            {
                List<WorkOrderRegister> workOrderRegistersList = new List<WorkOrderRegister>();
                var Workorder = db.FORM12_WO_REGISTER.FirstOrDefault(a => a.Form12_Id == Form12Id);
                if (Workorder != null)
                {
                    var model = new WorkOrderRegister
                    {
                        Form12_Id = Workorder.Form12_Id,
                        Date = Workorder.Date,
                        ProjectName = Workorder.ProjectName,
                        Work_Order_No = Workorder.Work_Order_No,
                        Ref_WIP_Register = Workorder.Ref_WIP_Register,
                        Estimated_Cost = Workorder.Estimated_Cost,
                        Tender_Cost = Workorder.Tender_Cost,
                        VendorNameId = Workorder.VendorNameId,
                        Sanctioned_Tender_Rates = Workorder.Sanctioned_Tender_Rates,
                        Sanctioned_Year = Workorder.Sanctioned_Year,
                        Planned_completion_date = Workorder.Planned_completion_date,
                        Total_Work_value = Workorder.Total_Work_value,
                        EMD_Receipt_No = Workorder.EMD_Receipt_No,
                        SD_Receipt_No = Workorder.SD_Receipt_No,
                        Actual_date_completion = Workorder.Actual_date_completion,
                        Completion_certificate_No = Workorder.Completion_certificate_No,
                        EMD_Amount = Workorder.EMD_Amount,
                        Remark = Workorder.Remark,
                        Signature = Workorder.Signature,
                        SD_Amount = Workorder.SD_Amount,
                    };
                    workOrderRegistersList.Add(model);
                }
                return workOrderRegistersList;
            }
            catch (Exception ex)
            {

                return new List<WorkOrderRegister>();
            }
        }


        //----------------------------Save-Update-Delete-for CHARTOFACCOUNT----------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<FUNCTIONCODE> Functioncodelist()
        {
            try
            {
                List<FUNCTIONCODE> fUNCTIONCODEslist = new List<FUNCTIONCODE>();
                var functioncode = db.FUNCTIONCODEs.ToList();
                foreach(var i in functioncode)
                {
                    var model = new FUNCTIONCODE
                    {
                        FunctionCode1 = i.FunctionCode1,
                        Description = i.Description,
                    };
                    fUNCTIONCODEslist.Add(model);
                }
                return fUNCTIONCODEslist;
            }
            catch (Exception)
            {
                return new List<FUNCTIONCODE>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<OBJECTCODE> OBJECTCODEslist (string AccountType)
        {
            try
            {
                List<OBJECTCODE> oBJECTCODEslist = new List<OBJECTCODE>();
                var ObjectCode = db.OBJECTCODEs.Where(a => a.Accounttype == AccountType);
                foreach(var i in ObjectCode)
                {
                    var model = new OBJECTCODE
                    {
                        Objectcode1 = i.Objectcode1,
                        ObjectDescription = i.ObjectDescription,
                        Majorhead = i.Majorhead,
                        Minorhead = i.Minorhead,
                        DetailedHead = i.DetailedHead,
                    };
                    oBJECTCODEslist.Add(model);
                }
                return oBJECTCODEslist;
            }
            catch (Exception)
            {

                return new List<OBJECTCODE>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<ACCOUNT_SUBTYPE> SelectSubtypeofChartofAccount(string AccountType)
        {
            var accounttype = db.ACCOUNT_TYPE.FirstOrDefault(a => a.AccountType == AccountType);
            var subtype = db.ACCOUNT_SUBTYPE.Where(a => a.AccTypeId == accounttype.AccTypeId);
            List<ACCOUNT_SUBTYPE> aCCOUNTSUBTYPEsList = new List<ACCOUNT_SUBTYPE>();
            foreach (var i in subtype)
            {
                var model = new ACCOUNT_SUBTYPE
                {
                    Acc_subtype_Id = i.Acc_subtype_Id,
                    NameSubType = i.NameSubType,
                };
                aCCOUNTSUBTYPEsList.Add(model);
            }
            return aCCOUNTSUBTYPEsList;
        }

        //[System.Web.Http.HttpPost]
        ////[BasicAuthentication]
        //public string saveChartofAccount(ChartOfAccount model)
        //{
        //    try
        //    {

        //        var AccountType = db.ACCOUNT_TYPE.FirstOrDefault(a => a.AccountType == model.AccountType);
        //        //if (model.Acc_Id <= 0)
        //        //{
        //            var savechartofaccount = new CHARTOFACCOUNT
        //            {
        //                Acc_Id = model.Acc_Id,
        //                FunctionCode = model.FunctionCode,
        //                ObjectCode = model.ObjectCode,

        //                Narration = model.Narration,
        //                AccountType = model.AccountType,
        //                AccTypeId = AccountType.AccTypeId,
        //                Date = model.Date,
        //                subgroup = model.subgroup,
        //                GroupName = model.GroupName,
        //                Acc_subtype_Id = model.Acc_subtype_Id,

        //            };
        //            db.CHARTOFACCOUNTs.Add(savechartofaccount);
        //            db.SaveChanges();
        //            var chartofaccounT = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == savechartofaccount.Acc_Id);
        //            {
        //                var number = (int)(Math.Log10((double)savechartofaccount.Acc_Id) + 1);
        //                var count = 4 - number;
        //                if(count <= 0)
        //                {
        //                    var accountcode = model.FunctionCode + model.ObjectCode  + savechartofaccount.Acc_Id;
        //                    chartofaccounT.ACC_Code = accountcode;
        //                    db.SaveChanges();
        //                }
        //                else
        //                {
        //                string zero = "";
        //                    if (count == 1)
        //                    {
        //                        zero = "0";
        //                    }
        //                    if (count == 2)
        //                    {
        //                        zero = "00";
        //                    }
        //                    if (count == 3)
        //                    {
        //                        zero = "000";
        //                    }

        //                    var accountcode = model.FunctionCode + model.ObjectCode + zero + savechartofaccount.Acc_Id;
        //                    chartofaccounT.ACC_Code = accountcode;
        //                    db.SaveChanges();
        //                }

        //                //db.CHARTOFACCOUNTs.Add(savechartofaccount);

        //            }                    
        //            return "Save";
        //        //}
        //        //else
        //        //{
        //        //    if (model.Acc_Id > 8)
        //        //    {
        //        //        var OLDChartofAccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == model.Acc_Id);
        //        //        if (OLDChartofAccount != null)
        //        //        {
        //        //            OLDChartofAccount.ACC_Code = model.ACC_Code;
        //        //            OLDChartofAccount.Narration = model.Narration;
        //        //            OLDChartofAccount.AccountType = model.AccountType;
        //        //            OLDChartofAccount.AccTypeId = AccountType.AccTypeId;
        //        //            OLDChartofAccount.Date = model.Date;
        //        //            OLDChartofAccount.subgroup = model.subgroup;
        //        //            OLDChartofAccount.GroupName = model.GroupName;
        //        //            OLDChartofAccount.Acc_subtype_Id = model.Acc_subtype_Id;
        //        //            db.SaveChanges();
        //        //            return "Done";
        //        //        };
        //        //    }
        //        //}
        //        //return "Error";

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string saveChartofAccount(ChartOfAccount model)
        {
            try
            {

                var AccountType = db.ACCOUNT_TYPE.FirstOrDefault(a => a.AccountType == model.AccountType);
                var accountcode = model.FunctionCode + model.ObjectCode;
                var chartofaccount = db.CHARTOFACCOUNTs.Where(a => a.FunctionCode == model.FunctionCode && a.ObjectCode == model.ObjectCode).ToList();
                if(chartofaccount != null)
                {
                    var chartofaccount1 = db.CHARTOFACCOUNTs.Where(a =>a.Narration == model.Narration).ToList();
                    //var chartofaccount1 = chartofaccount.Where(a => a.Narration == model.Narration).ToList();
                    if (chartofaccount1.Count>0)
                    {
                        return "Ledger Name Already exist";
                    }
                }
                var chartofaccountacccode1 = "";
                if (chartofaccount.Count > 0)
                {
                    var chartofaccountacccode = chartofaccount.Max(a => a.ACC_Code);
                    decimal? acccode = 0;
                    acccode = Convert.ToDecimal(chartofaccountacccode);
                    acccode = acccode + 1;
                    chartofaccountacccode1 = Convert.ToString(acccode);
                    var number = (int)(Math.Log10((double)acccode) + 1);
                    
                    if(number < 11)
                    {
                        number = 11 - number;
                        if(number == 1)
                        {
                            chartofaccountacccode1 = "0" + chartofaccountacccode1;
                        }
                        if(number == 2)
                        {
                            chartofaccountacccode1 = "0" + chartofaccountacccode1;
                        }
                    }
                }
                else
                {
                    chartofaccountacccode1 = model.FunctionCode + model.ObjectCode + "0001";
                }
                var savechartofaccount = new CHARTOFACCOUNT
                {
                    Acc_Id = model.Acc_Id,
                    FunctionCode = model.FunctionCode,
                    ObjectCode = model.ObjectCode,
                    Narration = model.Narration,
                    AccountType = model.AccountType,
                    AccTypeId = AccountType.AccTypeId,
                    Date = model.Date,
                    subgroup = model.subgroup,
                    GroupName = model.GroupName,
                    //Acc_subtype_Id = model.Acc_subtype_Id,
                    ACC_Code = chartofaccountacccode1,
                    FunctionCodeDescription = model.FunctionCodeDescription,
                    ObjectCodeDescription = model.ObjectCodeDescription,

                };
                db.CHARTOFACCOUNTs.Add(savechartofaccount);
                db.SaveChanges();    
                return "Save";
                

            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        public List<ChartOfAccount> ChartOfAccountsList()
        {
            try
            {
                List<ChartOfAccount> chartOfAccountList = new List<ChartOfAccount>();
                var chartofaccount = db.CHARTOFACCOUNTs.Where(a => a.ObjectCode != "3670").ToList();
                foreach (var i in chartofaccount)
                {
                    var model = new ChartOfAccount
                    {
                        Acc_Id = i.Acc_Id,
                        ACC_Code = i.ACC_Code,
                        AccountType = i.AccountType,
                        Date = i.Date,
                        GroupName = i.GroupName,
                        Narration = i.Narration,
                        subgroup = i.subgroup,
                        //Acc_subtype_Id = i.Acc_subtype_Id,
                        FunctionCode = i.FunctionCode,
                        ObjectCode = i.ObjectCode,
                        FunctionCodeDescription = i.FunctionCodeDescription,
                        ObjectCodeDescription = i.ObjectCodeDescription,
                    };
                    chartOfAccountList.Add(model);
                }
                return chartOfAccountList;
            }
            catch (Exception ex)
            {

                return new List<ChartOfAccount>();
            }
        }

        public List<ChartOfAccount> SelectChartOfAccount(int AccId)
        {
            try
            {
                List<ChartOfAccount> chartOfAccountsList = new List<ChartOfAccount>();
                var ChartOfAccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == AccId);
                if (ChartOfAccount != null)
                {
                    var model = new ChartOfAccount
                    {
                        Acc_Id = ChartOfAccount.Acc_Id,
                        ACC_Code = ChartOfAccount.ACC_Code,
                        AccountType = ChartOfAccount.AccountType,
                        Date = ChartOfAccount.Date,
                        subgroup = ChartOfAccount.subgroup,
                        Narration = ChartOfAccount.Narration,
                        GroupName = ChartOfAccount.GroupName,
                        //Acc_subtype_Id = ChartOfAccount.Acc_subtype_Id,
                        FunctionCode = ChartOfAccount.FunctionCode,
                        ObjectCode = ChartOfAccount.ObjectCode,
                        FunctionCodeDescription = ChartOfAccount.FunctionCodeDescription,
                        ObjectCodeDescription = ChartOfAccount.ObjectCodeDescription,
                    };
                    chartOfAccountsList.Add(model);
                }
                return chartOfAccountsList;
            }
            catch (Exception ex)
            {

                return new List<ChartOfAccount>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string DeleteChartOfAccount(int AccId)
        {
            try
            {
                var chartofAccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == AccId);
                if (chartofAccount.Acc_Id > 8)
                {
                    if (chartofAccount != null)
                    {
                        db.CHARTOFACCOUNTs.Remove(chartofAccount);
                        db.SaveChanges();
                        return "Deleted Suucesfully";
                    }
                }
                return "Not Deleted";

            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        //public List<AccountType> AccountTypeList()
        //{
        //    try
        //    {
        //        List<AccountType> accountTypesList = new List<AccountType>();
        //        var accounttype = db.ACCOUNT_TYPE.ToList();
        //        foreach(var i in accounttype)
        //        {
        //            var model = new AccountType
        //            {
        //                AccountType1 = i.AccountType,
        //            };
        //            accountTypesList.Add(model);
        //        }
        //        return accountTypesList;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}




        //----------------------------Save--for ACCOUNTPOSTING & BILL INWARDS----------------------//

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<ChartOfAccount> SelectNarrationFORBILL()
        {
            try
            {
                var chartofaccont = db.CHARTOFACCOUNTs.Where(a => a.AccountType == "Liability" && a.Acc_subtype_Id == 9).ToList();
                List<ChartOfAccount> chartOfAccountslist = new List<ChartOfAccount>();
                foreach (var i in chartofaccont)
                {
                    var model = new ChartOfAccount
                    {
                        Narration = i.Narration,
                        ACC_Code = i.ACC_Code,
                    };
                    chartOfAccountslist.Add(model);
                }
                return chartOfAccountslist;
            }
            catch (Exception ex)
            {

                return new List<ChartOfAccount>();
            }
        }
        public List<ChartOfAccount> SelectNarrationFORBILLExpense()
        {
            try
            {
                var chartofaccont = db.CHARTOFACCOUNTs.Where(a =>a.Acc_subtype_Id != 9).ToList();
                List<ChartOfAccount> chartOfAccountslist = new List<ChartOfAccount>();
                foreach (var i in chartofaccont)
                {
                    var model = new ChartOfAccount
                    {
                        Narration = i.Narration,
                        ACC_Code = i.ACC_Code,
                        Acc_Id = i.Acc_Id,
                    };
                    chartOfAccountslist.Add(model);
                }
                return chartOfAccountslist;
            }
            catch (Exception ex)
            {

                return new List<ChartOfAccount>();
            }
        }


        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string SaveBillInwards(CombineBillInwards model)
        {
            try
            {
                if (model.billINWARDs.Work_Order_NO != null)
                {
                    var description = ""; 
                    //var billinward = db.BILL_INWARD.FirstOrDefault(a => a.PSNNO == model.billINWARDs.PSNNO);
                    //if (billinward != null)
                    //{
                    //    return "Already present PSNNO in BILL";
                    //}

                    var AccountPosting = "";
                    var formdetail = db.FORM_DETAILS.FirstOrDefault(a => a.Form_Name == "BILLFORM");
                    var billinwards = new BILL_INWARD
                    {
                        Form_id = formdetail.Form_Id,
                        NetAmountPayble = model.billINWARDs.NetAmountPayble,
                        BillDate = model.billINWARDs.BillDate,
                        Attachment = model.billINWARDs.Attachment,
                        VendorName = model.billINWARDs.VendorName,
                        DeptName = model.billINWARDs.DeptName,
                        PSNNO = model.billINWARDs.PSNNO,
                        Work_Order_NO = model.billINWARDs.Work_Order_NO,
                        GrossAmount = model.billINWARDs.GrossAmount,
                        TotalTaxAmount = model.billINWARDs.TotalTaxAmount,
                        Status = 0,
                    };
                    db.BILL_INWARD.Add(billinwards);
                    db.SaveChanges();
                    var BILLinward = db.BILL_INWARD.FirstOrDefault(a => a.PSNNO == model.billINWARDs.PSNNO);
                    //----------------------------update-status-of-form12----------------------//
                    var form12 = db.FORM12_WO_REGISTER.FirstOrDefault(a => a.Work_Order_No == model.billINWARDs.Work_Order_NO);
                    if (form12 != null)
                    {
                        form12.Status = form12.Status + 1;
                        db.SaveChanges();
                    }
                    var vendorMaster = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == model.billINWARDs.VendorName);
                    if (vendorMaster != null)
                    {
                        vendorMaster.Status = vendorMaster.Status + 1;
                        db.SaveChanges();
                    }
                    foreach (var i in model.billInwardsParticularDetails)
                    {
                        var Chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.ACC_Code == i.AccountCode);
                        var BillinwardsParticularDetail = new BILL_INWARD_PARTICULARDETAILS
                        {
                            Bill_Id = BILLinward.Bill_Id,
                            AccountCode = i.AccountCode,
                            Narration = Chartofaccount.Narration,
                            Amount = i.Amount,
                            Quanity = i.Quanity,
                            Rate = i.Rate,
                            Percentage = i.Percentage,
                            Unit = i.Unit,
                            Description = i.Description,
                        };
                        db.BILL_INWARD_PARTICULARDETAILS.Add(BillinwardsParticularDetail);
                        db.SaveChanges();



                        if (Chartofaccount.AccountType == "Expense")
                        {
                            AccountPosting = "Dr";
                        }
                        if (Chartofaccount.AccountType == "Asset")
                        {
                            AccountPosting = "Dr";
                        }
                        if (Chartofaccount.AccountType == "Liability")
                        {
                            AccountPosting = "Cr";
                        }
                        if (Chartofaccount.AccountType == "Income")
                        {
                            AccountPosting = "Cr";
                        }
                        if (Chartofaccount.AccountType == "Equity")
                        {
                            AccountPosting = "Dr";
                        }
                        var accountposting = new ACCOUNTPOSTING
                        {
                            Form_id = formdetail.Form_Id,
                            Acc_id = Chartofaccount.Acc_Id,
                            RefDocDetails = BILLinward.Bill_Id,
                            PostingDate = model.billINWARDs.BillDate,
                            Accountposting1 = AccountPosting,
                            Amount = i.Amount,
                            Description = i.Description,
                        };
                        db.ACCOUNTPOSTINGs.Add(accountposting);
                        db.SaveChanges();
                        if(Chartofaccount.Acc_subtype_Id == null)
                        {
                            description = accountposting.Description;
                        }
                        if(Chartofaccount.Acc_subtype_Id != 9)
                        {
                            description = accountposting.Description;
                        }
                        
                    }




                    var chartAccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Narration == model.billINWARDs.VendorName);
                    var Accountamount = new ACCOUNTPOSTING
                    {
                        Form_id = formdetail.Form_Id,
                        Acc_id = chartAccount.Acc_Id,
                        Accountposting1 = "Cr",
                        Amount = model.billINWARDs.NetAmountPayble,
                        RefDocDetails = BILLinward.Bill_Id,
                        PostingDate = model.billINWARDs.BillDate,
                        Description = description,
                    };
                    db.ACCOUNTPOSTINGs.Add(Accountamount);
                    db.SaveChanges();
                    return "SAVE";
                }
                return "Error";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<BillINWARDs> BillINWARDsLIST()
        {
            try
            {
                List<BillINWARDs> billINWARDslist = new List<BillINWARDs>();
                var billinward = db.BILL_INWARD.ToList();
                foreach (var i in billinward)
                {
                    var model = new BillINWARDs
                    {
                        Bill_Id = i.Bill_Id,
                        VendorName = i.VendorName,
                        PSNNO = i.PSNNO,
                    };
                    billINWARDslist.Add(model);
                }
                return billINWARDslist;
            }
            catch (Exception ex)
            {

                return new List<BillINWARDs>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public SelectBillInward selectBillInwardlist(int BillId)
        {
            try
            {
                var billinwardlist = new SelectBillInward();
                billinwardlist.billInwardsParticulardetails = new List<BillInwardsParticularDetail>();

                var Billinward = db.BILL_INWARD.FirstOrDefault(a => a.Bill_Id == BillId);
                if (Billinward != null)
                {

                    billinwardlist.Bill_Id = Billinward.Bill_Id;
                    billinwardlist.Form_id = Billinward.Form_id;
                    billinwardlist.NetAmountPayble = Billinward.NetAmountPayble;
                    billinwardlist.BillDate = Billinward.BillDate;
                    billinwardlist.Attachment = Billinward.Attachment;
                    billinwardlist.VendorName = Billinward.VendorName;
                    billinwardlist.DeptName = Billinward.DeptName;
                    billinwardlist.PSNNO = Billinward.PSNNO;
                    billinwardlist.Work_Order_NO = Billinward.Work_Order_NO;
                    billinwardlist.GrossAmount = Billinward.GrossAmount;
                    billinwardlist.TotalTaxAmount = Billinward.TotalTaxAmount;

                    var billinwardparticular = db.BILL_INWARD_PARTICULARDETAILS.Where(a => a.Bill_Id == Billinward.Bill_Id).ToList();
                    foreach (var i in billinwardparticular)
                    {
                        var model = new BillInwardsParticularDetail
                        {
                            Bill_PD_Id = i.Bill_PD_Id,
                            Bill_Id = i.Bill_Id,
                            Narration = i.Narration,
                            AccountCode = i.AccountCode,
                            Quanity = i.Quanity,
                            Rate = i.Rate,
                            Unit = i.Unit,
                            Percentage = i.Percentage,
                            Amount = i.Amount,
                            Description = i.Description,
                        };
                        billinwardlist.billInwardsParticulardetails.Add(model);
                    }

                }
                return billinwardlist;
            }
            catch (Exception ex)
            {

                return new SelectBillInward();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<WorkOrderRegister> SelectWorkOrderNO(int VendorName)
        {
            try
            {
                List<WorkOrderRegister> workOrderRegisters = new List<WorkOrderRegister>();
                var WorkorderRegister = db.FORM12_WO_REGISTER.Where(a => a.VendorNameId == VendorName).ToList();
                foreach (var i in WorkorderRegister)
                {
                    var billinward = db.BILL_INWARD.FirstOrDefault(a => a.Work_Order_NO == i.Work_Order_No);
                    if(billinward == null) { 
                    var model = new WorkOrderRegister
                    {
                        Work_Order_No = i.Work_Order_No,
                    };
                    workOrderRegisters.Add(model);
                    }
                }
                return workOrderRegisters;
            }
            catch (Exception ex)
            {

                return new List<WorkOrderRegister>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string DeleteBillInwards(int BillId)
        {
            try
            {
                var BillInwards = db.BILL_INWARD.FirstOrDefault(a => a.Bill_Id == BillId);
                var BillinwardsParticularDetails = db.BILL_INWARD_PARTICULARDETAILS.Where(a => a.Bill_Id == BillId).ToList();
                var Form12WorkRegister = db.FORM12_WO_REGISTER.FirstOrDefault(a => a.Work_Order_No == BillInwards.Work_Order_NO);
                var VendorMstr = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == BillInwards.VendorName);
                var Accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Form_id == 100 && a.RefDocDetails == BillId).ToList();
                if (BillInwards != null || BillinwardsParticularDetails != null)
                {
                    if (BillInwards.Status == 0)
                    {
                        if(Form12WorkRegister != null)
                        {
                            if (Form12WorkRegister.Status > 0)
                            {
                                Form12WorkRegister.Status = Form12WorkRegister.Status - 1;
                                db.SaveChanges();
                            }
                        }
                       
                        if (VendorMstr != null)
                        {
                            if (VendorMstr.Status > 0)
                            {
                                VendorMstr.Status = VendorMstr.Status - 1;
                                db.SaveChanges();
                            }
                        }
                        db.BILL_INWARD.Remove(BillInwards);
                        db.BILL_INWARD_PARTICULARDETAILS.RemoveRange(BillinwardsParticularDetails);
                        db.ACCOUNTPOSTINGs.RemoveRange(Accountposting);
                        db.SaveChanges();
                        return "Deleted Successfully";
                    }
                    else
                    {
                        return "Bill Id Used in Form 64";
                    }
                    
                }
                return "Error";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        //----------------------------Show-Data-of-Last-2-Years-in-from-Form84ParticularDetails----------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<ChartOfAccount> SelectChartOfAccountforForm84(string Narration)
        {
            try
            {
                List<ChartOfAccount> chartOfAccountsList = new List<ChartOfAccount>();
                var ChartOfAccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Narration == Narration);
                if (ChartOfAccount != null)
                {
                    var model = new ChartOfAccount
                    {
                        
                        ACC_Code = ChartOfAccount.ACC_Code,
                        AccountType = ChartOfAccount.AccountType,                       
                        subgroup = ChartOfAccount.subgroup,
                        Narration = ChartOfAccount.Narration,
                        GroupName = ChartOfAccount.GroupName,                   
                       
                    };
                    chartOfAccountsList.Add(model);
                }
                return chartOfAccountsList;
            }
            catch (Exception ex)
            {

                return new List<ChartOfAccount>();
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public Form84ParticularDetails ShowFORM84PARTICULARDETAILS(string Accountcode,DateTime Date)
        {
            try
            {
                // List<Form84ParticularDetails> form84particularList = new List<Form84ParticularDetails>();
                var date = Date.AddYears(-1);
               var form84particularList = new Form84ParticularDetails();
                var sessionmstr = db.SESSION_MSTR.FirstOrDefault(a => a.StartDate>= date);
                var Form84details = db.FORM84_PARTICULARDETAILS.FirstOrDefault(a => a.Session == sessionmstr.Session && a.Accountcode == Accountcode);
                if(Form84details != null)
                {
                    form84particularList.Form84_PDID = Form84details.Form84_PDID;
                    form84particularList.Actual_last_year = Form84details.Actual_second_last_year;
                    form84particularList.Actual_second_last_year = Form84details.Actual_third_last_year;            
                }
               return form84particularList;
            }
            catch (Exception ex)
            {
                return new Form84ParticularDetails();
            }
        }

        //----------------------------Show-Data-of-Last-1-Years-in-from-Form84ParticularDetails----------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public Form84ParticularDetails ShowFORM84PARTICULARDETAILSforlastyear(string Accountcode,DateTime Date)
        {
            try
            {
                List<sesseionmstr> sesseionmstrslist = new List<sesseionmstr>();
                 List<AccountingPosting> AccountingPostingList = new List<AccountingPosting>();
                var date = Date.AddYears(-2);
                var startdate = new DateTime();
                Convert.ToDateTime("2019-12-27T00:00:00");
                startdate = Convert.ToDateTime(date);
                var form84particularList = new Form84ParticularDetails();
                var Sessionmstr = db.SESSION_MSTR.FirstOrDefault(a => a.StartDate >= startdate);
                
                var fromDate = new DateTime();
                var Todate = new DateTime();
                Convert.ToDateTime("2019-12-27T00:00:00");

                fromDate = Convert.ToDateTime(Sessionmstr.StartDate);
                Todate = Convert.ToDateTime(Sessionmstr.EndDate);


                var chartofaccout = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.ACC_Code == Accountcode);
                var Accoutposting = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == chartofaccout.Acc_Id).ToList();
                Accoutposting = Accoutposting.Where(a => a.PostingDate >= fromDate && a.PostingDate <= Todate).ToList();

                if (Accoutposting != null)
                {
                    foreach (var i in Accoutposting)
                    {
                        if (i.Accountposting1 != "Cr")
                        {
                            var model = new AccountingPosting
                            {
                                Amount = i.Amount,
                            };
                            AccountingPostingList.Add(model);
                        }
                    }
                }
                form84particularList.Actual_third_last_year = AccountingPostingList.Sum(a => a.Amount);
                return form84particularList;
            }
            catch (Exception ex)
            {
                return new Form84ParticularDetails();
            }
        }

        //----------------------------Save-Update-Delete-for-Form84ParticularDetails----------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string SaveForm84ParticularDetails(CombineForm84 model)
        {
            try
            {
                var sessionmstr = db.SESSION_MSTR.FirstOrDefault(a => a.StartDate >= model.form84ParticularDetails.Date);
                if (model.form84ParticularDetails.Form84_PDID <= 0)
                {
                    var Form84Details = db.FORM84_DETAIL_ESTIMATE.FirstOrDefault(a => a.Form84_Year == sessionmstr.Session);
                    if (Form84Details != null)
                    {
                        var particularDetails1 = new FORM84_PARTICULARDETAILS
                        {
                            Form84_PDID = model.form84ParticularDetails.Form84_PDID,
                            Form84_ID = Form84Details.Form84_ID,
                            Particulars = model.form84ParticularDetails.Particulars,
                            Accountcode = model.form84ParticularDetails.Accountcode,
                            Actual_last_year = model.form84ParticularDetails.Actual_last_year,
                            Actual_second_last_year = model.form84ParticularDetails.Actual_second_last_year,
                            Actual_third_last_year = model.form84ParticularDetails.Actual_third_last_year,
                            Budget_current8_month = model.form84ParticularDetails.Budget_current8_month,
                            Remaining4_month = model.form84ParticularDetails.Remaining4_month,
                            Total = model.form84ParticularDetails.Total,
                            Budget_Estimate = model.form84ParticularDetails.Budget_Estimate,
                            Remark_variation_difference = model.form84ParticularDetails.Remark_variation_difference,
                            Signature_image = model.form84ParticularDetails.Signature_image,
                            MainAccount = model.form84ParticularDetails.MainAccount,
                            SubAccount = model.form84ParticularDetails.SubAccount,
                            AccountCategory = model.form84ParticularDetails.AccountCategory,
                            Date = model.form84ParticularDetails.Date,
                            Session = sessionmstr.Session,
                            Status = 0,
                            Percentage = model.form84ParticularDetails.Percentage,
                        };
                        db.FORM84_PARTICULARDETAILS.Add(particularDetails1);
                        db.SaveChanges();

                        var OLDFORM84DETAILS = db.FORM84_DETAIL_ESTIMATE.FirstOrDefault(a => a.Form84_ID == Form84Details.Form84_ID);
                        if (OLDFORM84DETAILS != null)
                        {
                            var sumActuallastyear = db.FORM84_PARTICULARDETAILS.Where(a => a.Form84_ID == Form84Details.Form84_ID);
                            OLDFORM84DETAILS.TotalActual_last_year = sumActuallastyear.Sum(a => a.Actual_last_year);
                            OLDFORM84DETAILS.TotalActual_second_last_year = sumActuallastyear.Sum(a => a.Actual_second_last_year);
                            OLDFORM84DETAILS.TotalActual_third_last_year = sumActuallastyear.Sum(a => a.Actual_third_last_year);
                            OLDFORM84DETAILS.TotalBudget_current8_month = sumActuallastyear.Sum(a => a.Budget_current8_month);
                            OLDFORM84DETAILS.TotalBudget_Estimate = sumActuallastyear.Sum(a => a.Budget_Estimate);
                            OLDFORM84DETAILS.TotalRemaining4_month = sumActuallastyear.Sum(a => a.Remaining4_month);
                            OLDFORM84DETAILS.Total = sumActuallastyear.Sum(a => a.Total);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                       
                        var form84Details = new FORM84_DETAIL_ESTIMATE
                        {

                            Form84_Year = sessionmstr.Session,

                        };
                        db.FORM84_DETAIL_ESTIMATE.Add(form84Details);
                        db.SaveChanges();
                        var form84id = form84Details.Form84_ID;
                        var particularDetails = new FORM84_PARTICULARDETAILS
                        {
                            Form84_PDID = model.form84ParticularDetails.Form84_PDID,
                            Form84_ID = form84id,
                            Particulars = model.form84ParticularDetails.Particulars,
                            Accountcode = model.form84ParticularDetails.Accountcode,
                            Actual_last_year = model.form84ParticularDetails.Actual_last_year,
                            Actual_second_last_year = model.form84ParticularDetails.Actual_second_last_year,
                            Actual_third_last_year = model.form84ParticularDetails.Actual_third_last_year,
                            Budget_current8_month = model.form84ParticularDetails.Budget_current8_month,
                            Remaining4_month = model.form84ParticularDetails.Remaining4_month,
                            Total = model.form84ParticularDetails.Total,
                            Budget_Estimate = model.form84ParticularDetails.Budget_Estimate,
                            Remark_variation_difference = model.form84ParticularDetails.Remark_variation_difference,
                            Signature_image = model.form84ParticularDetails.Signature_image,
                            MainAccount = model.form84ParticularDetails.MainAccount,
                            SubAccount = model.form84ParticularDetails.SubAccount,
                            AccountCategory = model.form84ParticularDetails.AccountCategory,
                            Date = model.form84ParticularDetails.Date,
                            Session = sessionmstr.Session,
                            Percentage = model.form84ParticularDetails.Percentage,
                        };
                        db.FORM84_PARTICULARDETAILS.Add(particularDetails);
                        db.SaveChanges();
                        var OLDFORM84DETAILS = db.FORM84_DETAIL_ESTIMATE.FirstOrDefault(a => a.Form84_ID == form84id);
                        if (OLDFORM84DETAILS != null)
                        {
                            var sumActuallastyear = db.FORM84_PARTICULARDETAILS.Where(a => a.Form84_ID == form84id);
                            OLDFORM84DETAILS.TotalActual_last_year = sumActuallastyear.Sum(a => a.Actual_last_year);
                            OLDFORM84DETAILS.TotalActual_second_last_year = sumActuallastyear.Sum(a => a.Actual_second_last_year);
                            OLDFORM84DETAILS.TotalActual_third_last_year = sumActuallastyear.Sum(a => a.Actual_third_last_year);
                            OLDFORM84DETAILS.TotalBudget_current8_month = sumActuallastyear.Sum(a => a.Budget_current8_month);
                            OLDFORM84DETAILS.TotalBudget_Estimate = sumActuallastyear.Sum(a => a.Budget_Estimate);
                            OLDFORM84DETAILS.TotalRemaining4_month = sumActuallastyear.Sum(a => a.Remaining4_month);
                            OLDFORM84DETAILS.Total = sumActuallastyear.Sum(a => a.Total);
                            db.SaveChanges();
                        }
                        return "Done";
                    }
                }

                return "Save";
            }



            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public object UpdateForM84ParticularDetails(Form84ParticularDetails model)
        {
            try
            {
                var sessionmstr = db.SESSION_MSTR.FirstOrDefault(a => a.StartDate >= model.Date);
                if (model.Form84_PDID > 0)
                {
                    var Form84particulardetails = db.FORM84_PARTICULARDETAILS.FirstOrDefault(a => a.Form84_PDID == model.Form84_PDID);
                    if(Form84particulardetails.Status > 0)
                    {
                        return "Form84 is in use";
                    }
                    if(Form84particulardetails != null)
                    {
                        Form84particulardetails.Particulars = model.Particulars;
                        Form84particulardetails.Accountcode = model.Accountcode;
                        Form84particulardetails.Actual_last_year = model.Actual_last_year;
                        Form84particulardetails.Actual_second_last_year = model.Actual_second_last_year;
                        Form84particulardetails.Actual_third_last_year = model.Actual_third_last_year;
                        Form84particulardetails.Budget_current8_month = model.Budget_current8_month;
                        Form84particulardetails.Remaining4_month = model.Remaining4_month;
                        Form84particulardetails.Total = model.Total;
                        Form84particulardetails.Budget_Estimate = model.Budget_Estimate;
                        Form84particulardetails.Remark_variation_difference = model.Remark_variation_difference;
                        Form84particulardetails.Signature_image = model.Signature_image;
                        Form84particulardetails.MainAccount = model.MainAccount;
                        Form84particulardetails.SubAccount = model.SubAccount;
                        Form84particulardetails.AccountCategory = model.AccountCategory;
                        Form84particulardetails.Date = model.Date;
                        Form84particulardetails.Session = sessionmstr.Session;
                        Form84particulardetails.Percentage = model.Percentage;
                        db.SaveChanges();
                        return "Done";
                    }
                    
                }
                else
                {
                    return "Form84_PDID not found";
                }
                return "error";
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string DeleteForM84ParticularDetails(int Form84PDID)
        {
            try
            {
                var form84ParticularDetails = db.FORM84_PARTICULARDETAILS.FirstOrDefault(a => a.Form84_PDID == Form84PDID);
                if (form84ParticularDetails != null)
                {
                    if(form84ParticularDetails.Status == 0)
                    {
                        db.FORM84_PARTICULARDETAILS.Remove(form84ParticularDetails);
                        db.SaveChanges();
                        return "Deleted Succesfully";
                    }
                    return "BudgetCode is use In Form64";
                }
                return "NOT Deleted";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<Form84ParticularDetails> form84ParticularDetailsList()
        {
            try
            {
                List<Form84ParticularDetails> form84particulardetailslist = new List<Form84ParticularDetails>();
                var form84particulardetails = db.FORM84_PARTICULARDETAILS.ToList();
                foreach (var i in form84particulardetails)
                {
                    var model = new Form84ParticularDetails
                    {
                        Form84_PDID = i.Form84_PDID,
                        Particulars = i.Particulars,
                        Accountcode = i.Accountcode,
                        Actual_last_year = i.Actual_last_year,
                        Actual_second_last_year = i.Actual_second_last_year,
                        Actual_third_last_year = i.Actual_third_last_year,
                        Budget_current8_month = i.Budget_current8_month,
                        Remaining4_month = i.Remaining4_month,
                        Total = i.Total,
                        Budget_Estimate = i.Budget_Estimate,
                        Remark_variation_difference = i.Remark_variation_difference,
                        Signature_image = i.Signature_image,
                        MainAccount = i.MainAccount,
                        SubAccount = i.SubAccount,
                        AccountCategory = i.AccountCategory,
                        Date = i.Date,
                        Percentage = i.Percentage,
                    };
                    form84particulardetailslist.Add(model);
                }
                return form84particulardetailslist;
            }
            catch (Exception ex)
            {

                return new List<Form84ParticularDetails>();
            }
        }

       

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<Form84ParticularDetails> form84ParticularDetailsListbysession(string session)
        {
            try
            {
                List<Form84ParticularDetails> form84particulardetailslist = new List<Form84ParticularDetails>();
                var sessionmaster = db.SESSION_MSTR.FirstOrDefault(a => a.Session == session);
                var form84particulardetails = db.FORM84_PARTICULARDETAILS.Where(a=>a.Date >= sessionmaster.StartDate && a.Date <= sessionmaster.EndDate).ToList();
                foreach (var i in form84particulardetails)
                {
                    var model = new Form84ParticularDetails
                    {
                        Form84_PDID = i.Form84_PDID,
                        Particulars = i.Particulars,
                        Accountcode = i.Accountcode,
                        Actual_last_year = i.Actual_last_year,
                        Actual_second_last_year = i.Actual_second_last_year,
                        Actual_third_last_year = i.Actual_third_last_year,
                        Budget_current8_month = i.Budget_current8_month,
                        Remaining4_month = i.Remaining4_month,
                        Total = i.Total,
                        Budget_Estimate = i.Budget_Estimate,
                        Remark_variation_difference = i.Remark_variation_difference,
                        Signature_image = i.Signature_image,
                        MainAccount = i.MainAccount,
                        SubAccount = i.SubAccount,
                        AccountCategory = i.AccountCategory,
                        Date = i.Date,
                        Percentage = i.Percentage,
                    };
                    form84particulardetailslist.Add(model);
                }
                return form84particulardetailslist;
            }
            catch (Exception ex)
            {

                return new List<Form84ParticularDetails>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<Form84ParticularDetails> Selectform84ParticularDetailsList(int Form84PDID)
        {
            try
            {
                List<Form84ParticularDetails> form84particulardetailslist = new List<Form84ParticularDetails>();
                var form84particulardetails = db.FORM84_PARTICULARDETAILS.Where(a => a.Form84_PDID == Form84PDID).ToList();
                foreach (var i in form84particulardetails)
                {
                    var model = new Form84ParticularDetails
                    {
                        Form84_PDID = i.Form84_PDID,
                        Particulars = i.Particulars,
                        Accountcode = i.Accountcode,
                        Actual_last_year = i.Actual_last_year,
                        Actual_second_last_year = i.Actual_second_last_year,
                        Actual_third_last_year = i.Actual_third_last_year,
                        Budget_current8_month = i.Budget_current8_month,
                        Remaining4_month = i.Remaining4_month,
                        Total = i.Total,
                        Budget_Estimate = i.Budget_Estimate,
                        Remark_variation_difference = i.Remark_variation_difference,
                        Signature_image = i.Signature_image,
                        MainAccount = i.MainAccount,
                        SubAccount = i.SubAccount,
                        AccountCategory = i.AccountCategory,
                        Date = i.Date,
                        Percentage = i.Percentage,
                    };
                    form84particulardetailslist.Add(model);
                }
                return form84particulardetailslist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //----------------------------Save--for ACCOUNTPOSTING & BILL PAYMENTS----------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<WorkOrderRegister> SelectWorkOrderNOfromBill(int VendorId)
        {
            try
            {
                List<WorkOrderRegister> workOrderRegisters = new List<WorkOrderRegister>();
                var WorkorderRegister = db.FORM12_WO_REGISTER.Where(a => a.VendorNameId == VendorId).ToList();
                foreach (var i in WorkorderRegister)
                {
                    var model = new WorkOrderRegister
                    {
                        Work_Order_No = i.Work_Order_No,
                    };
                    workOrderRegisters.Add(model);
                }
                return workOrderRegisters;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public BILLIDINWARDS SelectBillInwardsParticularDetails(int BillId)
        {
            try
            {
                var billInwardsParticularDetailsList = new BILLIDINWARDS();
                var bILLINWARDS = db.BILL_INWARD.FirstOrDefault(a => a.Bill_Id == BillId);
                if (bILLINWARDS != null)
                {
                    billInwardsParticularDetailsList.NetAmountPayble = bILLINWARDS.NetAmountPayble;
                    var BillInwardsParticularDetails = db.BILL_INWARD_PARTICULARDETAILS.Where(a => a.Bill_Id == BillId).ToList();
                    billInwardsParticularDetailsList.billInwardsParticularDetails = new List<BillInwardsParticularDetail>();
                    if (BillInwardsParticularDetails != null)
                    {
                        foreach (var i in BillInwardsParticularDetails)
                        {
                            var model = new BillInwardsParticularDetail
                            {
                                Bill_PD_Id = i.Bill_PD_Id,
                                Bill_Id = i.Bill_Id,
                                AccountCode = i.AccountCode,
                                Narration = i.Narration,
                                Amount = i.Amount,
                                Quanity = i.Quanity,
                                Rate = i.Rate,
                                Unit = i.Unit,
                                Percentage = i.Percentage,
                                Description = i.Description,
                            };
                            billInwardsParticularDetailsList.billInwardsParticularDetails.Add(model);
                        }

                    }
                }
                return billInwardsParticularDetailsList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<BillINWARDs> SelectBillIdFromBillInwards(string WorkOrderNo)
        {
            try
            {
                List<BillINWARDs> billINWARDslist = new List<BillINWARDs>();
                var Billinward = db.BILL_INWARD.Where(a => a.Work_Order_NO == WorkOrderNo);
                foreach (var i in Billinward)
                {
                    var model = new BillINWARDs
                    {
                        Bill_Id = i.Bill_Id,
                        // NetAmountPayble = i.NetAmountPayble,
                    };
                    billINWARDslist.Add(model);
                }
                return billINWARDslist;
            }
            catch (Exception ex)
            {


                throw;
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string SaveBillPaymentVoucher(CombinePaymentVoucher model)
        {
            try
            {
                //var date = DateTime.Now;
                //       date = date.AddYears(-1);
                //       var sessionmstr = db.SESSION_MSTR.FirstOrDefault(a =>a.StartDate >= date);
                //var currentDate = new DateTime();
                //var Date = model.form64PAYMENTVOUCHER.Date;
                //Convert.ToDateTime("01/31/2019");
                //currentDate = Convert.ToDateTime(Date);
                //// var frmDate = DateTime.ParseExact(Date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                ////var Datetime = model.form64PAYMENTVOUCHER.Date.ToString("dd/mm/yyyy");
                //if (sessionmstr.StartDate >= currentDate)
                //{
                //    return "Date Is Out of session";
                //}
                //if(sessionmstr.EndDate <= currentDate)
                //{
                //    return "Date Is Out of session";
                //}
                var description = "";
                if (model.form64PAYMENTVOUCHER.PSN == null)
                {
                    return "Please Enter PSN NO";
                }
                var form64count = db.FORM64_PAYMENTVOUCHER.ToList();
                if (form64count.Count>0)
                {
                    var psn = db.FORM64_PAYMENTVOUCHER.FirstOrDefault(a => a.PSN == model.form64PAYMENTVOUCHER.PSN);
                    if (psn != null)
                    {
                        return "PSN Number Already Exist";
                    }
                }


                var formdetail = db.FORM_DETAILS.FirstOrDefault(a => a.Form_Name == "FORM64");
                var savebillpayment = new FORM64_PAYMENTVOUCHER
                {
                    Form_id = formdetail.Form_Id,
                    Bill_Id = model.form64PAYMENTVOUCHER.Bill_Id,
                    DepartmentName = model.form64PAYMENTVOUCHER.DepartmentName,
                    Date = model.form64PAYMENTVOUCHER.Date,
                    Bill_No = model.form64PAYMENTVOUCHER.Bill_No,
                    Bill_Details = model.form64PAYMENTVOUCHER.Bill_Details,
                    NamePayeeVendorContractor = model.form64PAYMENTVOUCHER.NamePayeeVendorContractor,
                    Form12_Id = model.form64PAYMENTVOUCHER.Form12_Id,
                    First_Advance = model.form64PAYMENTVOUCHER.First_Advance,
                    Running_Amount = model.form64PAYMENTVOUCHER.Running_Amount,
                    Final_Amount = model.form64PAYMENTVOUCHER.Final_Amount,
                    GrossAmtPayable = model.form64PAYMENTVOUCHER.GrossAmtPayable,
                    TotalDeduction = model.form64PAYMENTVOUCHER.TotalDeduction,
                    Net_AmtPayable = model.form64PAYMENTVOUCHER.Net_AmtPayable,
                    Amt_inwords = model.form64PAYMENTVOUCHER.Amt_inwords,
                    Budget_provision = model.form64PAYMENTVOUCHER.Budget_provision,
                    Budget_code = model.form64PAYMENTVOUCHER.Budget_code,
                    Balance = model.form64PAYMENTVOUCHER.Balance,
                    BillPaid_tillnow = model.form64PAYMENTVOUCHER.BillPaid_tillnow,
                    Measurementbookreference = model.form64PAYMENTVOUCHER.Measurementbookreference,
                    No_of_stock = model.form64PAYMENTVOUCHER.No_of_stock,
                    current_billamt = model.form64PAYMENTVOUCHER.current_billamt,
                    Book_No = model.form64PAYMENTVOUCHER.Book_No,
                    passed_paymentamt = model.form64PAYMENTVOUCHER.passed_paymentamt,
                    Work_Order_No = model.form64PAYMENTVOUCHER.Work_Order_No,
                    Bank = model.form64PAYMENTVOUCHER.Bank,
                    ChequeNo = model.form64PAYMENTVOUCHER.ChequeNo,
                    PSN = model.form64PAYMENTVOUCHER.PSN,
                };
                db.FORM64_PAYMENTVOUCHER.Add(savebillpayment);
                db.SaveChanges();
                var Form64Payment = db.FORM64_PAYMENTVOUCHER.FirstOrDefault(a => a.PSN == model.form64PAYMENTVOUCHER.PSN);
                //----------------------------update-status-of-form12----------------------//
                var form12 = db.FORM12_WO_REGISTER.FirstOrDefault(a => a.Work_Order_No == model.form64PAYMENTVOUCHER.Work_Order_No);
                if (form12 != null)
                {
                    form12.Status = form12.Status + 1;
                    db.SaveChanges();
                }
                var Billinward = db.BILL_INWARD.FirstOrDefault(a => a.Bill_Id == model.form64PAYMENTVOUCHER.Bill_Id);
                if(Billinward != null)
                {
                    Billinward.Status = Billinward.Status + 1;
                    db.SaveChanges();
                }
                var vendorMaster = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == model.form64PAYMENTVOUCHER.NamePayeeVendorContractor);
                if (vendorMaster != null)
                {
                    vendorMaster.Status = vendorMaster.Status + 1;
                    db.SaveChanges();
                }
                var Form84particular = db.FORM84_PARTICULARDETAILS.FirstOrDefault(a => a.Accountcode == model.form64PAYMENTVOUCHER.Budget_code);
                if(Form84particular != null)
                {
                    Form84particular.Status = Form84particular.Status + 1;
                    db.SaveChanges();
                }
                var departmentmaster = db.DEPARTMENT_MASTER.FirstOrDefault(a => a.DepartmentName == model.form64PAYMENTVOUCHER.DepartmentName);
                if(departmentmaster != null)
                {
                    departmentmaster.Status = departmentmaster.Status + 1;
                    db.SaveChanges();
                }
                foreach (var i in model.form64PARTICULARDETAILs)
                {
                   
                    var saveform64particulardetail = new FORM64_PARTICULARDETAILS
                    {
                        Form64_Id = Form64Payment.Form64_Id,
                        Bill_PD_Id = i.Bill_PD_Id,
                        Narration = i.Narration,
                        Accountcode = i.Accountcode,
                        Qty = i.Qty,
                        Rate = i.Rate,
                        Unit = i.Unit,
                        Amount = i.Amount,
                        Percentage = i.Percentage,
                        Description = i.Description,
                    };
                    db.FORM64_PARTICULARDETAILS.Add(saveform64particulardetail);
                    db.SaveChanges();
                }
                foreach(var i in model.fORM64Deductions)
                {
                    var Chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.ACC_Code == i.Accountcode);
                    var saveform64deduction = new FORM64_DEDUCTION
                    {
                        Form64_Id = Form64Payment.Form64_Id,
                        Narration = i.Narration,
                        Accountcode = i.Accountcode,
                        Amount = i.Amount,
                        Description = i.Description,
                    };
                    db.FORM64_DEDUCTION.Add(saveform64deduction);
                    db.SaveChanges();

                    if (Chartofaccount != null)
                    {
                        var AccountPosting = "";
                        if (Chartofaccount.AccountType == "Expense")
                        {
                            AccountPosting = "Dr";
                        }
                        if (Chartofaccount.AccountType == "Asset")
                        {
                            AccountPosting = "Cr";
                        }
                        if (Chartofaccount.AccountType == "Liability")
                        {
                            AccountPosting = "Dr";
                        }
                        if (Chartofaccount.AccountType == "Income")
                        {
                            AccountPosting = "Cr";
                        }
                        if (Chartofaccount.AccountType == "Equity")
                        {
                            AccountPosting = "Dr";
                        }
                        if (Chartofaccount.AccountType == "Income")
                        {
                            var saveaccountposting = new ACCOUNTPOSTING
                            {
                                Form_id = formdetail.Form_Id,
                                Acc_id = Chartofaccount.Acc_Id,
                                RefDocDetails = Form64Payment.Form64_Id,
                                PostingDate = model.form64PAYMENTVOUCHER.Date,
                                Accountposting1 = AccountPosting,
                                Amount = i.Amount,
                                Description = i.Description,
                            };
                            db.ACCOUNTPOSTINGs.Add(saveaccountposting);
                            db.SaveChanges();
                        }
                    }
                }


                var chartAccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Narration == model.form64PAYMENTVOUCHER.Bank);
                var Accountamount = new ACCOUNTPOSTING
                {
                    Form_id = formdetail.Form_Id,
                    Acc_id = chartAccount.Acc_Id,
                    RefDocDetails = Form64Payment.Form64_Id,
                    Accountposting1 = "Cr",
                    Amount = model.form64PAYMENTVOUCHER.Net_AmtPayable,
                    PostingDate = model.form64PAYMENTVOUCHER.Date,
                    Description = description,
                };
                db.ACCOUNTPOSTINGs.Add(Accountamount);
                db.SaveChanges();

                var chartofACCOUNT = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Narration == model.form64PAYMENTVOUCHER.NamePayeeVendorContractor);
                var accAmount = new ACCOUNTPOSTING
                {
                    RefDocDetails = Form64Payment.Form64_Id,
                    Form_id = formdetail.Form_Id,
                    Acc_id = chartofACCOUNT.Acc_Id,
                    Accountposting1 = "Dr",
                    Amount = model.form64PAYMENTVOUCHER.GrossAmtPayable,
                    PostingDate = model.form64PAYMENTVOUCHER.Date,
                    Description = description,
                };
                db.ACCOUNTPOSTINGs.Add(accAmount);
                db.SaveChanges();

                return "SAVE";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        [System.Web.Http.HttpPost]
        public List<ChartOfAccount> Selectnameofpayeevendor()
        {
            try
            {
                var chartofaccont = db.CHARTOFACCOUNTs.Where(a => a.AccountType != "Income").ToList();
                List<ChartOfAccount> chartOfAccountslist = new List<ChartOfAccount>();
                foreach (var i in chartofaccont)
                {
                    var model = new ChartOfAccount
                    {
                        Narration = i.Narration,
                       
                        Acc_Id = i.Acc_Id,
                    };
                    chartOfAccountslist.Add(model);
                }
                return chartOfAccountslist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<WorkOrderRegister> WorkorderRegisterListforform64()
        {
            try
            {
                List<WorkOrderRegister> workOrderRegistersList = new List<WorkOrderRegister>();
                var workOrder = db.FORM12_WO_REGISTER.ToList();
                foreach (var i in workOrder)
                {                   
                    var model = new WorkOrderRegister
                    {                       
                        Work_Order_No = i.Work_Order_No,                       
                    };
                    workOrderRegistersList.Add(model);
                }
                return workOrderRegistersList;
            }
            catch (Exception ex)
            {
                return new List<WorkOrderRegister>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<FORM64PAYMENTVOUCHER> PaymentVouchersList()
        {
            try
            {

                List<FORM64PAYMENTVOUCHER> fORM64PAYMENTVOUCHERslist = new List<FORM64PAYMENTVOUCHER>();
                var paymentvoucher = db.FORM64_PAYMENTVOUCHER.ToList();
                foreach (var i in paymentvoucher)
                {
                    var model = new FORM64PAYMENTVOUCHER
                    {
                        Form64_Id = i.Form64_Id,
                        Form_id = i.Form_id,
                        NamePayeeVendorContractor = i.NamePayeeVendorContractor,
                        PSN = i.PSN,

                    };
                    fORM64PAYMENTVOUCHERslist.Add(model);
                };
                return fORM64PAYMENTVOUCHERslist;
            }
            catch (Exception ex)
            {

                return new List<FORM64PAYMENTVOUCHER>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<FORM64PAYMENTVOUCHER> PaymentVouchersListBYSESSION(string session)
        {
            try
            {

                List<FORM64PAYMENTVOUCHER> fORM64PAYMENTVOUCHERslist = new List<FORM64PAYMENTVOUCHER>();
                var sessionmstr = db.SESSION_MSTR.FirstOrDefault(a => a.Session == session);
                if(sessionmstr!= null)
                {
                    var paymentvoucher = db.FORM64_PAYMENTVOUCHER.Where(a=>a.Date >= sessionmstr.StartDate && a.Date <= sessionmstr.EndDate).ToList();
                    foreach (var i in paymentvoucher)
                    {
                        var model = new FORM64PAYMENTVOUCHER
                        {
                            Form64_Id = i.Form64_Id,
                            Form_id = i.Form_id,
                            NamePayeeVendorContractor = i.NamePayeeVendorContractor,
                            PSN = i.PSN,

                        };
                        fORM64PAYMENTVOUCHERslist.Add(model);
                    };
                }
               
                return fORM64PAYMENTVOUCHERslist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public SelectPaymentVoucher SelectPaymentVouchersList(int Form64Id)
        {
            try
            {

                var selectfORM64PAYMENTVOUCHERslist = new SelectPaymentVoucher();
                selectfORM64PAYMENTVOUCHERslist.fORM64PARTICULARDETAILs = new List<FORM64PARTICULARDETAILS>();
                selectfORM64PAYMENTVOUCHERslist.fORM64Deductions = new List<FORM64Deduction>();

                var paymentvoucher = db.FORM64_PAYMENTVOUCHER.Where(a => a.Form64_Id == Form64Id);

                foreach (var i in paymentvoucher)
                {
                    selectfORM64PAYMENTVOUCHERslist.Form64_Id = i.Form64_Id;
                    selectfORM64PAYMENTVOUCHERslist.Form_id = i.Form_id;
                    selectfORM64PAYMENTVOUCHERslist.Bill_Id = i.Bill_Id;
                    selectfORM64PAYMENTVOUCHERslist.DepartmentName = i.DepartmentName;
                    selectfORM64PAYMENTVOUCHERslist.Date = i.Date;
                    selectfORM64PAYMENTVOUCHERslist.NamePayeeVendorContractor = i.NamePayeeVendorContractor;
                    selectfORM64PAYMENTVOUCHERslist.First_Advance = i.First_Advance;
                    selectfORM64PAYMENTVOUCHERslist.Running_Amount = i.Running_Amount;
                    selectfORM64PAYMENTVOUCHERslist.Final_Amount = i.Final_Amount;
                    selectfORM64PAYMENTVOUCHERslist.GrossAmtPayable = i.GrossAmtPayable;
                    selectfORM64PAYMENTVOUCHERslist.TotalDeduction = i.TotalDeduction;
                    selectfORM64PAYMENTVOUCHERslist.Net_AmtPayable = i.Net_AmtPayable;
                    selectfORM64PAYMENTVOUCHERslist.Amt_inwords = i.Amt_inwords;
                    selectfORM64PAYMENTVOUCHERslist.Budget_provision = i.Budget_provision;
                    selectfORM64PAYMENTVOUCHERslist.Budget_code = i.Budget_code;
                    selectfORM64PAYMENTVOUCHERslist.Balance = i.Balance;
                    selectfORM64PAYMENTVOUCHERslist.BillPaid_tillnow = i.BillPaid_tillnow;
                    selectfORM64PAYMENTVOUCHERslist.Measurementbookreference = i.Measurementbookreference;
                    selectfORM64PAYMENTVOUCHERslist.No_of_stock = i.No_of_stock;
                    selectfORM64PAYMENTVOUCHERslist.current_billamt = i.current_billamt;
                    selectfORM64PAYMENTVOUCHERslist.Book_No = i.Book_No;
                    selectfORM64PAYMENTVOUCHERslist.passed_paymentamt = i.passed_paymentamt;
                    selectfORM64PAYMENTVOUCHERslist.payment_inwords = i.payment_inwords;
                    selectfORM64PAYMENTVOUCHERslist.Work_Order_No = i.Work_Order_No;
                    selectfORM64PAYMENTVOUCHERslist.Bank = i.Bank;
                    selectfORM64PAYMENTVOUCHERslist.ChequeNo = i.ChequeNo;
                    selectfORM64PAYMENTVOUCHERslist.PSN = i.PSN;


                    var paymentparticularDetail = db.FORM64_PARTICULARDETAILS.Where(a => a.Form64_Id == i.Form64_Id).ToList();
                    if (paymentparticularDetail != null && paymentparticularDetail.Count != 0)
                    {
                        foreach (var j in paymentparticularDetail)
                        {
                            var paymentparticular = new FORM64PARTICULARDETAILS
                            {
                                Form64_ParticularDetailID = j.Form64_ParticularDetailID,
                                Narration = j.Narration,
                                Accountcode = j.Accountcode,
                                Qty = j.Qty,
                                Rate = j.Rate,
                                Unit = j.Unit,
                                Amount = j.Amount,
                                Percentage = j.Percentage,
                                Description = j.Description,
                            };
                            selectfORM64PAYMENTVOUCHERslist.fORM64PARTICULARDETAILs.Add(paymentparticular);
                        }

                    }
                    var paymentvoucherDeduction = db.FORM64_DEDUCTION.Where(a => a.Form64_Id == i.Form64_Id).ToList();
                    if(paymentvoucherDeduction != null && paymentvoucherDeduction.Count != 0)
                    {
                        foreach(var k in paymentvoucherDeduction)
                        {
                            var getpaymentvoucherDeduction = new FORM64Deduction
                            {
                                FORM64_DEDUCTIONID = k.FORM64_DEDUCTIONID,
                                Narration = k.Narration,
                                Accountcode = k.Accountcode,
                                Amount = k.Amount,
                                Description = k.Description,
                            };
                            selectfORM64PAYMENTVOUCHERslist.fORM64Deductions.Add(getpaymentvoucherDeduction);
                        }
                    }

                }

                return selectfORM64PAYMENTVOUCHERslist;
            }
            catch (Exception ex)
            {

                throw;
            };
        }
        //----------------------------DELETE-FROM-64-DELETE-PAYMENT-VOUCHER----------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string DeletePaymentVoucher(int FORM64ID)
        {
            try
            {
                var form64Paymentvoucher = db.FORM64_PAYMENTVOUCHER.FirstOrDefault(a => a.Form64_Id == FORM64ID);
                var form64particularDetails = db.FORM64_PARTICULARDETAILS.Where(a => a.Form64_Id == FORM64ID);
                var Form64particulardeduction = db.FORM64_DEDUCTION.Where(a => a.Form64_Id == FORM64ID);
                var accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Form_id == 101 && a.RefDocDetails == FORM64ID);
                var form12workorderregister = db.FORM12_WO_REGISTER.FirstOrDefault(a => a.Work_Order_No == form64Paymentvoucher.Work_Order_No);
                var BillInwards = db.BILL_INWARD.FirstOrDefault(a => a.Bill_Id == form64Paymentvoucher.Bill_Id);
                if(BillInwards != null)
                {
                    if(BillInwards.Status > 0)
                    {
                        BillInwards.Status = BillInwards.Status - 1;
                        db.SaveChanges();
                    }
                }
                if(form12workorderregister != null)
                {
                    if(form12workorderregister.Status > 0)
                    {
                        form12workorderregister.Status = form12workorderregister.Status - 1;
                        db.SaveChanges();
                    }
                }
                var VendorMstr = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == form64Paymentvoucher.NamePayeeVendorContractor);
                if (VendorMstr != null)
                {
                    if (VendorMstr.Status > 0)
                    {
                        VendorMstr.Status = VendorMstr.Status - 1;
                        db.SaveChanges();
                    }
                }
                var departmentmaster = db.DEPARTMENT_MASTER.FirstOrDefault(a => a.DepartmentName == form64Paymentvoucher.DepartmentName);
                if(departmentmaster!= null)
                {
                    if(departmentmaster.Status > 0)
                    {
                        departmentmaster.Status = departmentmaster.Status - 1;
                        db.SaveChanges();
                    }
                }
                if (form64Paymentvoucher != null)
                {
                    db.FORM64_PAYMENTVOUCHER.Remove(form64Paymentvoucher);
                    db.FORM64_PARTICULARDETAILS.RemoveRange(form64particularDetails);
                    db.ACCOUNTPOSTINGs.RemoveRange(accountposting);
                    db.FORM64_DEDUCTION.RemoveRange(Form64particulardeduction);
                    db.SaveChanges();
                    
                    return "Deleted Succesfully";
                }
                return "Not Deleted";
            }
            catch (Exception ex)
            {
                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        //----------------------------Show--BudgetCode-from-BILL PAYMENTS----------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public Form84ParticularDetails showform84Budgetcode(string AccountCode)
        {
            try
            {
                var form84particulardetails = new Form84ParticularDetails();
                List<FORM64_PARTICULARDETAILS> fORM64_PARTICULARDETAILslist = new List<FORM64_PARTICULARDETAILS>();
                var Form84particularDetails = db.FORM84_PARTICULARDETAILS.FirstOrDefault(a => a.Accountcode == AccountCode);
                if(Form84particularDetails != null)
                {

                    form84particulardetails.Accountcode = Form84particularDetails.Accountcode;
                    form84particulardetails.Budget_Estimate = Form84particularDetails.Budget_Estimate;
                    
               
                var form64particulardetails = db.FORM64_PARTICULARDETAILS.Where(a => a.Accountcode == AccountCode);
                foreach(var i in form64particulardetails)
                {
                    var form64particular = new FORM64_PARTICULARDETAILS
                    {
                        Amount = i.Amount,
                    };
                    fORM64_PARTICULARDETAILslist.Add(form64particular);
                }
                    form84particulardetails.Total = fORM64_PARTICULARDETAILslist.Sum(a => a.Amount);

                    if(fORM64_PARTICULARDETAILslist != null)
                    {
                        fORM64_PARTICULARDETAILslist = new List<FORM64_PARTICULARDETAILS>();
                    }
                    
                }
                return form84particulardetails;
            }
            catch (Exception ex) 
            {

                throw;
            }
        } 

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<ChartOfAccount> SelectBank()
        {
            try
            {
                var chartofaccont = db.CHARTOFACCOUNTs.Where(a => a.ObjectCode == "4810" || a.ObjectCode == "4820" || a.ObjectCode == "4821" || a.ObjectCode == "4822" || a.ObjectCode == "4823").ToList();
                List<ChartOfAccount> chartOfAccountslist = new List<ChartOfAccount>();
                foreach (var i in chartofaccont)
                {
                    var model = new ChartOfAccount
                    {
                        Narration = i.Narration,
                    };
                    chartOfAccountslist.Add(model);
                }
                return chartOfAccountslist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<ChartOfAccount> SelectNarration()
        {
            try
            {
                var chartofaccont = db.CHARTOFACCOUNTs.Where(a=>a.AccountType == "Expense").ToList();
                List<ChartOfAccount> chartOfAccountslist = new List<ChartOfAccount>();
                foreach (var i in chartofaccont)
                {
                    var model = new ChartOfAccount
                    {
                        Narration = i.Narration,
                      // ACC_Code = i.ACC_Code,
                        Acc_Id = i.Acc_Id,
                    };
                    chartOfAccountslist.Add(model);
                }
                return chartOfAccountslist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<ChartOfAccount> SelectNarrationforDeduction()
        {
            try
            {
                var chartofaccont = db.CHARTOFACCOUNTs.Where(a => a.ObjectCode == "3670" && a.AccTypeId == 3).ToList();
                List<ChartOfAccount> chartOfAccountslist = new List<ChartOfAccount>();
                foreach (var i in chartofaccont)
                {
                    var model = new ChartOfAccount
                    {
                        Narration = i.Narration,
                        ACC_Code = i.ACC_Code,
                    };
                    chartOfAccountslist.Add(model);
                }
                return chartOfAccountslist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ChartOfAccount> SelectChartOfAccountNARRATION(int Acc_Id)
        {
            try
            {
                List<ChartOfAccount> chartOfAccountsList = new List<ChartOfAccount>();
                var ChartOfAccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == Acc_Id);
                if (ChartOfAccount != null)
                {
                    var model = new ChartOfAccount
                    {
                        Acc_Id = ChartOfAccount.Acc_Id,
                        ACC_Code = ChartOfAccount.ACC_Code,
                    };
                    chartOfAccountsList.Add(model);
                }
                return chartOfAccountsList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //----------------------------Form-7 -- Trial Balance-------------------------------//
        public AccountingPostingsReport AccountingPostingsReport(string AccountCode)
        {
            try
            {
                var accountingPostingslist = new AccountingPostingsReport();
                 accountingPostingslist.accountingPostings = new List<AccountingPosting>();
                List<AccountingPostingDR> accountingPostingDRslist = new List<AccountingPostingDR>();
                List<AccountingPostingCR> accountingPostingCRslist = new List<AccountingPostingCR>();
                var chartofAccounT = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.ACC_Code == AccountCode);
                var Accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == chartofAccounT.Acc_Id);
                foreach (var i in Accountposting)
                {
                    var model = new AccountingPosting
                    {
                        Form_id = i.Form_id,
                        RefDocDetails = i.RefDocDetails,
                        PostingDate = i.PostingDate,
                        Accountposting1 = i.Accountposting1,
                        Amount = i.Amount,
                        Description = i.Description,
                    };
                    var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == i.Acc_id);
                    if (chartofaccount != null)
                    {
                        model.Narration = chartofaccount.Narration;
                        model.ACC_Code = chartofaccount.ACC_Code;
                    }
                    accountingPostingslist.accountingPostings.Add(model);
                }
                foreach(var i in accountingPostingslist.accountingPostings)
                {
                    if(i.Accountposting1 == "Dr")
                    {
                        var sumdr = new AccountingPostingDR
                        {
                           AmountDR = i.Amount,
                        };
                        accountingPostingDRslist.Add(sumdr);
                    }
                    if (i.Accountposting1 == "Cr")
                    {
                        var sumCr = new AccountingPostingCR
                        {
                            AmountCR = i.Amount,
                        };
                        accountingPostingCRslist.Add(sumCr);
                    }
                }
                var DRAddition = accountingPostingDRslist.Sum(a => a.AmountDR);
                var CRAddition = accountingPostingCRslist.Sum(a => a.AmountCR);

                var accounttype = chartofAccounT.AccountType;
                if (accounttype != null)
                {
                    if (accounttype == "Expense")
                    {                        
                        accountingPostingslist.Balance = DRAddition - CRAddition;
                        accountingPostingslist.accountposting = "Dr";
                    }
                    if (accounttype == "Asset")
                    {
                        accountingPostingslist.Balance = DRAddition - CRAddition;
                        accountingPostingslist.accountposting = "Dr";
                       
                    }
                    if (accounttype == "Liability")
                    {
                        accountingPostingslist.Balance = CRAddition - DRAddition;
                        accountingPostingslist.accountposting = "Cr";
                    }
                    if (accounttype == "Income")
                    {
                        accountingPostingslist.Balance = CRAddition - DRAddition;
                        accountingPostingslist.accountposting = "Cr";
                    }
                    if (accounttype == "Equity")
                    {
                         accountingPostingslist.Balance = DRAddition - CRAddition;
                        accountingPostingslist.accountposting = "Dr";
                    }
                }
                return accountingPostingslist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //----------------------------SAVE-JOURNAL-VOUCHER-------------------------------//

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<ChartOfAccount> SelectNarrationforJV()
        {
            try
            {
                var chartofaccont = db.CHARTOFACCOUNTs.ToList();
                List<ChartOfAccount> chartOfAccountslist = new List<ChartOfAccount>();
                foreach (var i in chartofaccont)
                {
                    var model = new ChartOfAccount
                    {
                        Narration = i.Narration,
                        ACC_Code = i.ACC_Code,
                        Acc_Id = i.Acc_Id,
                    };
                    chartOfAccountslist.Add(model);
                }
                return chartOfAccountslist;
            }
            catch (Exception ex)
            {

                return new List<ChartOfAccount>();
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string saveJournalVoucher(CombineJournalVoucher model)
        {
            try
            {
                var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.ACC_Code == model.journalVoucher.AccountCode);
                if (model.journalVoucher.JV_Id <= 0)
                {
                    var journalvoucher = new JOURNALVOUCHER
                    {

                        //AccountCode = model.journalVoucher.AccountCode,
                        JVDate = model.journalVoucher.JVDate,
                        VoucherNo = model.journalVoucher.VoucherNo,
                        //Amount = model.journalVoucher.Amount,
                        //AccountType = chartofaccount.AccountType,
                        //Narration = chartofaccount.Narration,
                        //Acc_subtype_Id = chartofaccount.Acc_subtype_Id,
                        Description = model.journalVoucher.Description,
                    };
                    db.JOURNALVOUCHERs.Add(journalvoucher);
                    db.SaveChanges();
                    var RefDoc = journalvoucher.JV_Id;
                    var FormDetail = db.FORM_DETAILS.FirstOrDefault(a => a.Form_Name == "JV");

                    foreach (var i in model.accountingPostings)
                    {
                        var chartofAccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.ACC_Code == i.ACC_Code);
                        var accountPosting = new ACCOUNTPOSTING
                        {
                            Form_id = FormDetail.Form_Id,
                            Acc_id = chartofAccount.Acc_Id,
                            PostingDate = model.journalVoucher.JVDate,
                            Accountposting1 = i.Accountposting1,
                            Amount = i.Amount,
                            RefDocDetails = RefDoc,
                            Description = model.journalVoucher.Description,
                        };
                        db.ACCOUNTPOSTINGs.Add(accountPosting);
                        db.SaveChanges();
                        var vendorMaster = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == chartofAccount.Narration);
                        if (vendorMaster != null)
                        {
                            vendorMaster.Status = vendorMaster.Status + 1;
                            db.SaveChanges();
                        }
                    }
                    return "Save";
                }
                else
                {
                    var OLDJOURNALVOUCHER = db.JOURNALVOUCHERs.FirstOrDefault(a => a.JV_Id == model.journalVoucher.JV_Id);
                    if (OLDJOURNALVOUCHER != null)
                    {
                        OLDJOURNALVOUCHER.JVDate = model.journalVoucher.JVDate;
                        //OLDJOURNALVOUCHER.AccountCode = model.journalVoucher.AccountCode;
                        //OLDJOURNALVOUCHER.VoucherNo = model.journalVoucher.VoucherNo;
                        //OLDJOURNALVOUCHER.Amount = model.journalVoucher.Amount;
                        //OLDJOURNALVOUCHER.AccountType = chartofaccount.AccountType;
                        //OLDJOURNALVOUCHER.Narration = chartofaccount.Narration;
                        //OLDJOURNALVOUCHER.Acc_subtype_Id = chartofaccount.Acc_subtype_Id;
                        OLDJOURNALVOUCHER.Description = model.journalVoucher.Description;

                        foreach (var i in model.accountingPostings)
                        {
                            var chartofAccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.ACC_Code == i.ACC_Code);
                            var OLDAccountPosting = db.ACCOUNTPOSTINGs.FirstOrDefault(a => a.Accountposting_Id == i.Accountposting_Id && a.Form_id == 104);
                            if (OLDAccountPosting != null)
                            {
                                if(OLDAccountPosting.Acc_id == chartofAccount.Acc_Id)
                                {
                                   
                                    OLDAccountPosting.PostingDate = model.journalVoucher.JVDate;
                                    OLDAccountPosting.Accountposting1 = i.Accountposting1;
                                    OLDAccountPosting.Amount = i.Amount;
                                    OLDAccountPosting.Description = i.Description;
                                    db.SaveChanges();
                                }
                                else
                                {
                                    OLDAccountPosting.Acc_id = chartofAccount.Acc_Id;
                                    OLDAccountPosting.PostingDate = model.journalVoucher.JVDate;
                                    OLDAccountPosting.Accountposting1 = i.Accountposting1;
                                    OLDAccountPosting.Amount = i.Amount;
                                    OLDAccountPosting.Description = i.Description;
                                    var OLDChartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == OLDAccountPosting.Acc_id);
                                    var OLDVendor = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == OLDChartofaccount.Narration);
                                    var NewVendor = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == chartofAccount.Narration);
                                    if (OLDVendor != null)
                                    {
                                        if (OLDVendor.Status > 0)
                                        {
                                            OLDVendor.Status = OLDVendor.Status - 1;
                                            
                                        }                                       
                                    }
                                    if (NewVendor != null)
                                    {
                                        NewVendor.Status = OLDVendor.Status - 1;
                                        
                                    }
                                    db.SaveChanges();
                                }
                               
                            }
                            else
                            {
                                var accountPosting1 = new ACCOUNTPOSTING
                                {
                                    Form_id = 104,
                                    Acc_id = chartofAccount.Acc_Id,
                                    PostingDate = model.journalVoucher.JVDate,
                                    Accountposting1 = i.Accountposting1,
                                    Amount = i.Amount,
                                    RefDocDetails = model.journalVoucher.JV_Id,
                                    Description = model.journalVoucher.Description,
                                };
                                db.ACCOUNTPOSTINGs.Add(accountPosting1);
                                db.SaveChanges();
                                var vendorMaster = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == chartofAccount.Narration);
                                if (vendorMaster != null)
                                {
                                    vendorMaster.Status = vendorMaster.Status + 1;
                                    db.SaveChanges();
                                }
                            }
                           
                        }
                        db.SaveChanges();
                        return "Done";
                    }
                }

                return "ERROR";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<JOURNALVOUCHER> JournalVoucherList()
        {
            try
            {
                List<JOURNALVOUCHER> jOURNALVOUCHERslist = new List<JOURNALVOUCHER>();
                var JOURNALVoucher = db.JOURNALVOUCHERs.ToList();
                if (JOURNALVoucher != null)
                {
                    foreach (var i in JOURNALVoucher)
                    {

                        var model = new JOURNALVOUCHER
                        {
                            JV_Id = i.JV_Id,
                            VoucherNo = i.VoucherNo,
                        };
                        jOURNALVOUCHERslist.Add(model);
                    }

                }
                return jOURNALVOUCHERslist;
            }
            catch (Exception ex)
            {

                return new List<JOURNALVOUCHER>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<JOURNALVOUCHER> JournalVoucherListBYSESSION(string Session)
        {
            try
            {
                List<JOURNALVOUCHER> jOURNALVOUCHERslist = new List<JOURNALVOUCHER>();
                var Sessionmaster = db.SESSION_MSTR.FirstOrDefault(a => a.Session == Session);
                
                var JOURNALVoucher = db.JOURNALVOUCHERs.Where(a=>a.JVDate >= Sessionmaster.StartDate && a.JVDate <= Sessionmaster.EndDate).ToList();
                if (JOURNALVoucher != null)
                {
                    foreach (var i in JOURNALVoucher)
                    {
                        var model = new JOURNALVOUCHER
                        {
                            JV_Id = i.JV_Id,
                            VoucherNo = i.VoucherNo,
                        };
                        jOURNALVOUCHERslist.Add(model);
                    }

                }
                return jOURNALVOUCHERslist;
            }
            catch (Exception ex)
            {

                return new List<JOURNALVOUCHER>();
            }
        }


        public GETJournalVoucher SelectJournalVoucher(int JVID)
        {
            try
            {
                var JournalVoucherList = new GETJournalVoucher();
                JournalVoucherList.accountPostings = new List<AccountingPosting>();
                var JournalVoucher = db.JOURNALVOUCHERs.FirstOrDefault(a => a.JV_Id == JVID);
                if (JournalVoucher != null)
                {
                    JournalVoucherList.JV_Id = JournalVoucher.JV_Id;
                    //JournalVoucherList.AccountCode = JournalVoucher.AccountCode;
                    JournalVoucherList.JVDate = JournalVoucher.JVDate;
                    //JournalVoucherList.Amount = JournalVoucher.Amount;
                    //JournalVoucherList.AccountType = JournalVoucher.AccountType;
                    //JournalVoucherList.Narration = JournalVoucher.Narration;
                    //JournalVoucherList.Acc_subtype_Id = JournalVoucher.Acc_subtype_Id;
                    //JournalVoucherList.VoucherNo = JournalVoucher.VoucherNo;
                    JournalVoucherList.Description = JournalVoucher.Description;

                    var AccountPosting = db.ACCOUNTPOSTINGs.Where(a => a.RefDocDetails == JVID && a.Form_id == 104).ToList();
                    foreach (var i in AccountPosting)
                    {
                        var chartaccounT = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == i.Acc_id);
                        var model = new AccountingPosting
                        {
                            Accountposting_Id = i.Accountposting_Id,
                            Accountposting1 = i.Accountposting1,
                            Narration = chartaccounT.Narration,
                            ACC_Code = chartaccounT.ACC_Code,
                            Amount = i.Amount,
                        };
                        JournalVoucherList.accountPostings.Add(model);
                    }
                }
                return JournalVoucherList;
            }
            catch (Exception ex)
            {

                return new GETJournalVoucher();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string DeleteJournalVoucher(int JVID)
        {
            try
            {
                var journalVoucher = db.JOURNALVOUCHERs.FirstOrDefault(a => a.JV_Id == JVID);
                var accountPosting = db.ACCOUNTPOSTINGs.Where(a => a.RefDocDetails == JVID).ToList();
                if(accountPosting != null)
                {
                    foreach (var i in accountPosting)
                    {
                        var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == i.Acc_id);
                        var VendorMstr = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == chartofaccount.Narration);
                        if (VendorMstr != null)
                        {
                            if (VendorMstr.Status > 0)
                            {
                                VendorMstr.Status = VendorMstr.Status - 1;
                                db.SaveChanges();
                            }
                        }
                    }
                
                
                
                if (journalVoucher != null)
                {
                    db.JOURNALVOUCHERs.Remove(journalVoucher);
                    db.ACCOUNTPOSTINGs.RemoveRange(accountPosting);
                    db.SaveChanges();
                    return "Deleted Succesfully";
                }
                }
                return "NOT Deleted";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }


        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public object  DeleteJournalvouchereByid(int AccountpostingId)
        {
            try
            {
                var accountposting = db.ACCOUNTPOSTINGs.FirstOrDefault(a => a.Form_id == 104 && a.Accountposting_Id == AccountpostingId);
                if(accountposting != null)
                {
                    var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == accountposting.Acc_id);
                    var VendorMstr = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == chartofaccount.Narration);
                    if (VendorMstr != null)
                    {
                        if (VendorMstr.Status > 0)
                        {
                            VendorMstr.Status = VendorMstr.Status - 1;
                            db.SaveChanges();
                        }
                    }

                    db.ACCOUNTPOSTINGs.Remove(accountposting);
                    db.SaveChanges();
                    return "Deleted Succesfully";
                }
                return "Not Deleted";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message; 
            }
        }
        //public List<TDSREPORT> TDSREPORTs()
        //{
        //    try
        //    {
        //        List<TDSREPORT> tDSREPORTslist = new List<TDSREPORT>();
        //        var TDSAccountposting = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == 9).ToList();
        //        if(TDSAccountposting!= null)
        //        {
        //            foreach(var i in TDSAccountposting)
        //            {
        //                var model = new 
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public addAmount AccountingPostingsforDR()
        {
            try
            {
                var accountingPostingslist = new addAmount();
                accountingPostingslist.postingCalculations = new List<PostingCalculation>();
                accountingPostingslist.accountingPosting1s = new List<AccountingPosting1>();
                accountingPostingslist.aMOUNTs = new List<AMOUNT>();

                var Chartofaccount = db.CHARTOFACCOUNTs.ToList();
                foreach (var acc in Chartofaccount)
                {

                    var Accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == acc.Acc_Id && a.Accountposting1 == "Dr").ToList();
                    if (Accountposting != null)
                    {

                        foreach (var i in Accountposting)
                        {
                            var model = new AccountingPosting1
                            {
                                Form_id = i.Form_id,
                                RefDocDetails = i.RefDocDetails,
                                PostingDate = i.PostingDate,
                                Accountposting1 = i.Accountposting1,
                                Acc_id = i.Acc_id,
                                Amount = i.Amount,
                                
                            };
                            accountingPostingslist.accountingPosting1s.Add(model);

                        }
                        var amount1 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount);
                        if (amount1 != 0)
                        {
                            var amount = new AMOUNT()
                            {
                                Amount1 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount),
                                ACC_Code = acc.ACC_Code,
                                Narration = acc.Narration,
                                Accountposting1 = "Dr",
                            };

                            accountingPostingslist.aMOUNTs.Add(amount);
                        }

                    }
                    if (accountingPostingslist.accountingPosting1s != null)
                    {
                        accountingPostingslist.accountingPosting1s = new List<AccountingPosting1>();
                    }

                    //for CR
                    var Accountposting1 = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == acc.Acc_Id && a.Accountposting1 == "Cr").ToList();
                    if (Accountposting != null)
                    {
                        foreach (var i in Accountposting1)
                        {
                            var model = new AccountingPosting1
                            {
                                Form_id = i.Form_id,
                                RefDocDetails = i.RefDocDetails,
                                PostingDate = i.PostingDate,
                                Accountposting1 = i.Accountposting1,
                                Acc_id = i.Acc_id,
                                Amount = i.Amount,
                            };
                            accountingPostingslist.accountingPosting1s.Add(model);

                        }
                        var amount1 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount);
                        if (amount1 != 0)
                        {
                            var amount = new AMOUNT()
                            {
                                Amount2 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount),
                                ACC_Code = acc.ACC_Code,
                                Narration = acc.Narration,
                                Accountposting1 = "Cr"
                            };
                            accountingPostingslist.aMOUNTs.Add(amount);
                        }
                        
                    }
                    if (accountingPostingslist.accountingPosting1s != null)
                    {
                        accountingPostingslist.accountingPosting1s = new List<AccountingPosting1>();
                    }
                    accountingPostingslist.AmountDR = 0;
                    accountingPostingslist.AmountCR = 0;
                    foreach (var i in accountingPostingslist.aMOUNTs)
                    {                        
                        if (i.Accountposting1 == "Dr")
                        {
                            accountingPostingslist.AmountDR = i.Amount1;
                        }
                        if (i.Accountposting1 == "Cr")
                        {
                            accountingPostingslist.AmountCR = i.Amount2;
                        }                      

                    }
                    if (acc.AccountType == "Asset")
                    {
                        var posting = new PostingCalculation();
                        {
                            posting.Narration = acc.Narration;
                            posting.ACC_Code = acc.ACC_Code;
                            posting.Balance = accountingPostingslist.AmountDR - accountingPostingslist.AmountCR;
                            if(posting.Balance < 0)
                            {
                                posting.Balance = posting.Balance * (-1);
                                posting.POSTING = "Cr";
                            }
                            else
                            {
                                posting.POSTING = "Dr";
                            }
                            
                        }
                        if (posting.Balance != 0)
                        {
                            accountingPostingslist.postingCalculations.Add(posting);
                        }
                    }
                    if (acc.AccountType == "Expense")
                    {
                        var posting = new PostingCalculation();
                        {
                            posting.Narration = acc.Narration;
                            posting.ACC_Code = acc.ACC_Code;
                            posting.Balance = accountingPostingslist.AmountDR - accountingPostingslist.AmountCR;
                            if (posting.Balance < 0)
                            {
                                posting.Balance = posting.Balance * (-1);
                                posting.POSTING = "Cr";
                            }
                            else
                            {
                                posting.POSTING = "Dr";
                            }
                            
                        }
                        if(posting.Balance != 0) { 
                        accountingPostingslist.postingCalculations.Add(posting);
                        }
                    }
                    if (acc.AccountType == "Liability")
                    {
                        var posting = new PostingCalculation();
                        {
                            posting.Narration = acc.Narration;
                            posting.ACC_Code = acc.ACC_Code;
                            posting.Balance = accountingPostingslist.AmountCR - accountingPostingslist.AmountDR;
                            
                            if (posting.Balance < 0)
                            {
                                posting.Balance = posting.Balance * (-1);
                                posting.POSTING = "Dr";
                            }
                            else
                            {
                                posting.POSTING = "Cr"; ;
                            }
                        }
                        if (posting.Balance != 0)
                        {
                            accountingPostingslist.postingCalculations.Add(posting);
                        }
                    }
                    if (acc.AccountType == "Income")
                    {
                        var posting = new PostingCalculation();
                        {
                            posting.Narration = acc.Narration;
                            posting.ACC_Code = acc.ACC_Code;
                            posting.Balance = accountingPostingslist.AmountCR - accountingPostingslist.AmountDR;
                            if (posting.Balance < 0)
                            {
                                posting.Balance = posting.Balance * (-1);
                                posting.POSTING = "Dr";
                            }
                            else
                            {
                                posting.POSTING = "Cr"; ;
                            }
                        }
                        if (posting.Balance != 0)
                        {
                            accountingPostingslist.postingCalculations.Add(posting);
                        }
                    }
                    if (acc.AccountType == "Equity")
                    {
                        var posting = new PostingCalculation();
                        {
                            posting.Narration = acc.Narration;
                            posting.ACC_Code = acc.ACC_Code;
                            posting.Balance = accountingPostingslist.AmountCR - accountingPostingslist.AmountDR;
                            if (posting.Balance < 0)
                            {
                                posting.Balance = posting.Balance * (-1);
                                posting.POSTING = "Dr";
                            }
                            else
                            {
                                posting.POSTING = "Cr"; ;
                            }
                        }
                        if (posting.Balance != 0)
                        {
                            accountingPostingslist.postingCalculations.Add(posting);
                        }
                    }
                    if (accountingPostingslist.aMOUNTs != null)
                    {
                        accountingPostingslist.aMOUNTs = new List<AMOUNT>();
                    }
                }

                

                return accountingPostingslist;
            }
            catch (Exception ex)
            {

                return new addAmount();
            }
        }


        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public addAmount AccountingPostingsforDRBYSESSION(string Session)
        {
            try
            {
                var accountingPostingslist = new addAmount();
                accountingPostingslist.postingCalculations = new List<PostingCalculation>();
                accountingPostingslist.accountingPosting1s = new List<AccountingPosting1>();
                accountingPostingslist.aMOUNTs = new List<AMOUNT>();
                var Sessiomaster = db.SESSION_MSTR.FirstOrDefault(a => a.Session == Session);
                var Chartofaccount = db.CHARTOFACCOUNTs.ToList();
                foreach (var acc in Chartofaccount)
                {

                    var Accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == acc.Acc_Id && a.Accountposting1 == "Dr" && a.PostingDate >= Sessiomaster.StartDate && a.PostingDate <= Sessiomaster.EndDate).ToList();
                    if (Accountposting != null)
                    {

                        foreach (var i in Accountposting)
                        {
                            var model = new AccountingPosting1
                            {
                                Form_id = i.Form_id,
                                RefDocDetails = i.RefDocDetails,
                                PostingDate = i.PostingDate,
                                Accountposting1 = i.Accountposting1,
                                Acc_id = i.Acc_id,
                                Amount = i.Amount,

                            };
                            accountingPostingslist.accountingPosting1s.Add(model);

                        }
                        var amount1 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount);
                        if (amount1 != 0)
                        {
                            var amount = new AMOUNT()
                            {
                                Amount1 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount),
                                ACC_Code = acc.ACC_Code,
                                Narration = acc.Narration,
                                Accountposting1 = "Dr",
                            };

                            accountingPostingslist.aMOUNTs.Add(amount);
                        }

                    }
                    if (accountingPostingslist.accountingPosting1s != null)
                    {
                        accountingPostingslist.accountingPosting1s = new List<AccountingPosting1>();
                    }

                    //for CR
                    var Accountposting1 = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == acc.Acc_Id && a.Accountposting1 == "Cr" && a.PostingDate >= Sessiomaster.StartDate && a.PostingDate <= Sessiomaster.EndDate).ToList();
                    if (Accountposting != null)
                    {
                        foreach (var i in Accountposting1)
                        {
                            var model = new AccountingPosting1
                            {
                                Form_id = i.Form_id,
                                RefDocDetails = i.RefDocDetails,
                                PostingDate = i.PostingDate,
                                Accountposting1 = i.Accountposting1,
                                Acc_id = i.Acc_id,
                                Amount = i.Amount,
                            };
                            accountingPostingslist.accountingPosting1s.Add(model);

                        }
                        var amount1 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount);
                        if (amount1 != 0)
                        {
                            var amount = new AMOUNT()
                            {
                                Amount2 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount),
                                ACC_Code = acc.ACC_Code,
                                Narration = acc.Narration,
                                Accountposting1 = "Cr"
                            };
                            accountingPostingslist.aMOUNTs.Add(amount);
                        }

                    }
                    if (accountingPostingslist.accountingPosting1s != null)
                    {
                        accountingPostingslist.accountingPosting1s = new List<AccountingPosting1>();
                    }
                    accountingPostingslist.AmountDR = 0;
                    accountingPostingslist.AmountCR = 0;
                    foreach (var i in accountingPostingslist.aMOUNTs)
                    {
                        if (i.Accountposting1 == "Dr")
                        {
                            accountingPostingslist.AmountDR = i.Amount1;
                        }
                        if (i.Accountposting1 == "Cr")
                        {
                            accountingPostingslist.AmountCR = i.Amount2;
                        }

                    }
                    if (acc.AccountType == "Asset")
                    {
                        var posting = new PostingCalculation();
                        {
                            posting.Narration = acc.Narration;
                            posting.ACC_Code = acc.ACC_Code;
                            posting.Balance = accountingPostingslist.AmountDR - accountingPostingslist.AmountCR;
                            if (posting.Balance < 0)
                            {
                                posting.Balance = posting.Balance * (-1);
                                posting.POSTING = "Cr";
                            }
                            else
                            {
                                posting.POSTING = "Dr";
                            }

                        }
                        if (posting.Balance != 0)
                        {
                            accountingPostingslist.postingCalculations.Add(posting);
                        }
                    }
                    if (acc.AccountType == "Expense")
                    {
                        var posting = new PostingCalculation();
                        {
                            posting.Narration = acc.Narration;
                            posting.ACC_Code = acc.ACC_Code;
                            posting.Balance = accountingPostingslist.AmountDR - accountingPostingslist.AmountCR;
                            if (posting.Balance < 0)
                            {
                                posting.Balance = posting.Balance * (-1);
                                posting.POSTING = "Cr";
                            }
                            else
                            {
                                posting.POSTING = "Dr";
                            }

                        }
                        if (posting.Balance != 0)
                        {
                            accountingPostingslist.postingCalculations.Add(posting);
                        }
                    }
                    if (acc.AccountType == "Liability")
                    {
                        var posting = new PostingCalculation();
                        {
                            posting.Narration = acc.Narration;
                            posting.ACC_Code = acc.ACC_Code;
                            posting.Balance = accountingPostingslist.AmountCR - accountingPostingslist.AmountDR;

                            if (posting.Balance < 0)
                            {
                                posting.Balance = posting.Balance * (-1);
                                posting.POSTING = "Dr";
                            }
                            else
                            {
                                posting.POSTING = "Cr"; ;
                            }
                        }
                        if (posting.Balance != 0)
                        {
                            accountingPostingslist.postingCalculations.Add(posting);
                        }
                    }
                    if (acc.AccountType == "Income")
                    {
                        var posting = new PostingCalculation();
                        {
                            posting.Narration = acc.Narration;
                            posting.ACC_Code = acc.ACC_Code;
                            posting.Balance = accountingPostingslist.AmountCR - accountingPostingslist.AmountDR;
                            if (posting.Balance < 0)
                            {
                                posting.Balance = posting.Balance * (-1);
                                posting.POSTING = "Dr";
                            }
                            else
                            {
                                posting.POSTING = "Cr"; ;
                            }
                        }
                        if (posting.Balance != 0)
                        {
                            accountingPostingslist.postingCalculations.Add(posting);
                        }
                    }
                    if (acc.AccountType == "Equity")
                    {
                        var posting = new PostingCalculation();
                        {
                            posting.Narration = acc.Narration;
                            posting.ACC_Code = acc.ACC_Code;
                            posting.Balance = accountingPostingslist.AmountCR - accountingPostingslist.AmountDR;
                            if (posting.Balance < 0)
                            {
                                posting.Balance = posting.Balance * (-1);
                                posting.POSTING = "Dr";
                            }
                            else
                            {
                                posting.POSTING = "Cr"; ;
                            }
                        }
                        if (posting.Balance != 0)
                        {
                            accountingPostingslist.postingCalculations.Add(posting);
                        }
                    }
                    if (accountingPostingslist.aMOUNTs != null)
                    {
                        accountingPostingslist.aMOUNTs = new List<AMOUNT>();
                    }
                }



                return accountingPostingslist;
            }
            catch (Exception ex)
            {

                return new addAmount();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public addAmount AccountingPostingsforCRDR()
        {
            try
            {
                var accountingPostingslist = new addAmount();
                accountingPostingslist.accountingPosting1s = new List<AccountingPosting1>();
                accountingPostingslist.aMOUNTs = new List<AMOUNT>();

                var Accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Accountposting1 == "Cr").ToList();
                var Accountposting1 = db.ACCOUNTPOSTINGs.Where(a => a.Accountposting1 == "Dr").ToList();
                if (Accountposting != null)
                {
                    foreach (var i in Accountposting)
                    {
                        var model = new AccountingPosting1
                        {
                            Amount = i.Amount,
                        };
                        accountingPostingslist.accountingPosting1s.Add(model);
                    }
                    var amount1 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount);
                    if (amount1 != 0)
                    {
                        var amount = new AMOUNT()
                        {
                            Amount1 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount),
                        };
                        accountingPostingslist.aMOUNTs.Add(amount);
                    }
                }
                return accountingPostingslist;
            }
            catch (Exception ex)
            {

               return new addAmount();
            }
        }


        //----------------------------Form-Ledger Report-------------------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public Accounts AccountingPostingsLedgerReport()
        {
            try
            {
                var Account = new Accounts();
                List<AccountingPostingLedgerReport> accountingPostingslistDR = new List<AccountingPostingLedgerReport>();
                List<AccountingPostingLedgerReport> accountingPostingslistCR = new List<AccountingPostingLedgerReport>();
                Account.accountingPostings = new List<AccountingPosting>();
                Account.OpeningBalance = new List<AccountingPosting>();
               var Accountposting = db.ACCOUNTPOSTINGs.Where(a=>a.Form_id != 105).ToList();
              
                foreach (var i in Accountposting)
                {
                    var model = new AccountingPosting
                    {
                        Form_id = i.Form_id,
                        RefDocDetails = i.RefDocDetails,
                        PostingDate = i.PostingDate,
                        Accountposting1 = i.Accountposting1,
                        Amount = i.Amount,
                        Description = i.Description,
                    };
                    var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == i.Acc_id);
                    if (chartofaccount != null)
                    {
                        model.Narration = chartofaccount.Narration;
                        model.ACC_Code = chartofaccount.ACC_Code;
                    }
                    Account.accountingPostings.Add(model);

                }

                var chartOfaccout = db.CHARTOFACCOUNTs.ToList();
                foreach (var k in chartOfaccout)
                {
                    var AccountpostingDR = db.ACCOUNTPOSTINGs.Where(a => a.Form_id == 105 && a.Accountposting1 == "Dr" && a.Acc_id == k.Acc_Id).ToList();
                    foreach (var dr in AccountpostingDR)
                    {
                        var modeldr = new AccountingPostingLedgerReport
                        {

                            Amount = dr.Amount,

                        };
                        accountingPostingslistDR.Add(modeldr);
                    }
                    var AmountDr = accountingPostingslistDR.Sum(a => a.Amount);
                    if (AmountDr != 0)
                    {
                        var openingDr = new AccountingPosting
                        {
                            Amount = AmountDr,
                            Accountposting1 = "Dr",
                            Form_id = 105,
                            Narration = k.Narration,
                            ACC_Code = k.ACC_Code,
                        };
                        Account.accountingPostings.Add(openingDr);
                    }

                    var AccountpostingCR = db.ACCOUNTPOSTINGs.Where(a => a.Form_id == 105 && a.Accountposting1 == "Cr" && a.Acc_id == k.Acc_Id).ToList();
                    foreach (var cr in AccountpostingCR)
                    {
                        var modelcr = new AccountingPostingLedgerReport
                        {

                            Amount = cr.Amount,

                        };
                        accountingPostingslistCR.Add(modelcr);
                    }
                    var AmountCr = accountingPostingslistCR.Sum(a => a.Amount);
                    if (AmountCr != 0)
                    {
                        var openingCr = new AccountingPosting
                        {
                            Amount = AmountCr,
                            Accountposting1 = "Cr",
                            Form_id = 105,
                            Narration = k.Narration,
                            ACC_Code = k.ACC_Code,
                        };
                        Account.accountingPostings.Add(openingCr);
                    }
                    if (accountingPostingslistCR != null)
                    {
                        accountingPostingslistCR = new List<AccountingPostingLedgerReport>();
                    }
                    if (accountingPostingslistDR != null)
                    {
                        accountingPostingslistDR = new List<AccountingPostingLedgerReport>();
                    }
                }


                //var AccountType = db.ACCOUNT_TYPE.ToList();               

                //    decimal? AmountDR = 0;
                //    decimal? AmountCR = 0;

                //var chartofAccount = db.CHARTOFACCOUNTs.ToList();
                //    foreach (var j in chartofAccount)
                //    {
                //        var accountpostingDR = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == j.Acc_Id && a.Accountposting1 == "Dr");
                //        foreach (var k in accountpostingDR)
                //        {
                //            var model1 = new AccountingPostingLedgerReport
                //            {
                //                Amount1 = k.Amount,
                //            };
                //            accountingPostingslistDR.Add(model1);
                //        }

                //         AmountDR = accountingPostingslistDR.Sum(a => a.Amount1);


                //        var accountpostingCR = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == j.Acc_Id && a.Accountposting1 == "Cr");
                //        foreach (var k in accountpostingCR)
                //        {
                //            var model1 = new AccountingPostingLedgerReport
                //            {
                //                Amount2 = k.Amount,
                //            };
                //            accountingPostingslistCR.Add(model1);
                //        }
                //         AmountCR = accountingPostingslistCR.Sum(a => a.Amount2);

                //    if (j.AccountType == "Asset")
                //    {
                //        var models = new AccountingPosting
                //        {
                //            Amount = AmountDR - AmountCR,
                //            Accountposting1 = "Dr",
                //            ACC_Code = j.ACC_Code,
                //            Narration = j.Narration,
                //        };
                //        Account.closingBalance.Add(models);
                //    }
                //    if (j.AccountType == "Liability")
                //    {
                //        var models = new AccountingPosting
                //        {
                //            Amount = AmountCR - AmountDR,
                //            Accountposting1 = "Cr",
                //            ACC_Code = j.ACC_Code,
                //            Narration = j.Narration,
                //        };
                //        Account.closingBalance.Add(models);
                //    }
                //    if (j.AccountType == "Income")
                //    {
                //        var models = new AccountingPosting
                //        {
                //            Amount = AmountCR - AmountDR,
                //            Accountposting1 = "Cr",
                //            ACC_Code = j.ACC_Code,
                //            Narration = j.Narration,
                //        };
                //        Account.closingBalance.Add(models);
                //    }
                //    if (j.AccountType == "Expense")
                //    {
                //        var models = new AccountingPosting
                //        {
                //            Amount = AmountDR - AmountCR,
                //            Accountposting1 = "Dr",
                //            ACC_Code = j.ACC_Code,
                //            Narration = j.Narration,
                //        };
                //        Account.closingBalance.Add(models);
                //    }
                //    if (j.AccountType == "Equity")
                //    {
                //        var models = new AccountingPosting
                //        {
                //            Amount = AmountCR - AmountDR,
                //            Accountposting1 = "Cr",
                //            ACC_Code = j.ACC_Code,
                //            Narration = j.Narration,
                //        };
                //        Account.closingBalance.Add(models);
                //    }
                //    if (accountingPostingslistCR != null)
                //    {
                //        accountingPostingslistCR = new List<AccountingPostingLedgerReport>();
                //    }
                //    if (accountingPostingslistDR != null)
                //    {
                //        accountingPostingslistDR = new List<AccountingPostingLedgerReport>();
                //    }
                //}
                return Account;
            }
            catch (Exception ex)
            {

                return new Accounts();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public Accounts AccountingPostingsLedgerReportBYSESSION(string Session)
        {
            try
            {
                var Account = new Accounts();
                List<AccountingPostingLedgerReport> accountingPostingslistDR = new List<AccountingPostingLedgerReport>();
                List<AccountingPostingLedgerReport> accountingPostingslistCR = new List<AccountingPostingLedgerReport>();
                Account.accountingPostings = new List<AccountingPosting>();
                Account.OpeningBalance = new List<AccountingPosting>();
                var Sessionmaster = db.SESSION_MSTR.FirstOrDefault(a => a.Session == Session);
                var Accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Form_id != 105 && a.PostingDate>= Sessionmaster.StartDate && a.PostingDate <= Sessionmaster.EndDate).ToList();

                foreach (var i in Accountposting)
                {
                    var model = new AccountingPosting
                    {
                        Form_id = i.Form_id,
                        RefDocDetails = i.RefDocDetails,
                        PostingDate = i.PostingDate,
                        Accountposting1 = i.Accountposting1,
                        Amount = i.Amount,
                        Description = i.Description,
                    };
                    var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == i.Acc_id);
                    if (chartofaccount != null)
                    {
                        model.Narration = chartofaccount.Narration;
                        model.ACC_Code = chartofaccount.ACC_Code;
                    }
                    Account.accountingPostings.Add(model);

                }

                var chartOfaccout = db.CHARTOFACCOUNTs.ToList();
                foreach (var k in chartOfaccout)
                {
                    var AccountpostingDR = db.ACCOUNTPOSTINGs.Where(a => a.Form_id == 105 && a.Accountposting1 == "Dr" && a.Acc_id == k.Acc_Id && a.PostingDate >= Sessionmaster.StartDate && a.PostingDate <= Sessionmaster.EndDate).ToList();
                    foreach (var dr in AccountpostingDR)
                    {
                        var modeldr = new AccountingPostingLedgerReport
                        {

                            Amount = dr.Amount,

                        };
                        accountingPostingslistDR.Add(modeldr);
                    }
                    var AmountDr = accountingPostingslistDR.Sum(a => a.Amount);
                    if (AmountDr != 0)
                    {
                        var openingDr = new AccountingPosting
                        {
                            Amount = AmountDr,
                            Accountposting1 = "Dr",
                            Form_id = 105,
                            Narration = k.Narration,
                            ACC_Code = k.ACC_Code,
                        };
                        Account.accountingPostings.Add(openingDr);
                    }

                    var AccountpostingCR = db.ACCOUNTPOSTINGs.Where(a => a.Form_id == 105 && a.Accountposting1 == "Cr" && a.Acc_id == k.Acc_Id && a.PostingDate >= Sessionmaster.StartDate && a.PostingDate <= Sessionmaster.EndDate).ToList();
                    foreach (var cr in AccountpostingCR)
                    {
                        var modelcr = new AccountingPostingLedgerReport
                        {

                            Amount = cr.Amount,

                        };
                        accountingPostingslistCR.Add(modelcr);
                    }
                    var AmountCr = accountingPostingslistCR.Sum(a => a.Amount);
                    if (AmountCr != 0)
                    {
                        var openingCr = new AccountingPosting
                        {
                            Amount = AmountCr,
                            Accountposting1 = "Cr",
                            Form_id = 105,
                            Narration = k.Narration,
                            ACC_Code = k.ACC_Code,
                        };
                        Account.accountingPostings.Add(openingCr);
                    }
                    if (accountingPostingslistCR != null)
                    {
                        accountingPostingslistCR = new List<AccountingPostingLedgerReport>();
                    }
                    if (accountingPostingslistDR != null)
                    {
                        accountingPostingslistDR = new List<AccountingPostingLedgerReport>();
                    }
                }


                //var AccountType = db.ACCOUNT_TYPE.ToList();               

                //    decimal? AmountDR = 0;
                //    decimal? AmountCR = 0;

                //var chartofAccount = db.CHARTOFACCOUNTs.ToList();
                //    foreach (var j in chartofAccount)
                //    {
                //        var accountpostingDR = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == j.Acc_Id && a.Accountposting1 == "Dr");
                //        foreach (var k in accountpostingDR)
                //        {
                //            var model1 = new AccountingPostingLedgerReport
                //            {
                //                Amount1 = k.Amount,
                //            };
                //            accountingPostingslistDR.Add(model1);
                //        }

                //         AmountDR = accountingPostingslistDR.Sum(a => a.Amount1);


                //        var accountpostingCR = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == j.Acc_Id && a.Accountposting1 == "Cr");
                //        foreach (var k in accountpostingCR)
                //        {
                //            var model1 = new AccountingPostingLedgerReport
                //            {
                //                Amount2 = k.Amount,
                //            };
                //            accountingPostingslistCR.Add(model1);
                //        }
                //         AmountCR = accountingPostingslistCR.Sum(a => a.Amount2);

                //    if (j.AccountType == "Asset")
                //    {
                //        var models = new AccountingPosting
                //        {
                //            Amount = AmountDR - AmountCR,
                //            Accountposting1 = "Dr",
                //            ACC_Code = j.ACC_Code,
                //            Narration = j.Narration,
                //        };
                //        Account.closingBalance.Add(models);
                //    }
                //    if (j.AccountType == "Liability")
                //    {
                //        var models = new AccountingPosting
                //        {
                //            Amount = AmountCR - AmountDR,
                //            Accountposting1 = "Cr",
                //            ACC_Code = j.ACC_Code,
                //            Narration = j.Narration,
                //        };
                //        Account.closingBalance.Add(models);
                //    }
                //    if (j.AccountType == "Income")
                //    {
                //        var models = new AccountingPosting
                //        {
                //            Amount = AmountCR - AmountDR,
                //            Accountposting1 = "Cr",
                //            ACC_Code = j.ACC_Code,
                //            Narration = j.Narration,
                //        };
                //        Account.closingBalance.Add(models);
                //    }
                //    if (j.AccountType == "Expense")
                //    {
                //        var models = new AccountingPosting
                //        {
                //            Amount = AmountDR - AmountCR,
                //            Accountposting1 = "Dr",
                //            ACC_Code = j.ACC_Code,
                //            Narration = j.Narration,
                //        };
                //        Account.closingBalance.Add(models);
                //    }
                //    if (j.AccountType == "Equity")
                //    {
                //        var models = new AccountingPosting
                //        {
                //            Amount = AmountCR - AmountDR,
                //            Accountposting1 = "Cr",
                //            ACC_Code = j.ACC_Code,
                //            Narration = j.Narration,
                //        };
                //        Account.closingBalance.Add(models);
                //    }
                //    if (accountingPostingslistCR != null)
                //    {
                //        accountingPostingslistCR = new List<AccountingPostingLedgerReport>();
                //    }
                //    if (accountingPostingslistDR != null)
                //    {
                //        accountingPostingslistDR = new List<AccountingPostingLedgerReport>();
                //    }
                //}
                return Account;
            }
            catch (Exception ex)
            {

                return new Accounts();
            }
        }
        //----------------------------Form-Ledger Report-DROPDOWNLIST------------------------------//
        //public List<AccountingPosting> AccountingPostingsDropDownlist()
        //{
        //    try
        //    {
        //        List<AccountingPosting> accountingPostingslist = new List<AccountingPosting>();
        //        var Accountposting = db.ACCOUNTPOSTINGs.ToList();
        //        foreach (var i in Accountposting)
        //        {
        //            var model = new AccountingPosting
        //            {
        //                Form_id = i.Form_id,
        //                RefDocDetails = i.RefDocDetails,
        //                //PostingDate = i.PostingDate,
        //                //Accountposting1 = i.Accountposting1,
        //                //Amount = i.Amount,
        //            };
        //            var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == i.Acc_id);
        //            if (chartofaccount != null)
        //            {
        //                model.Narration = chartofaccount.Narration;
        //                //model.ACC_Code = chartofaccount.ACC_Code;
        //            }
        //            accountingPostingslist.Add(model);
        //        }
        //        return accountingPostingslist;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}


        //----------------------------Form-TRIAL-BALANCE-FILTER-Report-BY-DATE------------------------------//

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public addAmount AccountingPostingsBYDATE(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var accountingPostingslist = new addAmount();
                accountingPostingslist.accountingPosting1s = new List<AccountingPosting1>();
                accountingPostingslist.aMOUNTs = new List<AMOUNT>();

                var Chartofaccount = db.CHARTOFACCOUNTs.ToList();
                foreach (var acc in Chartofaccount)
                {

                    var Accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == acc.Acc_Id && a.Accountposting1 == "Dr").ToList();
                    //if (!string.IsNullOrEmpty(fromDate))
                    //{
                    //    var frmDate = DateTime.ParseExact(fromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    //    Accountposting = Accountposting.Where(a => a.PostingDate > fromDate)
                    //}
                    if (fromDate != null)
                    {
                        Accountposting = Accountposting.Where(a => a.PostingDate >= fromDate).ToList();
                    }
                    if (toDate != null)
                    {
                        Accountposting = Accountposting.Where(a => a.PostingDate <= toDate).ToList();
                    }
                    if (Accountposting != null)
                    {

                        foreach (var i in Accountposting)
                        {
                            var model = new AccountingPosting1
                            {
                                Form_id = i.Form_id,
                                RefDocDetails = i.RefDocDetails,
                                PostingDate = i.PostingDate,
                                Accountposting1 = i.Accountposting1,
                                Acc_id = i.Acc_id,
                                Amount = i.Amount,
                            };
                            accountingPostingslist.accountingPosting1s.Add(model);

                        }
                        var amount1 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount);
                        if (amount1 != 0)
                        {
                            var amount = new AMOUNT()
                            {
                                Amount1 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount),
                                ACC_Code = acc.ACC_Code,
                                Narration = acc.Narration,
                                Accountposting1 = "Dr",
                            };

                            accountingPostingslist.aMOUNTs.Add(amount);
                        }

                    }
                    if (accountingPostingslist.accountingPosting1s != null)
                    {
                        accountingPostingslist.accountingPosting1s = new List<AccountingPosting1>();
                    }

                    //for CR
                    var Accountposting1 = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == acc.Acc_Id && a.Accountposting1 == "Cr").ToList();
                    if (fromDate != null)
                    {
                        Accountposting1 = Accountposting.Where(a => a.PostingDate >= fromDate).ToList();
                    }
                    if (toDate != null)
                    {
                        Accountposting1 = Accountposting.Where(a => a.PostingDate <= toDate).ToList();
                    }
                    if (Accountposting1 != null)
                    {

                        foreach (var i in Accountposting1)
                        {
                            var model = new AccountingPosting1
                            {
                                Form_id = i.Form_id,
                                RefDocDetails = i.RefDocDetails,
                                PostingDate = i.PostingDate,
                                Accountposting1 = i.Accountposting1,
                                Acc_id = i.Acc_id,
                                Amount = i.Amount,
                            };
                            accountingPostingslist.accountingPosting1s.Add(model);

                        }
                        var amount1 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount);
                        if (amount1 != 0)
                        {
                            var amount = new AMOUNT()
                            {
                                Amount2 = accountingPostingslist.accountingPosting1s.Sum(a => a.Amount),
                                ACC_Code = acc.ACC_Code,
                                Narration = acc.Narration,
                                Accountposting1 = "Cr"
                            };

                            accountingPostingslist.aMOUNTs.Add(amount);
                        }

                    }
                    if (accountingPostingslist.accountingPosting1s != null)
                    {
                        accountingPostingslist.accountingPosting1s = new List<AccountingPosting1>();
                    }
                }



                return accountingPostingslist;
            }
            catch (Exception ex)
            {

                return new addAmount();
            }
        }

        //----------------------------For-memorandum-List-Addition----------------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<FORM64PARTICULARDETAILS> memorandumList()
        {
            try
            {
                List<FORM64PARTICULARDETAILS> fRM64PARTICULARDETAILsLIST = new List<FORM64PARTICULARDETAILS>();
                List<FORM64_PARTICULARDETAILS> fORM64_PARTICULARDETAILslist = new List<FORM64_PARTICULARDETAILS>();
                var ChartofAccount = db.CHARTOFACCOUNTs.Where(a => a.AccountType == "Expense" || a.AccountType == "Asset").ToList();
                if(ChartofAccount != null)
                {
                    foreach(var i in ChartofAccount)
                    {
                        var Form64particularDetails = db.FORM64_PARTICULARDETAILS.Where(a => a.Accountcode == i.ACC_Code);
                        if(Form64particularDetails != null)
                        {
                            foreach(var j in Form64particularDetails)
                            {
                                var model = new FORM64_PARTICULARDETAILS
                                {
                                    Accountcode = j.Accountcode,
                                    Narration = j.Narration,
                                    Amount = j.Amount,
                                    Form64_Id = j.Form64_Id,
                                };
                                fORM64_PARTICULARDETAILslist.Add(model);                               
                            }
                            
                        }
                        var Amount1 = fORM64_PARTICULARDETAILslist.Sum(a => a.Amount);
                        if (Amount1 != 0)
                        {
                            var frm64particulardetails = new FORM64PARTICULARDETAILS
                            {
                                
                                Accountcode = i.ACC_Code,
                                Narration = i.Narration,
                                Amount = fORM64_PARTICULARDETAILslist.Sum(a => a.Amount),
                            };
                            fRM64PARTICULARDETAILsLIST.Add(frm64particulardetails);
                        }

                        if (fORM64_PARTICULARDETAILslist != null)
                        {
                            fORM64_PARTICULARDETAILslist = new List<FORM64_PARTICULARDETAILS>();
                        }
                    }
                    
                }
                return fRM64PARTICULARDETAILsLIST;
            }
            catch (Exception ex)
            {

                return new List<FORM64PARTICULARDETAILS>();
            }
        }
        //----------------------------For-memorandum-List-using-Account-Code----------------------------//
        //public List<FORM64PARTICULARDETAILS> memorandumRegisterList(string Accountcode)
        //{
        //    try
        //    {
        //        List<FORM64PARTICULARDETAILS> fRM64PARTICULARDETAILsLIST = new List<FORM64PARTICULARDETAILS>();
        //        var Form64particularDetails = db.FORM64_PARTICULARDETAILS.Where(a=>a.a)
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
        //----------------------------For-FORM-64-REPORT----------------------------//
        //[System.Web.Http.HttpPost]
        ////[BasicAuthentication]
        //public List<SelectPaymentVoucher11> SelectmemorandumregisterList()
        //{
        //    try
        //    {

        //        List<SelectPaymentVoucher11> selectfORM64PAYMENTVOUCHERslist = new List<SelectPaymentVoucher11>();
               
        //        var paymentvoucher = db.FORM64_PAYMENTVOUCHER.ToList();

        //        foreach (var i in paymentvoucher)
        //        {
        //            var model = new SelectPaymentVoucher11
        //            {
        //                Form64_Id = i.Form64_Id,
        //                GrossAmtPayable = i.GrossAmtPayable,
        //                Net_AmtPayable = i.Net_AmtPayable,
        //                TotalDeduction = i.TotalDeduction,
        //                Date = i.Date,
        //                NamePayeeVendorContractor = i.NamePayeeVendorContractor,
        //            };
        //            var vendormastr = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == i.NamePayeeVendorContractor);
        //            if(vendormastr != null)
        //            {
        //                model.GSTINNO = vendormastr.GISTINNo;
        //            }
        //            var chartofaccount = db.CHARTOFACCOUNTs.Where(a => a.AccountType == "Expense" || a.AccountType == "Asset");
                   
        //            foreach(var j in chartofaccount)
        //            {
        //                var paymentparticularDetail = db.FORM64_PARTICULARDETAILS.FirstOrDefault(a => a.Form64_Id == i.Form64_Id && a.Accountcode == j.ACC_Code);
        //                if (paymentparticularDetail != null)
        //                {
        //                    model.Form64_ParticularDetailID = paymentparticularDetail.Form64_ParticularDetailID;
        //                    model.Narration = paymentparticularDetail.Narration;
        //                    model.Accountcode = paymentparticularDetail.Accountcode;
        //                    model.Amount = paymentparticularDetail.Amount;
        //                    model.Description = paymentparticularDetail.Description;
        //                }
        //            }
        //            var chartofaccountdeduction = db.CHARTOFACCOUNTs.Where(a => a.ObjectCode == "3670" && a.AccTypeId == 3);
        //            if(chartofaccountdeduction != null)
        //            {
        //                foreach(var k in chartofaccountdeduction)
        //                {
        //                    var TDSparticulardetail = db.FORM64_PARTICULARDETAILS.FirstOrDefault(a => a.Narration == k.Narration && a.Form64_Id == i.Form64_Id);
        //                    if(TDSparticulardetail != null)
        //                    {
        //                        if(TDSparticulardetail.Narration == "TDS")
        //                        {
        //                            model.TDSAmount = TDSparticulardetail.Amount;
        //                        }
        //                        if (TDSparticulardetail.Narration == "GST")
        //                        {
        //                            model.GSTAmount = TDSparticulardetail.Amount;
        //                        }
        //                        if (TDSparticulardetail.Narration == "BIMA")
        //                        {
        //                            model.BIMAAmount = TDSparticulardetail.Amount;
        //                        }
        //                        if (TDSparticulardetail.Narration == "Royalty")
        //                        {
        //                            model.ROYALTYAmount = TDSparticulardetail.Amount;
        //                        }
        //                        if (TDSparticulardetail.Narration == "Security Deposit")
        //                        {
        //                            model.SDAmount = TDSparticulardetail.Amount;
        //                        }
        //                    }

        //                }
        //            }

        //            selectfORM64PAYMENTVOUCHERslist.Add(model);
        //        }

        //        return selectfORM64PAYMENTVOUCHERslist;
        //    }
        //    catch (Exception ex)
        //    {

        //       return new List<SelectPaymentVoucher11>();
        //    }
        //}

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<SelectPaymentVoucher11> SelectmemorandumregisterListBYSESSION(string Session)
        {
            try
            {

                List<SelectPaymentVoucher11> selectfORM64PAYMENTVOUCHERslist = new List<SelectPaymentVoucher11>();
                var SessionMaster = db.SESSION_MSTR.FirstOrDefault(a=>a.Session == Session);               
                var paymentvoucher = db.FORM64_PAYMENTVOUCHER.Where(a=>a.Date >= SessionMaster.StartDate && a.Date <= SessionMaster.EndDate).ToList();

                foreach (var i in paymentvoucher)
                {
                    var model = new SelectPaymentVoucher11
                    {
                        Form64_Id = i.Form64_Id,
                        GrossAmtPayable = i.GrossAmtPayable,
                        Net_AmtPayable = i.Net_AmtPayable,
                        TotalDeduction = i.TotalDeduction,
                        Date = i.Date,
                        NamePayeeVendorContractor = i.NamePayeeVendorContractor,
                    };
                    var vendormastr = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == i.NamePayeeVendorContractor);
                    if (vendormastr != null)
                    {
                        model.GSTINNO = vendormastr.GISTINNo;
                    }
                    var chartofaccount = db.CHARTOFACCOUNTs.Where(a => a.AccountType == "Expense" || a.AccountType == "Asset");

                    foreach (var j in chartofaccount)
                    {
                        var paymentparticularDetail = db.FORM64_PARTICULARDETAILS.FirstOrDefault(a => a.Form64_Id == i.Form64_Id && a.Accountcode == j.ACC_Code);
                        if (paymentparticularDetail != null)
                        {
                            model.Form64_ParticularDetailID = paymentparticularDetail.Form64_ParticularDetailID;
                            model.Narration = paymentparticularDetail.Narration;
                            model.Accountcode = paymentparticularDetail.Accountcode;
                            model.Amount = paymentparticularDetail.Amount;
                            model.Description = paymentparticularDetail.Description;
                        }
                    }
                    var chartofaccountdeduction = db.CHARTOFACCOUNTs.Where(a => a.ObjectCode == "3670" && a.AccTypeId == 3);
                    if (chartofaccountdeduction != null)
                    {
                        foreach (var k in chartofaccountdeduction)
                        {
                            var TDSparticulardetail = db.FORM64_DEDUCTION.FirstOrDefault(a => a.Narration == k.Narration && a.Form64_Id == i.Form64_Id);
                            if (TDSparticulardetail != null)
                            {
                                if (TDSparticulardetail.Narration == "TDS")
                                {
                                    model.TDSAmount = TDSparticulardetail.Amount;
                                }
                                if (TDSparticulardetail.Narration == "SGST")
                                {
                                    model.SGSTAmount = TDSparticulardetail.Amount;
                                }
                                if (TDSparticulardetail.Narration == "CGST")
                                {
                                    model.CGSTAmount = TDSparticulardetail.Amount;
                                }
                                if (TDSparticulardetail.Narration == "IGST")
                                {
                                    model.IGSTAmount = TDSparticulardetail.Amount;
                                }
                                if (TDSparticulardetail.Narration == "BIMA")
                                {
                                    model.BIMAAmount = TDSparticulardetail.Amount;
                                }
                                if (TDSparticulardetail.Narration == "Royalty")
                                {
                                    model.ROYALTYAmount = TDSparticulardetail.Amount;
                                }
                                if (TDSparticulardetail.Narration == "Security Deposit")
                                {
                                    model.SDAmount = TDSparticulardetail.Amount;
                                }
                            }

                        }
                    }

                    selectfORM64PAYMENTVOUCHERslist.Add(model);
                }

                return selectfORM64PAYMENTVOUCHERslist;
            }
            catch (Exception ex)
            {

                return new List<SelectPaymentVoucher11>();
            }
        }

        //----------------------------For-FORM-CASH-BOOK-REPORT----------------------------//
        //[System.Web.Http.HttpPost]
        //[BasicAuthentication]
        //public List<ACCOUNTPOSTINGFORCASHBOOK> SelectFORM64CASHBOOKLIST()
        //{
        //    try
        //    {

        //        List<ACCOUNTPOSTINGFORCASHBOOK> ACCOUNTPOSTINGlist = new List<ACCOUNTPOSTINGFORCASHBOOK>();
               
        //        var CHARTOFACCOUNT = db.CHARTOFACCOUNTs.Where(a => a.ObjectCode == "4810");
        //        foreach(var i in CHARTOFACCOUNT)
        //        {
        //            var accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == i.Acc_Id);
        //            if(accountposting != null)
        //            {
        //                foreach(var j in accountposting)
        //                {
        //                    var model = new ACCOUNTPOSTINGFORCASHBOOK
        //                    {
        //                        Particular = i.Narration,
        //                        AccountCode = i.ACC_Code,
        //                        Amount = j.Amount,
        //                        Accountposting1 = j.Accountposting1,
        //                        PostingDate = j.PostingDate,
        //                        Description = j.Description,
        //                    };
        //                    var FORMID = j.Form_id;
        //                    if (FORMID == 101)
        //                    {
        //                        var Form64paymentvoucher = db.FORM64_PAYMENTVOUCHER.FirstOrDefault(a => a.Form64_Id == j.RefDocDetails);
        //                        if (Form64paymentvoucher != null)
        //                        {
        //                            model.PSNNO = Form64paymentvoucher.PSN;
        //                            model.VendorName = Form64paymentvoucher.NamePayeeVendorContractor;
        //                        }
        //                    }
        //                    if (FORMID == 104)
        //                    {
        //                        var journalvoucher = db.JOURNALVOUCHERs.FirstOrDefault(a => a.JV_Id == j.RefDocDetails);
        //                        if (journalvoucher != null)
        //                        {
        //                            model.PSNNO = journalvoucher.JV_Id;
        //                        };
        //                    }
        //                    ACCOUNTPOSTINGlist.Add(model);
        //                }
        //            }

        //        }
               
        //        return ACCOUNTPOSTINGlist;
        //    }
        //    catch (Exception ex)
        //    {

        //        return new List<ACCOUNTPOSTINGFORCASHBOOK>();
        //    };
        //}

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<ACCOUNTPOSTINGFORCASHBOOK> SelectFORM64CASHBOOKLISTBYSESSION(string Session)
        {
            try
            {

                List<ACCOUNTPOSTINGFORCASHBOOK> ACCOUNTPOSTINGlist = new List<ACCOUNTPOSTINGFORCASHBOOK>();               
                var CHARTOFACCOUNT = db.CHARTOFACCOUNTs.Where(a => a.ObjectCode == "4810");
               var sessionmaster = db.SESSION_MSTR.FirstOrDefault(a => a.Session == Session);
                foreach (var i in CHARTOFACCOUNT)
                {
                    var accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == i.Acc_Id && a.PostingDate >= sessionmaster.StartDate && a.PostingDate <= sessionmaster.EndDate);
                    if (accountposting != null)
                    {
                        foreach (var j in accountposting)
                        {
                            var model = new ACCOUNTPOSTINGFORCASHBOOK
                            {
                                Particular = i.Narration,
                                AccountCode = i.ACC_Code,
                                Amount = j.Amount,
                                Accountposting1 = j.Accountposting1,
                                PostingDate = j.PostingDate,
                                Description = j.Description,
                                RefDocDetails = j.RefDocDetails,
                                Form_id = j.Form_id,
                            };
                            var FORMID = j.Form_id;
                            if (FORMID == 101)
                            {
                                var Form64paymentvoucher = db.FORM64_PAYMENTVOUCHER.FirstOrDefault(a => a.Form64_Id == j.RefDocDetails);
                                if (Form64paymentvoucher != null)
                                {
                                    model.PSNNO = Form64paymentvoucher.Form64_Id;
                                    model.VendorName = Form64paymentvoucher.NamePayeeVendorContractor;
                                }
                            }
                            if (FORMID == 104)
                            {
                                var journalvoucher = db.JOURNALVOUCHERs.FirstOrDefault(a => a.JV_Id == j.RefDocDetails);
                                if (journalvoucher != null)
                                {
                                    model.PSNNO = journalvoucher.JV_Id;
                                };
                            }
                            ACCOUNTPOSTINGlist.Add(model);
                        }
                    }

                }

                return ACCOUNTPOSTINGlist;
            }
            catch (Exception ex)
            {

                return new List<ACCOUNTPOSTINGFORCASHBOOK>();
            }
        }

       // ----------------------------For-FORM-BANK--REPORT----------------------------//
        //[System.Web.Http.HttpPost]
        //[BasicAuthentication]
        //public List<ACCOUNTPOSTINGFORCASHBOOK> SelectFORM64BANKBOOKLIST()
        //{
        //    try
        //    {

        //        List<ACCOUNTPOSTINGFORCASHBOOK> ACCOUNTPOSTINGlist = new List<ACCOUNTPOSTINGFORCASHBOOK>();
              
        //        var CHARTOFACCOUNT = db.CHARTOFACCOUNTs.Where(a => a.ObjectCode == "4820");
        //        foreach (var i in CHARTOFACCOUNT)
        //        {
        //            var accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == i.Acc_Id);
        //            if (accountposting != null)
        //            {
        //                foreach (var j in accountposting)
        //                {
        //                    var model = new ACCOUNTPOSTINGFORCASHBOOK
        //                    {
        //                        Particular = i.Narration,
        //                        AccountCode = i.ACC_Code,
        //                        Amount = j.Amount,
        //                        Accountposting1 = j.Accountposting1,
        //                        PostingDate = j.PostingDate,
        //                        Description = j.Description,
        //                    };
        //                    var FORMID = j.Form_id;
        //                    if(FORMID == 101)
        //                    {
        //                        var Form64paymentvoucher = db.FORM64_PAYMENTVOUCHER.FirstOrDefault(a => a.Form64_Id == j.RefDocDetails);
        //                        if (Form64paymentvoucher != null)
        //                        {
        //                            model.PSNNO = Form64paymentvoucher.PSN;
        //                            model.VendorName = Form64paymentvoucher.NamePayeeVendorContractor;
        //                            model.ChequeNo = Form64paymentvoucher.ChequeNo;
        //                        }
        //                    }
        //                    if(FORMID == 104)
        //                    {
        //                        var journalvoucher = db.JOURNALVOUCHERs.FirstOrDefault(a => a.JV_Id == j.RefDocDetails);
        //                        if(journalvoucher != null)
        //                        {
        //                            model.PSNNO = journalvoucher.JV_Id;
        //                        };
        //                    }
                            
        //                    ACCOUNTPOSTINGlist.Add(model);
        //                }
        //            }
        //        }

        //        return ACCOUNTPOSTINGlist;
        //    }
        //    catch (Exception ex)
        //    {

        //        return new List<ACCOUNTPOSTINGFORCASHBOOK>();
        //    };
        //}

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<ACCOUNTPOSTINGFORCASHBOOK> SelectFORM64BANKBOOKLISTBYSession(string Session)
        {
            try
            {

                List<ACCOUNTPOSTINGFORCASHBOOK> ACCOUNTPOSTINGlist = new List<ACCOUNTPOSTINGFORCASHBOOK>();

                var CHARTOFACCOUNT = db.CHARTOFACCOUNTs.Where(a =>a.ObjectCode == "4820" || a.ObjectCode == "4821" || a.ObjectCode == "4822" || a.ObjectCode == "4823");
                var sessionmaster = db.SESSION_MSTR.FirstOrDefault(a => a.Session == Session);
                foreach (var i in CHARTOFACCOUNT)
                {
                    var accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Acc_id == i.Acc_Id && a.PostingDate >= sessionmaster.StartDate && a.PostingDate <= sessionmaster.EndDate);
                    if (accountposting != null)
                    {
                        foreach (var j in accountposting)
                        {
                            var model = new ACCOUNTPOSTINGFORCASHBOOK
                            {
                                Particular = i.Narration,
                                AccountCode = i.ACC_Code,
                                Amount = j.Amount,
                                Accountposting1 = j.Accountposting1,
                                PostingDate = j.PostingDate,
                                Description = j.Description,
                                RefDocDetails = j.RefDocDetails,
                                Form_id = j.Form_id,
                            };
                            var FORMID = j.Form_id;
                            if (FORMID == 101)
                            {
                                var Form64paymentvoucher = db.FORM64_PAYMENTVOUCHER.FirstOrDefault(a => a.Form64_Id == j.RefDocDetails);
                                if (Form64paymentvoucher != null)
                                {
                                    model.PSNNO = Form64paymentvoucher.Form64_Id;
                                    model.VendorName = Form64paymentvoucher.NamePayeeVendorContractor;
                                    model.ChequeNo = Form64paymentvoucher.ChequeNo;
                                }
                            }
                            if (FORMID == 104)
                            {
                                var journalvoucher = db.JOURNALVOUCHERs.FirstOrDefault(a => a.JV_Id == j.RefDocDetails);
                                if (journalvoucher != null)
                                {
                                    model.PSNNO = journalvoucher.JV_Id;
                                };
                            }

                            ACCOUNTPOSTINGlist.Add(model);
                        }
                    }
                }

                return ACCOUNTPOSTINGlist;
            }
            catch (Exception ex)
            {

                return new List<ACCOUNTPOSTINGFORCASHBOOK>();
            }
        }

        //----------------------------For-FORM-TDS-REPORT----------------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<SelectPaymentVoucher11> SelectFORM64TDSLIST()
        {
            try
            {

                List<SelectPaymentVoucher11> selectfORM64PAYMENTVOUCHERslist = new List<SelectPaymentVoucher11>();
                

                var paymentvoucher = db.FORM64_PAYMENTVOUCHER.ToList();

                foreach (var i in paymentvoucher)
                {
                    var model = new SelectPaymentVoucher11
                    {
                        Form64_Id = i.Form64_Id,
                        GrossAmtPayable = i.GrossAmtPayable,
                        Net_AmtPayable = i.Net_AmtPayable,
                        TotalDeduction = i.TotalDeduction,
                        Date = i.Date,
                        NamePayeeVendorContractor = i.NamePayeeVendorContractor,
                    };
                    var vendormastr = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == i.NamePayeeVendorContractor);
                    if (vendormastr != null)
                    {
                        model.GSTINNO = vendormastr.GISTINNo;
                    }
                    var chartofaccount = db.CHARTOFACCOUNTs.Where(a => a.AccountType == "Expense" || a.AccountType == "Asset");

                    foreach (var j in chartofaccount)
                    {
                        var paymentparticularDetail = db.FORM64_PARTICULARDETAILS.FirstOrDefault(a => a.Form64_Id == i.Form64_Id && a.Accountcode == j.ACC_Code);
                        if (paymentparticularDetail != null)
                        {
                            model.Form64_ParticularDetailID = paymentparticularDetail.Form64_ParticularDetailID;
                            model.Narration = paymentparticularDetail.Narration;
                            model.Accountcode = paymentparticularDetail.Accountcode;
                            model.Amount = paymentparticularDetail.Amount;
                            model.Description = paymentparticularDetail.Description;
                        }
                    }
                    selectfORM64PAYMENTVOUCHERslist.Add(model);
                }

                return selectfORM64PAYMENTVOUCHERslist;
            }
            catch (Exception ex)
            {

                return new List<SelectPaymentVoucher11>();
            }
        }
        //----------------------------For-Company-mstr----------------------------//
        //[BasicAuthentication]
        public string saveCompanymaster(CompanyMSTR model)
        {
            try
            {
                if(model.CompanyId <= 0)
                {
                    var CompanyMaster = db.COMPANY_MSTR.ToList();
                    if (CompanyMaster.Count == 0)
                    {
                        var savecompany = new COMPANY_MSTR
                        {
                            Comp_code = model.Comp_code,
                            Company_Name = model.Company_Name,
                            Address = model.Address,
                            Pincode = model.Pincode,
                            City = model.City,
                            States = model.States,
                            EMail = model.EMail,
                            Mobile_number = model.Mobile_number,
                            Pancard = model.Pancard,
                            GST_Number = model.GST_Number,
                            Company_Type = model.Company_Type,
                            Company_Formation_Date = model.Company_Formation_Date,
                            CIN_Number = model.CIN_Number,
                            Validfrom = model.Validfrom,
                            ValidTo = model.ValidTo,
                            No_User_Allowed = model.No_User_Allowed,
                            Status = model.Status,
                            Username = model.Username,
                            Password = model.Password,
                            Role_assign = model.Role_assign,
                            Image = model.Image,
                        };
                        db.COMPANY_MSTR.Add(savecompany);
                        db.SaveChanges();
                        return "Save";
                    }
                    return "Company Already Exist";
                }
                else
                {
                    var Companymaster = db.COMPANY_MSTR.FirstOrDefault(a => a.CompanyId == model.CompanyId);
                    if(Companymaster != null)
                    {
                        Companymaster.Comp_code = model.Comp_code;
                        Companymaster.Company_Name = model.Company_Name;
                        Companymaster.Address = model.Address;
                        Companymaster.Pincode = model.Pincode;
                        Companymaster.City = model.City;
                        Companymaster.States = model.States;
                        Companymaster.EMail = model.EMail;
                        Companymaster.Mobile_number = model.Mobile_number;
                        Companymaster.Pancard = model.Pancard;
                        Companymaster.GST_Number = model.GST_Number;
                        Companymaster.Company_Type = model.Company_Type;
                        Companymaster.Company_Formation_Date = model.Company_Formation_Date;
                        Companymaster.CIN_Number = model.CIN_Number;
                        Companymaster.ValidTo = model.ValidTo;
                        Companymaster.Validfrom = model.Validfrom;
                        Companymaster.No_User_Allowed = model.No_User_Allowed;
                        Companymaster.Status = model.Status;
                        Companymaster.Username = model.Username;
                        Companymaster.Password = model.Password;
                        Companymaster.Role_assign = model.Role_assign;
                        Companymaster.Image = model.Image;
                        db.SaveChanges();
                        return "Done";
                    }
                }
                return "Error";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message ;
            }
        }
        //----------------------------For-show-Company-mstr----------------------------//
        //[BasicAuthentication]
        public List<CompanyMSTR> ShowcompanyMSTRslist()
        {
            try
            {
                List<CompanyMSTR> companyMSTRslist = new List<CompanyMSTR>();
                var comapanymaster = db.COMPANY_MSTR.ToList();
                foreach(var i in comapanymaster)
                {
                    var model = new CompanyMSTR
                    {
                        CompanyId = i.CompanyId,
                        Comp_code = i.Comp_code,
                        Company_Name = i.Company_Name,
                        Address= i.Address,
                        Pincode = i.Pincode,
                        City = i.City,
                        States = i.States,
                        EMail = i.EMail,
                        Mobile_number = i.Mobile_number,
                        Pancard = i.Pancard,
                        GST_Number = i.GST_Number,
                        Company_Type = i.Company_Type,
                        Company_Formation_Date = i.Company_Formation_Date,
                        CIN_Number = i.CIN_Number,
                        ValidTo = i.ValidTo,
                        Validfrom = i.Validfrom,
                        No_User_Allowed = i.No_User_Allowed,
                        Status = i.Status,
                        Username = i.Username,
                        Password = i.Password,
                        Role_assign = i.Role_assign,
                        Image = i.Image,
                    };
                    companyMSTRslist.Add(model);
                };
                return companyMSTRslist;
            }
            catch (Exception ex)
            {

                return new List<CompanyMSTR>();
            }
        }
        public List<CompanyMSTR> SelectcompanyMSTRslist(int companyid)
        {
            try
            {
                List<CompanyMSTR> companyMSTRslist = new List<CompanyMSTR>();
                var comapanymaster = db.COMPANY_MSTR.FirstOrDefault(a => a.CompanyId == companyid);

                var model = new CompanyMSTR
                {
                    CompanyId = comapanymaster.CompanyId,
                    Comp_code = comapanymaster.Comp_code,
                    Company_Name = comapanymaster.Company_Name,
                    Address = comapanymaster.Address,
                    Pincode = comapanymaster.Pincode,
                    City = comapanymaster.City,
                    States = comapanymaster.States,
                    EMail = comapanymaster.EMail,
                    Mobile_number = comapanymaster.Mobile_number,
                    Pancard = comapanymaster.Pancard,
                    GST_Number = comapanymaster.GST_Number,
                    Company_Type = comapanymaster.Company_Type,
                    Company_Formation_Date = comapanymaster.Company_Formation_Date,
                    CIN_Number = comapanymaster.CIN_Number,
                    ValidTo = comapanymaster.ValidTo,
                    Validfrom = comapanymaster.Validfrom,
                    No_User_Allowed = comapanymaster.No_User_Allowed,
                    Status = comapanymaster.Status,
                    Username = comapanymaster.Username,
                    Password = comapanymaster.Password,
                    Role_assign = comapanymaster.Role_assign,
                    Image = comapanymaster.Image,
                };
                companyMSTRslist.Add(model);                
                return companyMSTRslist;
            }
            catch (Exception ex)
            {

                return new List<CompanyMSTR>();
            }
        }

        //----------------------------For-OPENING-BALANCE----------------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<sesseionmstr> sesseionmstrslist()
        {
            try
            {
                List<sesseionmstr> sesseionmstrslist = new List<sesseionmstr>();
                var sessionmaster = db.SESSION_MSTR.ToList();
                foreach(var i in sessionmaster)
                {
                    var model = new sesseionmstr
                    {
                        Session = i.Session,
                    };
                    sesseionmstrslist.Add(model);
                }
                return sesseionmstrslist;
            }
            catch (Exception ex)
            {

                return new List<sesseionmstr>();
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<ChartOfAccount> SelectNarrationforOB()
        {
            try
            {
                var chartofaccont = db.CHARTOFACCOUNTs.Where(a => a.AccountType == "Liability" || a.AccountType == "Asset" || a.AccountType == "Equity").ToList();
                List <ChartOfAccount> chartOfAccountslist = new List<ChartOfAccount>();
                foreach (var i in chartofaccont)
                {
                    var model = new ChartOfAccount
                    {
                        Acc_Id = i.Acc_Id,
                        Narration = i.Narration,
                        ACC_Code = i.ACC_Code,
                    };
                    chartOfAccountslist.Add(model);
                }
                return chartOfAccountslist;
            }
            catch (Exception ex)
            {

                return new List<ChartOfAccount>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public object SelectAccCode(int Acc_Id)
        {
            try
            {
                var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == Acc_Id);
                var model = new ChartOfAccount
                {
                    ACC_Code = chartofaccount.ACC_Code,
                };
             return model;
            }
            catch (Exception ex)
            {

                return new ChartOfAccount();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string SaveOpeningBalance(AccountingPostingOpeningbalance model)
        {
            try
            {
                var AccountpostingOpeningBal = new AccountingPostingOpeningbalance();
                AccountpostingOpeningBal.accountingPostings = new List<AccountingPosting>();
                if(model.CreationDate == null)
                {
                    return "Date is not send";
                }
                //var sessionmastr = db.SESSION_MSTR.FirstOrDefault(a => a.StartDate >= model.CreationDate);
                if(model.OPBID <= 0) {
                    var OLDopeningbalance = db.OPENING_BALANCE.FirstOrDefault(a => a.YearId == model.YearId);
                    if(OLDopeningbalance != null)
                    {
                        return "Opening Balance for this year Already Exist";
                    }
                    var openingbalance = new OPENING_BALANCE
                    {
                        CreationDate = model.CreationDate,
                        YearId = model.YearId,
                        Remarks = model.Remarks,
                        TotalDr = model.TotalDr,
                        TotalCr = model.TotalCr,
                    };
                    db.OPENING_BALANCE.Add(openingbalance);
                    db.SaveChanges();
                    var OPBid = openingbalance.OPBID;

                foreach (var i in model.accountingPostings)
                {                  

                    var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.ACC_Code == i.ACC_Code);
                    //var AccountPosting = "";
                  
                    //if (chartofaccount.AccountType == "Asset")
                    //{
                    //    AccountPosting = "Dr";
                    //}
                    //if (chartofaccount.AccountType == "Liability")
                    //{
                    //    AccountPosting = "Cr";
                    //}                    
                    //if (chartofaccount.AccountType == "Equity")
                    //{
                    //    AccountPosting = "Cr";
                    //}
                    var accountposting = new ACCOUNTPOSTING
                    {
                        Form_id = 105,
                        Acc_id = chartofaccount.Acc_Id,
                        PostingDate = model.CreationDate,
                        Accountposting1 = i.Accountposting1,
                        Amount = i.Amount,
                        RefDocDetails = OPBid,  
                        Description = i.Description,
                    };
                    db.ACCOUNTPOSTINGs.Add(accountposting);
                    db.SaveChanges();
                        var vendorMaster = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == chartofaccount.Narration);
                        if (vendorMaster != null)
                        {
                            vendorMaster.Status = vendorMaster.Status + 1;
                            db.SaveChanges();
                        }
                    }
                  
                return "save";
                }
                else
                {
                    var OLDopeningBalance = db.OPENING_BALANCE.FirstOrDefault(a => a.OPBID == model.OPBID);
                    if (OLDopeningBalance != null)
                    {
                        //OLDopeningBalance.CreationDate = model.CreationDate;
                        OLDopeningBalance.TotalCr = model.TotalCr;
                        OLDopeningBalance.TotalDr = model.TotalDr;
                        OLDopeningBalance.YearId = model.YearId;
                        OLDopeningBalance.Remarks = model.Remarks;
                        db.SaveChanges();
                        foreach (var i in model.accountingPostings)
                        {
                            if (i.Accountposting_Id > 0)
                            {
                                var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.ACC_Code == i.ACC_Code);
                                
                                var accountpost = db.ACCOUNTPOSTINGs.FirstOrDefault(a => a.Accountposting_Id == i.Accountposting_Id);
                                if (accountpost != null)
                                {
                                    if (accountpost.Acc_id == chartofaccount.Acc_Id)
                                    {
                                        accountpost.Accountposting1 = i.Accountposting1;
                                        accountpost.Amount = i.Amount;
                                        accountpost.Description = i.Description;
                                        db.SaveChanges();
                                    }
                                    else
                                    {
                                       var OlDChartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == accountpost.Acc_id);
                                        var OldVendor = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == OlDChartofaccount.Narration);
                                        var NewVendor = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == chartofaccount.Narration);
                                        if (OldVendor != null)
                                        {
                                           if(OldVendor.Status > 0)
                                            {
                                                OldVendor.Status = OldVendor.Status - 1;
                                                db.SaveChanges();
                                            }
                                        }
                                        if (NewVendor != null)
                                        {

                                            NewVendor.Status = OldVendor.Status + 1;
                                                db.SaveChanges();
                                           
                                        }
                                        accountpost.Acc_id = chartofaccount.Acc_Id;
                                        accountpost.Accountposting1 = i.Accountposting1;
                                        accountpost.Amount = i.Amount;
                                        accountpost.Description = i.Description;
                                        db.SaveChanges();
                                    }
                                                                   
                                }
                               
                            }
                            else
                            {
                                var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.ACC_Code == i.ACC_Code);
                                //var AccountPosting = "";

                                //if (chartofaccount.AccountType == "Asset")
                                //{
                                //    AccountPosting = "Dr";
                                //}
                                //if (chartofaccount.AccountType == "Liability")
                                //{
                                //    AccountPosting = "Cr";
                                //}
                                //if (chartofaccount.AccountType == "Equity")
                                //{
                                //    AccountPosting = "Cr";
                                //}
                                var accountposting = new ACCOUNTPOSTING
                                {
                                    Form_id = 105,
                                    Acc_id = chartofaccount.Acc_Id,
                                    PostingDate = model.CreationDate,
                                    Accountposting1 = i.Accountposting1,
                                    Amount = i.Amount,
                                    RefDocDetails = model.OPBID,
                                    Description = i.Description,
                                };
                                db.ACCOUNTPOSTINGs.Add(accountposting);
                                db.SaveChanges();
                                var vendorMaster = db.VENDOR_MSTR.FirstOrDefault(a => a.VendorName == chartofaccount.Narration);
                                if (vendorMaster != null)
                                {
                                    vendorMaster.Status = vendorMaster.Status + 1;
                                    db.SaveChanges();
                                }
                            }
                            
                        }
                        return "Done";
                    }
                    return "ERROR";
                }
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<OPENING_BALANCE> OpeningBalanceList()
        {
            try
            {
                List<OPENING_BALANCE> oPENING_BALANCEslist = new List<OPENING_BALANCE>();
                var openiningbalance = db.OPENING_BALANCE.ToList();
                foreach(var i in openiningbalance)
                {
                    var model = new OPENING_BALANCE
                    {
                        OPBID = i.OPBID,
                        YearId = i.YearId,
                    };
                    oPENING_BALANCEslist.Add(model);                
                }
                return oPENING_BALANCEslist;
            }
            catch (Exception ex)
            {

                return new List<OPENING_BALANCE>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<OPENING_BALANCE> OpeningBalanceListBYSESSION(string Session)
        {
            try
            {
                List<OPENING_BALANCE> oPENING_BALANCEslist = new List<OPENING_BALANCE>();
                var SessionMaster = db.SESSION_MSTR.FirstOrDefault(a => a.Session == Session);
                var openiningbalance = db.OPENING_BALANCE.Where(a=>a.CreationDate >= SessionMaster.StartDate && a.CreationDate <= SessionMaster.EndDate).ToList();
                foreach (var i in openiningbalance)
                {
                    var model = new OPENING_BALANCE
                    {
                        OPBID = i.OPBID,
                        YearId = i.YearId,
                    };
                    oPENING_BALANCEslist.Add(model);
                }
                return oPENING_BALANCEslist;
            }
            catch (Exception ex)
            {

                return new List<OPENING_BALANCE>();
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string DeleteOpeningBalance(int OPBID)
        {
            try
            {
                var OpeningBalance = db.OPENING_BALANCE.FirstOrDefault(a => a.OPBID == OPBID);
                var Accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Form_id == 105 && a.RefDocDetails == OPBID).ToList();
                foreach(var i in Accountposting)
                {
                    var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == i.Acc_id);
                    var vendormaster = db.VENDOR_MSTR.FirstOrDefault(a => a.Vendor_Id == chartofaccount.Acc_subtype_Id);
                    if(vendormaster != null)
                    {
                        vendormaster.Status = vendormaster.Status - 1;
                        db.SaveChanges();
                    }
                }

                if(OpeningBalance != null)
                {
                    db.OPENING_BALANCE.Remove(OpeningBalance);
                    db.ACCOUNTPOSTINGs.RemoveRange(Accountposting);
                    db.SaveChanges();
                    return "Deleted Successfully";
                }
                return "Error";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public object DeleteOPBbyACCPostINGID(List<AccountingPostingID> model)
        {
            try
            {
                foreach(var i in model)
                {
                    var Accountposting = db.ACCOUNTPOSTINGs.FirstOrDefault(a => a.Form_id == 105 && a.Accountposting_Id == i.Accountposting_Id);
                    if(Accountposting != null)
                    {
                        var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == Accountposting.Acc_id);
                        var vendormaster = db.VENDOR_MSTR.FirstOrDefault(a => a.Vendor_Id == chartofaccount.Acc_subtype_Id);
                        if (vendormaster != null)
                        {
                            vendormaster.Status = vendormaster.Status - 1;
                            db.SaveChanges();
                        }

                        if (Accountposting != null)
                        {
                            db.ACCOUNTPOSTINGs.Remove(Accountposting);
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        return "Accountposting Id Is NA";
                    }
                        
                }
               
                 return "Deleted Successfully"; 
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }



        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public object UpdateOPBTotal(OpeningBalance model)
        {
            try
            {
                var openingBalance = db.OPENING_BALANCE.FirstOrDefault(a => a.OPBID == model.OPBID);
                if(openingBalance != null)
                {
                    openingBalance.TotalCr = model.TotalCr;
                    openingBalance.TotalDr = model.TotalDr;
                    db.SaveChanges();
                    return "Updatede Successfully";
                }
                return "OPBID does not match";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public AccountingPostingOpeningbalance SelectOpeningbalance(int OPBID)
        {
            try
            {
                var openingbalancelist = new AccountingPostingOpeningbalance();
                openingbalancelist.accountingPostings = new List<AccountingPosting>();
                var openingbalance = db.OPENING_BALANCE.FirstOrDefault(a => a.OPBID == OPBID);
                if(openingbalance != null)
                {

                    openingbalancelist.OPBID = openingbalance.OPBID;
                    openingbalancelist.CreationDate = openingbalance.CreationDate;
                    openingbalancelist.YearId = openingbalance.YearId;
                    openingbalancelist.TotalCr = openingbalance.TotalCr;
                    openingbalancelist.TotalDr = openingbalance.TotalDr;
                    openingbalancelist.Remarks = openingbalance.Remarks;

                   var accountposting = db.ACCOUNTPOSTINGs.Where(a => a.Form_id == 105 && a.RefDocDetails == OPBID);
                    var chartofaccountliability = db.CHARTOFACCOUNTs.Where(a => a.AccountType == "Liability").ToList();
                    var chartofaccountasset = db.CHARTOFACCOUNTs.Where(a => a.AccountType == "Asset").ToList();
                    foreach(var liability in chartofaccountliability)
                    {
                        var Accountpostingliability = accountposting.Where(a => a.Acc_id == liability.Acc_Id);
                        foreach (var i in Accountpostingliability)
                        {
                            var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == i.Acc_id && a.AccountType == "Liability");
                            var accoutpost = new AccountingPosting
                            {
                                Accountposting_Id = i.Accountposting_Id,
                                Narration = chartofaccount.Narration,
                                ACC_Code = chartofaccount.ACC_Code,
                                Amount = i.Amount,
                                Accountposting1 = i.Accountposting1,
                                Description = i.Description,
                            };
                            openingbalancelist.accountingPostings.Add(accoutpost);
                        }
                    }
                    
                    foreach(var asset in chartofaccountasset)
                    {
                        var Accountpostingasset = accountposting.Where(a => a.Acc_id == asset.Acc_Id).ToList();
                        foreach (var i in Accountpostingasset)
                        {
                            var chartofaccount = db.CHARTOFACCOUNTs.FirstOrDefault(a => a.Acc_Id == i.Acc_id && a.AccountType == "Asset");
                            var accoutpost = new AccountingPosting
                            {
                                Accountposting_Id = i.Accountposting_Id,
                                Narration = chartofaccount.Narration,
                                ACC_Code = chartofaccount.ACC_Code,
                                Amount = i.Amount,
                                Accountposting1 = i.Accountposting1,
                                Description = i.Description,
                            };
                            openingbalancelist.accountingPostings.Add(accoutpost);
                        }
                    }
                    
                    
                }
                return openingbalancelist;
            }
            catch (Exception ex)
            {

                return new AccountingPostingOpeningbalance();
            }
        }

        //----------------------------For-user-mstr----------------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string SaveUserMaster(UserMstr model)
        {
            try
            {
                if(model.User_Id <= 0) {
                    var usermaster = new USER_MSTR
                    {
                        Name = model.Name,
                        Address = model.Address,
                        Email = model.Email,
                        State = model.State,
                        City = model.City,
                        ContactNo = model.ContactNo,
                        Username = model.Username,
                        Password = model.Password,
                        RoleAssign = model.RoleAssign,
                        Status = model.Status,
                        Image = model.Image,
                        Loginstatus = 0,
                    };
                    db.USER_MSTR.Add(usermaster);
                    db.SaveChanges();
                    return "Save";
                }
                else
                {
                    var usermaster = db.USER_MSTR.FirstOrDefault(a => a.User_Id == model.User_Id);
                    if(usermaster != null)
                    {
                        usermaster.Name = model.Name;
                        usermaster.Address = model.Address;
                        usermaster.Email = model.Email;
                        usermaster.State = model.State;
                        usermaster.City = model.City;
                        usermaster.ContactNo = model.ContactNo;
                        usermaster.Username = model.Username;
                        usermaster.Password = model.Password;
                        usermaster.RoleAssign = model.RoleAssign;
                        usermaster.Status = model.Status;
                        usermaster.Image = model.Image;
                        db.SaveChanges();
                        return "Done";
                    }
                }

                return "Error";

            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }
        //----------------------------GET-user-mstr----------------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<UserMstr> GetUserMstrslist()
        {
            try
            {
                List<UserMstr> userMstrslist = new List<UserMstr>();
               var Usermaster = db.USER_MSTR.ToList();
                
                foreach (var i in Usermaster)
                {
                    var model = new UserMstr
                    {
                        User_Id = i.User_Id,
                        Name = i.Name,
                        Address = i.Address,
                        Email = i.Email,
                        State = i.State,
                        City = i.City,
                        ContactNo = i.ContactNo,
                        Username = i.Username,
                        Password = i.Password,
                        RoleAssign = i.RoleAssign,
                        Image = i.Image,
                        Status = i.Status,
                    };
                    userMstrslist.Add(model);
                }
                return userMstrslist;
            }
            catch (Exception ex)
            {

                return new List<UserMstr>();
            }
        }
        //----------------------------Select-user-mstr----------------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<UserMstr> SelectUserMstrslist(int UserId)
        {
            try
            {
                List<UserMstr> userMstrslist = new List<UserMstr>();
                var Usermaster = db.USER_MSTR.FirstOrDefault(a=>a.User_Id == UserId);
                if(Usermaster != null)
                {
                    var model = new UserMstr
                    {
                        User_Id = Usermaster.User_Id,
                        Name = Usermaster.Name,
                        Address = Usermaster.Address,
                        Email = Usermaster.Email,
                        State = Usermaster.State,
                        City = Usermaster.City,
                        ContactNo = Usermaster.ContactNo,
                        Username = Usermaster.Username,
                        Password = Usermaster.Password,
                        RoleAssign = Usermaster.RoleAssign,
                        Image = Usermaster.Image,
                        Status = Usermaster.Status,
                    };
                    userMstrslist.Add(model);
                }    
                return userMstrslist;
            }
            catch (Exception ex)
            {

                return new List<UserMstr>();
            }
        }
        //----------------------------Delete-user-mstr----------------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string DeleteUserMaster(int UserId)
        {
            try
            {
                var Usermaster = db.USER_MSTR.FirstOrDefault(a => a.User_Id == UserId);
                if(Usermaster != null)
                {
                    db.USER_MSTR.Remove(Usermaster);
                    db.SaveChanges();
                    return "Delete SuccesFully";
                }
                return "Not Deleted";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        //----------------------------SAVE-AND-UPDATE-DEPARTMENT-MASTER----------------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string SaveDepartmentMaster(Departmentmaster model)
        {
            try
            {
                if(model.DepId <= 0)
                {
                    var departmentmstr = db.DEPARTMENT_MASTER.Where(a => a.DepartmentName == model.DepartmentName).ToList();
                    if(departmentmstr.Count != 0)
                    {
                        return "Department Name already created";
                    }
                    var savedepartment = new DEPARTMENT_MASTER
                    {
                        Date = model.Date,
                        DepartmentName = model.DepartmentName,
                        AuthorizePersonName = model.AuthorizePersonName,
                        Status = 0,
                    };
                    db.DEPARTMENT_MASTER.Add(savedepartment);
                    db.SaveChanges();
                    return "Save";
                }
                else
                {
                    var departmentmaster = db.DEPARTMENT_MASTER.FirstOrDefault(a => a.DepId == model.DepId);
                    if (departmentmaster != null)
                    {
                        if (departmentmaster.Status == 0)
                        {
                            var Departmentmstr = db.DEPARTMENT_MASTER.Where(a => a.DepId != model.DepId).ToList();
                           var dEPARTMENT = Departmentmstr.FirstOrDefault(a => a.DepartmentName == model.DepartmentName);
                            if(dEPARTMENT != null)
                            {
                                return "Department Name already created";
                            }

                            departmentmaster.Date = model.Date;
                            departmentmaster.DepartmentName = model.DepartmentName;
                            departmentmaster.AuthorizePersonName = model.AuthorizePersonName;
                            db.SaveChanges();
                            return "Done";
                        }
                        return "Departname is used in FORM 64";
                    }
                }
                return "Error";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public List<Departmentmaster> Departmentmasterslist()
        {
            try
            {
                List<Departmentmaster> departmentmasterslist = new List<Departmentmaster>();
                var departmentmaster = db.DEPARTMENT_MASTER.ToList();
                if(departmentmaster != null)
                {
                    foreach(var i in departmentmaster)
                    {
                        var model = new Departmentmaster
                        {
                            DepId = i.DepId,
                            Date = i.Date,
                            DepartmentName = i.DepartmentName,
                            AuthorizePersonName = i.AuthorizePersonName,
                        };
                        departmentmasterslist.Add(model);
                    }
                }
                return departmentmasterslist;
            }
            catch (Exception ex)
            {

                return new List<Departmentmaster>();
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public object selectDepartmentmaster(int DepId)
        {
            try
            {
                var departmentmaster = db.DEPARTMENT_MASTER.FirstOrDefault(a => a.DepId == DepId); 
                 var model = new Departmentmaster
                    {
                        DepId = departmentmaster.DepId,
                        Date = departmentmaster.Date,
                        DepartmentName = departmentmaster.DepartmentName,
                        AuthorizePersonName = departmentmaster.AuthorizePersonName,
                    };         
                return model;
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        //----------------------------Delete-Department-mstr----------------------------//
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public string DeleteDepartmentmaster(int DepId)
        {
            try
            {
                var departementmaster = db.DEPARTMENT_MASTER.FirstOrDefault(a => a.DepId == DepId);
                if(departementmaster != null)
                {
                    if(departementmaster.Status == 0)
                    {
                        db.DEPARTMENT_MASTER.Remove(departementmaster);
                        db.SaveChanges();
                        return "Deleted Succsefully";
                    }
                    return "Department is in used";
                }
                return "Not Deleted";
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }
        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public object UserforAuthorizePerson()
        {
            try
            {                
                var usermaster = db.USER_MSTR.Select(a => a.Username).ToList();
                return usermaster;
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public object DepartnameList()
        {
            try
            {
                var Departmentmaster = db.DEPARTMENT_MASTER.Select(a => a.DepartmentName).ToList();
                return Departmentmaster;
            }
            catch (Exception ex)
            {

                return "ex=" + ex.Message + " innerexception=" + ex.InnerException.Message;
            }
        }

        [System.Web.Http.HttpPost]
        //[BasicAuthentication]
        public object Logout(string username, int Status)
        {
            try
            {
                var usermstr = db.USER_MSTR.FirstOrDefault(a => a.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
                if (usermstr != null && usermstr.Loginstatus == 1) 
                {
                    usermstr.Loginstatus = 0;
                    db.SaveChanges();
                    return "Logout Succesfully";
                }
                return "Logout fail";
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
