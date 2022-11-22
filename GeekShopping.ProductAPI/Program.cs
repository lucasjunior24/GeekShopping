
using AutoMapper;
using GeekShopping.ProductAPI.Config;
using GeekShopping.ProductAPI.Models.Context;
using GeekShopping.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

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

MappingConfig mapper = new MappingConfig();
IMapper mapperConfiguration = mapper.RegisterMaps().CreateMapper();

builder.Services.AddSingleton(mapperConfiguration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
