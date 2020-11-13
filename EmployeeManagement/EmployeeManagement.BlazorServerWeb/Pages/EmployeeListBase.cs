using EmployeeManagement.BlazorServerWeb.Services;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeManagement.BlazorServerWeb.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject]//to inject services
        public IEmployeeService EmployeeService { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
        public bool ShowFooter { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            //LoadEmployees();
            //return base.OnInitializedAsync();or

            //await Task.Run(LoadEmployees);//hard coded data

            Employees=(await EmployeeService.GetEmployees()).ToList();//data from API 
        }

        public int SelectedEmployeesCount { get; set; } = 0;
        protected async Task EmployeeDeleted()
        {
            Employees = (await EmployeeService.GetEmployees()).ToList();
        }







        protected void EmployeeSelectionChanged(bool isSelected)
        {
            if (isSelected)
            {
                SelectedEmployeesCount++;
            }
            else
            {
                SelectedEmployeesCount--;
            }
        }


        private void LoadEmployees()
        {
            Thread.Sleep(5000);
            Employee e1 = new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Hastings",
                Email = "David@pragimtech.com",
                DateOfBrith = new DateTime(1980, 10, 5),
                Gender = Gender.Male,
                DepartmentId = 1,
                PhotoPath = "images/john.png"
            };

            Employee e2 = new Employee
            {
                EmployeeId = 2,
                FirstName = "Sam",
                LastName = "Galloway",
                Email = "Sam@pragimtech.com",
                DateOfBrith = new DateTime(1981, 12, 22),
                Gender = Gender.Male,
                DepartmentId = 2, 
                PhotoPath = "images/sam.jpg"
            };

            Employee e3 = new Employee
            {
                EmployeeId = 3,
                FirstName = "Mary",
                LastName = "Smith",
                Email = "mary@pragimtech.com",
                DateOfBrith = new DateTime(1979, 11, 11),
                Gender = Gender.Female,
                DepartmentId = 1,
                PhotoPath = "images/mary.png"
            };

            Employee e4 = new Employee
            {
                EmployeeId = 3,
                FirstName = "Sara",
                LastName = "Longway",
                Email = "sara@pragimtech.com",
                DateOfBrith = new DateTime(1982, 9, 23),
                Gender = Gender.Female,
                DepartmentId = 3,
                PhotoPath = "images/sara.png"
            };

            Employees = new List<Employee> { e1, e2, e3, e4 };
        }
    }
}
