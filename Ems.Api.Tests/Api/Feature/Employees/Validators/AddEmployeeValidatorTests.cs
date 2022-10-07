// <copyright file="AddEmployeeValidatorTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Validators;

using System.Linq;
using Ems.Api.Feature.Common.Models;
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
        this.validator.Validate(employee);

        // Assert
        this.validator.IsValid.ShouldBeTrue();
        this.validator.Errors.Count.ShouldBe(0);
    }

    [TestMethod]
    public void Should_Return_IsValid_False_When_FirstName_Length_Exceeds_Limit()
    {
        // Arrange
        var employee = new Employee()
        {
            FirstName = new string('q', 51),
            LastName = "LastName",
            Age = 10,
            Email = "Email@email.com",
        };

        // Act
        this.validator.Validate(employee);

        // Assert
        this.validator.IsValid.ShouldBeFalse();
        this.validator.Errors.Any(e =>
        e.ErrorCode == ErrorCode.EmployeeFirstNameExceedsMaxLength.ToString("D") &&
        e.ErrorCategory == ErrorCategory.Error.ToString() &&
        e.ErrorElement == "FirstName" &&
        e.ElementValue == employee.FirstName).ShouldBeTrue();
    }

    [TestMethod]
    public void Should_Return_IsValid_False_When_FirstName_Is_Empty()
    {
        // Arrange
        var employee = new Employee()
        {
            LastName = "LastName",
            Age = 10,
            Email = "Email@email.com",
        };

        // Act
        this.validator.Validate(employee);

        // Assert
        this.validator.IsValid.ShouldBeFalse();
        this.validator.Errors.Any(e =>
        e.ErrorCode == ErrorCode.EmployeeFirstNameIsRequired.ToString("D") &&
        e.ErrorCategory == ErrorCategory.Error.ToString() &&
        e.ErrorElement == "FirstName").ShouldBeTrue();
    }

    [TestMethod]
    public void Should_Return_IsValid_False_When_LastName_Length_Exceeds_Limit()
    {
        // Arrange
        var employee = new Employee()
        {
            FirstName = "FirstName",
            LastName = new string('q', 51),
            Age = 10,
            Email = "Email@email.com",
        };

        // Act
        this.validator.Validate(employee);

        // Assert
        this.validator.IsValid.ShouldBeFalse();
        this.validator.Errors.Any(e => e.ErrorCode == ErrorCode.EmployeeLastNameExceedsMaxLength.ToString("D") &&
        e.ErrorCategory == ErrorCategory.Error.ToString() &&
        e.ErrorElement == "LastName" &&
        e.ElementValue == employee.LastName).ShouldBeTrue();
    }

    [TestMethod]
    public void Should_Return_IsValid_False_When_LastName_Is_Empty()
    {
        // Arrange
        var employee = new Employee()
        {
            FirstName = "FirstName",
            Age = 10,
            Email = "Email@email.com",
        };

        // Act
        this.validator.Validate(employee);

        // Assert
        this.validator.IsValid.ShouldBeFalse();
        this.validator.Errors.Any(e => e.ErrorCode == ErrorCode.EmployeeLastNameIsRequired.ToString("D") &&
        e.ErrorCategory == ErrorCategory.Error.ToString() &&
        e.ErrorElement == "LastName").ShouldBeTrue();
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void Should_Return_IsValid_False_When_Age_Is_Invalid(int age)
    {
        // Arrange
        var employee = new Employee()
        {
            FirstName = "FirstName",
            LastName = "LastName",
            Email = "Email@email.com",
            Age = age,
        };

        // Act
        this.validator.Validate(employee);

        // Assert
        this.validator.IsValid.ShouldBeFalse();
        this.validator.Errors.Any(e => e.ErrorCode == ErrorCode.EmployeeAgeIsRequiredAndShouldBeGreaterThanZero.ToString("D") &&
        e.ErrorCategory == ErrorCategory.Error.ToString() &&
        e.ErrorElement == "Age" &&
        e.ElementValue == employee.Age.ToString()).ShouldBeTrue();
    }

    [TestMethod]
    public void Should_Return_IsValid_False_When_Email_Is_Empty()
    {
        // Arrange
        var employee = new Employee()
        {
            FirstName = "FirstName",
            Age = 10,
            LastName = "lastName",
        };

        // Act
        this.validator.Validate(employee);

        // Assert
        this.validator.IsValid.ShouldBeFalse();
        this.validator.Errors.Any(e => e.ErrorCode == ErrorCode.EmployeeEmailIsRequired.ToString("D") &&
        e.ErrorCategory == ErrorCategory.Error.ToString() &&
        e.ErrorElement == "Email").ShouldBeTrue();
    }

    [TestMethod]
    public void Should_Return_IsValid_False_When_Email_Length_Exceeds_Limit()
    {
        // Arrange
        var employee = new Employee()
        {
            FirstName = "FirstName",
            Age = 10,
            LastName = "lastName",
            Email = new string('q', 51) + "@email.com",
        };

        // Act
        this.validator.Validate(employee);

        // Assert
        this.validator.IsValid.ShouldBeFalse();
        this.validator.Errors.Any(e => e.ErrorCode == ErrorCode.EmployeeEmailExceedsMaxLength.ToString("D") &&
        e.ErrorCategory == ErrorCategory.Error.ToString() &&
        e.ErrorElement == "Email" &&
        e.ElementValue == employee.Email).ShouldBeTrue();
    }
}
