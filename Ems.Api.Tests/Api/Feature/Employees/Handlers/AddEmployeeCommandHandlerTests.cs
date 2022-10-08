// <copyright file="AddEmployeeCommandHandlerTests.cs" company="EmsApi Company">
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
    public class AddEmployeeCommandHandlerTests
    {
        private IEmployeeRepository repository;
        private AddEmployeeCommandHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.repository = A.Fake<IEmployeeRepository>();

            this.handler = new AddEmployeeCommandHandler(
                this.repository);
        }

        [TestMethod]
        public void Handler_Should_Be_Of_Type_IRequestHandler()
        {
            // Assert
            this.handler.ShouldBeAssignableTo<IRequestHandler<AddEmployeeCommand, AddEmployeeResponse>>();
        }

        [TestMethod]
        public async Task Handler_Should_Call_AddEmployee()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email@email.com",
                Age = 12,
            };

            var command = new AddEmployeeCommand(employee);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None).ConfigureAwait(true);

            // Assert
            result.ShouldNotBeNull();
            A.CallTo(() => this.repository.AddEmployeeAsync(A<EmployeeDto>.That.Matches(e => e.Email == employee.Email &&
            e.FirstName == employee.FirstName &&
            e.LastName == employee.LastName &&
            e.Age == employee.Age))).MustHaveHappened();
        }

        [TestMethod]
        public async Task Handler_Should_Return_AddEmployeeReponse()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email@email.com",
                Age = 12,
            };

            var command = new AddEmployeeCommand(employee);
            A.CallTo(() => this.repository.AddEmployeeAsync(A<EmployeeDto>._)).Returns(new EmployeeDto
            {
                EmployeeId = 25,
            });

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None).ConfigureAwait(true);

            // Assert
            result.ShouldNotBeNull();
            result.EmployeeId.ShouldBe(25);
        }

        [TestMethod]
        public async Task Handler_Should_Return_Error_When_Employee_Already_Exists()
        {
            // Arrange
            var employee = new Employee
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email@email.com",
                Age = 12,
            };

            var command = new AddEmployeeCommand(employee);

            var employees = new List<EmployeeDto>();
            employees.Add(new EmployeeDto()
            {
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email@email.com",
                Age = 12,
            });

            A.CallTo(() => this.repository.GetAll()).Returns(employees);

            // Act
            var result = await this.handler.Handle(command, CancellationToken.None).ConfigureAwait(true);

            // Assert
            result.ShouldNotBeNull();
            result.EmployeeId.ShouldBe(0);
            result.Details.Any(e => e.ErrorCode == ErrorCode.EmployeeAlreadyExists.ToString("D")).ShouldBeTrue();

            A.CallTo(() => this.repository.AddEmployeeAsync(A<EmployeeDto>._)).MustNotHaveHappened();
        }
    }
}
