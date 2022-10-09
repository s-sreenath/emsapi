// <copyright file="EmployeeSearchCommandTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Ems.Api.Feature.Employees.Commands;
    using Ems.Api.Feature.Employees.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EmployeeSearchCommandTests
    {
        [TestMethod]
        public void Should_Create_Search_Command_From_Request()
        {
            // Arrange
            var request = new EmployeeSearchRequest()
            {
                FirstName = "FirstName",
                LastName = "LastName",
            };

            // Act
            var command = new EmployeeSearchCommand(request);

            // Assert
            command.FirstName.ShouldBe(request.FirstName);
            command.LastName.ShouldBe(request.LastName);
        }
    }
}
