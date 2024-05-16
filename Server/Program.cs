using Server;
using Server.Extensions;
using Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.AddServerServices();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

}
app.UseExceptionHandling(app.Environment);
//builder.Host.UseSerilog();
app.UseForwarding(builder.Configuration);
//app.UseExceptionHandling(env);
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Files")),
//    RequestPath = new PathString("/Files")
//});
app.UseRequestLocalizationByCulture();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//app.UseHangfireDashboard("/jobs", new DashboardOptions
//{
//    DashboardTitle = localizer["BlazorHero Jobs"],
//    Authorization = new[] { new HangfireAuthorizationFilter() }
//});

app.ConfigureSwagger();
app.Initialize(builder.Configuration);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseEndpoints();

app.Run();
