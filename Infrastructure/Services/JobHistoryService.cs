using System.Net;
using Dapper;
using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Npgsql;

namespace Infrastructure.Services;

public class JobHistoryService
{
    private DapperContext _context;


    public JobHistoryService(DapperContext context)
    {
        _context = context;
    }
    

    public async Task<Response<List<GetJobHistory>>> GetJobHistories()
    {
       using (var conn = _context.CreateConnection())
        {
            var sql = $"select j.employee_id as employeeId,  d.department_name as departmentname, j.start_date as startdate, j.end_date as enddate, jo.job_title as jobtitle ,Concat(e.first_name, '' ,e.last_name) as fullname " +
                            $"from job_history as j " +
                            $"join jobs as jo " +
                            $"on j.job_id = jo.job_id " +
                            $"join employees as e " +
                            $"on j.employee_id = e.employee_id " +
                               $"join departments as d " +
                            $"on j.department_id = d.department_id";
       

            var  result = await conn.QueryAsync<GetJobHistory>(sql);
            return new Response<List<GetJobHistory>>(result.ToList());
            
        }
    }
        
       


    public async Task<Response<int>> InsertJobHistory(JobHistory JobHistory)
    {
      using (var conn = _context.CreateConnection())
        {

            var sql = 
              $"insert into Job_History (Employee_Id,Department_Id,job_id,Start_Date,End_Date) values " +
              $"({JobHistory.EmployeeId} ," + 
              $"{JobHistory.DepartmentId}, " + 
               $"{JobHistory.JobId}, " + 
              $"'{JobHistory.StartDate}', " + 
              $"'{JobHistory.EndDate}' )";
            var result = await conn.ExecuteAsync(sql);
            
            return new Response<int>(result);
            
        }
    }
        public async Task<Response<int>> UpdateJobHistory(JobHistory JobHistory)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = 
              $"Update Job_History set " +
              $"Employee_Id =  {JobHistory.EmployeeId} ," + 
              $"Department_Id = {JobHistory.DepartmentId}, " + 
              $"Start_Date = '{JobHistory.StartDate}', " + 
              $"End_Date = '{JobHistory.EndDate}' " + 
              $"where Employee_Id = {JobHistory.EmployeeId}";


                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
        public async Task<Response<int>> DeleteJobHistory(int id)
        {
            using (var conn = _context.CreateConnection())
            {
                var sql = $"DELETE FROM Job_History WHERE employee_id = {id} ";

                var result = await conn.ExecuteAsync(sql);

                return new Response<int>(result);
            }
        }
}
    
