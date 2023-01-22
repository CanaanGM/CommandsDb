using CommandAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICommandAPIRepo, CommandAPIRepo>();

builder.Services.AddControllers();


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
