// <copyright file="AddEmployeeCommandTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Commands;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ems.Api.Feature.Employees.Commands;
using Ems.Api.Feature.Employees.Models;
using Ems.Api.Feature.Employees.Models.Response;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

[TestClass]
[ExcludeFromCodeCoverage]
public class AddEmployeeCommandTests
{
    [TestMethod]
    public void Should_Map_From_Request()
    {
        // Arrange
        var employee = new Employee
        {
            FirstName = "firstName",
            LastName = "lastName",
            Email = "email@email.com",
            Age = 12,
        };

        // Act
        var command = new AddEmployeeCommand(employee);

        // Assert
        command.ShouldBeAssignableTo<IRequest<AddEmployeeResponse>>();
        command.FirstName.ShouldBe(employee.FirstName);
        command.LastName.ShouldBe(employee.LastName);
        command.Email.ShouldBe(employee.Email);
        command.Age.ShouldBe(employee.Age);
    }
}
