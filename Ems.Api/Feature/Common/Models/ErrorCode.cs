// <copyright file="ErrorCode.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Common.Models
{
    public enum ErrorCode
    {
        /// <summary>
        /// Employee first name is required.
        /// </summary>
        EmployeeFirstNameIsRequired = 1000,

        /// <summary>
        /// Employee first name length is greated than max limit.
        /// </summary>
        EmployeeFirstNameExceedsMaxLength = 1001,

        /// <summary>
        /// Employee last name is required.
        /// </summary>
        EmployeeLastNameIsRequired = 1002,

        /// <summary>
        /// Employee last name length is greated than max limit.
        /// </summary>
        EmployeeLastNameExceedsMaxLength = 1003,

        /// <summary>
        /// Employee age is required and should be greated than zero.
        /// </summary>
        EmployeeAgeIsRequiredAndShouldBeGreaterThanZero = 1004,
    }
}
