using Microsoft.AspNetCore.Rewrite;
using TechStore.Auth.Configs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.CustomizeController();
builder.Services.AddControllers();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddUnitOfWork();
builder.Services.AddAuthorize();
builder.Services.AddAuthentication();
builder.Services.AddSwaggerConfig();
builder.Services.AddDatabase();
builder.Services.AddServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var options = new RewriteOptions().Add(new LowerCaseControllers());
app.UseRewriter(options);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
