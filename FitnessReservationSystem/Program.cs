using FitnessReservationSystem.Configuration;
using FitnessReservationSystem.Data;
using FitnessReservationSystem.Interfaces;
using FitnessReservationSystem.Models;
using FitnessReservationSystem.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<DatabaseContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); });

builder.Services.AddControllers();
// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Dependency injection
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ILectureRepository, LectureRepository>();
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequiredLength = 5;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = false;
});
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);
var tokenValidationParameter = new TokenValidationParameters()
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateIssuer = false, //for development
    ValidateAudience = false, //for development
    RequireExpirationTime = false, //for development
    ValidateLifetime = true
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{    
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParameter;
});
builder.Services.AddSingleton(tokenValidationParameter);
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedEmail = false).AddEntityFrameworkStores<DatabaseContext>();

    //AddDefaultIdentity<ApplicationUser>(options => options.SignIn.Required.AddEntityFrameworkStores<DbContext>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetService<IConfiguration>();

builder.Services.AddCors(Options =>
{
    var frontendURL = configuration.GetValue<string>("FrontendUrl");
    var frontendLocalHost = configuration.GetValue<string>("frontendLocalHost");
    Options.AddDefaultPolicy(builder => 
    { 
        builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
        builder.WithOrigins(frontendLocalHost).AllowAnyMethod().AllowAnyHeader();
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

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
