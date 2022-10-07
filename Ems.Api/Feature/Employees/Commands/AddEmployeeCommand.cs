// <copyright file="AddEmployeeCommand.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Commands;

using Ems.Api.Feature.Employees.Models;
using Ems.Api.Feature.Employees.Models.Response;
using MediatR;

public class AddEmployeeCommand : IRequest<AddEmployeeResponse>
{
    public AddEmployeeCommand(Employee employee)
    {
        this.FirstName = employee.FirstName;
        this.LastName = employee.LastName;
        this.Age = employee.Age;
        this.Email = employee.Email;
    }

    public object FirstName { get; }

    public object LastName { get; }

    public object Email { get; }

    public object Age { get; }
}
