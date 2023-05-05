using Infraestructure.Interfaces;
using Infraestructure.Setings;
using Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configuration
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();
builder.Services.AddTransient<IProductsRepository, ProductsRepository>();
builder.Services.AddTransient<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddTransient<IShippersRepository, ShippersRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();

builder.Services.Configure<InfraestructureSettings>(builder.Configuration);


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
