using Microsoft.AspNetCore.Http;

namespace Domain.Dtos;

public class Location
{
    public int LocationId { get; set; }
    public string StreetAddress { get; set; }
    public int PostalCode { get; set; }
    public string City { get; set; }
    public string StateProvince { get; set; }
    public int CountryId { get; set; }
}


public class GetLocation
{
    public int LocationId { get; set; }
    public string StreetAddress { get; set; }
    public int PostalCode { get; set; }
    public string City { get; set; }
    public string StateProvince { get; set; }
    public string CountryName { get; set; }

}
