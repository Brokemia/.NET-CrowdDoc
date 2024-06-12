using Blazorme;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using XMLDocCrowdSourcer.Components;
using XMLDocCrowdSourcer.Components.Account;
using XMLDocCrowdSourcer.Components.Pages.Project;
using XMLDocCrowdSourcer.Data;
using XMLDocCrowdSourcer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();
builder.Services.AddDataGridEntityFrameworkAdapter();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

// Auth

builder.Services.AddAuthentication(options => {
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddGoogle(googleOptions => {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
        googleOptions.Scope.Remove("email");
    })
    .AddDiscord(discordOptions => {
        discordOptions.ClientId = builder.Configuration["Authentication:Discord:ClientId"]!;
        discordOptions.ClientSecret = builder.Configuration["Authentication:Discord:ClientSecret"]!;
    })
    .AddIdentityCookies();

builder.Services.AddAuthorization(options => {
    options.AddPolicy("Project.Mappings.Edit", policy =>
        policy.AddRequirements(new ProjectRequirement { AllowManagers = true, AllowOwners = true }));
    options.AddPolicy("Project.Proposals.Approve", policy =>
        policy.AddRequirements(new ProjectRequirement { AllowManagers = true, AllowOwners = true }));
    options.AddPolicy("Project.Managers.Edit", policy =>
        policy.AddRequirements(new ProjectRequirement { AllowOwners = true }));
    // Currently overwrites a ton of stuff, so definitely needs stricter access
    // Also there's not really a reason you'd want to do this often
    options.AddPolicy("Project.Mappings.Import", policy =>
        policy.AddRequirements(new ProjectRequirement { AllowOwners = true }));
});
builder.Services.AddSingleton<IAuthorizationHandler, ProjectOwnerHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ProjectManagerHandler>();

// Database setup
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IDocumentationParser, DocumentationParser>();

// Diff component
builder.Services.AddDiff();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseMigrationsEndPoint();
} else {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
