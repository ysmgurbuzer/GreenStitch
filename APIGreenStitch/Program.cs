using BusinessLayer;
using static BusinessLayer.DependencyResolvers.DependencyExtension;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using BusinessLayer._Helpers;
using EntityLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using APIGreenStitch.Jwt;
using APIGreenStitch.Models;
using System.Security.Claims;
using Hangfire;
using Microsoft.CodeAnalysis.Options;
using Microsoft.OpenApi.Models;
using System.Text;
using Dtos;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("GreenStitchApiCors", opts =>
    {
        opts.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials(); ;
    });
});
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
   
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});
builder.Services.AddDbContext<GreenContext>(opt => opt.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DefaultConnectionString").Value));
var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");


builder.Services.AddHangfire(config => config
.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
.UseSimpleAssemblyNameTypeSerializer()
.UseRecommendedSerializerSettings()
.UseSqlServerStorage(hangfireConnectionString));


builder.Services.AddHangfireServer();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy= CookieSecurePolicy.Always;   
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentity<Member, Role>()
            .AddEntityFrameworkStores<GreenContext>()
            .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 6;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})


.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
        ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
    };
});





builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddDependencies(builder.Configuration);

var profiles = ProfileHelpers.GetProfiles();
var configuration = new AutoMapper.MapperConfiguration(opt =>
{
    opt.CreateMap<AdvertProductApiModel, AdvertisementCreateDto>()
        .ReverseMap();
    opt.CreateMap<AdvertProductApiModel, ProductCreateDto>()
        .ReverseMap();
    opt.CreateMap<AdvertProductApiModel, AdvertisementUpdateDto>()
        .ReverseMap();
    opt.CreateMap<AdvertProductApiModel, ProductUpdateDto>()
        .ReverseMap();
    opt.CreateMap<AdvertProductApiModel, ProductSize>()
        .ReverseMap();
    opt.AddProfiles(profiles);
   
});
var mapper = configuration.CreateMapper();
builder.Services.AddSingleton(mapper);




var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("GreenStitchApiCors");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.UseHangfireDashboard();
app.MapHangfireDashboard("/hangfire");

app.Run();
