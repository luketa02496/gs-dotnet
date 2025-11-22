using Oracle.ManagedDataAccess.Client;
using System.Data;
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

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
