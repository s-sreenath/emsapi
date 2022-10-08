// <copyright file="EmployeeInquiryQueryHandlerTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Handlers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Ems.Api.Data.DTO;
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
    public class EmployeeInquiryQueryHandlerTests
    {
        private IEmployeeRepository repository;
        private EmployeeInquiryQueryHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.repository = A.Fake<IEmployeeRepository>();
            this.handler = new EmployeeInquiryQueryHandler(
                this.repository);
        }

        [TestMethod]
        public void Handler_Should_Be_Of_Type_IRequestHandler()
        {
            // Assert
            this.handler.ShouldBeAssignableTo<IRequestHandler<EmployeeInquiryQuery, EmployeeInquiryResponse>>();
        }

        [TestMethod]
        public async Task Handler_Should_Return_EmployeeInquiry_Response()
        {
            // Arrange
            var employeeId = 152;
            var request = new EmployeeInquiryQuery(employeeId);

            A.CallTo(() => this.repository.GetByIdAsync(employeeId)).Returns(new EmployeeDto()
            {
                Age = 12,
                LastName = "LastName",
                Email = "Email@Email.com",
                FirstName = "FirstName",
                EmployeeId = employeeId,
            });

            // Act
            var result = await this.handler.Handle(request, CancellationToken.None).ConfigureAwait(true);

            // Assert
            result.ShouldNotBeNull();
            result.Employee.EmployeeId.ShouldBe(employeeId);
            A.CallTo(() => this.repository.GetByIdAsync(employeeId)).MustHaveHappened();
        }
    }
}
