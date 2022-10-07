// <copyright file="IEmployeeRepository.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Data.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Ems.Api.Data.DTO;

    public interface IEmployeeRepository
    {
        int AddEmployee(EmployeeDto dto);
    }
}
