using Docker.Redis.Sample.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomMediatr();
builder.Services.AddCustomServices();
builder.Services.AddCustomControllers();
builder.Services.AddCustomConfiguration(builder.Configuration);

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

app.UseHttpsRedirection();
app.MapControllers();
app.Run();