// <copyright file="EmployeeSearchRequestTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Models
{
    using System.Diagnostics.CodeAnalysis;
    using Ems.Api.Feature.Employees.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EmployeeSearchRequestTests
    {
        [TestMethod]
        public void Should_Be_Able_To_Set()
        {
            // Arrange
            var firstName = "firstName";
            var lastName = "lastName";

            // Act
            var request = new EmployeeSearchRequest()
            {
                FirstName = firstName,
                LastName = lastName,
            };

            // Assert
            request.FirstName.ShouldBe(firstName);
            request.LastName.ShouldBe(lastName);
        }
    }
}
