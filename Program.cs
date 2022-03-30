// var builder = WebApplication.CreateBuilder(args);
// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();

using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using testAutofac.Data;
using testAutofac.Data.Profiles;
using testAutofac.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DatabaseSettings:MySQLSettings:ConnectionStrings:DefaultConnection");
//builder.Services.AddDbContext<AppDbContext>(options =>
//            options.UseSqlServer(connection));
//builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    // Declare your services with proper lifetime
    builder.Register(x =>
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
        return new ApplicationDbContext(optionsBuilder.Options);
    }).InstancePerLifetimeScope();

    var testAutofac = Assembly.GetExecutingAssembly();

    builder.RegisterAssemblyTypes(testAutofac)
       .Where(t => t.Name.EndsWith("Service"))
       .AsImplementedInterfaces();
    builder.RegisterAssemblyTypes(testAutofac)
    .Where(t => t.Name.EndsWith("Service"))
    .AsImplementedInterfaces();
    builder.RegisterType<RepositoryWrapper>().As<IRepositoryWrapper>().SingleInstance();
});
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
