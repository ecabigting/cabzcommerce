using System.Text;
using cabzcommerce.api.Helpers;
using cabzcommerce.api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

//
// Add services to the container
//
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

//
// Setup DB Settings
//
var dbSettings = builder.Configuration.GetSection(nameof(DBSettings)).Get<DBSettings>();
var apiSettings = builder.Configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>();

//
// Adding the Authentication method
//
builder.Services
.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = apiSettings.Issuer,
        ValidAudience = apiSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSettings.HashKey))
    };
});

//
// setting up the connection string
// Inject MongoClient to app
//
builder.Services.AddSingleton<IMongoClient>(serviceProvider => {                
    return new MongoClient(dbSettings.ConnString);
});

//
// Inject Configs
//
builder.Services.AddSingleton(dbSettings);
builder.Services.AddSingleton(apiSettings);

//
// Inject repos
//
builder.Services.AddSingleton<IUserRepo, UserRepo>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options => {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Description = "Bearer Authentication with JWT Token",
            Type = SecuritySchemeType.Http
        });
        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    new List<string>()
                }
            }
        );
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
