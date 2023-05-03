using k8s;
using K8sManagementApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCors(c =>
{
    c.AddPolicy("MyAllowAll", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add Kubernetes client
builder.Services.AddSingleton<Kubernetes>(sp => {
    var config = new KubernetesClientConfiguration { Host = "http://192.168.59.1:8001/" };
    return new Kubernetes(config);
});


//// Configure Kubernetes client options
//var configuration = builder.Configuration;
//builder.Services.Configure<KubernetesClientOptions>(configuration.GetSection("KubernetesClient"));

//builder.Services.AddSingleton<IKubernetes>(sp =>
//{
//    var options = sp.GetService<IOptions<KubernetesClientOptions>>().Value;
//    var config = KubernetesClientConfiguration.BuildConfigFromConfigFile(options.KubeConfigPath);
//    return new Kubernetes(config);
//});




var app = builder.Build();


app.UseCors("MyAllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
