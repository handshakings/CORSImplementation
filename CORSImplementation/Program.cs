var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://www.techilm.com", "https://www.youtube.com").AllowAnyHeader()
            .WithHeaders("Access-Control-Allow-Headers", "Content-Type");
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Add cors middleware
app.UseCors();
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Response from CORS program.cs file, CORS Implementation");
});
app.Run();
