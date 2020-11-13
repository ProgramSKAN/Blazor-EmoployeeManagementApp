using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Api.Models;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                return Ok(await employeeRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result=await employeeRepository.GetEmployee(id);
                if (result == null)  return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee employee)//here model binding works if [ApiController] kept for class. or use [FromBody] attribute in method parameter
        {
            try
            {
                //if(ModelState.IsValid)//this is automatic in .net core using model annotaions
                if (employee == null)
                    return BadRequest();

                var emp = await employeeRepository.GetEmployeeByEmail(employee.Email);
                if (emp != null)
                {
                    ModelState.AddModelError("email", "Employee Email alreay in use");
                    return BadRequest(ModelState);
                }

                var createdEmployee = await employeeRepository.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee),
                    new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new employee record");
            }
        }

        //[HttpPut("{id:int}")]
        [HttpPut()]
        public async Task<ActionResult<Employee>> UpdateEmployee(/*int id,*/ Employee employee)
        {
            try
            {
                //if (id != employee.EmployeeId)
                //    return BadRequest("Employee ID mismatch");

                var employeeToUpdate = await employeeRepository.GetEmployee(employee.EmployeeId);

                if (employeeToUpdate == null)
                    return NotFound($"Employee with Id = {employee.EmployeeId} not found");

                return await employeeRepository.UpdateEmployee(employee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await employeeRepository.GetEmployee(id);

                if (employeeToDelete == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                return await employeeRepository.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Error deleting data");
            }
        }

        //[HttpGet("{search}/{name}/{gender?}")]
        //api/employees/search/john/male       >not preferred

        // api/employees/search?name=john
        // api/employees/search?gender=male
        // api/employees/search?name=john&gender=male
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender)
        {
            try
            {
                var result = await employeeRepository.Search(name, gender);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}