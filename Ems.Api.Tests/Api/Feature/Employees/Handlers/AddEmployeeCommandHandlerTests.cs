// <copyright file="AddEmployeeCommandHandlerTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Ems.Api.Feature.Employees.Commands;
    using Ems.Api.Feature.Employees.Handlers;
    using Ems.Api.Feature.Employees.Models.Response;
    using MediatR;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    public class AddEmployeeCommandHandlerTests
    {
        private AddEmployeeCommandHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.handler = new AddEmployeeCommandHandler();
        }

        [TestMethod]
        public void Handler_Should_Be_Of_Type_IRequestHandler()
        {
            // Assert
            this.handler.ShouldBeAssignableTo<IRequestHandler<AddEmployeeCommand, AddEmployeeResponse>>();
        }
    }
}
