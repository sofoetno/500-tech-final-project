using System.Reflection;
using System.Text.Json.Serialization;
using FinalProject;
using FinalProject.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<EcommerceDBContext>((options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"))));
builder.Services.AddDbContext<EcommerceDbContext>();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<UserAccountRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<UserTypeRepository>();
builder.Services.AddScoped<CartRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CartItemRepository>();
builder.Services.AddScoped<OrderRepository>();
builder.Services.AddScoped<DeliveryRepository>();
builder.Services.AddScoped<PaymentRepository>();
builder.Services.AddScoped<TransactionRepository>();

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