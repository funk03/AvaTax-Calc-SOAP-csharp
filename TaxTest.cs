using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalara.AvaTax.Adapter;
using Avalara.AvaTax.Adapter.TaxService;
using Avalara.AvaTax.Adapter.AddressService;

namespace AvaTaxCalcSOAP
{
    class TaxTest
    {
        public static void cancelTaxTest(GetTaxRequest calcReq, string acctNum, string licKey, string webAddr)
        {
            TaxSvc tSvc = new TaxSvc(); //create service object and add configuration
            tSvc.Configuration.Security.Account = acctNum;
            tSvc.Configuration.Security.License = licKey;
            tSvc.Configuration.Url = webAddr;
            tSvc.Configuration.ViaUrl = webAddr;
            //create request object
            CancelTaxRequest cancelReq = new CancelTaxRequest();
            cancelReq.CompanyCode = calcReq.CompanyCode;
            cancelReq.DocCode = calcReq.DocCode;
            cancelReq.DocType = calcReq.DocType;
            cancelReq.CancelCode = CancelCode.DocDeleted; //this should vary according to your use case.
            try
            {
                CancelTaxResult cancelResult = tSvc.CancelTax(cancelReq); //Let's void this document to demonstrate tax/cancel
                //You would normally initiate a tax/cancel call upon voiding or deleting the document in your system.
                Console.WriteLine("CancelTax test result: " + cancelResult.ResultCode.ToString() + ", Document Voided");
                //Let's display the result of the cancellation. At this point, you would allow your system to complete the delete/void.
            }
            catch (Exception ex)
            { Console.WriteLine("CancelTax test: Exception " + ex.Message); }
        }
        public static void getTaxTest(GetTaxRequest calcReq, string acctNum, string licKey, string webAddr)
        {
            TaxSvc tSvc = new TaxSvc(); //create service object and add configuration
            tSvc.Configuration.Security.Account = acctNum;
            tSvc.Configuration.Security.License = licKey;
            tSvc.Configuration.Url = webAddr;
            tSvc.Configuration.ViaUrl = webAddr;
            try
            {
                GetTaxResult calcResult = tSvc.GetTax(calcReq); //Calculates tax on document
                Console.WriteLine("GetTax test result: " + calcResult.ResultCode.ToString() + ", " +
                "TotalTax=" + calcResult.TotalTax.ToString()); //At this point, you would write the tax calculated to your database and display to the user.
            }
            catch (Exception ex)
            { Console.WriteLine("GetTax test: Exception " + ex.Message); }
        }
        public static void postTaxTest(GetTaxRequest calcReq, string acctNum, string licKey, string webAddr)
        {
            TaxSvc tSvc = new TaxSvc(); //create service object and add configuration
            tSvc.Configuration.Security.Account = acctNum;
            tSvc.Configuration.Security.License = licKey;
            tSvc.Configuration.Url = webAddr;
            tSvc.Configuration.ViaUrl = webAddr;
            PostTaxRequest postReq = new PostTaxRequest();
            postReq.Commit = true; //will change the document state to "committed" - note that this can also be done by calling GetTax with Commit = true.
            postReq.CompanyCode = calcReq.CompanyCode;
            postReq.DocCode = calcReq.DocCode;
            postReq.DocType = calcReq.DocType;
            postReq.DocDate = calcReq.DocDate;
            //TotalAmount and TotalTax are required fields and should represent 
            //the pre-tax total amount and total calculated tax amount on the recorded transaction.
            //A mismatch will return a warning from the service, but it will still complete the PostTax.
            postReq.TotalAmount = 0; 
            postReq.TotalTax = 0;
            try
            {
                PostTaxResult postResult = tSvc.PostTax(postReq);
                Console.WriteLine("PostTax test result: " + postResult.ResultCode.ToString());
            }
            catch (Exception ex)
            { Console.WriteLine("PostTax test: Exception " + ex.Message); }
        }
        public static void getTaxHistoryTest(GetTaxRequest calcReq, string acctNum, string licKey, string webAddr)
        {
            TaxSvc tSvc = new TaxSvc(); //create service object and add configuration
            tSvc.Configuration.Security.Account = acctNum;
            tSvc.Configuration.Security.License = licKey;
            tSvc.Configuration.Url = webAddr;
            tSvc.Configuration.ViaUrl = webAddr;

            GetTaxHistoryRequest histReq = new GetTaxHistoryRequest();
            histReq.CompanyCode = calcReq.CompanyCode;
            histReq.DetailLevel = DetailLevel.Tax; //note that you can pull back a different tax detail here than was originally used on the calculation
            histReq.DocCode = calcReq.DocCode;
            histReq.DocType = calcReq.DocType;
            try {
                GetTaxHistoryResult histResult = tSvc.GetTaxHistory(histReq);
                Console.WriteLine("GetTaxHistory test result: " + histResult.ResultCode.ToString()+", "+"Tax calculated was: "+
                    histResult.GetTaxResult.TotalTax.ToString());
            }
            catch (Exception ex)
            { Console.WriteLine("GetTaxHistory test: Exception " + ex.Message); }
        }
    }
}
