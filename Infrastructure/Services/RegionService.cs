using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class RegionService
{
    private DapperContext _context;

    public RegionService(DapperContext context)
    {
        _context = context;
    }
    

    public async Task<Response<List<Region>>> GetCountries()
    {
       using (var conn = _context.CreateConnection())
        {
            var sql = $"select region_id as regionid, region_name as regionname from regions ";
         
       

            var  result = await conn.QueryAsync<Region>(sql);
            return new Response<List<Region>>(result.ToList());
            
        }
    }
        
       

    public async Task<Response<int>> InsertRegion(Region Region)
    {
      using (var conn = _context.CreateConnection())
        {

            var sql = 
              $"insert into Regions (Region_Name) values " +
              $"('{Region.RegionName}')" ;
            var result = await conn.ExecuteAsync(sql);
            
            return new Response<int>(result);
            
        }
    }
        public async Task<Response<int>> UpdateRegion(Region Region)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = 
              $"Update Regions set " +
              $"Region_Name =  '{Region.RegionName} '" + 
              $"where Region_id = {Region.Regionid}" ;



                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
        public async Task<Response<int>> DeleteRegion(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"DELETE FROM Regions WHERE Region_id = {id} ";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
}
    
