using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController
{
    private CountryService _CountryService;
    public CountryController(CountryService CountryService)
    {
        _CountryService = CountryService;
    }
    

    [HttpGet("GetCountry")]
    public async Task<Response<List<GetCountry>>> GetCountries()
    {
        return  await _CountryService.GetCountries();
    }
       [HttpPost("InsertCountry")]
    public async Task<Response<int>> InsertCountry( Country Country)
    {
        return await _CountryService.InsertCountry(Country);
    }

    [HttpPut("UpdateCountry")]
    public async Task<Response<int>> UpdateCountry(Country Country)
    {
        return await _CountryService.UpdateCountry(Country);
    }
    [HttpDelete("DeleteCountry")]
    public async Task<Response<int>> DeleteCountry(int id)
    {
        return await _CountryService.DeleteCountry(id);
    }
}