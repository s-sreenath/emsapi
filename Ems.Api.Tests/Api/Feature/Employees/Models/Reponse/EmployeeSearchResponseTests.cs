// <copyright file="EmployeeSearchResponseTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Models.Reponse
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.Json;
    using Ems.Api.Feature.Employees.Models;
    using Ems.Api.Feature.Employees.Models.Response;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EmployeeSearchResponseTests
    {
        [TestMethod]
        public void Should_Be_Able_To_Add_Employees()
        {
            // Arrange
            var employee = new Employee()
            {
                Age = 15,
                Email = "Email@Email.com",
                EmployeeId = 12,
                FirstName = "FirstName",
                LastName = "LastName",
            };

            // Act
            var searchReponse = new EmployeeSearchResponse();
            searchReponse.Employees.Add(employee);

            // Assert
            searchReponse.Employees[0].Age.ShouldBe(employee.Age);
            searchReponse.Employees[0].Email.ShouldBe(employee.Email);
            searchReponse.Employees[0].EmployeeId.ShouldBe(employee.EmployeeId);
            searchReponse.Employees[0].FirstName.ShouldBe(employee.FirstName);
            searchReponse.Employees[0].LastName.ShouldBe(employee.LastName);
        }

        [TestMethod]
        public void Should_Be_Serializable()
        {
            // Arrange
            var employee = new Employee()
            {
                Age = 15,
                Email = "Email@Email.com",
                EmployeeId = 12,
                FirstName = "FirstName",
                LastName = "LastName",
            };

            var searchReponse = new EmployeeSearchResponse();

            searchReponse.Employees.Add(employee);
            var expectedJson = "{\"Employees\":[{\"FirstName\":\"FirstName\",\"LastName\":\"LastName\",\"Email\":\"Email@Email.com\",\"Age\":15,\"EmployeeId\":12}]}";

            // Act
            var json = JsonSerializer.Serialize(searchReponse);

            // Assert
            json.ShouldBe(expectedJson);
        }
    }
}
