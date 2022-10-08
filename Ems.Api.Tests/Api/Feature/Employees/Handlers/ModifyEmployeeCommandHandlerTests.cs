// <copyright file="ModifyEmployeeCommandHandlerTests.cs" company="EmsApi Company">
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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ModifyEmployeeCommandHandlerTests
    {
        private IEmployeeRepository repository;
        private ModifyEmployeeCommandHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.repository = A.Fake<IEmployeeRepository>();
            this.handler = new ModifyEmployeeCommandHandler(this.repository);
        }

        [TestMethod]
        public void Handler_Should_Be_Of_Type_IRequestHandler()
        {
            // Assert
            this.handler.ShouldBeAssignableTo<IRequestHandler<ModifyEmployeeCommand, ModifyEmployeeResponse>>();
        }

        [TestMethod]
        public async Task Handler_Should_Call_UpdateEmployee()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email@email.com",
                Age = 12,
                EmployeeId = 152,
            };

            var command = new ModifyEmployeeCommand(employee.EmployeeId, employee);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None).ConfigureAwait(true);

            // Assert
            result.ShouldNotBeNull();
            A.CallTo(() => this.repository.UpdateAsync(A<EmployeeDto>.That.Matches(e => e.Email == employee.Email &&
            e.FirstName == employee.FirstName &&
            e.LastName == employee.LastName &&
            e.Age == employee.Age &&
            e.EmployeeId == employee.EmployeeId))).MustHaveHappened();
        }

        [TestMethod]
        public async Task Handler_Should_Return_Error_When_Another_Employee_Exists_With_Same_Details()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email@email.com",
                Age = 12,
                EmployeeId = 152,
            };

            var command = new ModifyEmployeeCommand(employee.EmployeeId, employee);

            var employees = new List<EmployeeDto>();
            employees.Add(new EmployeeDto()
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email@email.com",
                Age = 12,
                EmployeeId = 153,
            });

            A.CallTo(() => this.repository.GetAll()).Returns(employees);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None).ConfigureAwait(true);

            // Assert
            result.ShouldNotBeNull();
            result.Details.Any(e => e.ErrorCode == ErrorCode.EmployeeAlreadyExists.ToString("D")).ShouldBeTrue();
        }
    }
}
