using DocumentDbDemo.Models;
using DocumentDbDemo.Services;
using MessagePack.AspNetCoreMvcFormatter;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoConnectionSettings>(builder.Configuration.GetSection("MongoDocumentDatabase"));

// Add services to the container.

builder.Services.AddControllers(c =>
{
    c.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
    c.OutputFormatters.Add(new MessagePackOutputFormatter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDocumentService, MongoDocumentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
