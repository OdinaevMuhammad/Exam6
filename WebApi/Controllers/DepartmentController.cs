using Domain.Dtos;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class DepartmentController
{
    private DepartmentService _DepartmentService;
    public DepartmentController(DepartmentService DepartmentService)
    {
        _DepartmentService = DepartmentService;
    }
    

    [HttpGet("GetDepartment")]
    public async Task<Response<List<GetDepartment>>> GetDepartments()
    {
        return  await _DepartmentService.GetDepartments();
    }
       [HttpPost("InsertDepartment")]
    public async Task<Response<int>> InsertDepartment( Department Department)
    {
        return await _DepartmentService.InsertDepartment(Department);
    }

    [HttpPut("UpdateDepartment")]
    public async Task<Response<int>> Update(Department Department)
    {
        return await _DepartmentService.UpdateDepartment(Department);
    }
    [HttpDelete("DeleteDepartment")]
    public async Task<Response<int>> DeleteDepartment(int id)
    {
        return await _DepartmentService.DeleteDepartment(id);
    }
}