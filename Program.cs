using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using WorkWell.Api.Data;
using WorkWell.Api.Repositories;



var builder = WebApplication.CreateBuilder(args);

// ------------ Configurações / Serviços ------------
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// Connection string
var connStr = builder.Configuration.GetConnectionString("OracleDb")
              ?? throw new InvalidOperationException("Connection string 'OracleDb' not found.");

// Registro do Oracle/Dapper
builder.Services.AddScoped<IDbConnection>(sp => new OracleConnection(connStr));

// Repositórios
builder.Services.AddScoped<IUseresRepository, UserRepository>();
builder.Services.AddScoped<IAssessmentRepository, AssessmentRepository>();

builder.Services.AddHealthChecks()
    .AddOracle(builder.Configuration.GetConnectionString("OracleDb")!, name: "oracle");

builder.Services.AddOpenTelemetry()
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddConsoleExporter();
    })
    .WithMetrics(metrics =>
    {
        metrics.AddAspNetCoreInstrumentation();
    });


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleDb")));


// ------------ Build app ------------
var app = builder.Build();

// ------------ Pipeline HTTP ------------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("RequestLogger");

    logger.LogInformation(" Request: {method} {url}", context.Request.Method, context.Request.Path);

    await next.Invoke();

    logger.LogInformation(" Response: {statusCode}", context.Response.StatusCode);
});

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.MapHealthChecks("/health");

app.Run();
