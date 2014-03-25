namespace AvaTaxCalcSOAP
{    
    using System;
    using Avalara.AvaTax.Adapter;
    using Avalara.AvaTax.Adapter.TaxService;

    public class GetTaxHistoryTest
    {
        public static void Test()
        {
            string accountNumber = "1234567890";
            string licenseKey = "A1B2C3D4E5F6G7H8";
            string serviceURL = "https://development.avalara.net";

            TaxSvc taxSvc = new TaxSvc();

            // Header Level Parameters
            // Required Header Parameters
            taxSvc.Configuration.Security.Account = accountNumber;
            taxSvc.Configuration.Security.License = licenseKey;
            taxSvc.Configuration.Url = serviceURL;
            taxSvc.Configuration.ViaUrl = serviceURL;
            taxSvc.Profile.Client = "AvaTaxSample";

            // Optional Header Parameters
            taxSvc.Profile.Name = "Development";

            GetTaxHistoryRequest getTaxHistoryRequest = new GetTaxHistoryRequest();

            // Required Request Parameters
            getTaxHistoryRequest.CompanyCode = "APITrialCompany";
            getTaxHistoryRequest.DocType = DocumentType.SalesInvoice;
            getTaxHistoryRequest.DocCode = "INV001";

            // Optional Request Parameters
            getTaxHistoryRequest.DetailLevel = DetailLevel.Tax;

            GetTaxHistoryResult getTaxHistoryResult = taxSvc.GetTaxHistory(getTaxHistoryRequest);

            Console.WriteLine("GetTaxHistoryTest Result: " + getTaxHistoryResult.ResultCode.ToString());
            if (!getTaxHistoryResult.ResultCode.Equals(SeverityLevel.Success))
            {
                foreach (Message message in getTaxHistoryResult.Messages)
                {
                    Console.WriteLine(message.Summary);
                }
            }
            else
            {
                Console.WriteLine("Document Code: " + getTaxHistoryResult.GetTaxResult.DocCode + " Total Tax: " + getTaxHistoryResult.GetTaxResult.TotalTax);
                foreach (TaxLine taxLine in getTaxHistoryResult.GetTaxResult.TaxLines)
                {
                    Console.WriteLine("    " + "Line Number: " + taxLine.No + " Line Tax: " + taxLine.Tax.ToString());
                    foreach (TaxDetail taxDetail in taxLine.TaxDetails)
                    {
                        Console.WriteLine("        " + "Jurisdiction: " + taxDetail.JurisName + " Tax: " + taxDetail.Tax.ToString());
                    }
                }
            }
        }
    }
}
