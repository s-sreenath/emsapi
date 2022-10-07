// <copyright file="AddEmployeeValidator.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Validators;

using Ems.Api.Feature.Common.Models;
using Ems.Api.Feature.Employees.Models;

public class AddEmployeeValidator
{
    public AddEmployeeValidator()
    {
        this.Errors = new List<ErrorDetail>();
    }

    public bool IsValid => !this.Errors.Any();

    public List<ErrorDetail> Errors { get; }

    public void Validate(Employee employee)
    {
        if (string.IsNullOrEmpty(employee.FirstName))
        {
            this.Errors.Add(new ErrorDetail()
            {
                ErrorCode = ErrorCode.EmployeeFirstNameIsRequired.ToString("D"),
                ErrorCategory = ErrorCategory.Error.ToString(),
                ErrorElement = nameof(employee.FirstName),
            });
        }

        if (employee.FirstName.Length > 50)
        {
            this.Errors.Add(new ErrorDetail()
            {
                ErrorCode = ErrorCode.EmployeeFirstNameExceedsMaxLength.ToString("D"),
                ErrorCategory = ErrorCategory.Error.ToString(),
                ElementValue = employee.FirstName,
                ErrorElement = nameof(employee.FirstName),
            });
        }

        if (string.IsNullOrEmpty(employee.LastName))
        {
            this.Errors.Add(new ErrorDetail()
            {
                ErrorCode = ErrorCode.EmployeeLastNameIsRequired.ToString("D"),
                ErrorCategory = ErrorCategory.Error.ToString(),
                ErrorElement = nameof(employee.LastName),
            });
        }

        if (employee.LastName.Length > 50)
        {
            this.Errors.Add(new ErrorDetail()
            {
                ErrorCode = ErrorCode.EmployeeLastNameExceedsMaxLength.ToString("D"),
                ErrorCategory = ErrorCategory.Error.ToString(),
                ElementValue = employee.LastName,
                ErrorElement = nameof(employee.LastName),
            });
        }

        if (employee.Age == 0)
        {
            this.Errors.Add(new ErrorDetail()
            {
                ErrorCode = ErrorCode.EmployeeAgeIsRequiredAndShouldBeGreaterThanZero.ToString("D"),
                ErrorCategory = ErrorCategory.Error.ToString(),
                ElementValue = employee.Age.ToString(),
                ErrorElement = nameof(employee.Age),
            });
        }
    }
}
