﻿using ChatApp.Server.Application.Core.Abstractions;
using ChatApp.Server.Domain.Users;
using ChatApp.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Server.Api;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(defaultConnection));

        builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

        builder.Services.AddIdentityCore<User>().AddEntityFrameworkStores<ApplicationDbContext>();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        return app;
    }
}