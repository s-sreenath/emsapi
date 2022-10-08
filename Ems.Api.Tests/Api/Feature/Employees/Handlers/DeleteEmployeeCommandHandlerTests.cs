// <copyright file="DeleteEmployeeCommandHandlerTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Handlers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Ems.Api.Data.Repository;
    using Ems.Api.Feature.Employees.Commands;
    using Ems.Api.Feature.Employees.Handlers;
    using Ems.Api.Feature.Employees.Models.Response;
    using FakeItEasy;
    using MediatR;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeleteEmployeeCommandHandlerTests
    {
        private IEmployeeRepository repository;
        private DeleteEmployeeCommandHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.repository = A.Fake<IEmployeeRepository>();
            this.handler = new DeleteEmployeeCommandHandler(this.repository);
        }

        [TestMethod]
        public void Handler_Should_Be_Of_Type_IRequestHandler()
        {
            // Assert
            this.handler.ShouldBeAssignableTo<IRequestHandler<DeleteEmployeeCommand, DeleteEmployeeResponse>>();
        }

        [TestMethod]
        public async Task Handler_Should_Call_DeleteEmployee()
        {
            // Arrange
            var employeeId = 145;

            var command = new DeleteEmployeeCommand(employeeId);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None).ConfigureAwait(true);

            // Assert
            result.ShouldNotBeNull();

            A.CallTo(() => this.repository.DeleteAsync(employeeId)).MustHaveHappened();
        }
    }
}
