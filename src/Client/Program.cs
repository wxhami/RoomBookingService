using Application;
using Client.Endpoints.Extensions;
using Client.ExceptionHandlers;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

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

await app.InitialiseDatabaseAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfire();

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

app.MapEndpoints();


app.Run();