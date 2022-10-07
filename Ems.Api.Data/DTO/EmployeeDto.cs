// <copyright file="EmployeeDto.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Data.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EmployeeDto
    {
        public EmployeeDto()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public int EmployeeId { get; set; }
    }
}
