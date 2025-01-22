using novaTentativa.Server;

var builder = WebApplication.CreateBuilder(args);

//cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("Access-Control-Allow-Origin");
    });
});

//controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//###############################################################################################################################
//ao terminar a controller, sempre lembrar que ? peciso referenciar aqui
builder.Services.AddHttpClient<Cantor_Server>(client =>
{
    client.BaseAddress = new Uri("https://guilhermeonrails.github.io/");
});

builder.Services.AddHttpClient<Cantor_Server>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
