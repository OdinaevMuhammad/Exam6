using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class JobService
{
    private DapperContext _context;

    public JobService(DapperContext context)
    {
        _context = context;
    }
    

    public async Task<Response<List<Job>>> GetCountries()
    {
       using (var conn = _context.CreateConnection())
        {
            var sql = $"select job_id as jobid, job_title as jobtitle, Min_Salary as MinSalary,Max_Salary as MaxSalary   from jobs " ;

       

            var  result = await conn.QueryAsync<Job>(sql);
            return new Response<List<Job>>(result.ToList());
            
        }
    }
        
       

    public async Task<Response<int>> InsertJob(Job Job)
    {
      using (var conn = _context.CreateConnection())
        {

            var sql = 
              $"insert into Jobs (job_title, Min_Salary,Max_Salary) values " +
              $"('{Job.JobTitle}' ," + 
              $"{Job.MinSalary}," + 
              $"{Job.MaxSalary} )";
            var result = await conn.ExecuteAsync(sql);
            
            return new Response<int>(result);
            
        }
    }
        public async Task<Response<int>> UpdateJob(Job Job)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = 
              $"Update Jobs set " +
              $"Min_Salary =  {Job.MinSalary} ," + 
              $"Max_Salary = {Job.MaxSalary}, " +
              $"Job_Title = '{Job.JobTitle}' " + 
              $"where Job_id = {Job.jobId}" ;



                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
        public async Task<Response<int>> DeleteJob(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"DELETE FROM Jobs WHERE Job_id = {id} ";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
}
    
