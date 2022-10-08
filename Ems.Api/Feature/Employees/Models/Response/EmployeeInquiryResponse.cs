// <copyright file="EmployeeInquiryResponse.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Models.Response
{
    public class EmployeeInquiryResponse
    {
        public EmployeeInquiryResponse()
        {
            this.Employee = new Employee();
        }

        public Employee Employee { get; set; }
    }
}
