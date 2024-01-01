using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Mappings;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Repositories;
using MusicProgressLogAPI.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MusicProgressLogDbContext>(options =>
options.UseSqlServer(builder.Configuration["MusicProgressLog:ConnectionString"], o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddScoped<IProgressLogRepository, SQLProgressLogRepository_old>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepositoryBase<>));
builder.Services.AddScoped<IRepository<Piece>, SqlPieceRepository>();
builder.Services.AddScoped<IRepository<UserRelationship>, SqlUserRelationshipRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperMappings));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
