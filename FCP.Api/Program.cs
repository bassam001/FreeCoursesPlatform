using FCP.Application;
using FCP.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS for Angular
builder.Services.AddCors(o => o.AddPolicy("ng", p =>
    p.AllowAnyHeader()
     .AllowAnyMethod()
     .WithOrigins("http://localhost:4200")));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("ng");

app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => "API is running ✅");

app.Run();
