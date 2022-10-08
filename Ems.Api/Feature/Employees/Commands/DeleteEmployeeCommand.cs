// <copyright file="DeleteEmployeeCommand.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Commands
{
    using Ems.Api.Feature.Employees.Models.Response;
    using MediatR;

    public class DeleteEmployeeCommand : IRequest<DeleteEmployeeResponse>
    {
        public DeleteEmployeeCommand(int employeeId)
        {
            this.EmployeeId = employeeId;
        }

        public int EmployeeId { get; }
    }
}
