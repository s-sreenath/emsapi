// <copyright file="ModifyEmployeeCommand.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Commands
{
    using Ems.Api.Feature.Employees.Models;
    using Ems.Api.Feature.Employees.Models.Response;
    using MediatR;

    public class ModifyEmployeeCommand : IRequest<ModifyEmployeeResponse>
    {
        public ModifyEmployeeCommand(int employeeId, Employee employee)
        {
            this.EmployeeId = employeeId;
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.Age = employee.Age;
            this.Email = employee.Email;
        }

        public int EmployeeId { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public int Age { get; }

        public string Email { get; }
    }
}
