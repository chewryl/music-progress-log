using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Mappings;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Repositories;
using MusicProgressLogAPI.Repositories.Interfaces;
using MusicProgressLogAPI.Services;
using MusicProgressLogAPI.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MusicProgressLogDbContext>(options =>
options.UseSqlServer(builder.Configuration["MusicProgressLog:ConnectionString"], o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

builder.Services.AddDbContext<MusicProgressLogAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration["MusicProgressLogAuth:ConnectionString"]));

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepositoryBase<>));
builder.Services.AddScoped<IRepository<UserRelationship>, SqlUserRelationshipRepository>();
builder.Services.AddScoped<IUserRelationshipRepository<ProgressLog>, SqlProgressLogRepository>();
builder.Services.AddScoped<IUserRelationshipRepository<Piece>, SqlPieceRepository>();
builder.Services.AddScoped<IProgressLogService, ProgressLogService>();
builder.Services.AddAutoMapper(typeof(AutoMapperMappings));

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
