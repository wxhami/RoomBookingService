using Application;
using Client.Endpoints.Extensions;
using Client.ExceptionHandlers;
using Hangfire;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<Program>(true).AddEnvironmentVariables();


builder.Services.AddInfrastructure(builder.Configuration, builder.Host).AddApplication(builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(options => { });

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme,
    options => { options.BearerTokenExpiration = TimeSpan.FromDays(365); });
builder.Services.AddAuthorizationBuilder();

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddSignInManager()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddApiEndpoints();

builder.Services
    .AddExceptionHandler<ObjectNotFoundExceptionHandler>();

builder.Services.AddAntiforgery();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfire();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();


app.UseAuthentication();

app.UseAuthorization();


app.UseAntiforgery();


app.UseHttpLogging();

app.MapGroup("api/v1/identity/").MapIdentityApi<ApplicationUser>();
app.MapEndpoints();

await MigrateDatabaseAsync(app);

app.Run();

static async Task MigrateDatabaseAsync(IHost app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    await context.Database.MigrateAsync();
}