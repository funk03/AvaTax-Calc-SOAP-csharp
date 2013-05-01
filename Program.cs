using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalara.AvaTax.Adapter;
using Avalara.AvaTax.Adapter.TaxService;
using Avalara.AvaTax.Adapter.AddressService;

namespace AvaTaxCalcSOAP
{
    class Program
    {
        public const string ACCTNUM = ""; //TODO: this should be your Avatax Account Number e.g. 1100012345
        public const string KEY = ""; //TODO: this should be the license key for the account above, e.g. 23CF4C53939C9725
        public const string COMPANYCODE = ""; //this should be the company code you set on your Admin Console, e.g. TEST
        public const string WEBADDR = "https://development.avalara.net";

        static void Main()
        {
            GetTaxRequest calcReq = DocumentLoader.Load(); //Loads document from file to generate request
            calcReq.CompanyCode = COMPANYCODE;
            //Run address validation test (address/validate) 
            //Will take an address and return a single non-ambiguous validated result or an error. 
            AddressTest.validateAddressTest(calcReq, ACCTNUM, KEY, WEBADDR);

            //Run tax calculation test (tax/get POST)
            //Calculates tax on a transaction and, depending on parameters, records the transaction to the Admin Console.
            TaxTest.getTaxTest(calcReq, ACCTNUM, KEY, WEBADDR);
            //Run post tax test (no REST equivalent)
            //Changes the status of a recorded transaction on the Admin Console to Posted or Committed.
            TaxTest.postTaxTest(calcReq, ACCTNUM, KEY, WEBADDR);
            //Run get tax history test (no REST equivalent)
            //Retrieves the detail of the original request and result of a tax document stored on the Admin Console.
            TaxTest.getTaxHistoryTest(calcReq, ACCTNUM, KEY, WEBADDR);
            //Run cancel tax test (tax/cancel)
            //Changes the status of a recorded transaction on the Admin Console to Voided.
            TaxTest.cancelTaxTest(calcReq, ACCTNUM, KEY, WEBADDR);

            //Note that this sample does not demonstrate usage of all of the methods available in the SOAP API - 
            //just those that are most commonly used. If you are interested in a complete demonstration of the 
            //functionality available, please take a look at the AvaTaxCalcSOAP-extended sample.

            Console.WriteLine("Done");
        }
    }
}
