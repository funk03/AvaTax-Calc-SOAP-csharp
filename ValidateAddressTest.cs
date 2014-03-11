namespace AvaTaxCalcSOAP
{
    using System;
    using Avalara.AvaTax.Adapter;
    using Avalara.AvaTax.Adapter.AddressService;

    public class ValidateAddressTest
    {
        public static void Test()
        {
            string accountNumber = "1234567890";
            string licenseKey = "A1B2C3D4E5F6G7H8";
            string serviceURL = "https://development.avalara.net";

            var addressSvc = new AddressSvc
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

            var validateRequest = new ValidateRequest
            {
            // Required Request Parameters
                Address = 
                {
                        Line1 = "118 N Clark St",
                        City = "Chicago",
                        Region = "IL",

                        // Optional Request Parameters
                        Line2 = "Suite 100",
                        Line3 = "ATTN Accounts Payable",
                        Country = "US",
                        PostalCode = "60602"
                },
                Coordinates = true,
                Taxability = true,
                TextCase = TextCase.Upper
            };

            ValidateResult validateResult = addressSvc.Validate(validateRequest);

            Console.WriteLine("ValidateAddressTest Result: " + validateResult.ResultCode.ToString());
            if (!validateResult.ResultCode.Equals(SeverityLevel.Success))
            {
                foreach (Message message in validateResult.Messages)
                {
                    Console.WriteLine(message.Summary);
                }
            }
            else
            {
                Console.WriteLine(validateResult.Addresses[0].Line1
                    + " "
                    + validateResult.Addresses[0].City
                    + ", "
                    + validateResult.Addresses[0].Region
                    + " "
                    + validateResult.Addresses[0].PostalCode);
            }
        }
    }
}
