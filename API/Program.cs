using API.Middleware;
using Core.Identity;
using Core.Interfaces;
using Infrastucture.Data;
using Infrastucture.Identity;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

builder.Services.AddDbContext<StoreContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDbContext<AppIdentityDbContext>(opt =>
{
	opt.UseSqlite(builder.Configuration.GetConnectionString("IdentityConnection"));
});
builder.Services.AddIdentityCore<AppUser>(opt =>
{

})
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddSignInManager<SignInManager<AppUser>>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


builder.Services.AddSingleton<IConnectionMultiplexer>(c => //setting the Redis for store the basket
{
    var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
    return ConnectionMultiplexer.Connect(options);
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// to handel the CORS Origin for Angular
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {                                                                  // angular soket
		policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

/**********************************************/ // to auto update-database after Run program
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<StoreContext>();

var identityContext = services.GetRequiredService<AppIdentityDbContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();

var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await identityContext.Database.MigrateAsync();
    await StoreContextSeed.SeedAsync(context);
    await AppIdentityDbContextSeed.SeedUserAsync(userManager);
}
catch(Exception ex)
{
    Debug.WriteLine("Hello World");

	logger.LogError(ex, "Error Migration");
}
/**********************************************/



app.Run();
