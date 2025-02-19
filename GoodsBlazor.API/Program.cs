using GoodsBlazor.API.Extensions;
using GoodsBlazor.API.Middleware;
using GoodsBlazor.BLL.Extensions;
using GoodsBlazor.DAL.Extensions;
using GoodsBlazor.DAL.Seeders;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddAuthorization();

    builder.AddPresentation();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);

    //builder.Services.AddCors(options =>
    //{
    //    options.AddDefaultPolicy(policy =>
    //    {
    //        policy.WithOrigins("https://localhost:7221", "https://localhost:7053")
    //              .AllowAnyMethod()
    //              .AllowAnyHeader()
    //              .AllowCredentials();
    //    });
    //});

     builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();

    await seeder.Seed();

    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application startup failed");
}
finally
{
    Log.CloseAndFlush();
}