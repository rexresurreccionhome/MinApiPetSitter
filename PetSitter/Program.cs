using System.Text.Json.Serialization;
using Scalar.AspNetCore;

using PetSitter.Domain.Interface;
using PetSitter.DataStore;
using PetSitter.DataStore.Models;
using PetSitter.Routes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<ConnectionStringSettings>(
    builder.Configuration.GetSection("ConnectionStrings")
);
builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<ISitterRepository, SitterRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();

var app = builder.Build();
app.UseAuthentication();
builder.Services.AddAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/api-docs", options =>
    {
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.MapGroup(PetRoute.baseUrl).MapPetApi();
app.MapGroup(SitterRoute.baseUrl).MapSitterApi();
app.MapGroup(AppointmentRoute.baseUrl).MapAppointmentApi();
app.Run();
