// <copyright file="ModifyEmployeeResponse.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Models.Response
{
    using Ems.Api.Feature.Common.Models;

    public class ModifyEmployeeResponse
    {
        public ModifyEmployeeResponse()
        {
            this.Details = new List<ErrorDetail>();
        }

        public List<ErrorDetail> Details { get; }
    }
}
