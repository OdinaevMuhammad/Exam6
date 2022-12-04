using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class CountryService
{
    private DapperContext _context;

    public CountryService(DapperContext context)
    {
        _context = context;
    }
    

    public async Task<Response<List<GetCountry>>> GetCountries()
    {
       using (var conn = _context.CreateConnection())
        {
            var sql = $"select c.country_id as countryid, c.country_name as countryname, region_name as regionname " +
                            $"from countries as c " +
                            $"join regions as r " +
                            $"on c.region_id = r.region_id " ;
       

            var  result = await conn.QueryAsync<GetCountry>(sql);
            return new Response<List<GetCountry>>(result.ToList());
            
        }
    }
        
       

    public async Task<Response<int>> InsertCountry(Country Country)
    {
      using (var conn = _context.CreateConnection())
        {

            var sql = 
              $"insert into Countries (Country_Name,Region_id) values " +
              $"('{Country.CountryName} '," + 
              $"{Country.Regionid} )";
            var result = await conn.ExecuteAsync(sql);
            
            return new Response<int>(result);
            
        }
    }
        public async Task<Response<int>> UpdateCountry(Country Country)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = 
              $"Update Countries set " +
              $"Country_Name =  '{Country.CountryName} '," + 
              $"Region_id = '{Country.Regionid}' " +
              $"where Country_id = {Country.CountryId}" ;



                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
        public async Task<Response<int>> DeleteCountry(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"DELETE FROM Countryies WHERE Country_id = {id} ";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
}
    
