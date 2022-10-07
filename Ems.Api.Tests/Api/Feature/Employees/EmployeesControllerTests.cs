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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

[TestClass]
public class EmployeesControllerTests
{
    private IAddEmployeeValidator addEmployeeValidator;
    private IMediator mediator;
    private EmployeesController controller;

    [TestInitialize]
    public void TestInitialize()
    {
        this.addEmployeeValidator = A.Fake<IAddEmployeeValidator>();
        this.mediator = A.Fake<IMediator>();

        this.controller = new EmployeesController(
            this.addEmployeeValidator,
            this.mediator);
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

        A.CallTo(() => this.addEmployeeValidator.IsValid).Returns(false);
        A.CallTo(() => this.addEmployeeValidator.Errors).Returns(expectedErrorList);

        // Act
        var result = await this.controller.AddEmployeeAsync(employee).ConfigureAwait(true);
        var badRequestObjectResult = result.Result as BadRequestObjectResult;
        var errorList = badRequestObjectResult?.Value as List<ErrorDetail>;

        // Assert
        result.ShouldNotBeNull();
        badRequestObjectResult.ShouldNotBeNull();

        A.CallTo(() => this.addEmployeeValidator.Validate(employee)).MustHaveHappened();

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

        A.CallTo(() => this.addEmployeeValidator.IsValid).Returns(true);

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
}
