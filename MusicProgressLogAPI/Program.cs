using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Mappings;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Repositories;
using MusicProgressLogAPI.Repositories.Interfaces;
using MusicProgressLogAPI.Services;
using MusicProgressLogAPI.Services.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.OpenApi.Models;
using Serilog;
using MusicProgressLogAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    //.WriteTo.File("C:/temp/MusicProgressLog/MusicProgressLog_.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Music Progress Log API",
        Version = "v1" 
    });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                },
                Scheme = "oauth2",
                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});


builder.Services.AddDbContext<MusicProgressLogDbContext>(options =>
options.UseSqlServer(builder.Configuration["MusicProgressLog:ConnectionString"], o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepositoryBase<>));
builder.Services.AddScoped<IRepository<ApplicationUser>, SqlUserRepository>();
builder.Services.AddScoped<IUserRepository<ProgressLog>, SqlProgressLogRepository>();
builder.Services.AddScoped<IUserRepository<Piece>, SqlPieceRepository>();
builder.Services.AddScoped<IProgressLogService, ProgressLogService>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<DbContext, MusicProgressLogDbContext>();
builder.Services.AddAutoMapper(typeof(AutoMapperMappings));
builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole<Guid>>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("MusicProgressLog")
    .AddEntityFrameworkStores<MusicProgressLogDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

var AllowedOrigins = "_allowedOrigins";
var origins = builder.Configuration.GetSection("MusicProgressLog:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowedOrigins,
        policy =>
        {
            policy.WithOrigins(origins)
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

builder.Services.AddAuthorization(options =>
    options.AddPolicy("UserOnly", policy =>
    policy.RequireAssertion(context =>
    {
        // Validate whether user NameIdentifier claim matches userId for resource
        var userIdClaim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var userIdRouteValue = new HttpContextAccessor().HttpContext.Request.RouteValues["userId"]?.ToString();
        return userIdClaim == userIdRouteValue;
    })));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors(AllowedOrigins);
//app.UseCors(options => options.WithOrigins("*").AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
