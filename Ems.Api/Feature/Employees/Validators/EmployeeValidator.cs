// <copyright file="EmployeeValidator.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Validators;

using Ems.Api.Feature.Common.Models;
using Ems.Api.Feature.Employees.Models;
using Ems.Api.Feature.Employees.Validators.Interfaces;
using System.Text.RegularExpressions;

public class EmployeeValidator : IEmployeeValidator
{
    private const int FirstNameMaxLength = 50;
    private const int LastNameMaxLength = 50;
    private const int EmailMaxLength = 50;

    public EmployeeValidator()
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
        this.ValidateEmail(employee);
    }

    private void ValidateEmail(Employee employee)
    {
        if (string.IsNullOrEmpty(employee.Email))
        {
            this.Errors.Add(new ErrorDetail()
            {
                ErrorCode = ErrorCode.EmployeeEmailIsRequired.ToString("D"),
                ErrorCategory = ErrorCategory.Error.ToString(),
                ErrorElement = nameof(employee.Email),
            });
        }

        if (employee.Email.Length > EmailMaxLength)
        {
            this.Errors.Add(new ErrorDetail()
            {
                ErrorCode = ErrorCode.EmployeeEmailExceedsMaxLength.ToString("D"),
                ErrorCategory = ErrorCategory.Error.ToString(),
                ElementValue = employee.Email,
                ErrorElement = nameof(employee.Email),
            });
        }

        if (!Regex.IsMatch(employee.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
        {
            this.Errors.Add(new ErrorDetail()
            {
                ErrorCode = ErrorCode.EmployeeEmailIsNotValid.ToString("D"),
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

        if (employee.FirstName.Length > FirstNameMaxLength)
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

        if (employee.LastName.Length > LastNameMaxLength)
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
