// <copyright file="IAddEmployeeValidator.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Validators.Interfaces;

using Ems.Api.Feature.Common.Models;
using Ems.Api.Feature.Employees.Models;

public interface IAddEmployeeValidator
{
    List<ErrorDetail> Errors { get; }

    bool IsValid { get; }

    void Validate(Employee employee);
}
