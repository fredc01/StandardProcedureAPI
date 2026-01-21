using Microsoft.EntityFrameworkCore;
using StandardProcedureAPI.Data;

var builder = WebApplication.CreateBuilder(args);

/* ===============================
   SERVICES
================================ */

// Controllers
builder.Services.AddControllers();

// EF Core - PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// Supabase Client (Singleton)
builder.Services.AddSingleton(_ =>
    new Supabase.Client(
        builder.Configuration["Supabase:Url"]!,
        builder.Configuration["Supabase:Key"]!
    )
);

// CORS (Allows local HTML + frontend apps)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* ===============================
   APP
================================ */

var app = builder.Build();

// Swagger (keep enabled for now)
app.UseSwagger();
app.UseSwaggerUI();

// HTTPS
app.UseHttpsRedirection();

// Routing
app.UseRouting();

// CORS (MUST be after routing, before auth)
app.UseCors("AllowFrontend");

// Authorization
app.UseAuthorization();

// Controllers
app.MapControllers();

// Run app
app.Run();
