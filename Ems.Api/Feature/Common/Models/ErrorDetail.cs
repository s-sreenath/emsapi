// <copyright file="ErrorDetail.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Common.Models;

public class ErrorDetail
{
    public ErrorDetail()
    {
        this.ErrorCategory = string.Empty;
        this.ErrorCode = string.Empty;
        this.ErrorDescription = string.Empty;
        this.ErrorElement = string.Empty;
        this.ElementValue = string.Empty;
    }

    public string ErrorCode { get; set; }

    public string ErrorCategory { get; set; }

    public string ErrorDescription { get; set; }

    public string ErrorElement { get; set; }

    public string ElementValue { get; set; }
}
