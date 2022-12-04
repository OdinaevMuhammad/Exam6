namespace Domain.Dtos;

public class Department
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public int Locationid { get; set; }
}

public class GetDepartment
{
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
        public string StreetAddress { get; set; }
            public string City { get; set; }


}