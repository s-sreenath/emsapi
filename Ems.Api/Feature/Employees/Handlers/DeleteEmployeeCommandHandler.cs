// <copyright file="DeleteEmployeeCommandHandler.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ems.Api.Data.Repository;
    using Ems.Api.Feature.Common.Extensions;
    using Ems.Api.Feature.Employees.Commands;
    using Ems.Api.Feature.Employees.Models.Response;
    using MediatR;

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, DeleteEmployeeResponse>
    {
        private readonly ILogger<DeleteEmployeeCommandHandler> logger;
        private readonly IEmployeeRepository repository;

        public DeleteEmployeeCommandHandler(
            ILogger<DeleteEmployeeCommandHandler> logger,
            IEmployeeRepository repository)
        {
            this.logger = logger;
            this.repository = repository;
        }

        public async Task<DeleteEmployeeResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"{nameof(this.Handle)} is called with {nameof(request)} - {request.ToJson<DeleteEmployeeCommand>()}");

            await this.repository.DeleteAsync(request.EmployeeId).ConfigureAwait(true);

            return new DeleteEmployeeResponse();
        }
    }
}
