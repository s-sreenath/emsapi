// <copyright file="EmployeesController.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees;

using Ems.Api.Feature.Common.Models;
using Ems.Api.Feature.Employees.Models;
using Ems.Api.Feature.Employees.Models.Response;
using Ems.Api.Feature.Employees.Validators.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
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

    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AddEmployeeResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(List<ErrorDetail>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(List<ErrorDetail>), (int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<AddEmployeeResponse>> AddEmployeeAsync([FromBody]Employee employee)
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
