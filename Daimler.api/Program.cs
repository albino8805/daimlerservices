using Daimler.api.Hubs;
using Daimler.data.IRepository;
using Daimler.data.Models;
using Daimler.data.Repository;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;
using Daimler.domain.Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using OfficeOpenXml;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://serthi-admin.com", "http://localhost:4200", "*").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

string dbConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DAIMLERContext>(p => p.UseSqlServer(dbConnection));

builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSetting"));

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

#region Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IModuleRepository, ModuleRepository>();
builder.Services.AddScoped<IActionRepository, ActionRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IModuleActionRepository, ModuleActionRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<ITownRepository, TownRepository>();
builder.Services.AddScoped<IBinnacleRepository, BinnacleRepository>();
builder.Services.AddScoped<IFolderRepository, FolderRepository>();
#endregion

#region Managers
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<IAuthenticationManager, AuthenticationManager>();
builder.Services.AddScoped<IModuleManager, ModuleManager>();
builder.Services.AddScoped<IActionManager, ActionManager>();
builder.Services.AddScoped<IProfileManager, ProfileManager>();
builder.Services.AddScoped<IModuleActionManager, ModuleActionManager>();
builder.Services.AddScoped<ICountryManager, CountryManager>();
builder.Services.AddScoped<IStateManager, StateManager>();
builder.Services.AddScoped<ITownManager, TownManager>();
builder.Services.AddScoped<IBinnacleManager, BinnacleManager>();
builder.Services.AddScoped<IFolderManager, FolderManager>();

#endregion

#region Helpers
builder.Services.AddScoped<IFolderHelper, FolderHelper>();
#endregion

var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<DAIMLERContext>();

ApplyMigrations(dbContext);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.MapHub<ChatHub>("/hubs/chat");

app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseStaticFiles(new StaticFileOptions
//{
//	FileProvider = new PhysicalFileProvider(
//		   Path.Combine(builder.Environment.ContentRootPath, "Upload")),
//	RequestPath = "/Upload"
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ApplyMigrations(DAIMLERContext dBContext)
{
    bool applayMigrations = true;

#if DEBUG
    Console.WriteLine("Las migraciones no se aplican en Modo debug.");
    applayMigrations = false;
#endif

    if (applayMigrations)
    {
        List<string> pendingMigrations = dBContext.Database.GetPendingMigrations().ToList();
        DateTime startMigrations = DateTime.UtcNow;

        if (pendingMigrations.Any())
        {
            IMigrator migrator = dBContext.Database.GetService<IMigrator>();
            DateTime startMigration;
            Console.WriteLine("++++ ============================ [ Migrations ] ================================ ++++");
            foreach (string targetMigration in pendingMigrations)
            {
                startMigration = DateTime.UtcNow;
                migrator.Migrate(targetMigration);
                TimeSpan timeSpan = DateTime.UtcNow - startMigration;
                Console.WriteLine($" Execution time: {timeSpan:hh\\:mm\\:ss\\:ffff} | Applied migration: {targetMigration}");
            }
            TimeSpan timeSpanMigrations = DateTime.UtcNow - startMigrations;
            Console.WriteLine($" Total run time: {timeSpanMigrations:hh\\:mm\\:ss\\:ffff}");
            Console.WriteLine("++++ ============================================================================ ++++");
        }
    }
}