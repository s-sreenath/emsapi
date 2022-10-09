// <copyright file="EmployeesControllerTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees;

using Ems.Api.Feature.Common.Models;
using Ems.Api.Feature.Employees;
using Ems.Api.Feature.Employees.Commands;
using Ems.Api.Feature.Employees.Models;
using Ems.Api.Feature.Employees.Models.Response;
using Ems.Api.Feature.Employees.Validators.Interfaces;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

[TestClass]
[ExcludeFromCodeCoverage]
public class EmployeesControllerTests
{
    private ILogger<EmployeesController> logger;
    private IEmployeeValidator employeeValidator;
    private IMediator mediator;
    private EmployeesController controller;
    private IEmployeeSearchValidator searchValidator;

    [TestInitialize]
    public void TestInitialize()
    {
        this.logger = new NullLogger<EmployeesController>();
        this.employeeValidator = A.Fake<IEmployeeValidator>();
        this.mediator = A.Fake<IMediator>();
        this.searchValidator = A.Fake<IEmployeeSearchValidator>();

        this.controller = new EmployeesController(
            this.logger,
            this.employeeValidator,
            this.mediator,
            this.searchValidator);
    }

    [TestMethod]
    public async Task AddEmployee_Should_Return_BadRequest_When_Request_Is_InValid()
    {
        // Arrange
        var employee = new Employee()
        {
            FirstName = "firstName",
            Email = "email",
            Age = 15,
        };

        List<ErrorDetail> expectedErrorList = new List<ErrorDetail>()
        {
            new ErrorDetail()
            {
                ErrorCode = "ErrorCode",
                ElementValue = "ElementValue",
                ErrorCategory = "ErrorCategory",
                ErrorDescription = "ErrorDescription",
                ErrorElement = "ErrorElement",
            },
        };

        A.CallTo(() => this.employeeValidator.IsValid).Returns(false);
        A.CallTo(() => this.employeeValidator.Errors).Returns(expectedErrorList);

        // Act
        var result = await this.controller.AddEmployeeAsync(employee).ConfigureAwait(true);
        var badRequestObjectResult = result.Result as BadRequestObjectResult;
        var errorList = badRequestObjectResult?.Value as List<ErrorDetail>;

        // Assert
        result.ShouldNotBeNull();
        badRequestObjectResult.ShouldNotBeNull();

        A.CallTo(() => this.employeeValidator.Validate(employee)).MustHaveHappened();

        errorList.ShouldNotBeNull();
        errorList.Count.ShouldBe(1);
        errorList[0].ShouldNotBeNull();
        errorList[0].ErrorCode.ShouldBe(expectedErrorList[0].ErrorCode);
        errorList[0].ElementValue.ShouldBe(expectedErrorList[0].ElementValue);
        errorList[0].ErrorCategory.ShouldBe(expectedErrorList[0].ErrorCategory);
        errorList[0].ErrorDescription.ShouldBe(expectedErrorList[0].ErrorDescription);
        errorList[0].ErrorElement.ShouldBe(expectedErrorList[0].ErrorElement);

        A.CallTo(() => this.mediator.Send(A<AddEmployeeCommand>._, CancellationToken.None)).MustNotHaveHappened();
    }

    [TestMethod]
    public async Task AddEmployee_Should_Return_BadRequest_When_Handler_Returns_Errors()
    {
        // Arrange
        var employee = new Employee()
        {
            FirstName = "firstName",
            Email = "email",
            Age = 15,
        };

        List<ErrorDetail> expectedErrorList = new List<ErrorDetail>()
        {
            new ErrorDetail()
            {
                ErrorCode = "ErrorCode",
                ElementValue = "ElementValue",
                ErrorCategory = "Error",
                ErrorDescription = "ErrorDescription",
                ErrorElement = "ErrorElement",
            },
        };

        var response = new AddEmployeeResponse();
        response.Details.AddRange(expectedErrorList);

        A.CallTo(() => this.employeeValidator.IsValid).Returns(true);
        A.CallTo(() => this.mediator.Send(A<AddEmployeeCommand>._, CancellationToken.None)).Returns(response);

        // Act
        var result = await this.controller.AddEmployeeAsync(employee).ConfigureAwait(true);
        var badRequestObjectResult = result.Result as BadRequestObjectResult;
        var errorList = badRequestObjectResult?.Value as List<ErrorDetail>;

        // Assert
        result.ShouldNotBeNull();
        badRequestObjectResult.ShouldNotBeNull();

        A.CallTo(() => this.employeeValidator.Validate(employee)).MustHaveHappened();

        errorList.ShouldNotBeNull();
        errorList.Count.ShouldBe(1);
        errorList[0].ShouldNotBeNull();
        errorList[0].ErrorCode.ShouldBe(expectedErrorList[0].ErrorCode);
        errorList[0].ElementValue.ShouldBe(expectedErrorList[0].ElementValue);
        errorList[0].ErrorCategory.ShouldBe(expectedErrorList[0].ErrorCategory);
        errorList[0].ErrorDescription.ShouldBe(expectedErrorList[0].ErrorDescription);
        errorList[0].ErrorElement.ShouldBe(expectedErrorList[0].ErrorElement);
    }

    [TestMethod]
    public async Task AddEmployee_Should_Return_Ok_Response_When_Request_IsValid()
    {
        // Arrange
        var employee = new Employee()
        {
            FirstName = "firstName",
            LastName = "LastName",
            Email = "email@email.com",
            Age = 15,
        };

        A.CallTo(() => this.employeeValidator.IsValid).Returns(true);

        // Act
        var result = await this.controller.AddEmployeeAsync(employee).ConfigureAwait(true);
        var okObjectResult = result.Result as OkObjectResult;
        var resultObject = okObjectResult?.Value as AddEmployeeResponse;

        // Assert
        result.ShouldNotBeNull();
        okObjectResult.ShouldNotBeNull();
        resultObject.ShouldNotBeNull();
        A.CallTo(() => this.mediator.Send(
            A<AddEmployeeCommand>.That.Matches(e => e.FirstName == employee.FirstName &&
            e.LastName == employee.LastName &&
            e.Age == employee.Age &&
            e.Email == employee.Email), CancellationToken.None)).MustHaveHappened();
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    public async Task GetEmployee_Should_Return_404_When_EmployeeId_Is_Less_Than_Equal_To_0(int employeeId)
    {
        // Act
        var result = await this.controller.GetEmployee(employeeId).ConfigureAwait(true);
        var notFoundResult = result.Result as NotFoundResult;

        // Assert
        result.ShouldNotBeNull();
        notFoundResult.ShouldNotBeNull();
    }

    [TestMethod]
    public async Task GetEmployee_Should_Return_404_When_EmployeeId_Is_Not_Found()
    {
        // Arrange
        var employeeId = 457;
        A.CallTo(() => this.mediator.Send(A<EmployeeInquiryQuery>._, CancellationToken.None)).Returns(new EmployeeInquiryResponse());

        // Act
        var result = await this.controller.GetEmployee(employeeId).ConfigureAwait(true);
        var notFoundResult = result.Result as NotFoundResult;

        // Assert
        result.ShouldNotBeNull();
        notFoundResult.ShouldNotBeNull();
    }

    [TestMethod]
    public async Task GetEmployee_Should_Return_Employee_Record()
    {
        // Arrange
        var employeeId = 15;
        var inquiryReponse = new EmployeeInquiryResponse()
        {
            Employee = new Employee()
            {
                FirstName = "FirstName",
                EmployeeId = employeeId,
                Age = 15,
                Email = "Email@Email.com",
                LastName = "LastName",
            },
        };

        A.CallTo(() => this.mediator.Send(A<EmployeeInquiryQuery>.That.Matches(e => e.EmployeeId == employeeId), CancellationToken.None)).Returns(inquiryReponse);

        // Act
        var result = await this.controller.GetEmployee(employeeId).ConfigureAwait(true);
        var okObjectResult = result.Result as OkObjectResult;
        var resultObject = okObjectResult?.Value as EmployeeInquiryResponse;

        // Assert
        result.ShouldNotBeNull();
        okObjectResult.ShouldNotBeNull();
        resultObject.ShouldNotBeNull();
        resultObject.Employee.ShouldNotBeNull();
        resultObject.Employee.EmployeeId.ShouldBe(employeeId);
    }

    [TestMethod]
    public async Task ModifyEmployee_Should_Return_NoContent_Responses_When_Request_Is_Valid()
    {
        // Arrange
        var employeeId = 134;
        var employee = new Employee()
        {
            FirstName = "firstName",
            LastName = "LastName",
            Email = "email@Email.com",
            Age = 15,
            EmployeeId = employeeId,
        };

        A.CallTo(() => this.mediator.Send(A<EmployeeInquiryQuery>._, CancellationToken.None)).Returns(new EmployeeInquiryResponse()
        {
            Employee = employee,
        });

        A.CallTo(() => this.employeeValidator.IsValid).Returns(true);

        // Act
        var result = await this.controller.ModifyEmployee(employeeId, employee).ConfigureAwait(true);
        var noContentResult = result as NoContentResult;

        // Assert
        result.ShouldNotBeNull();
        noContentResult.ShouldNotBeNull();
        A.CallTo(() => this.mediator.Send(A<ModifyEmployeeCommand>._, CancellationToken.None)).MustHaveHappened();
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public async Task ModifyEmployee_Should_Return_NotFound_Responses_When_EmployeeId_Is_Less_Than_Equal_To_0(int employeeId)
    {
        // Arrange
        var employee = new Employee()
        {
            FirstName = "firstName",
            LastName = "LastName",
            Email = "email@Email.com",
            Age = 15,
        };

        // Act
        var result = await this.controller.ModifyEmployee(employeeId, employee).ConfigureAwait(true);
        var notFoundResult = result as NotFoundResult;

        // Assert
        result.ShouldNotBeNull();
        notFoundResult.ShouldNotBeNull();
        A.CallTo(() => this.mediator.Send(A<ModifyEmployeeCommand>._, CancellationToken.None)).MustNotHaveHappened();
    }

    [TestMethod]
    public async Task ModifyEmployee_Should_Return_NotFound_Responses_When_EmployeeId_NotFound()
    {
        // Arrange
        var employeeId = 154;
        var employee = new Employee()
        {
            FirstName = "firstName",
            LastName = "LastName",
            Email = "email@Email.com",
            Age = 15,
        };

        A.CallTo(() => this.mediator.Send(A<EmployeeInquiryQuery>._, CancellationToken.None)).Returns(new EmployeeInquiryResponse());

        // Act
        var result = await this.controller.ModifyEmployee(employeeId, employee).ConfigureAwait(true);
        var notFoundResult = result as NotFoundResult;

        // Assert
        result.ShouldNotBeNull();
        notFoundResult.ShouldNotBeNull();
        A.CallTo(() => this.mediator.Send(A<EmployeeInquiryQuery>._, CancellationToken.None)).MustHaveHappened();
        A.CallTo(() => this.mediator.Send(A<ModifyEmployeeCommand>._, CancellationToken.None)).MustNotHaveHappened();
    }

    [TestMethod]
    public async Task ModifyEmployee_Should_Return_BadRequest_Responses_When_Request_Is_InValid()
    {
        // Arrange
        var employeeId = 154;
        var employee = new Employee()
        {
            FirstName = "firstName",
            LastName = "LastName",
            Email = "email@Email.com",
            Age = 15,
            EmployeeId = employeeId,
        };

        A.CallTo(() => this.mediator.Send(A<EmployeeInquiryQuery>._, CancellationToken.None)).Returns(new EmployeeInquiryResponse()
        {
            Employee = employee,
        });

        List<ErrorDetail> expectedErrorList = new List<ErrorDetail>()
        {
            new ErrorDetail()
            {
                ErrorCode = "ErrorCode",
                ElementValue = "ElementValue",
                ErrorCategory = "ErrorCategory",
                ErrorDescription = "ErrorDescription",
                ErrorElement = "ErrorElement",
            },
        };

        A.CallTo(() => this.employeeValidator.IsValid).Returns(false);
        A.CallTo(() => this.employeeValidator.Errors).Returns(expectedErrorList);

        // Act
        var result = await this.controller.ModifyEmployee(employeeId, employee).ConfigureAwait(true);
        var badRequestObjectResult = result as BadRequestObjectResult;
        var errorList = badRequestObjectResult?.Value as List<ErrorDetail>;

        // Assert
        result.ShouldNotBeNull();
        badRequestObjectResult.ShouldNotBeNull();

        A.CallTo(() => this.employeeValidator.Validate(employee)).MustHaveHappened();

        errorList.ShouldNotBeNull();
        errorList.Count.ShouldBe(1);
        errorList[0].ShouldNotBeNull();
        errorList[0].ErrorCode.ShouldBe(expectedErrorList[0].ErrorCode);
        errorList[0].ElementValue.ShouldBe(expectedErrorList[0].ElementValue);
        errorList[0].ErrorCategory.ShouldBe(expectedErrorList[0].ErrorCategory);
        errorList[0].ErrorDescription.ShouldBe(expectedErrorList[0].ErrorDescription);
        errorList[0].ErrorElement.ShouldBe(expectedErrorList[0].ErrorElement);

        A.CallTo(() => this.mediator.Send(A<ModifyEmployeeCommand>._, CancellationToken.None)).MustNotHaveHappened();
    }

    [TestMethod]
    public async Task DeleteEmployee_Should_Return_NoContent_Responses_When_Handler_Success()
    {
        // Arrange
        var employeeId = 134;

        A.CallTo(() => this.mediator.Send(A<DeleteEmployeeCommand>._, CancellationToken.None)).Returns(new DeleteEmployeeResponse());

        // Act
        var result = await this.controller.DeleteEmployee(employeeId).ConfigureAwait(true);
        var noContentResult = result as NoContentResult;

        // Assert
        result.ShouldNotBeNull();
        noContentResult.ShouldNotBeNull();
        A.CallTo(() => this.mediator.Send(A<DeleteEmployeeCommand>._, CancellationToken.None)).MustHaveHappened();
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public async Task DeleteEmployee_Should_Return_NotFound_Responses_When_EmployeeId_Is_Less_Than_Equal_To_0(int employeeId)
    {
        // Act
        var result = await this.controller.DeleteEmployee(employeeId).ConfigureAwait(true);
        var notFoundResult = result as NotFoundResult;

        // Assert
        result.ShouldNotBeNull();
        notFoundResult.ShouldNotBeNull();
        A.CallTo(() => this.mediator.Send(A<DeleteEmployeeCommand>._, CancellationToken.None)).MustNotHaveHappened();
    }

    [TestMethod]
    public async Task SearchEmployee_Should_Return_BadRequest_When_Validator_Returns_An_Error()
    {
        // Arrange
        var request = new EmployeeSearchRequest();

        List<ErrorDetail> expectedErrorList = new List<ErrorDetail>()
        {
            new ErrorDetail()
            {
                ErrorCode = "ErrorCode",
                ElementValue = "ElementValue",
                ErrorCategory = "ErrorCategory",
                ErrorDescription = "ErrorDescription",
                ErrorElement = "ErrorElement",
            },
        };

        A.CallTo(() => this.searchValidator.IsValid).Returns(false);
        A.CallTo(() => this.searchValidator.Errors).Returns(expectedErrorList);

        // Act
        var result = await this.controller.SearchEmployee(request);
        var badRequestObject = result.Result as BadRequestObjectResult;
        var errorList = badRequestObject?.Value as List<ErrorDetail>;

        // Assert
        result.ShouldNotBeNull();
        badRequestObject.ShouldNotBeNull();
        errorList.ShouldNotBeNull();
        errorList.Count.ShouldBeGreaterThan(0);
        A.CallTo(() => this.mediator.Send(A<EmployeeSearchCommand>._, CancellationToken.None)).MustNotHaveHappened();
    }

    [TestMethod]
    public async Task SearchEmployee_Should_Return_Call_Handler_When_Request_Is_Valid()
    {
        // Arrange
        var request = new EmployeeSearchRequest();

        List<ErrorDetail> expectedErrorList = new List<ErrorDetail>()
        {
            new ErrorDetail()
            {
                ErrorCode = "ErrorCode",
                ElementValue = "ElementValue",
                ErrorCategory = "ErrorCategory",
                ErrorDescription = "ErrorDescription",
                ErrorElement = "ErrorElement",
            },
        };

        var response = new EmployeeSearchResponse();
        response.Employees.Add(new Employee());

        A.CallTo(() => this.mediator.Send(A<EmployeeSearchCommand>._, CancellationToken.None)).Returns(response);
        A.CallTo(() => this.searchValidator.IsValid).Returns(true);

        // Act
        var result = await this.controller.SearchEmployee(request);
        var okObjectResult = result.Result as OkObjectResult;
        var searchResult = okObjectResult?.Value as EmployeeSearchResponse;

        // Assert
        result.ShouldNotBeNull();
        okObjectResult.ShouldNotBeNull();
        searchResult.ShouldNotBeNull();
        searchResult.Employees.Count.ShouldBeGreaterThan(0);
    }
}
