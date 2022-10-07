// <copyright file="EmployeesControllerTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees;

using Ems.Api.Feature.Employees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

public class EmployeesControllerTests
{
    private EmployeesController controller;

    [TestInitialize]
    public void TestInitialize()
    {
        this.controller = new EmployeesController();
    }

    [TestMethod]
    public void AddEmployee_Should_Return()
    {
    }
}
