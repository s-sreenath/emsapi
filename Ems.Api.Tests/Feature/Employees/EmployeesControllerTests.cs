// <copyright file="EmployeesControllerTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Feature.Employees;

using Ems.Api.Feature.Employees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
