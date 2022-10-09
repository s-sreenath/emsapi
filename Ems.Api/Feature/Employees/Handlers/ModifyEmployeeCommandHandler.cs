// <copyright file="ModifyEmployeeCommandHandler.cs" company="EmsApi Company">
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

    public class ModifyEmployeeCommandHandler : IRequestHandler<ModifyEmployeeCommand, ModifyEmployeeResponse>
    {
        private readonly ILogger<ModifyEmployeeCommandHandler> logger;
        private readonly IEmployeeRepository repository;

        public ModifyEmployeeCommandHandler(
            ILogger<ModifyEmployeeCommandHandler> logger,
            IEmployeeRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public async Task<ModifyEmployeeResponse> Handle(ModifyEmployeeCommand request, CancellationToken cancellationToken)
        {
            var response = new ModifyEmployeeResponse();

            var allEmployees = this.repository.GetAll();

            if (allEmployees.Any(e => e.Email == request.Email &&
            e.FirstName == request.FirstName &&
            e.LastName == request.LastName &&
            e.EmployeeId != request.EmployeeId))
            {
                response.Details.Add(new ErrorDetail()
                {
                    ErrorCode = ErrorCode.EmployeeAlreadyExists.ToString("D"),
                    ErrorCategory = ErrorCategory.Error.ToString(),
                });

                return response;
            }

            await this.repository.UpdateAsync(new EmployeeDto()
            {
                Age = request.Age,
                Email = request.Email,
                EmployeeId = request.EmployeeId,
                FirstName = request.FirstName,
                LastName = request.LastName,
            }).ConfigureAwait(true);

            return response;
        }
    }
}
