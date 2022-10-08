// <copyright file="AddEmployeeResponseTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Models.Reponse
{
    using System.Diagnostics.CodeAnalysis;
    using Ems.Api.Feature.Employees.Models.Response;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AddEmployeeResponseTests
    {
        [TestMethod]
        public void Should_Be_Able_To_Get_Set()
        {
            // Arrange
            var expectedEmployeeId = 15;

            // Act
            var response = new AddEmployeeResponse()
            {
                EmployeeId = expectedEmployeeId,
            };

            // Assert
            response.EmployeeId.ShouldBe(expectedEmployeeId);
            response.Details.ShouldNotBeNull();
        }
    }
}
