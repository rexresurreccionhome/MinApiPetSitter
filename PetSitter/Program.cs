using System.Text.Json.Serialization;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using PetSitter.Domain.Interface;
using PetSitter.DataStore;
using PetSitter.DataStore.Models;
using PetSitter.Routes;
using PetSitter.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer((jwtOptions) =>
{
    new JwtBearerValidator(builder.Configuration.GetSection("Jwt")).Configure(jwtOptions);
});
builder.Services.AddAuthorizationBuilder()
    .AddPolicy(
        AuthorizationByClaims.Member.authName,
        AuthorizationByClaims.Member.Configure
    )
    .AddPolicy(
        AuthorizationByClaims.Staff.authName,
        AuthorizationByClaims.Staff.Configure
    );
builder.Services.AddOpenApi();
builder.Services.Configure<ConnectionStringSettings>(
    builder.Configuration.GetSection("ConnectionStrings")
);
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt")
);
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<ISitterRepository, SitterRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<JwtTokenProvider>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/api-docs", options =>
    {
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.MapGroup(PetRoute.baseUrl).MapPetApi().RequireAuthorization(AuthorizationByClaims.Member.authName);
app.MapGroup(SitterRoute.baseUrl).MapSitterApi().RequireAuthorization(AuthorizationByClaims.Staff.authName);
app.MapGroup(AppointmentRoute.baseUrl).MapAppointmentApi();
app.MapPost(OauthHandler.baseUrl, OauthHandler.GenerateToken);

app.Run();
