using CodeSentryAI.Application.Interfaces.Authentication;
using CodeSentryAI.Application.Interfaces.Persistence;
using CodeSentryAI.Infrastructure.Authentication.Jwt;
using CodeSentryAI.Infrastructure.Authentication.Passwords;
using CodeSentryAI.Infrastructure.Persistence.Context;
using CodeSentryAI.Infrastructure.Persistence.Repositories;
using CodeSentryAI.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CodeSentryAI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(
            configuration.GetSection(JwtSettings.SectionName));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IReviewRepository, ReviewRepository>();

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}