using cabzcommerce.api.Helpers;
using cabzcommerce.api.Repositories;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
// Setup DB Settings
var dbSettings = builder.Configuration.GetSection(nameof(DBSettings)).Get<DBSettings>();
var apiSettings = builder.Configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>();
// setting up the connection string
// Inject MongoClient to app
builder.Services.AddSingleton<IMongoClient>(serviceProvider => {                
    return new MongoClient(dbSettings.ConnString);
});
// Add configs
builder.Services.AddSingleton(dbSettings);
builder.Services.AddSingleton(apiSettings);

// Add repos
builder.Services.AddSingleton<IUserRepo, UserRepo>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
