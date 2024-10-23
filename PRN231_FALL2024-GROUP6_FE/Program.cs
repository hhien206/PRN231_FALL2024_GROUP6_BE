using Service.IService;
using Service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(); // For Razor Pages
builder.Services.AddControllers(); // For API controllers (if you're using them)
builder.Services.AddHttpClient(); // HTTP client for API calls
builder.Services.AddScoped<IJobService, JobService>(); // Register JobService

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true; // Security: prevent client-side access
    options.Cookie.IsEssential = true; // Essential cookies
});

builder.Services.AddHttpContextAccessor(); // Needed to access the HTTP context

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts(); // Enable HSTS for production
}

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS
app.UseStaticFiles(); // Serve static files like CSS, JS, and images

app.UseRouting(); // Enable routing

app.UseSession(); // Enable session handling (must come before app.UseAuthorization)

app.UseAuthorization(); // Enable authorization handling

// Map routes
app.MapRazorPages(); // Map Razor Pages
app.MapControllers(); // Map API controllers

app.Run();
