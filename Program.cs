using _2FAService.Extensions;
using _2FAService.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "2FAService";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(90);
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var docPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "2FAService.xml");
builder.Services.AddSwaggerGen(c =>
{
    c.IncludeXmlComments(docPath);

});
//inject our configuration 
var myAppSetting = new Service2FAsetting();
builder.Configuration.GetSection("Service2FASetting").Bind(myAppSetting);

//builder.Services.AddScoped<IService2FASetting, Service2FAsetting>();
builder.Services.AddSingleton(typeof(IService2FASetting), myAppSetting);
//add the Storage Provider
builder.Services.AddSingleton(typeof(MyCustomStorage),
    MyCustomStorage.Instance(new FileStorageProvider(myAppSetting.StoragePath)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();

