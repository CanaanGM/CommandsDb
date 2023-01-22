using CommandAPI.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICommandAPIRepo, CommandAPIRepo>();

builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

string posgres_user, posgress_password;
posgres_user = builder.Configuration["POSGRES_USER"];
posgress_password = builder.Configuration["POSGRES_PASSWORD"];

builder.Services.AddDbContext<CommandContext>(
    opt => opt.UseNpgsql(
        $"{builder.Configuration.GetConnectionString("postgres")}User Id={posgres_user};Password={posgress_password}")
    );

builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();


if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}


// Apply migrations Automatically ? ?
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<CommandContext>();
    await context.Database.MigrateAsync();

}
catch (Exception ex)
{
    // add logger lator
    Console.WriteLine( ex );
    //Environment.Exit( -1 );
	
}


app.Run();
