using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Avalara.AvaTax.Adapter.TaxService;
using Avalara.AvaTax.Adapter.AddressService;
using Avalara.AvaTax.Adapter;
using System.IO;

namespace AvaTaxCalcSOAP
{
    class DocumentLoader
    {
        //This loads some test data and returns a GetTaxRequest object for tax calculation.
        //TODO: These values should not be hardcoded in your integration. You will need to map to the values appropriate to your system.
        public static GetTaxRequest Load()
        {
            GetTaxRequest getTaxRequest = new GetTaxRequest();

            //Document Level Setup  
            //	     R: indicates Required Element
            //	     O: Indicates Optional Element
            //

            // Set the tax document properties - Required unless noted as Optional
            getTaxRequest.CompanyCode = "SDK";                              // R: Company Code from the accounts Admin Console
            DateTime docDate = DateTime.Today;
            getTaxRequest.DocCode = "SampleDoc: " + docDate.ToString();  	// R: Invoice or document tracking number - Must be unique
            getTaxRequest.DocType = DocumentType.SalesInvoice;              // R: Typically SalesOrder,SalesInvoice, ReturnInvoice
            getTaxRequest.DocDate = docDate;                                // R:  sets reporting date and default tax date
            getTaxRequest.CustomerCode = "TaxSvcTest";                   	// R: String - Customer Tracking number or Exemption Customer Code
            getTaxRequest.DetailLevel = DetailLevel.Tax;             		// R: Chose Summary, Document, Line or Tax - varying levels of results detail 
            getTaxRequest.Commit = false;                           		// O: Default is "false" - Set to "true" to commit the Document
            //getTaxRequest.CustomerUsageType="G";						    // O: Send for tax exempt transactions only.
            //getTaxRequest.ExemptionNo="12334";						    // O: Send for tax exempt transactions only.
            //getTaxRequest.Discount=0;									    // O: Send for document-level discounts only.
            getTaxRequest.PurchaseOrderNo = "PO 23423";					    // O: Specifies the purchase order number associated with the transaction. This value can be used to track single-use exemption certficates.
            getTaxRequest.ReferenceCode = "";								// O: This is a reportable value that does not affect tax calculation.

            //	          Begin Address Section
            //	          Add the origin and destination addresses

            Address origin = new Address();
            origin.Line1 = "Avalara";
            origin.Line2 = "100 Ravine Lane NE";
            origin.Line3 = "Suite 220";
            origin.City = "Bainbridge Island";
            origin.Region = "WA";
            origin.PostalCode = "98110";
            origin.Country = "USA";

            Address destination = new Address();
            destination.Line1 = "7462 Kearny Street";
            destination.City = "Commerce City";
            destination.Region = "CO";
            destination.PostalCode = "80022";
            destination.Country = "USA";

            //
            // Alternate:  Latitude / Longitude addressing
            //	         
            //	            
            //	            Address origin = new Address();
            //	            origin.Latitude="47.6253";
            //	            origin.Longitude="-122.515114";
            //
            //	            Address destination = new Address();
            //	            destination.Latitude="39.833597";
            //	            destination.Longitude="-104.917220";	            
            // End Address Section

            getTaxRequest.OriginAddress = origin;
            getTaxRequest.DestinationAddress = destination;

            // Add invoice lines

            Line line1 = new Line();                                        // New instance of a line                                
            line1.No = "101";                                               // R: string - line Number of invoice - must be unique.
            line1.ItemCode = "Item001";                                     // R: string - SKU or short name of Item
            line1.Qty = 1;                                                  // R: decimal - The number of items -- Qty of product sold. Does not function as a mulitplier for Amount
            line1.Amount = 1000;                                            // R: decimal - the "NET" amount -- Amount should be the 'extended' or 'net' amount
            line1.CustomerUsageType = "";                                   // O: string - AKA Entity Use Code - Typically A - L =G = Reseller)
            line1.Description = "ITEM1";                                    // O: string - Description or category of item sold.
            line1.TaxCode = "";                                             // O: string - Pass standard, custom or Pro-Tax code
            //              Can be NULL to default to tangible personal property =P0000000)
            line1.OriginAddress = origin;                      	            // R: Value representing the Origin Address
            line1.DestinationAddress = destination;                 	  	// R: Value representing the Destination Address

            //Line 2 - Shipping/Freight line - See property descriptions above
            Line line2 = new Line();                                        // New instance of a line
            line2.No = "102";                                               // R: string - SKU or short name of Item
            line2.ItemCode = "Shipping";                                    // R: string - SKU or short name of Item
            line2.Description = "Shipping- Freight Charges";                // O: string - Description or category of item sold.
            line2.Qty = 1;                                                  // R: decimal - The number of items -- Qty of product sold. Does not function as a mulitplier for Amount
            line2.Amount = 10;                                              // R: decimal - the "NET" amount -- Amount should be the 'extended' or 'net' amount
            line2.TaxCode = "FR";                                           // O: string - Pass standard, custom or Pro-Tax code FR020100
            line2.OriginAddress = origin;                      	            // R: Value representing the Origin Address
            line2.DestinationAddress = destination;                 	  	// R: Value representing the Destination Address

            getTaxRequest.Lines.Add(line1);                                 // Sets array of lines
            getTaxRequest.Lines.Add(line2);


            return getTaxRequest;
        }
    }
}
