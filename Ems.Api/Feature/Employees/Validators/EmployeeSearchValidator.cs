// <copyright file="EmployeeSearchValidator.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Validators
{
    using Ems.Api.Feature.Common.Models;
    using Ems.Api.Feature.Employees.Models;
    using Ems.Api.Feature.Employees.Validators.Interfaces;

    public class EmployeeSearchValidator : IEmployeeSearchValidator
    {
        public EmployeeSearchValidator()
        {
            this.Errors = new List<ErrorDetail>();
        }

        public List<ErrorDetail> Errors { get; }

        public bool IsValid => !this.Errors.Any();

        public void Validate(EmployeeSearchRequest request)
        {
            if (string.IsNullOrEmpty(request.FirstName) && string.IsNullOrEmpty(request.LastName))
            {
                this.Errors.Add(new ErrorDetail()
                {
                    ErrorCode = ErrorCode.EmployeeSearchAtLeastOneSearchParameterIsRequired.ToString("D"),
                });
            }
        }
    }
}
