using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalara.AvaTax.Adapter.AddressService;
using Avalara.AvaTax.Adapter.TaxService;
using Avalara.AvaTax.Adapter;

namespace AvaTaxCalcSOAP
{
    class AddressTest
    {
        //will pull address out of parsed invoice object and validate it
        public static void validateAddressTest(GetTaxRequest calcReq, string acctNum, string licKey, string webAddr)
        {
            AddressSvc addrSvc = new AddressSvc(); //create service object and add configuration
            addrSvc.Configuration.Security.Account = acctNum;
            addrSvc.Configuration.Security.License = licKey;
            addrSvc.Configuration.Url = webAddr;
            addrSvc.Configuration.ViaUrl = webAddr;

            //we also need to create a validation request object
            ValidateRequest addrReq = new ValidateRequest();
            addrReq.Address = calcReq.DestinationAddress;
            addrReq.Coordinates = true; //Used to return geographical coordinates on a successful validation. Default is false.

            try
            {
                ValidateResult addressResult = addrSvc.Validate(addrReq); //Validates a given address.
                Console.WriteLine("ValidateAddress test result: " + addressResult.ResultCode.ToString() + ", \nAddress="
                    + addressResult.Addresses[0].Line1 + " " + addressResult.Addresses[0].City + " " + addressResult.Addresses[0].Region + " " + addressResult.Addresses[0].PostalCode);//At this point, you would display the validated result to the user for approval, and write it to the customer record.
            }
            catch (Exception ex)
            { Console.WriteLine("ValidateAddress test: Exception " + ex.Message); }
        }
    }
}
