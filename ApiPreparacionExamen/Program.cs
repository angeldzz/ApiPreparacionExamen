using ApiPreparacionExamen.Data;
using ApiPreparacionExamen.Helpers;
using ApiPreparacionExamen.Repositories;
using Azure.Security.KeyVault.Secrets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAzureClients(factory =>
{
    factory.AddSecretClient
    (builder.Configuration.GetSection("KeyVault"));
});
SecretClient secretClient =
    builder.Services.BuildServiceProvider()
    .GetService<SecretClient>();
KeyVaultSecret secreto =
    await secretClient.GetSecretAsync("sqlServer");
string connectionString = secreto.Value;
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<HospitalContext>
    (options => options.UseSqlServer(connectionString));
builder.Services.AddTransient<RepositoryHospitales>();
builder.Services.AddTransient<HelperSubirFoto>();
builder.Services.AddTransient<HelperCryptography>();
HelperActionOAuthService helper = new HelperActionOAuthService(builder.Configuration);
builder.Services.AddSingleton<HelperActionOAuthService>(helper);
builder.Services.AddAuthentication(helper.GetAuthenticationSchema())
    .AddJwtBearer(helper.GetJWTBearerOptions());
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference();
app.MapGet("/", context =>
{
    context.Response.Redirect("/scalar");
    return Task.CompletedTask;
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
