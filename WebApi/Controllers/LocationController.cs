using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController
{
    private LocationService _LocationService;
    public LocationController(LocationService LocationService)
    {
        _LocationService = LocationService;
    }
    

    [HttpGet("GetLocation")]
    public async Task<Response<List<GetLocation>>> GetLocations()
    {
        return  await _LocationService.GetLocations();
    }
       [HttpPost("InsertLocation")]
    public async Task<Response<int>> InsertLocation( Location Location)
    {
        return await _LocationService.InsertLocation(Location);
    }

    [HttpPut("UpdateLocation")]
    public async Task<Response<int>> Update(Location Location)
    {
        return await _LocationService.UpdateLocation(Location);
    }
    [HttpDelete("DeleteLocation")]
    public async Task<Response<int>> DeleteLocation(int id)
    {
        return await _LocationService.DeleteLocation(id);
    }
}