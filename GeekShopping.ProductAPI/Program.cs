
using AutoMapper;
using GeekShopping.ProductAPI.Config;
using GeekShopping.ProductAPI.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.ProductAPI", Version = "V1"})
); 

var conection = builder.Configuration["MySQLConection:MySQLContextConectionString"];
builder.Services.AddDbContext<MySQLContext>(    
    options => options.UseMySql(
        conection, new MySqlServerVersion(
            new Version(8, 0, 31)
        )
    )
 );

IMapper mapper = new MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
