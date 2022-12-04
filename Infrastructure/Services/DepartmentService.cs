using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class DepartmentService
{
    private DapperContext _context;

    public DepartmentService(DapperContext context,IWebHostEnvironment env)
    {
        _context = context;
    }
    

    public async Task<Response<List<GetDepartment>>> GetDepartments()
    {
       using (var conn = _context.CreateConnection())
        {
            var sql = $"select d.department_id as departmentid, d.department_name as departmentname, l.city, l.street_address as streetaddress " +
                            $"from departments as d " +
                            $"join locations as l " +
                            $"on d.location_id = l.location_id " ;
       

            var  result = await conn.QueryAsync<GetDepartment>(sql);
            return new Response<List<GetDepartment>>(result.ToList());
            
        }
    }
        
       
  

    public async Task<Response<int>> InsertDepartment(Department Department)
    {
      using (var conn = _context.CreateConnection())
        {

            var sql = 
              $"insert into Departments (Department_Name,Location_id) values " +
              $"('{Department.DepartmentName} '," + 
              $"{Department.Locationid})";
            var result = await conn.ExecuteAsync(sql);
            
            return new Response<int>(result);
            
        }
    }
        public async Task<Response<int>> UpdateDepartment(Department Department)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = 
              $"Update Departments set " +
              $"Department_Name =  '{Department.DepartmentName}'," + 
              $"Location_id = {Department.Locationid} "  +
              $"where department_id = {Department.DepartmentId}" ;


                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
        public async Task<Response<int>> DeleteDepartment(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"DELETE FROM Departments WHERE Department_id = {id} ";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
}
    
