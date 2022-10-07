// <copyright file="EmployeeTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Models;

using Ems.Api.Feature.Employees.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Text.Json;

[TestClass]
public class EmployeeTests
{
    [TestMethod]
    public void Should_Be_Able_To_Set_And_Get()
    {
        // Arrange
        var firstName = "firstName";
        var lastName = "lastName";
        var email = "email@email.com";
        var age = 12;

        // Act
        var employee = new Employee();
        employee.FirstName = firstName;
        employee.LastName = lastName;
        employee.Email = email;
        employee.Age = age;

        // Assert
        employee.FirstName.ShouldBe(firstName);
        employee.LastName.ShouldBe(lastName);
        employee.Email.ShouldBe(email);
        employee.Age.ShouldBe(age);
    }

    [TestMethod]
    public void Should_Serialize()
    {
        // Arrange
        var firstName = "firstName";
        var lastName = "lastName";
        var email = "email@email.com";
        var age = 12;

        var employee = new Employee();
        employee.FirstName = firstName;
        employee.LastName = lastName;
        employee.Email = email;
        employee.Age = age;

        var expectedJson = "{\"FirstName\":\"firstName\",\"LastName\":\"lastName\",\"Email\":\"email@email.com\",\"Age\":12}";

        // Act
        var result = JsonSerializer.Serialize(employee);

        // Assert
        result.ShouldBe(expectedJson);
    }

    [TestMethod]
    public void Should_Deserialize()
    {
        // Arrange
        var firstName = "firstName";
        var lastName = "lastName";
        var email = "email@email.com";
        var age = 12;

        var json = "{\"FirstName\":\"firstName\",\"LastName\":\"lastName\",\"Email\":\"email@email.com\",\"Age\":12}";

        // Act
        var result = JsonSerializer.Deserialize<Employee>(json);

        // Assert
        result.ShouldNotBeNull();
        result.FirstName.ShouldBe(firstName);
        result.LastName.ShouldBe(lastName);
        result.Email.ShouldBe(email);
        result.Age.ShouldBe(age);
    }
}
