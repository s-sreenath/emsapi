// <copyright file="Program.cs" company="EmsApi Company">
// Copyright (c) EmsApi Company. All rights reserved.
// </copyright>

using System.Reflection;
using Ems.Api.Data;
using Ems.Api.Data.Repository;
using Ems.Api.Feature.Employees.Validators;
using Ems.Api.Feature.Employees.Validators.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

        builder.Services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

        var dbContext = new DatabaseContext();

        builder.Services.AddTransient<IEmployeeValidator, EmployeeValidator>();
        builder.Services.AddTransient<IEmployeeSearchValidator, EmployeeSearchValidator>();
        builder.Services.AddScoped<DatabaseContext>();
        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => options.SupportNonNullableReferenceTypes());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}