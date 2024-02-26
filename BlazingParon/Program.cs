using BlazingParon.Components;
using BlazingParon.Data;
using BlazingParon.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSqlite<InventoryManagementSystemDB>("Data Source=Paron.db");
builder.Services.AddHttpClient();
builder.Services.AddControllers();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


// Initialize the database
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InventoryManagementSystemDB>();
    if (db.Database.EnsureCreated())
    {
        SeedData.Initialize(db);
    }
}

// Print the database
using (var scope = scopeFactory.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<InventoryManagementSystemDB>();
    Console.WriteLine("Database contents:");
    foreach (var product in db.Products)
    {
        Console.WriteLine($"- {product.ProductId} ({product.Name})");
    }
}
app.Run();