// <copyright file="ModifyEmployeeCommandTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Commands;

using System.Diagnostics.CodeAnalysis;
using Ems.Api.Feature.Employees.Commands;
using Ems.Api.Feature.Employees.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

[TestClass]
[ExcludeFromCodeCoverage]
public class ModifyEmployeeCommandTests
{
    [TestMethod]
    public void Should_Map_From_Request()
    {
        // Arrange
        var employeeId = 123;
        var firstName = "firstName";
        var lastName = "lastName";
        var email = "email@email.com";
        var age = 12;

        var employee = new Employee
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Age = age,
            EmployeeId = employeeId,
        };

        // Act
        var command = new ModifyEmployeeCommand(employeeId, employee);

        // Assert
        command.EmployeeId.ShouldBe(employeeId);
        command.FirstName.ShouldBe(firstName);
        command.LastName.ShouldBe(lastName);
        command.Email.ShouldBe(email);
        command.Age.ShouldBe(age);
    }
}
