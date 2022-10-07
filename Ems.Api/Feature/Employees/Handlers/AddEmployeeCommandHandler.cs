// <copyright file="AddEmployeeCommandHandler.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ems.Api.Data.DTO;
    using Ems.Api.Data.Repository;
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
            await Task.Delay(10);

            var response = new AddEmployeeResponse();

            var employeeId = this.repository.AddEmployee(new EmployeeDto()
            {
                Age = request.Age,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            });

            return response;
        }
    }
}
