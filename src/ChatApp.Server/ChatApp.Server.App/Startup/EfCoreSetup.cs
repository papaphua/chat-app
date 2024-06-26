﻿using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Domain.Core.Identity;
using ChatApp.Server.Domain.Users;
using ChatApp.Server.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.App.Startup;

public static class EfCoreSetup
{
    public static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        var defaultConnection = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(defaultConnection));

        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());

        services.AddIdentityCore<User>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserValidator<CustomUserValidator>()
            .AddDefaultTokenProviders();

        return services;
    }
}