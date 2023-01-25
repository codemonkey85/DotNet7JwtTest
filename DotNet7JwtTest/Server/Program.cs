var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

services.AddControllersWithViews();
services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.MapUsersEndpoints();

app.Run();
