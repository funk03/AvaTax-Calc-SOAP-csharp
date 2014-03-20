﻿namespace AvaTaxCalcSOAP
{
    using System;
    using Avalara.AvaTax.Adapter;
    using Avalara.AvaTax.Adapter.AddressService;
    using Avalara.AvaTax.Adapter.TaxService;

    public class GetTaxTest
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
         

            GetTaxRequest getTaxRequest = new GetTaxRequest();

            // Document Level Parameters
            // Required Request Parameters
            getTaxRequest.CustomerCode = "ABC4335";
            getTaxRequest.DocDate = DateTime.Parse("2014-01-01");
            //// getTaxRequest.Lines is also required, and is presented later in this file.

            // Best Practice Request Parameters
            getTaxRequest.CompanyCode = "APITrialCompany";
            getTaxRequest.DocCode = "INV001";
            getTaxRequest.DetailLevel = DetailLevel.Tax;
            getTaxRequest.Commit = false;
            getTaxRequest.DocType = DocumentType.SalesInvoice;

            // Situational Request Parameters
            // getTaxRequest.BusinessIdentificationNo = "234243";
            // getTaxRequest.CustomerUsageType = "G";
            // getTaxRequest.ExemptionNo = "12345";
            // getTaxRequest.Discount = 50;
            // getTaxRequest.LocationCode = "01";
            // getTaxRequest.TaxOverride.TaxOverrideType = TaxOverrideType.TaxDate;
            // getTaxRequest.TaxOverride.Reason = "Adjustment for return";
            // getTaxRequest.TaxOverride.TaxDate = DateTime.Parse("2013-07-01");
            // getTaxRequest.TaxOverride.TaxAmount = 0;
            // getTaxRequest.ServiceMode = ServiceMode.Automatic;

            // Optional Request Parameters
            getTaxRequest.PurchaseOrderNo = "PO123456";
            getTaxRequest.ReferenceCode = "ref123456";
            getTaxRequest.PosLaneCode = "09";
            getTaxRequest.CurrencyCode = "USD";
            getTaxRequest.ExchangeRate = (decimal)1.0;
            getTaxRequest.ExchangeRateEffDate = DateTime.Parse("2013-01-01");
            getTaxRequest.SalespersonCode = "Bill Sales";

            // Address Data
            Address address1 = new Address();
            address1.Line1 = "45 Fremont Street";
            address1.City = "San Francisco";
            address1.Region = "CA";

            Address address2 = new Address();
            address2.Line1 = "118 N Clark St";
            address2.Line2 = "Suite 100";
            address2.Line3 = "ATTN Accounts Payable";
            address2.City = "Chicago";
            address2.Region = "IL";
            address2.Country = "US";
            address2.PostalCode = "60602";

            Address address3 = new Address();
            address3.Latitude = "47.627935";
            address3.Longitude = "-122.51702";

            // Line Data
            // Required Parameters
            Line line1 = new Line();
            line1.No = "01";
            line1.ItemCode = "N543";
            line1.Qty = 1;
            line1.Amount = 10;
            line1.OriginAddress = address1;
            line1.DestinationAddress = address2;

            // Best Practice Request Parameters
            line1.Description = "Red Size 7 Widget";
            line1.TaxCode = "NT";

            // Situational Request Parameters
            // line1.CustomerUsageType = "L";
            // line1.ExemptionNo = "12345";
            // line1.Discounted = true;
            // line1.TaxIncluded = true;
            // line1.TaxOverride.TaxOverrideType = TaxOverrideType.TaxDate;
            // line1.TaxOverride.Reason = "Adjustment for return";
            // line1.TaxOverride.TaxDate = DateTime.Parse("2013-07-01");
            // line1.TaxOverride.TaxAmount = 0;

            // Optional Request Parameters
            line1.Ref1 = "ref123";
            line1.Ref2 = "ref456";

            Line line2 = new Line();
            line2.No = "02";
            line2.ItemCode = "T345";
            line2.Qty = 3;
            line2.Amount = 150;
            line2.OriginAddress = address1;
            line2.DestinationAddress = address3;
            line2.Description = "Size 10 Green Running Shoe";
            line2.TaxCode = "PC030147";

            Line line3 = new Line();
            line3.No = "02-FR";
            line3.ItemCode = "FREIGHT";
            line3.Qty = 1;
            line3.Amount = 15;
            line3.OriginAddress = address1;
            line3.DestinationAddress = address3;
            line3.Description = "Shipping Charge";
            line3.TaxCode = "FR";

            GetTaxResult getTaxResult = taxSvc.GetTax(getTaxRequest);

            Console.WriteLine("GetTaxTest Result: " + getTaxResult.ResultCode.ToString());
            if (!getTaxResult.ResultCode.Equals(SeverityLevel.Success))
            {
                foreach (Message message in getTaxResult.Messages)
                {
                    Console.WriteLine(message.Summary);
                }
            }
            else
            {
                Console.WriteLine("Document Code: " + getTaxResult.DocCode + " Total Tax: " + getTaxResult.TotalTax);
                foreach (TaxLine taxLine in getTaxResult.TaxLines)
                {
                    Console.WriteLine("    " + "Line Number: " + taxLine.No + " Line Tax: " + getTaxResult.TotalTax.ToString());
                    foreach (TaxDetail taxDetail in taxLine.TaxDetails)
                    {
                        Console.WriteLine("        " + "Jurisdiction: " + taxDetail.JurisName + " Tax: " + taxDetail.Tax.ToString());
                    }
                }
            }
        }
    }
}
