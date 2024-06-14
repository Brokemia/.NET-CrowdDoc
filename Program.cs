using Blazorme;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.FluentUI.AspNetCore.Components;
using XMLDocCrowdSourcer.Components;
using XMLDocCrowdSourcer.Components.Account;
using XMLDocCrowdSourcer.Components.Pages.Project;
using XMLDocCrowdSourcer.Data;
using XMLDocCrowdSourcer.Services;

DotNetEnv.Env.Load();

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
        googleOptions.AccessDeniedPath = "/Account/Login";
    })
    .AddDiscord(discordOptions => {
        discordOptions.ClientId = builder.Configuration["Authentication:Discord:ClientId"]!;
        discordOptions.ClientSecret = builder.Configuration["Authentication:Discord:ClientSecret"]!;
        discordOptions.AccessDeniedPath = "/Account/Login";
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
    options.AddPolicy("Project.Create", policy =>
        policy.RequireRole("SuperAdmin"));
});
builder.Services.AddSingleton<IAuthorizationHandler, ProjectOwnerHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ProjectManagerHandler>();

// Database setup
if (builder.Environment.IsDevelopment()) {
    builder.Services.AddDbContext<ApplicationDbContext>(options => {
        options.EnableSensitiveDataLogging();
    });
} else {
    // Use postgresql in production
    builder.Services.AddDbContext<ApplicationDbContext, PostgresApplicationDbContext>();
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IDocumentationParser, DocumentationParser>();

// Diff component
builder.Services.AddDiff();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions {
    ForwardedHeaders = ForwardedHeaders.All
});

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

app.UseAuthentication();
app.UseAuthorization();

await RoleInitializer.InitializeAsync(app.Services);
//var roleManager = app.Services.GetRequiredService<RoleManager<IdentityRole>>();//.Users.Find("ead22c21-fedc-40d2-9244-33a51dcc9e27").
//var scope = app.Services.CreateScope();
//var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//var user = UserManager.FindByIdAsync("34914022-4dc0-43c7-b2a6-cdfe135f16df").Result;
//var res=UserManager.AddToRoleAsync(user, "SuperAdmin").Result;
//db.SaveChanges();

app.Run();
