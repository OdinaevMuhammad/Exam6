namespace Domain.Dtos;

public class Country
{
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public int Regionid { get; set; }
}


public class GetCountry
{
    public int CountryId { get; set; }
    public string CountryName { get; set; }
    public string RegionName { get; set; }

}