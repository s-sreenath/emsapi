// <copyright file="ErrorDetailTests.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Tests.Api.Feature.Common.Models;

using Ems.Api.Feature.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

[TestClass]
[ExcludeFromCodeCoverage]
public class ErrorDetailTests
{
    [TestMethod]
    public void Should_Initialize_To_Empty()
    {
        // Act
        var errorDetail = new ErrorDetail();

        // Assert
        errorDetail.ErrorCode.ShouldBeEmpty();
        errorDetail.ErrorCategory.ShouldBeEmpty();
        errorDetail.ErrorElement.ShouldBeEmpty();
        errorDetail.ErrorDescription.ShouldBeEmpty();
        errorDetail.ElementValue.ShouldBeEmpty();
    }

    [TestMethod]
    public void Should_Be_Able_To_Set_And_Get()
    {
        // Arrange
        var errorCode = "code";
        var errorCategory = "erroCategory";
        var errorDescription = "description";
        var errorElement = "errorElement";
        var elementValue = "elementValue";

        // Act
        var errorDetails = new ErrorDetail()
        {
            ErrorCode = errorCode,
            ErrorCategory = errorCategory,
            ErrorDescription = errorDescription,
            ErrorElement = errorElement,
            ElementValue = elementValue,
        };

        // Assert
        errorDetails.ErrorCode.ShouldBe(errorCode);
        errorDetails.ErrorCategory.ShouldBe(errorCategory);
        errorDetails.ErrorElement.ShouldBe(errorElement);
        errorDetails.ErrorDescription.ShouldBe(errorDescription);
        errorDetails.ElementValue.ShouldBe(elementValue);
    }

    [TestMethod]
    public void Should_Serialize()
    {
        // Arrange
        var errorCode = "code";
        var errorCategory = "erroCategory";
        var errorDescription = "description";
        var errorElement = "errorElement";
        var elementValue = "elementValue";

        // Act
        var errorDetails = new ErrorDetail()
        {
            ErrorCode = errorCode,
            ErrorCategory = errorCategory,
            ErrorDescription = errorDescription,
            ErrorElement = errorElement,
            ElementValue = elementValue,
        };

        var expectedJson = "{\"ErrorCode\":\"code\",\"ErrorCategory\":\"erroCategory\",\"ErrorDescription\":\"description\",\"ErrorElement\":\"errorElement\",\"ElementValue\":\"elementValue\"}";

        // Act
        var result = JsonSerializer.Serialize(errorDetails);

        // Assert
        result.ShouldBe(expectedJson);
    }

    [TestMethod]
    public void Should_Deserialize()
    {
        // Arrange
        var errorCode = "code";
        var errorCategory = "erroCategory";
        var errorDescription = "description";
        var errorElement = "errorElement";
        var elementValue = "elementValue";

        var json = "{\"ErrorCode\":\"code\",\"ErrorCategory\":\"erroCategory\",\"ErrorDescription\":\"description\",\"ErrorElement\":\"errorElement\",\"ElementValue\":\"elementValue\"}";

        // Act
        var result = JsonSerializer.Deserialize<ErrorDetail>(json);

        // Assert
        result.ShouldNotBeNull();
        result.ErrorCode.ShouldBe(errorCode);
        result.ErrorCategory.ShouldBe(errorCategory);
        result.ErrorElement.ShouldBe(errorElement);
        result.ErrorDescription.ShouldBe(errorDescription);
        result.ElementValue.ShouldBe(elementValue);
    }
}
