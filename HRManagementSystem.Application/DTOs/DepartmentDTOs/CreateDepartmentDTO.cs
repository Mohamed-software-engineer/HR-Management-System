namespace HRManagementSystem.Application.DTOs.DepartmentDTOs
{
    public class CreateDepartmentDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public int? ManagerId { get; set; }
    }
}