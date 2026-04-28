extern alias AzureIdentity;

using ApiPreparacionExamen.Data;
using ApiPreparacionExamen.Helpers;
using ApiPreparacionExamen.Repositories;
using Azure;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Security.KeyVault.Secrets;
using DefaultAzureCredential = AzureIdentity::Azure.Identity.DefaultAzureCredential;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var keyVaultUri = builder.Configuration["KeyVault:VaultUri"];
if (!string.IsNullOrWhiteSpace(keyVaultUri))
{
    builder.Configuration.AddAzureKeyVault(
        new Uri(keyVaultUri),
        new DefaultAzureCredential());
}

string? connectionString = null;
if (!string.IsNullOrWhiteSpace(keyVaultUri))
{
    var secretClient = new SecretClient(
        new Uri(keyVaultUri),
        new DefaultAzureCredential());

    var secret = await secretClient.GetSecretAsync("sqlServer");
    connectionString = secret.Value.Value;
}
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("SqlServer connection string is missing. Configure it in Key Vault.");
}
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
