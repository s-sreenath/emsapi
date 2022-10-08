// <copyright file="IEmployeeRepository.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Data.Repository
{
    using Ems.Api.Data.DTO;

    public interface IEmployeeRepository
    {
        Task<EmployeeDto> AddEmployeeAsync(EmployeeDto dto);

        IEnumerable<EmployeeDto> GetAll();
    }
}
