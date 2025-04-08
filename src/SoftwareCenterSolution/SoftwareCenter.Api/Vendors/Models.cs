using System.Text.Json.Serialization;

namespace SoftwareCenter.Api.Vendors;

/*{
    "name": "Microsoft",
    "site": "https://www.microsoft.com",
    
    "poc": {
        "name": {
            "first": "William",
            "last": "Gates"            
        },
        "contactMechanisms": {
          
                "primary_phone": "800 bill-g",
                "primary_email": "billg@microsoft.com"
            
        }
    }
}
*/

public enum ContactMechanisms {
    // primary_phone
    [JsonStringEnumMemberName("primary_phone")]
    primaryPhone,
    // primary_email
    [JsonStringEnumMemberName("primary_email")]
    PrimaryEmail }
public record CommercialVendorCreate(
    string Name,
    string Site,
    PointOfContact Poc
    
    );

public record PointOfContact(
    NameContact Name,
    Dictionary<ContactMechanisms, string> ContactMechanisms
    );


public record NameContact(string First, string Last);