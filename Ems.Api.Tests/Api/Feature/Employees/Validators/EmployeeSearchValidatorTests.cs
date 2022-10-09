// <copyright file="EmployeeSearchValidatorTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Employees.Validators
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Ems.Api.Feature.Common.Models;
    using Ems.Api.Feature.Employees.Models;
    using Ems.Api.Feature.Employees.Validators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EmployeeSearchValidatorTests
    {
        private EmployeeSearchValidator validator;

        [TestInitialize]
        public void TestInitialize()
        {
            this.validator = new EmployeeSearchValidator();
        }

        [TestMethod]
        public void Should_Return_At_Least_One_Search_Parameter_Is_Required()
        {
            // Arrange
            var request = new EmployeeSearchRequest();

            // Act
            this.validator.Validate(request);

            // Assert
            this.validator.IsValid.ShouldBeFalse();
            this.validator.Errors.Any(e => e.ErrorCode == ErrorCode.EmployeeSearchAtLeastOneSearchParameterIsRequired.ToString("D")).ShouldBeTrue();
        }

        [TestMethod]
        public void Should_Return_IsValid_True()
        {
            // Arrange
            var request = new EmployeeSearchRequest()
            {
                FirstName = "FirstName",
            };

            // Act
            this.validator.Validate(request);

            // Assert
            this.validator.IsValid.ShouldBeTrue();
        }
    }
}
