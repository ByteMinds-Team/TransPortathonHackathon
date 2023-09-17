using Application;
using Application.Common.Extensions;
using Infrastructure;
using Microsoft.AspNetCore.Http.Features;
using Persistence;
using WebAPI;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddSwagger();
builder.Services.AddJWT(builder.Configuration);

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600;
});

builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// if (app.Environment.IsProduction())
app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseCors(x => x.WithOrigins("http://localhost:4200/", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat",
    options => { options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets; });

app.Run();