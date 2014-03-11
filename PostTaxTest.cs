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

            var taxSvc = new TaxSvc
            {
                // Header Level Parameters
                // Required Header Parameters
                Configuration =
                {
                    Security =
                    {
                        Account = accountNumber,
                        License = licenseKey
                    },
                    Url = serviceURL,
                    ViaUrl = serviceURL
                },
                Profile =
                {
                    Client = "AvaTaxSample",

                    // Optional Header Parameters
                    Name = "Development"
                }
            };

            var postTaxRequest = new PostTaxRequest
            {

                // Required Request Parameters
                CompanyCode = "APITrialCompany",
                DocType = DocumentType.SalesInvoice,
                DocCode = "INV001",
                Commit = false,
                DocDate = DateTime.Parse("2014-01-01"),
                TotalTax = (decimal)34.07,
                TotalAmount = 170,

                // Optional Request Parameters
                NewDocCode = "INV001-1"
            };

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
