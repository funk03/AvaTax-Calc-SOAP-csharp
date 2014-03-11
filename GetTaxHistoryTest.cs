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

            var getTaxHistoryRequest = new GetTaxHistoryRequest
            {
                // Required Request Parameters
                CompanyCode = "APITrialCompany",
                DocType = DocumentType.SalesInvoice,
                DocCode = "INV001",

                // Optional Request Parameters
                DetailLevel = DetailLevel.Tax
            };

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
