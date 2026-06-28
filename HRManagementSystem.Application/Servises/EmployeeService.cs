using System.Net;
using HRManagementSystem.Application.DTOs.EmployeeDTOs;
using HRManagementSystem.Application.Interfaces;
using HRManagementSystem.Domains.Entities;
using HRManagementSystem.Domains.Enums;

namespace HRManagementSystem.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUniteOfWork _uniteOfWork;
        public EmployeeService(IUniteOfWork uniteOfWork)
        {
            _uniteOfWork = uniteOfWork;
        }

        public async Task<int> CreateCommissionEmployeeAsync(CreateCommissionEmployeeDTO dto)
        {
            var employee = new CommissionEmployee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Gender = dto.Gender,
                JobTitle = dto.JobTitle,
                PhoneNumber = dto.PhoneNumber,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                DateOfBirth = dto.DateOfBirth,
                DateOfHire = dto.DateOfHire,
                DepartmentId = dto.DepartmentId,
                ManagerId = dto.ManagerId,
                Rate = dto.Rate,
                Target = dto.Target
            };

            await _uniteOfWork.EmployeeRepository.AddEmployeeAsync(employee);
            await _uniteOfWork.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<int> CreateHourlyEmployeeAsync(CreateHourlyEmployeeDTO dto)
        {
            var employee = new HourlyEmployee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Gender = dto.Gender,
                JobTitle = dto.JobTitle,
                PhoneNumber = dto.PhoneNumber,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                DateOfBirth = dto.DateOfBirth,
                DateOfHire = dto.DateOfHire,
                DepartmentId = dto.DepartmentId,
                ManagerId = dto.ManagerId,
                HourlyRate = dto.HourlyRate,
                HoursWorked = dto.HoursWorked
            };

            await _uniteOfWork.EmployeeRepository.AddEmployeeAsync(employee);
            await _uniteOfWork.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<int> CreateManagerEmployeeAsync(CreateManagerEmployeeDTO dto)
        {
            var employee = new ManagerEmployee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Gender = dto.Gender,
                JobTitle = dto.JobTitle,
                PhoneNumber = dto.PhoneNumber,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                DateOfBirth = dto.DateOfBirth,
                DateOfHire = dto.DateOfHire,
                DepartmentId = dto.DepartmentId,
                ManagerId = dto.ManagerId,
                MonthlySalary = dto.MonthlySalary,
                Bonus = dto.Bonus,
            };

            await _uniteOfWork.EmployeeRepository.AddEmployeeAsync(employee);
            await _uniteOfWork.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<int> CreateSalariedEmployeeAsync(CreateSalariedEmployeeDTO dto)
        {
            var employee = new SalariedEmployee
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Gender = dto.Gender,
                JobTitle = dto.JobTitle,
                PhoneNumber = dto.PhoneNumber,
                Street = dto.Street,
                City = dto.City,
                State = dto.State,
                DateOfBirth = dto.DateOfBirth,
                DateOfHire = dto.DateOfHire,
                DepartmentId = dto.DepartmentId,
                ManagerId = dto.ManagerId,
                MonthlySalary = dto.MonthlySalary
            };

            await _uniteOfWork.EmployeeRepository.AddEmployeeAsync(employee);
            await _uniteOfWork.SaveChangesAsync();

            return employee.Id;
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _uniteOfWork.EmployeeRepository.DeleteEmployeeAsync(id);
            await _uniteOfWork.SaveChangesAsync();
        }

        public async Task<ICollection<EmployeeResponseDTO>> GetAllEmployeesAsync()
        {
            var response = await _uniteOfWork.EmployeeRepository.GetAllEmployeesAsync();

            var employees = response.Select(c => new EmployeeResponseDTO
            {
                Id = c.Id,
                FullName = c.FirstName + " " + c.LastName,
                CalculatedSalary = c.CalculateSalary(),
                JobTitle = c.JobTitle,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                Address = c.Street + ", " + c.City + ", " + c.State,
                Gender = c.Gender,
                DateOfHire = c.DateOfHire,
                DateOfBirth = c.DateOfBirth,
                ManagerName = c.Manager != null ? c.Manager.FullName : " ",
                DepartmentName = c.Department != null ? c.Department.Name : " ",
                BenefitNames = c.EmployeeBenefits.Where(b => b.IsActive == true).Select(b => b.Benefit.Name).ToList(),
            }).ToList();
            return employees;
        }

        public async Task<EmployeeResponseDTO> GetEmployeeByIdAsync(int id)
        {
            var response = await _uniteOfWork.EmployeeRepository.GetEmployeeByIdAsync(id);
            var employee = new EmployeeResponseDTO
            {
                Id = response.Id,
                FullName = response.FirstName + " " + response.LastName,
                CalculatedSalary = response.CalculateSalary(),
                JobTitle = response.JobTitle,
                PhoneNumber = response.PhoneNumber,
                Email = response.Email,
                Address = response.Street + ", " + response.City + ", " + response.State,
                Gender = response.Gender,
                DateOfHire = response.DateOfHire,
                DateOfBirth = response.DateOfBirth,
                ManagerName = response.Manager != null ? response.Manager.FullName : " ",
                DepartmentName = response.Department != null ? response.Department.Name : " ",
                BenefitNames = response.EmployeeBenefits.Where(b=>b.IsActive == true).Select(b => b.Benefit.Name).ToList(),
            };

            return employee;
        }
        public async Task UpdateEmployeeAsync(int id, UpdatedEmployeeDTO employee)
        {
            var response = await _uniteOfWork.EmployeeRepository.GetEmployeeByIdAsync(id);

            UpdateCommonFields(employee, response);
            UpdateSpecificFields(employee, response);
            if (employee.BenefitIds != null)
                UpdateEmployeeBenefits(employee, response);

            //await _uniteOfWork.EmployeeRepository.UpdateEmployeeAsync(response);
            await _uniteOfWork.SaveChangesAsync();

        }
        private void UpdateSpecificFields(UpdatedEmployeeDTO employee, Employee response)
        {
            switch (response)
            {
                case HourlyEmployee hourlyEmployee:
                    if (employee.HourlyRate.HasValue)
                        hourlyEmployee.HourlyRate = employee.HourlyRate.Value;
                    if (employee.HoursWorked.HasValue)
                        hourlyEmployee.HoursWorked = employee.HoursWorked.Value;
                    break;
                case CommissionEmployee commissionEmployee:
                    if (employee.Rate.HasValue)
                        commissionEmployee.Rate = employee.Rate.Value;
                    if (employee.Target.HasValue)
                        commissionEmployee.Target = employee.Target.Value;
                    break;
                case SalariedEmployee salariedEmployee:
                    if (employee.MonthlySalary.HasValue)
                        salariedEmployee.MonthlySalary = employee.MonthlySalary.Value;
                    break;
                case ManagerEmployee managerEmployee:
                    if (employee.MonthlySalary.HasValue)
                        managerEmployee.MonthlySalary = employee.MonthlySalary.Value;
                    if (employee.Bonus.HasValue)
                        managerEmployee.Bonus = employee.Bonus.Value;
                    break;
            }
        }
        private static void UpdateCommonFields(UpdatedEmployeeDTO employee, Employee response)
        {
            if (employee.FirstName != null)
                response.FirstName = employee.FirstName;

            if (employee.LastName != null)
                response.LastName = employee.LastName;

            if (employee.Email != null)
                response.Email = employee.Email;

            if (employee.JobTitle != null)
                response.JobTitle = employee.JobTitle;

            if (employee.PhoneNumber != null)
                response.PhoneNumber = employee.PhoneNumber;

            if (employee.Street != null)
                response.Street = employee.Street;

            if (employee.City != null)
                response.City = employee.City;

            if (employee.State != null)
                response.State = employee.State;

            if (employee.DateOfBirth != null)
                response.DateOfBirth = employee.DateOfBirth.Value;

            if (employee.DateOfHire != null)
                response.DateOfHire = employee.DateOfHire.Value;

            if (employee.ManagerId != null)
                response.ManagerId = employee.ManagerId;

            if (employee.DepartmentId != null)
                response.DepartmentId = employee.DepartmentId;
        }
        private void UpdateEmployeeBenefits(UpdatedEmployeeDTO employee, Employee response)
        {
            var activeBenefits = response.EmployeeBenefits.Where(b => b.IsActive);
            foreach (var emp in activeBenefits)
            {
                if (!employee.BenefitIds!.Contains(emp.BenefitId))
                {
                    emp.IsActive = false;
                }
            }
            foreach (var emp in employee.BenefitIds!)
            {
                var existing = response.EmployeeBenefits.FirstOrDefault(eb => eb.BenefitId == emp);

                if (existing != null)
                {
                    existing.IsActive = true;
                }
                else
                {
                    var newBenefit = new EmployeeBenefit();
                    newBenefit.BenefitId = emp;
                    newBenefit.IsActive = true;
                    newBenefit.EnrollmentDate = DateTime.Today;
                    response.EmployeeBenefits.Add(newBenefit);
                }
            }

        }
    }
}