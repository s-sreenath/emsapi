// <copyright file="EmployeeTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Feature.Employees.Models;

using Ems.Api.Feature.Employees.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

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
}
