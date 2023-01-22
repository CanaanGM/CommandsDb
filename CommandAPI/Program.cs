using CommandAPI.Data;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICommandAPIRepo, CommandAPIRepo>();

builder.Services.AddControllers()
    //.AddJsonOptions(
    //opt => opt.JsonSerializerOptions.Converters.Add(new Jsonconvert))
    ;


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

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


app.Run();
