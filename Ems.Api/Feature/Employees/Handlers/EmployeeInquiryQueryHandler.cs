﻿// <copyright file="EmployeeInquiryQueryHandler.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ems.Api.Data.Repository;
    using Ems.Api.Feature.Employees.Commands;
    using Ems.Api.Feature.Employees.Models.Response;
    using MediatR;

    public class EmployeeInquiryQueryHandler : IRequestHandler<EmployeeInquiryQuery, EmployeeInquiryResponse>
    {
        private readonly IEmployeeRepository repository;

        public EmployeeInquiryQueryHandler(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<EmployeeInquiryResponse> Handle(EmployeeInquiryQuery request, CancellationToken cancellationToken)
        {
            var results = await this.repository.GetByIdAsync(request.EmployeeId).ConfigureAwait(true);

            if (results != null)
            {
                return new EmployeeInquiryResponse()
                {
                    Employee = new Models.Employee()
                    {
                        Age = results.Age,
                        Email = results.Email,
                        EmployeeId = results.EmployeeId,
                        FirstName = results.FirstName,
                        LastName = results.LastName,
                    },
                };
            }

            return new EmployeeInquiryResponse();
        }
    }
}
