global using Microsoft.EntityFrameworkCore;
using ChinesOuctionServer.BL.Donators;
using ChinesOuctionServer.BL.Gifts;
using ChinesOuctionServer.BL.OrderItems;
using ChinesOuctionServer.BL.Orders;
using ChinesOuctionServer.BL.Ouctions;
using ChinesOuctionServer.DAL;
using ChinesOuctionServer.DAL.Donators;
using ChinesOuctionServer.DAL.Gifts;
using ChinesOuctionServer.DAL.OrderItems;
using ChinesOuctionServer.DAL.Orders;
using ChinesOuctionServer.DAL.Ouctions;
using ChinesOuctionServer.DAL.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using ChinesOuctionServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ChinesOuctionServer.BL.Useres;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddScoped<IDonatorService, DonatorService>();
builder.Services.AddScoped<IDonatorDal, DonatorDal>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserDal, UserDal>();
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<IGiftDal, GiftDal>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDal, OrderDal>();
builder.Services.AddScoped<IOuctionService, OuctionService>();
builder.Services.AddScoped<IOuctionDal, OuctionDal>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderItemDal, OrderItemDal>();


builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200",
                                "development web site")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                ;
        });
});

builder.Services.AddDbContext<HSContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("HSContext")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:7171/",
        ValidAudience = "http://localhost:4200",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
    };
});
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/Order"), orderApp =>
{
    orderApp.Use(async (context, next) =>
    {
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var authorizationHeader = context.Request.Headers["Authorization"].ToString();
            if (authorizationHeader.StartsWith("Bearer "))
            {
                context.Request.Headers["Authorization"] = authorizationHeader.Substring("Bearer ".Length);
            }
        }

        await next();
    });
    orderApp.UseMiddleware<AuthenticationMiddleware>();
});
IConfiguration configuration = app.Configuration;
IWebHostEnvironment environment = app.Environment;

app.MapControllers();



app.Run();

//builder.Services.AddControllers();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddDbContext<HSContext>(options =>
//                                          options.UseSqlServer(builder.Configuration.GetConnectionString("HSContext")));
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy", builder =>
//    {
//        builder.WithOrigins("http://localhost:4200", "development web site")
//            .AllowAnyHeader()
//            .AllowAnyMethod();
//    });
//});

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = "http://localhost:7171/",
//        ValidAudience = "http://localhost:7171/",
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//    };
//});


//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
//    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        Description = "JWT Authorization header using the Bearer scheme",
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer"
//    });
//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//            {
//                {
//                    new OpenApiSecurityScheme
//                    {
//                        Reference = new OpenApiReference
//                        {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                        }
//                    },
//                    Array.Empty<string>()
//                }
//            });

//    c.OperationFilter<SecurityRequirementsOperationFilter>();
//});

//builder.Services.AddMvc();
//builder.Services.AddControllers();
//builder.Services.AddRazorPages();
//builder.Services.AddDbContext<HSContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("OrdersContext")));
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("CorsPolicy",
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:4200",
//                            "development web site")
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//        });
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}
//app.UseRouting();
//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseCors("CorsPolicy");


//app.UseHttpsRedirection();
//app.UseRouting();
//app.UseCors("CorsPolicy");
//app.UseAuthentication();
//app.UseAuthorization();
//app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/Order"), orderApp =>
//{
//    orderApp.Use(async (context, next) =>
//    {
//        if (context.Request.Headers.ContainsKey("Authorization"))
//        {
//            var authorizationHeader = context.Request.Headers["Authorization"].ToString();
//            if (authorizationHeader.StartsWith("Bearer "))
//            {
//                context.Request.Headers["Authorization"] = authorizationHeader.Substring("Bearer ".Length);
//            }
//        }

//        await next();
//    });
//    orderApp.UseMiddleware<AuthenticationMiddleware>();
//});

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

//app.Run();




