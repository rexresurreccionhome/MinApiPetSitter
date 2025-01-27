using PetSitter.Routes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
builder.Services.AddAuthorization();

app.MapGroup(PetRoute.baseUrl).MapPetApi();
app.MapGroup(SitterRoute.baseUrl).MapSitterApi();
app.Run();
