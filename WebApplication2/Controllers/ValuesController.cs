using Microsoft.AspNetCore.Mvc;
/// Correct namespace for Employee model
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    // Simulating a database with an in-memory list of employees
    private static List<Employee> employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "John Doe", Age = 30 },
            new Employee { Id = 2, Name = "Jane Smith", Age = 25 },
            new Employee { Id = 3, Name = "Mark Johnson", Age = 35 }
        };

    // 1. GET api/item (Get All Employees)
    [HttpGet]
    public ActionResult<IEnumerable<Employee>> GetEmployees()
    {
        return Ok(employees);  // Returns all employees as a JSON response
    }

    // 2. POST api/item (Add New Employee)
    [HttpPost]
    public ActionResult<Employee> AddEmployee([FromBody] Employee newEmployee)
    {
        // Generate new ID for the new employee
        newEmployee.Id = employees.Max(e => e.Id) + 1;

        employees.Add(newEmployee);  // Add the new employee to the list
        return CreatedAtAction(nameof(GetEmployees), new { id = newEmployee.Id }, newEmployee);  // Returns 201 Created response
    }

    // 3. PUT api/item/{id} (Edit Employee Data)
    [HttpPut("{id}")]
    public ActionResult<Employee> EditEmployee(int id, [FromBody] Employee updatedEmployee)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();  // If employee not found, return 404 Not Found
        }

        employee.Name = updatedEmployee.Name;
        employee.Age = updatedEmployee.Age;
        return Ok(employee);  // Return updated employee
    }

    // 4. DELETE api/item/{id} (Delete Employee)
    [HttpDelete("{id}")]
    public ActionResult DeleteEmployee(int id)
    {
        var employee = employees.FirstOrDefault(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();  // If employee not found, return 404 Not Found
        }

        employees.Remove(employee);  // Remove employee from the list
        return NoContent();
    }// Return 204 No Content response after deletion
    public class Employee
    {
        [Required(ErrorMessage = "please enter id")]    
        
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
    

