using OrganizationData.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureOptions();

// Add services to the container.

builder.Services
    .ConfigureOrganizationApplication()
    .AddOrganizationAuthentication()
    .AddOrganizationAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

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
