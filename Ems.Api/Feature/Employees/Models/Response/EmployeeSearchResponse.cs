// <copyright file="EmployeeSearchResponse.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Models.Response
{
    public class EmployeeSearchResponse
    {
        public EmployeeSearchResponse()
        {
            this.Employees = new List<Employee>();
        }

        public List<Employee> Employees { get; }
    }
}
