using AssetManagementApi.Dtos.Employees;
using AssetManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AssetManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>List of employees.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetEmployeesAsync();
            return Ok(employees);
        }

        /// <summary>
        /// Gets an employee and their assigned assets.
        /// </summary>
        /// <param name="id">The employee id</param>
        /// <returns>The employee</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeAsync(id);
            return employee is null ? NotFound() : Ok(employee);
        }

        /// <summary>
        /// Creates a new employee.
        /// </summary>
        /// <param name="dto">The employee data</param>
        /// <returns>employee</returns>
        [Authorize(Roles = "Admin,IT")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateEmployee(EmployeeCreateUpdateDto dto)
        {
            var result = await _employeeService.CreateEmployeeAsync(dto);

            if (!result.Succeeded)
            {
                return result.IsNotFound ? NotFound() : BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(GetEmployee), new { id = result.Data!.Id }, result.Data);
        }

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="id">The employee id</param>
        /// <param name="dto">The employee data to update.</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,IT")]
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeCreateUpdateDto dto)
        {
            var result = await _employeeService.UpdateEmployeeAsync(id, dto);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return result.IsNotFound ? NotFound() : BadRequest(result.Errors);
        }

        /// <summary>
        /// Deletes an employee.
        /// </summary>
        /// <param name="id">The employee id</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin,IT")]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return result.IsNotFound ? NotFound() : BadRequest(result.Errors);
        }
    }
}