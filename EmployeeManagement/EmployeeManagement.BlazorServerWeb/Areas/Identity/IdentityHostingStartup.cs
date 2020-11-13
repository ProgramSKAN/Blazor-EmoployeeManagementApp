using System;
using EmployeeManagement.BlazorServerWeb.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EmployeeManagement.BlazorServerWeb.Areas.Identity.IdentityHostingStartup))]
namespace EmployeeManagement.BlazorServerWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<EmployeeManagementBlazorServerWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("EmployeeManagementBlazorServerWebContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<EmployeeManagementBlazorServerWebContext>();
            });
        }
    }
}