namespace AvaTaxCalcSOAP
{
    using System;
    using Avalara.AvaTax.Adapter;
    using Avalara.AvaTax.Adapter.TaxService;

    public class PostTaxTest
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

            PostTaxRequest postTaxRequest = new PostTaxRequest();

            // Required Request Parameters
            postTaxRequest.CompanyCode = "APITrialCompany";
            postTaxRequest.DocType = DocumentType.SalesInvoice;
            postTaxRequest.DocCode = "INV001";
            postTaxRequest.Commit = false;
            postTaxRequest.DocDate = DateTime.Parse("2014-01-01");
            postTaxRequest.TotalTax = (decimal)34.07;
            postTaxRequest.TotalAmount = 170;

            // Optional Request Parameters
            postTaxRequest.NewDocCode = "INV001-1";

            PostTaxResult postTaxResult = taxSvc.PostTax(postTaxRequest);

            Console.WriteLine("PostTaxTest Result: " + postTaxResult.ResultCode.ToString());
            if (!postTaxResult.ResultCode.Equals(SeverityLevel.Success))
            {
                foreach (Message message in postTaxResult.Messages)
                {
                    Console.WriteLine(message.Summary);
                }
            }
        }
    }
}
