// <copyright file="AddEmployeeCommandHandler.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ems.Api.Data.DTO;
    using Ems.Api.Data.Repository;
    using Ems.Api.Feature.Common.Models;
    using Ems.Api.Feature.Employees.Commands;
    using Ems.Api.Feature.Employees.Models.Response;
    using MediatR;

    public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, AddEmployeeResponse>
    {
        private IEmployeeRepository repository;

        public AddEmployeeCommandHandler(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<AddEmployeeResponse> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = new AddEmployeeResponse();

            var employees = this.repository.GetAll();

            if (employees.Any(e => e.Email == request.Email && e.FirstName == request.FirstName && e.LastName == request.LastName))
            {
                response.Details.Add(new ErrorDetail()
                {
                    ErrorCode = ErrorCode.EmployeeAlreadyExists.ToString("D"),
                    ErrorCategory = ErrorCategory.Error.ToString(),
                });

                return response;
            }

            var employee = await this.repository.AddEmployeeAsync(new EmployeeDto()
            {
                Age = request.Age,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            }).ConfigureAwait(true);

            response.EmployeeId = employee.EmployeeId;

            return response;
        }
    }
}
