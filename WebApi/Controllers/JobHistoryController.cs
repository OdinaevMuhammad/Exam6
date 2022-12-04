using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JobHistoryController
{
    private JobHistoryService _JobHistoryService;
    public JobHistoryController(JobHistoryService JobHistoryService)
    {
        _JobHistoryService = JobHistoryService;
    }
    

    [HttpGet("GetJobHistory")]
    public async Task<Response<List<GetJobHistory>>> GetJobHistorys()
    {
        return  await _JobHistoryService.GetJobHistories();
    }
       [HttpPost("InsertJobHistory")]
    public async Task<Response<int>> InsertJobHistory( JobHistory JobHistory)
    {
        return await _JobHistoryService.InsertJobHistory(JobHistory);
    }

    [HttpPut("UpdateJobHistory")]
    public async Task<Response<int>> Update(JobHistory JobHistory)
    {
        return await _JobHistoryService.UpdateJobHistory(JobHistory);
    }
    [HttpDelete("DeleteJobHistory")]
    public async Task<Response<int>> DeleteJobHistory(int id)
    {
        return await _JobHistoryService.DeleteJobHistory(id);
    }
}