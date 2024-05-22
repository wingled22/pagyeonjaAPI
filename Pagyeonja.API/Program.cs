using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using pagyeonjaAPI.Controllers;
using Microsoft.Extensions.FileProviders;
using Pagyeonja.Entities.Entities;
using PagyeonjaServices.Services;
using Pagyeonja.Repositories.Repositories;
using Pagyeonja.Services.Services;
using Microsoft.AspNetCore.Identity;
using Pagyeonja.Identities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Pagyeonja.Repositories;
using Pagyeonja.Services;
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDbContext<HitchContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, AppRole>(option =>
{
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireDigit = false;
    option.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<ICommuterService, CommuterService>();
builder.Services.AddScoped<ICommuterRepository, CommuterRepository>();
builder.Services.AddScoped<ApprovalService>();
builder.Services.AddScoped<IApprovalService, ApprovalService>();
builder.Services.AddScoped<IDatabaseTransactionRepository, DatabaseTransactionRepository>();
builder.Services.AddScoped<IApprovalRepository, ApprovalRepository>();
builder.Services.AddScoped<IRiderService, RiderService>();
builder.Services.AddScoped<IRiderRepository, RiderRepository>();
builder.Services.AddScoped<RiderService>();
builder.Services.AddScoped<ISuspensionService, SuspensionService>();
builder.Services.AddScoped<ISuspensionRepository, SuspensionRepository>();
builder.Services.AddScoped<IUpdateSuspensionService, UpdateSuspensionService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IRideHistoryService, RideHistoryService>();
builder.Services.AddScoped<IRideHistoryRepository, RideHistoryRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ITopupHistoryService, TopupHistoryService>();
builder.Services.AddScoped<ITopupHistoryRepository, TopupHistoryRepository>();
builder.Services.AddScoped<IBookingRequestService, BookingRequestService>();
builder.Services.AddScoped<IBookingRequestRepository, BookingRequestRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        // ValidateLifetime = true,
        // ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["JWT:ValidIssuer"],
        ValidAudience = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    // option.SwaggerDoc("v1", new OpenApiInfo { Title = "gradTrackerAPI", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[]{}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddControllers().AddJsonOptions(
    options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    }
);

// Add static files service
builder.Services.AddDirectoryBrowser();

builder.Services.AddHostedService<BackgroundServiceSuspension>();

var app = builder.Build();

// Use static files middleware
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
    RequestPath = ""
});

app.UseCors();

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
