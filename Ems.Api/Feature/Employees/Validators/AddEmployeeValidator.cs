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
        this.ValidateFirstName(employee);
        this.ValidateLastName(employee);
        this.ValidatrAge(employee);

        if (string.IsNullOrEmpty(employee.Email))
        {
            this.Errors.Add(new ErrorDetail()
            {
                ErrorCode = ErrorCode.EmployeeEmailIsRequired.ToString("D"),
                ErrorCategory = ErrorCategory.Error.ToString(),
                ErrorElement = nameof(employee.Email),
            });
        }

        if (employee.Email.Length > 50)
        {
            this.Errors.Add(new ErrorDetail()
            {
                ErrorCode = ErrorCode.EmployeeEmailExceedsMaxLength.ToString("D"),
                ErrorCategory = ErrorCategory.Error.ToString(),
                ElementValue = employee.Email,
                ErrorElement = nameof(employee.Email),
            });
        }
    }

    private void ValidateFirstName(Employee employee)
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
    }

    private void ValidateLastName(Employee employee)
    {
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
    }

    private void ValidatrAge(Employee employee)
    {
        if (employee.Age <= 0)
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
