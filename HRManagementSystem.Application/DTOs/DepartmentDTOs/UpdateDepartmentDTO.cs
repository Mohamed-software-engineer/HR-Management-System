namespace HRManagementSystem.Application.DTOs.DepartmentDTOs
{
    public class UpdateDepartmentDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? ManagerId { get; set; }
    }
}