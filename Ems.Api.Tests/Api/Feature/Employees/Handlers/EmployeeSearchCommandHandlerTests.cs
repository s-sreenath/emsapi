// <copyright file="EmployeeSearchCommandHandlerTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Handlers
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Ems.Api.Data.DTO;
    using Ems.Api.Data.Repository;
    using Ems.Api.Feature.Common.Models;
    using Ems.Api.Feature.Employees.Commands;
    using Ems.Api.Feature.Employees.Handlers;
    using Ems.Api.Feature.Employees.Models;
    using Ems.Api.Feature.Employees.Models.Response;
    using FakeItEasy;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EmployeeSearchCommandHandlerTests
    {
        private NullLogger<EmployeeSearchCommandHandler> logger;
        private IEmployeeRepository repository;
        private EmployeeSearchCommandHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.logger = new NullLogger<EmployeeSearchCommandHandler>();
            this.repository = A.Fake<IEmployeeRepository>();

            this.handler = new EmployeeSearchCommandHandler(
                this.logger,
                this.repository);
        }

        [TestMethod]
        public void Handler_Should_Be_Of_Type_IRequestHandler()
        {
            // Assert
            this.handler.ShouldBeAssignableTo<IRequestHandler<EmployeeSearchCommand, EmployeeSearchResponse>>();
        }

        [TestMethod]
        public async Task Handler_Should_Search_Return_SearchResponse()
        {
            // Arrange
            var request = new EmployeeSearchRequest()
            {
                FirstName = "FirstName",
                LastName = "LastName",
            };

            var command = new EmployeeSearchCommand(request);

            // Act
            var response = await this.handler.Handle(command, CancellationToken.None).ConfigureAwait(true);

            // Assert
            response.ShouldNotBeNull();
            A.CallTo(() => this.repository.GetAll()).MustHaveHappened();
        }
    }
}
