using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class LocationService
{
    private DapperContext _context;


    public LocationService(DapperContext context)
    {
        _context = context;
    }
    

    public async Task<Response<List<GetLocation>>> GetLocations()
    {
       using (var conn = _context.CreateConnection())
        {
            var sql = $"select l.location_id as locationid, l.street_address as streetaddress, l.city, l.state_province as stateprovince,c.country_name as countryname " +
                            $"from Locations as l " +
                            $"join countries as c " +
                            $"on l.country_id  = c.country_id " ;
       

            var  result = await conn.QueryAsync<GetLocation>(sql);
            return new Response<List<GetLocation>>(result.ToList());
            
        }
    }
        
       


    public async Task<Response<int>> InsertLocation(Location Location)
    {
      using (var conn = _context.CreateConnection())
        {

            var sql = 
              $"insert into Locations (country_id,Street_Address,Postal_Code,City,State_Province) values " +
              $"({Location.CountryId} ," +
              $"'{Location.StreetAddress} '," + 
              $"{Location.PostalCode}, " + 
              $"'{Location.City}', " + 
              $"'{Location.StateProvince}' )";
            var result = await conn.ExecuteAsync(sql);
            
            return new Response<int>(result);
            
        }
    }
        public async Task<Response<int>> UpdateLocation(Location Location)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = 
              $"Update Locations set " +
              $"Country_Id =  {Location.CountryId} ," + 
              $"Street_Address =  '{Location.StreetAddress} '," + 
              $"Postal_Code = {Location.PostalCode}, " + 
              $"City = '{Location.City}', " + 
              $"State_Province = '{Location.StateProvince}' " + 
              $"where location_id = {Location.LocationId}";


                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
        public async Task<Response<int>> DeleteLocation(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"DELETE FROM Locations WHERE Location_id = {id} ";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
}
    
