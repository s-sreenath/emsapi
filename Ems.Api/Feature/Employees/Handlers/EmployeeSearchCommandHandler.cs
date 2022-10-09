// <copyright file="EmployeeSearchCommandHandler.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ems.Api.Data.DTO;
    using Ems.Api.Data.Repository;
    using Ems.Api.Feature.Common.Extensions;
    using Ems.Api.Feature.Employees.Commands;
    using Ems.Api.Feature.Employees.Models;
    using Ems.Api.Feature.Employees.Models.Response;
    using MediatR;
    using Microsoft.Extensions.Logging.Abstractions;

    public class EmployeeSearchCommandHandler : IRequestHandler<EmployeeSearchCommand, EmployeeSearchResponse>
    {
        private readonly ILogger<EmployeeSearchCommandHandler> logger;
        private readonly IEmployeeRepository repository;

        public EmployeeSearchCommandHandler(
            ILogger<EmployeeSearchCommandHandler> logger,
            IEmployeeRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public async Task<EmployeeSearchResponse> Handle(EmployeeSearchCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"{nameof(this.Handle)} is called with {nameof(request)} - {request.ToJson()}");

            var response = new EmployeeSearchResponse();

            var employees = this.repository.GetAll();

            var filteredEmployees = new List<EmployeeDto>();

            if (!string.IsNullOrEmpty(request.FirstName) && string.IsNullOrEmpty(request.LastName))
            {
                filteredEmployees.AddRange(employees.Where(e => e.FirstName.Equals(request.FirstName, StringComparison.InvariantCultureIgnoreCase)).ToList());
            }

            if (string.IsNullOrEmpty(request.FirstName) && !string.IsNullOrEmpty(request.LastName))
            {
                filteredEmployees.AddRange(employees.Where(e => e.LastName.Equals(request.LastName, StringComparison.InvariantCultureIgnoreCase)).ToList());
            }

            if (!string.IsNullOrEmpty(request.FirstName) && !string.IsNullOrEmpty(request.LastName))
            {
                filteredEmployees.AddRange(employees.Where(e => e.FirstName.Equals(request.FirstName, StringComparison.InvariantCultureIgnoreCase)
                    && e.LastName.Equals(request.LastName, StringComparison.InvariantCultureIgnoreCase)).ToList());
            }

            await Task.Run(() =>
            {
                foreach (var item in filteredEmployees)
                {
                    response.Employees.Add(new Employee()
                    {
                        EmployeeId = item.EmployeeId,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        Age = item.Age,
                    });
                }
            });

            return response;
        }
    }
}
