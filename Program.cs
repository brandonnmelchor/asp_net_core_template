using Microsoft.EntityFrameworkCore;
using Template.Configuration;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

var clientPath = "../client";
var corsPolicy = "CorsPolicy";

// - - - service registration - - -

// define components that register dependencies in the application service container
// order is important -- place components that depend on others below their dependencies
// 
var components = new IComponent[] {
    new Template.Data.Component(config),
    new Template.Components.Auth.Component(),
    new Template.Components.Template.Component(),
};

// enable UseSensitiveDataLogging in the config file
// this allows you to see values bound to parameters in sql statements for debugging
// don't forget to disable it production
// 
var logSensitiveData = config.GetValue<bool>("asp_net_core_template:UseSensitiveDataLogging");

builder.Services
    .AddDbContextPool<AppDbContext>(options => options
        .UseNpgsql(config.GetConnectionString("AppConnection"))
        .EnableSensitiveDataLogging(logSensitiveData));

builder.Services.AddMemoryCache();
builder.Services.AddControllers();

// add clients to CorsOrigins in the config file
// this enables the app to serve requests from other origins
//
builder.Services.AddCors(options =>
{
    var origins = config.GetSection("asp_net_core_template:CorsOrigins")
        .Get<List<string>>()
        .ToArray();

    options.AddPolicy(corsPolicy, builder => builder
        .WithOrigins(origins)
        .AllowCredentials()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// components bind services to the the application container here
// 
foreach (var component in components)
{
    component.Register(builder.Services);
}

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSpaStaticFiles(options =>
    {
        options.RootPath = clientPath + "/dist";
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

// - - - request pipeline and endpoint configuration - - -

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors(corsPolicy);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// enable UseDevExceptions in the config file
// this allows you to see more informative error messages in endpoint responses
// don't forget to disable it production
// 
if (config.GetValue<bool>("asp_net_core_template:UseDevExceptions"))
{
    app.UseDeveloperExceptionPage();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSpaStaticFiles();

    app.MapWhen(context => !context.Request.Path.StartsWithSegments("/api"), client =>
    {
        app.UseSpa(spa =>
        {
            spa.Options.SourcePath = clientPath;
            spa.UseProxyToSpaDevelopmentServer(config.GetValue<Uri>("asp_net_core_template:ClientDevServer"));
        });
    });
}

app.Run();
