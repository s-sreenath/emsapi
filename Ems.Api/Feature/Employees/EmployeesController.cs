// <copyright file="EmployeesController.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees;

using Ems.Api.Feature.Employees.Models;
using Ems.Api.Feature.Employees.Models.Response;
using Ems.Api.Feature.Employees.Validators.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/v1/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private IAddEmployeeValidator addEmployeeValidator;

    public EmployeesController(IAddEmployeeValidator addEmployeeValidator)
    {
        this.addEmployeeValidator = addEmployeeValidator;
    }

    public async Task<ActionResult<AddEmployeeResponse>> AddEmployeeAsync(Employee employee)
    {
        await Task.Delay(10);

        this.addEmployeeValidator.Validate(employee);

        if (!this.addEmployeeValidator.IsValid)
        {
            return this.BadRequest(this.addEmployeeValidator.Errors);
        }

        return this.Ok();
    }
}
