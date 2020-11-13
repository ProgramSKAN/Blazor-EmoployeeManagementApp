using AutoMapper;
using EmployeeManagement.BlazorServerWeb.Models;
using EmployeeManagement.BlazorServerWeb.Services;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.BlazorServerWeb.Pages
{
    public class EditEmployeeBase:ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public IDepartmentService DepartmentService { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        private Employee Employee { get; set; } = new Employee();
        public List<Department> Departments { get; set; } = new List<Department>();
        //public string DepartmentId { get; set; }//sice binding a select element to int is not supported.so created string property

        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();
        public string PageHeaderText { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected async override Task OnInitializedAsync()
        {
            int.TryParse(Id, out int employeeId);
            if (employeeId != 0)
            {
                PageHeaderText = "Edit Employee";
                Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            }
            else
            {
                PageHeaderText = "Create Employee";
                Employee = new Employee
                {
                    DepartmentId = 1,
                    DateOfBrith = DateTime.Now,
                    PhotoPath = "images/nophoto.jpg"
                };
            }
            Departments = (await DepartmentService.GetDepartments()).ToList();
            //DepartmentId = Employee.DepartmentId.ToString();

            //EditEmployeeModel.EmployeeId = Employee.EmployeeId;
            //EditEmployeeModel.FirstName = Employee.FirstName;
            //EditEmployeeModel.LastName = Employee.LastName;
            //EditEmployeeModel.Email = Employee.Email;
            //EditEmployeeModel.ConfirmEmail = Employee.Email;
            //EditEmployeeModel.DateOfBrith = Employee.DateOfBrith;
            //EditEmployeeModel.Gender = Employee.Gender;
            //EditEmployeeModel.PhotoPath = Employee.PhotoPath;
            //EditEmployeeModel.DepartmentId = Employee.DepartmentId;
            //EditEmployeeModel.Department = Employee.Department;
            //OR
            Mapper.Map(Employee, EditEmployeeModel);

        }
        protected async Task HandleValidSubmit() 
        {
            Mapper.Map(EditEmployeeModel, Employee);
            Employee result = null;

            if (Employee.EmployeeId != 0)
            {
                result = await EmployeeService.UpdateEmployee(Employee);
            }
            else
            {
                result = await EmployeeService.CreateEmployee(Employee);
            }

            if (result != null)
            {
                NavigationManager.NavigateTo("/");
            }

        }

        protected async Task Delete_Click()
        {
            await EmployeeService.DeleteEmployee(Employee.EmployeeId);
            NavigationManager.NavigateTo("/");
        }
    }
}
