// <copyright file="AddEmployeeValidator.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Validators;

using Ems.Api.Feature.Common.Models;

public class AddEmployeeValidator
{
    public AddEmployeeValidator()
    {
        this.Errors = new List<ErrorDetail>();
    }

    public bool IsValid => !this.Errors.Any();

    public List<ErrorDetail> Errors { get; }

    public void Validate()
    {
    }
}
