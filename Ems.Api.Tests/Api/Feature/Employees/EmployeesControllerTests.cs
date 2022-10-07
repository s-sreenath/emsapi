// <copyright file="EmployeesControllerTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees;

using Ems.Api.Feature.Employees;
using Ems.Api.Feature.Employees.Models;
using Ems.Api.Feature.Employees.Validators.Interfaces;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Threading.Tasks;

[TestClass]
public class EmployeesControllerTests
{
    private IAddEmployeeValidator addEmployeeValidator;
    private EmployeesController controller;

    [TestInitialize]
    public void TestInitialize()
    {
        this.addEmployeeValidator = A.Fake<IAddEmployeeValidator>();
        this.controller = new EmployeesController(
            this.addEmployeeValidator);
    }

    [TestMethod]
    public async Task AddEmployee_Should_Return_BadRequest_When_Request_Is_InValid()
    {
        // Arrange
        var employee = new Employee();

        A.CallTo(() => this.addEmployeeValidator.IsValid).Returns(false);

        // Act
        var result = await this.controller.AddEmployeeAsync(employee).ConfigureAwait(true);
        var badRequestObjectResult = result.Result as BadRequestObjectResult;

        // Assert
        result.ShouldNotBeNull();
        badRequestObjectResult.ShouldNotBeNull();
        A.CallTo(() => this.addEmployeeValidator.Validate(employee)).MustHaveHappened();
    }
}
