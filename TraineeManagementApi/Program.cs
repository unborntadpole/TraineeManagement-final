using TraineeManagementApi.Services;
using TraineeManagementApi.db;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RabbitMQ.Client;




var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "http://localhost:5173");
                      });
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TraineeManagementApi",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Enter your JWT token.\nExample: eyJhbGciOi..."
    });

    options.AddSecurityRequirement(document => 
    {
        var requirement = new OpenApiSecurityRequirement();
        var schemeReference = new OpenApiSecuritySchemeReference("Bearer", document);
        requirement.Add(schemeReference, new List<string>());
        return requirement;
    });
});

builder.Services.AddScoped<ITraineeService, TraineeService>();
builder.Services.AddScoped<IMentorService, MentorService>();
builder.Services.AddScoped<ILearningTaskService, LearningTaskService>();
builder.Services.AddScoped<ITraineeRepository, TraineeRepository>();
builder.Services.AddScoped<IMentorRepository, MentorRepository>();
builder.Services.AddScoped<ILearningTaskRepository, LearningTaskRepository>();
builder.Services.AddScoped<TaskAssignmentRepository>();
builder.Services.AddScoped<TaskAssignmentService>();
builder.Services.AddScoped<SubmissionRepository>();
builder.Services.AddScoped<SubmissionService>();
builder.Services.AddScoped<SubmissionFileRepository>();
builder.Services.AddScoped<SubmissionFileService>();
builder.Services.AddScoped<ReviewRepository>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<ProcessingJobsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher<string>, PasswordHasher<string>>();
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<CreateTraineeRequestValidator>();
builder.Services.AddScoped<UpdateTraineeRequestValidator>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddExceptionHandler<ExceptionHandlerService>();
// try
// {
//     var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQ");
//     var factory = new ConnectionFactory
//     {
//         Uri = new Uri(rabbitMQSettings["Uri"])
//     };
//     IConnection connection = await factory.CreateConnectionAsync();
//     builder.Services.AddSingleton<IConnection>(connection);
//     // builder.Services.AddSingleton(sp => new ConnectionFactory
//     // {
//     //     Uri = new Uri(rabbitMQSettings["Uri"]),
//     //     AutomaticRecoveryEnabled = true, // Automatically reconnects if network drops
//     //     TopologyRecoveryEnabled = true   // Re-declares queues/exchanges upon reconnection
//     // });
// }
// catch(Exception e)
// {
//     Console.WriteLine($"RabbitMQ connection failed: {e.Message}");
// }

// var rabbitMQSettings = builder.Configuration.GetSection("RabbitMQ");
var rabbitMQSettings = builder.Configuration.GetConnectionString("RabbitMqURI");
// var uriString = rabbitMQSettings["Uri"] ?? throw new InvalidOperationException("RabbitMQ URI is missing.");
var uriString = rabbitMQSettings ?? throw new InvalidOperationException("RabbitMQ URI is missing.");
builder.Services.AddSingleton<IConnection>(serviceProvider =>
{
    var factory = new ConnectionFactory
    {
        Uri = new Uri(uriString)
    };
    return factory.CreateConnectionAsync().GetAwaiter().GetResult();
}); // this seems like the better way to do it

builder.Services.AddSingleton<RabbitMQProducer>();

var jwtSettings = builder.Configuration.GetSection("JwtConfig");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine("JWT AUTH CRITICAL FAILURE: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// builder.Services.AddFluentValidationAutoValidation();
// builder.Services.AddValidatorsFromAssemblyContaining<CreateTraineeRequestValidator>();

builder.Services.AddOpenApi();

// builder.Services.AddDbContext<ApplicationDbContext>(
//     options => options.UseInMemoryDatabase("MyTestDatabase")
// );

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
?? throw new InvalidOperationException("Connections String: 'Default connection string not found'");

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseMySQL(
        connectionString
    ));



// builder.Services.AddStackExchangeRedisCache(options =>
// {
//     options.Configuration = builder.Configuration.GetConnectionString("RedisConnection") 
//                             ?? "localhost:6379";
//     options.InstanceName = "TMapi_"; // Prefixes all keys in Redis
// });


var app = builder.Build();

RedisCacheHelper.Initialize(app.Configuration);

if (app.Environment.IsDevelopment())
{
    // app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });

}

// app.UseHttpsRedirection();

// app.UseStaticFiles(new StaticFileOptions
// {
//     FileProvider = new PhysicalFileProvider(
//            Path.Combine(builder.Environment.ContentRootPath, UploadFilesConstants.UploadDirectory))//,
//     // RequestPath = UploadFilesConstants.RequestPath
// });

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
