// <copyright file="EmployeeSearchRequest.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Models
{
    public class EmployeeSearchRequest
    {
        public EmployeeSearchRequest()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
