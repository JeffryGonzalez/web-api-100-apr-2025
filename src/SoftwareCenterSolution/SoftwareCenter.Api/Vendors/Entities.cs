namespace SoftwareCenter.Api.Vendors;

public enum VendorTypes {  Commercial, OpenSource, InHouse}

// this represents the thing I'm storing in the database.
public class VendorEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Site { get; set; } = string.Empty;

    public string AddedBy { get; set; } = string.Empty;
    public DateTimeOffset AddedOn { get; set; }
    public VendorTypes VendorType { get; set; } = VendorTypes.Commercial;
    public PointOfContact? Poc { get; init; } 

}