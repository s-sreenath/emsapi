// <copyright file="EmployeeRepository.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Data.Repository
{
    using System.Diagnostics.CodeAnalysis;
    using Ems.Api.Data.DTO;

    [ExcludeFromCodeCoverage]
    public class EmployeeRepository : IEmployeeRepository
    {
        public int AddEmployee(EmployeeDto dto)
        {
            using (var context = new DatabaseContext())
            {
                context.Add(dto);
                context.SaveChanges();
            }

            return 0;
        }
    }
}
