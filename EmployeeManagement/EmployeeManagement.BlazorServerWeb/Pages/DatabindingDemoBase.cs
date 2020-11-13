using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.BlazorServerWeb.Pages
{
    public class DatabindingDemoBase:ComponentBase
    {
        protected string Name { get; set; } = "Jack";
        protected string Gender { get; set; } = "Male";
        protected string Colour { get; set; } = "background-color:white";

        public string Description { get; set; } = string.Empty;
    }
}
