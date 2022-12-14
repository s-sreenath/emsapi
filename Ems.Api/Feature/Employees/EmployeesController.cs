// <copyright file="EmployeesController.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees;

using Ems.Api.Feature.Common.Extensions;
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
    private readonly ILogger<EmployeesController> logger;
    private readonly IEmployeeValidator employeeValidator;
    private readonly IMediator mediator;
    private readonly IEmployeeSearchValidator searchValidator;

    public EmployeesController(
        ILogger<EmployeesController> logger,
        IEmployeeValidator addEmployeeValidator,
        IMediator mediator,
        IEmployeeSearchValidator searchValidator)
    {
        this.logger = logger;
        this.employeeValidator = addEmployeeValidator;
        this.mediator = mediator;
        this.searchValidator = searchValidator;
    }

    [HttpPost]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AddEmployeeResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(List<ErrorDetail>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(List<ErrorDetail>), (int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<AddEmployeeResponse>> AddEmployeeAsync([FromBody]Employee employee)
    {
        this.logger.LogInformation($"{nameof(this.AddEmployeeAsync)} is called with {nameof(Employee)} - {employee.ToJson<Employee>()}");

        this.employeeValidator.Validate(employee);

        if (!this.employeeValidator.IsValid)
        {
            return this.BadRequest(this.employeeValidator.Errors);
        }

        var command = new AddEmployeeCommand(employee);
        var response = await this.mediator.Send(command).ConfigureAwait(true);

        if (response.Details.Any(e => e.ErrorCategory == ErrorCategory.Error.ToString()))
        {
            return this.BadRequest(response.Details);
        }

        return this.Ok(response);
    }

    [HttpGet]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("{employeeId}")]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(EmployeeInquiryResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(List<ErrorDetail>), (int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult<EmployeeInquiryResponse>> GetEmployee([FromRoute]int employeeId)
    {
        this.logger.LogInformation($"{nameof(this.GetEmployee)} is called with {nameof(employeeId)} - {employeeId}");

        if (employeeId <= 0)
        {
            return this.NotFound();
        }

        var command = new EmployeeInquiryQuery(employeeId);
        var response = await this.mediator.Send(command).ConfigureAwait(true);

        if (response.Employee.EmployeeId <= 0)
        {
            return this.NotFound();
        }

        return this.Ok(response);
    }

    [HttpPut]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("{employeeId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(List<ErrorDetail>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(List<ErrorDetail>), (int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> ModifyEmployee([FromRoute]int employeeId, [FromBody]Employee employee)
    {
        this.logger.LogInformation($"{nameof(this.ModifyEmployee)} is called with {nameof(employeeId)} - {employeeId}, {nameof(employee)} - {employee.ToJson<Employee>()}");

        if (employeeId <= 0)
        {
            return this.NotFound();
        }

        var inquiryQuery = new EmployeeInquiryQuery(employeeId);
        var employeeInquiryResponse = await this.mediator.Send(inquiryQuery).ConfigureAwait(true);

        if (employeeInquiryResponse?.Employee.EmployeeId != employeeId)
        {
            return this.NotFound();
        }

        this.employeeValidator.Validate(employee);

        if (!this.employeeValidator.IsValid)
        {
            return this.BadRequest(this.employeeValidator.Errors);
        }

        var command = new ModifyEmployeeCommand(employeeId, employee);
        await this.mediator.Send(command).ConfigureAwait(true);

        return this.NoContent();
    }

    [HttpDelete]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("{employeeId}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> DeleteEmployee(int employeeId)
    {
        if (employeeId <= 0)
        {
            return this.NotFound();
        }

        var command = new DeleteEmployeeCommand(employeeId);
        await this.mediator.Send(command).ConfigureAwait(true);

        return this.NoContent();
    }

    [HttpPost]
    [Route("searches")]
    [ProducesResponseType(typeof(List<ErrorDetail>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(EmployeeSearchResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<EmployeeSearchResponse>> SearchEmployee([FromBody]EmployeeSearchRequest request)
    {
        this.logger.LogInformation($"{nameof(this.SearchEmployee)} is called with {nameof(request)} - {request.ToJson()}");

        this.searchValidator.Validate(request);

        if (!this.searchValidator.IsValid)
        {
            return this.BadRequest(this.searchValidator.Errors);
        }

        var command = new EmployeeSearchCommand(request);
        var response = await this.mediator.Send(command).ConfigureAwait(true);

        return this.Ok(response);
    }
}
