﻿// <copyright file="EmployeeInquiryQueryTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Commands;
using System.Diagnostics.CodeAnalysis;
using Ems.Api.Feature.Employees.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

[TestClass]
[ExcludeFromCodeCoverage]
public class EmployeeInquiryQueryTests
{
    [TestMethod]
    public void Should_Map_From_Request()
    {
        // Arrange
        var employeeId = 123;

        // Act
        var command = new EmployeeInquiryQuery(employeeId);

        // Assert
        command.EmployeeId.ShouldBe(employeeId);
    }
}
