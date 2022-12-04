namespace Domain.Dtos;
using Microsoft.AspNetCore.Http;
public class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber  { get; set; }
    public int DepartmentId { get; set; }
    public int ManagerId { get; set; }
    public string Commision { get; set; }
    public int Salary { get; set; }
    public int JobId { get; set; }
    public DateTime HireDate { get; set; }
    public IFormFile profileImage { get; set; }
}
public  class GetEmployee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber  { get; set; }
    public int DepartmentId { get; set; }
    public int ManagerId { get; set; }
    public string Commision { get; set; }
    public int Salary { get; set; }
    public int JobId { get; set; }
    public DateTime HireDate { get; set; }
    public string DepartmentName { get; set; }
     public IFormFile profileImage { get; set; }

}