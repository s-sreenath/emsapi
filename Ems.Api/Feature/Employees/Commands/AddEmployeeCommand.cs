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

    public string FirstName { get; }

    public string LastName { get; }

    public string Email { get; }

    public int Age { get; }
}
