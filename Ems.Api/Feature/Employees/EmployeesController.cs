// <copyright file="EmployeesController.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees;

using Ems.Api.Feature.Common.Models;
using Ems.Api.Feature.Employees.Commands;
using Ems.Api.Feature.Employees.Models;
using Ems.Api.Feature.Employees.Models.Response;
using Ems.Api.Feature.Employees.Validators.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

[Route("api/v1/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private readonly IAddEmployeeValidator addEmployeeValidator;
    private readonly IMediator mediator;

    public EmployeesController(
        IAddEmployeeValidator addEmployeeValidator,
        IMediator mediator)
    {
        this.addEmployeeValidator = addEmployeeValidator;
        this.mediator = mediator;
    }

    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AddEmployeeResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(List<ErrorDetail>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(List<ErrorDetail>), (int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<AddEmployeeResponse>> AddEmployeeAsync([FromBody]Employee employee)
    {
        this.addEmployeeValidator.Validate(employee);

        if (!this.addEmployeeValidator.IsValid)
        {
            return this.BadRequest(this.addEmployeeValidator.Errors);
        }

        var command = new AddEmployeeCommand(employee);
        var response = await this.mediator.Send(command).ConfigureAwait(true);

        if (response.Details.Any(e => e.ErrorCategory == ErrorCategory.Error.ToString()))
        {
            return this.BadRequest(response.Details);
        }

        return this.Ok(response);
    }
}
