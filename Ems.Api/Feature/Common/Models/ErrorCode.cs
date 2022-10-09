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
        /// Employee first name length is greater than max limit.
        /// </summary>
        EmployeeFirstNameExceedsMaxLength = 1001,

        /// <summary>
        /// Employee last name is required.
        /// </summary>
        EmployeeLastNameIsRequired = 1002,

        /// <summary>
        /// Employee last name length is greater than max limit.
        /// </summary>
        EmployeeLastNameExceedsMaxLength = 1003,

        /// <summary>
        /// Employee age is required and should be greater than zero.
        /// </summary>
        EmployeeAgeIsRequiredAndShouldBeGreaterThanZero = 1004,

        /// <summary>
        /// Employee email is required.
        /// </summary>
        EmployeeEmailIsRequired = 1005,

        /// <summary>
        /// Employee email length is greater than max limit.
        /// </summary>
        EmployeeEmailExceedsMaxLength = 1006,

        /// <summary>
        /// Employee email is not valid
        /// </summary>
        EmployeeEmailIsNotValid = 1007,

        /// <summary>
        /// Employee Already exists
        /// </summary>
        EmployeeAlreadyExists = 1008,

        /// <summary>
        /// Employee Search At Least One Search Parameter Is Required
        /// </summary>
        EmployeeSearchAtLeastOneSearchParameterIsRequired = 1009,
    }
}
