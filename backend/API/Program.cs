using API;
using API.Data;
using API.DTOs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

app.UseCors("AngularCors");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        await SeedData.SeedUsersData(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An Error Occured While Migration");
        throw;
    }
}

app.Run();


// [
//     { "Id": "14629847-905a-4a0e-9abe-80b61655c5cb", "Name": "Maged", "Email": "maged922001@gmail.com" },
//     { "Id": "56bf46a4-02b8-4693-a0f5-0a95e2218bdc", "Name": "Tarek", "Email": "tarek@gmail.com" },
//     { "Id": "8f30bedc-47dd-4286-8950-73d8a68e5d41", "Name": "Ali", "Email": "ali@gmail.com" }
// ]