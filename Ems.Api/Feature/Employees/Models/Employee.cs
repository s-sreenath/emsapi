// <copyright file="Employee.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

namespace Ems.Api.Feature.Employees.Models;

using System.ComponentModel.DataAnnotations;

public class Employee
{
    public Employee()
    {
        this.FirstName = string.Empty;
        this.LastName = string.Empty;
        this.Email = string.Empty;
    }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public int Age { get; set; }
}
