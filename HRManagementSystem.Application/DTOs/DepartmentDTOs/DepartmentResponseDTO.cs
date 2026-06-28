namespace HRManagementSystem.Application.DTOs.DepartmentDTOs
{
    public class DepartmentResponseDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ManagerName { get; set; }
        public List<string>? EmployeeNames {get; set;}
    }
}