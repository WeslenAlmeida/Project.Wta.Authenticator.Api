using Application.Infrastructure;
using CrossCutting.Configuration;
using Domain.Shared.v1.Authorization;

var builder = WebApplication.CreateBuilder(args);

var bootstrapper = new Bootstrapper(builder.Services);

builder.Services.AddControllers(options => options.Filters.Add(new RestrictDomainAttribute(AppSettings.DomainsAllowed.Domains!)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
    
app.UseSwaggerUI();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
