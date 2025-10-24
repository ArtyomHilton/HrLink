using System.Text;
using HrLink.Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HrLink.Auth.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection serviceCollection)
    {
        using var scope = serviceCollection.BuildServiceProvider().CreateScope();
        var options = scope.ServiceProvider.GetRequiredService<IOptions<JwtBearerOptions>>();

        serviceCollection.AddTransient<IJwtProvider, JwtProvider>();
        
        serviceCollection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(configure =>
            {
                configure.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = options.Value.Issuer,
                    ValidateIssuer = options.Value.ValidateIssuer,
                    ValidAudience = options.Value.Audience,
                    ValidateAudience = options.Value.ValidateAudience,
                    ValidateLifetime = options.Value.ValidateLifetime,
                    ValidateIssuerSigningKey = options.Value.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey))
                };

                configure.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["X-TASTY-X"];

                        return Task.CompletedTask;
                    }
                };
            });
        
        serviceCollection.AddAuthorization();

        return serviceCollection;
    }
}