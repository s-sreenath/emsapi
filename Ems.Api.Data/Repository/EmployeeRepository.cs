// <copyright file="EmployeeRepository.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Data.Repository
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Ems.Api.Data.DTO;

    [ExcludeFromCodeCoverage]
    public class EmployeeRepository : IEmployeeRepository
    {
        private DatabaseContext context;

        public EmployeeRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<EmployeeDto> AddEmployeeAsync(EmployeeDto dto)
        {
            this.context.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(true);

            return dto;
        }

        public async Task DeleteAsync(int employeeId)
        {
            this.context.Remove(new EmployeeDto()
            {
                EmployeeId = employeeId,
            });

            await this.context.SaveChangesAsync().ConfigureAwait(true);
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            if (this.context.Employees != null)
            {
                return this.context.Employees.ToList();
            }

            return Enumerable.Empty<EmployeeDto>();
        }

        public async Task<EmployeeDto?> GetByIdAsync(int employeeId)
        {
            return await this.context.FindAsync(typeof(EmployeeDto), employeeId).ConfigureAwait(true) as EmployeeDto;
        }

        public async Task UpdateAsync(EmployeeDto employeeDto)
        {
            this.context.Update(employeeDto);
            await this.context.SaveChangesAsync().ConfigureAwait(true);
        }
    }
}
