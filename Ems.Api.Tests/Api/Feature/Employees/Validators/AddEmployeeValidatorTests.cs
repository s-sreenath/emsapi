// <copyright file="AddEmployeeValidatorTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Validators;

using System.Linq;
using Ems.Api.Feature.Employees.Models;
using Ems.Api.Feature.Employees.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

[TestClass]
public class AddEmployeeValidatorTests
{
    private AddEmployeeValidator validator;

    [TestInitialize]
    public void TestInitialize()
    {
        this.validator = new AddEmployeeValidator();
    }

    [TestMethod]
    public void Should_Pass_When_Inputs_Are_Valid()
    {
        // Arrange
        var employee = new Employee()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Age = 10,
            Email = "Email@email.com",
        };

        // Act
        this.validator.Validate();

        // Assert
        this.validator.IsValid.ShouldBeTrue();
        this.validator.Errors.Count.ShouldBe(0);
    }
}
