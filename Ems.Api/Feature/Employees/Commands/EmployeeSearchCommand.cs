// <copyright file="EmployeeSearchCommand.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Commands
{
    using Ems.Api.Feature.Employees.Models;
    using Ems.Api.Feature.Employees.Models.Response;
    using MediatR;

    public class EmployeeSearchCommand : IRequest<EmployeeSearchResponse>
    {
        public EmployeeSearchCommand(EmployeeSearchRequest request)
        {
            this.FirstName = request.FirstName;
            this.LastName = request.LastName;
        }

        public string FirstName { get; }

        public string LastName { get; }
    }
}
