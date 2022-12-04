using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class EmployeeService
{
    private DapperContext _context;

 private readonly IWebHostEnvironment _hostEnvironment;

    public EmployeeService(DapperContext context,IWebHostEnvironment env)
    {
        _context = context;
        _hostEnvironment = env;
    }
    

    public async Task<Response<List<GetEmployee>>> GetEmployees()
    {
       using (var conn = _context.CreateConnection())
        {
            var sql = $"select  e.employee_id as employeeid, e.first_name as firstname,e.last_name as lastname, e.email,e.phone_number as phonenumber, " +
                            $"d.department_name as departmentname, e.comission,e.salary,e.hire_date " +
                            $"from employees as e " +
                            $"join departments as d " +
                            $"on e.department_id  = d.department_id " ;
       

            var  result = await conn.QueryAsync<GetEmployee>(sql);
            return new Response<List<GetEmployee>>(result.ToList());
            
        }
    }
        
       
    public async Task<Employee> AddImage(Employee image)
    {
        var path = Path.Combine(_hostEnvironment.WebRootPath, "images");
        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }
        
        var filePath = Path.Combine(path, image.profileImage.FileName);
        using (var stream = File.Create(filePath))
        {
            await image.profileImage.CopyToAsync(stream);
        }
        


        return image;
    }


    public async Task<Response<int>> InsertEmployee(Employee Employee)
    {
      using (var conn = _context.CreateConnection())
        {
             var path = Path.Combine(_hostEnvironment.WebRootPath, "images",Employee.profileImage.FileName);
            
            using (var stream = File.Create(path))
            {
                await Employee.profileImage.CopyToAsync(stream);
            }
            var sql = 
              $"insert into employees (first_name,last_name,email,phone_number,department_id,comission,salary,hire_date) values " +
              $"('{Employee.FirstName} '," + 
              $"'{Employee.LastName}', " + 
              $"'{Employee.Email}', " + 
              $"'{Employee.PhoneNumber}', " + 
              $"{Employee.DepartmentId}, " + 
              $"'{Employee.Commision}', " + 
              $"{Employee.Salary}, " + 
              $"'{Employee.HireDate}' )";
            var result = await conn.ExecuteAsync(sql);
            
            return new Response<int>(result);
            
        }
    }
        public async Task<Response<int>> UpdateEmployee(Employee Employee)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = 
              $"Update employees set " +
              $"first_name =  '{Employee.FirstName} '," + 
              $"last_name = '{Employee.LastName}', " + 
              $" email = '{Employee.Email}', " + 
              $"phone_number = phone_number = '{Employee.PhoneNumber}', " + 
              $"department_id = {Employee.DepartmentId}, " + 
              $"comission = '{Employee.Commision}', " + 
              $"salary = {Employee.Salary}, " + 
              $"hire_date = '{Employee.HireDate}' " +
              $"where employee_id = {Employee.EmployeeId}" ;



                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
        public async Task<Response<int>> DeleteEmployee(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"DELETE FROM Employees WHERE employee_id = {id} ";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
}
    
