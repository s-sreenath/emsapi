﻿// <copyright file="AddEmployeeResponse.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Models.Response;

using Ems.Api.Feature.Common.Models;

public class AddEmployeeResponse
{
    public AddEmployeeResponse()
    {
        this.Details = new List<ErrorDetail>();
    }

    public int EmployeeId { get; set; }

    public List<ErrorDetail> Details { get; }
}
