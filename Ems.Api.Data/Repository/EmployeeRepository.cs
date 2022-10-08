﻿// <copyright file="EmployeeRepository.cs" company="EmsApi Company">
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
        public async Task<EmployeeDto> AddEmployeeAsync(EmployeeDto dto)
        {
            using (var context = new DatabaseContext())
            {
                context.Add(dto);
                await context.SaveChangesAsync().ConfigureAwait(true);
            }

            return dto;
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            using (var context = new DatabaseContext())
            {
                if (context.Employees != null)
                {
                    return context.Employees.ToList();
                }

                return Enumerable.Empty<EmployeeDto>();
            }
        }
    }
}