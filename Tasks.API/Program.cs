using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tasks.API.Controllers.Authentication;
using Tasks.API.Services;
using Tasks.API.Services.Interfaces;
using Tasks.Data.Interfaces;
using Tasks.Data.Models;
using Tasks.Data.Services;
using Tasks.Log.Data;
using Tasks.Log.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
    options.Filters.Add(typeof(ValdiateUserIdFilter));
})
.AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskDatabaseContext>(
    dbContextOptions => dbContextOptions.UseSqlServer(
        builder.Configuration["ConnectionStrings:TasksDBConnectionString"]));
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));


builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

builder.Services.AddScoped<ITaskStatusesRepository, TaskStatusesRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<ISystemUsersRepository, SystemUsersRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>(); 
builder.Services.AddScoped<ITaskCategoriesRepository, TaskCategoriesRepository>();

builder.Services.AddScoped<ILogDatabaseRepository, MongoDbDatabase>();

builder.Services.AddScoped<IPasswordHashHandler, PasswordHashHandler>();
builder.Services.AddScoped<ITokenManager, TokenManager>();
builder.Services.AddScoped<ValdiateUserIdFilter>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:Secret"]))
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAngularAppPolicy",
        corsPolicy =>
        {
            corsPolicy.WithOrigins(builder.Configuration["WebApplicationSettings:Url"])
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyAngularAppPolicy");

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
