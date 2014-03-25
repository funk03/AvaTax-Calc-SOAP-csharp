namespace AvaTaxCalcSOAP
{
    using System;
    using Avalara.AvaTax.Adapter;
    using Avalara.AvaTax.Adapter.AddressService;
    using Avalara.AvaTax.Adapter.TaxService;

    public class CommitTaxTest
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

            CommitTaxRequest commitTaxRequest = new CommitTaxRequest();
            
            // Required Parameters
            commitTaxRequest.DocCode = "INV001";
            commitTaxRequest.DocType = DocumentType.SalesInvoice;
            commitTaxRequest.CompanyCode = "APITrialCompany";

            // Optional Parameters
            commitTaxRequest.NewDocCode = "INV001-1";

            CommitTaxResult commitTaxResult = taxSvc.CommitTax(commitTaxRequest);

            Console.WriteLine("CommitTaxTest Result: " + commitTaxResult.ResultCode.ToString());
            if (!commitTaxResult.ResultCode.Equals(SeverityLevel.Success))
            {
                foreach (Message message in commitTaxResult.Messages)
                {
                    Console.WriteLine(message.Summary);
                }
            }
        }
    }
}
