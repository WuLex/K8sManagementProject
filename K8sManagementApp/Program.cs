using k8s;
using K8sManagementApp.Models;
using K8sManagementApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment;
var config = builder.Configuration;

#region 配置

//// 读取配置项
//IConfiguration configuration = config.GetSection("Kubernetes");
//builder.Services.Configure<KubernetesConfig>(configuration);

//builder.Services.AddScoped<IKubernetesService, KubernetesService>();

//builder.Services.AddKubernetesOperator(); // config / settings here
// 注册 IKubernetesService 接口的实现类 KubernetesService
//var operatorBuilder = services.AddKubernetesOperator(configure => {
//configure.Name = "platform-operator";
//configure.WatcherHttpTimeout = 60 * 60; // TODO: temporarily workaround for next issue: https://github.com/buehler/dotnet-operator-sdk/issues/477. Remove after the fix
//});

//if (this.Environment.IsDevelopment())
//{
//operatorBuilder.AddWebhookLocaltunnel();
//}

// Register Kubernetes clients

//if (true)
//{
//    builder.Services.TryAddSingleton<IKubernetes>(sp => new Kubernetes(KubernetesClientConfiguration.InClusterConfig()));
//    //builder.Services.TryAddSingleton<IKubernetesClient>(sp => new KubernetesClient(KubernetesClientConfiguration.InClusterConfig()));
//}
//else
//{
//    builder.Services.TryAddSingleton<IKubernetes>(sp => new Kubernetes(KubernetesClientConfiguration.BuildConfigFromConfigFile()));
//    //builder.Services.TryAddSingleton<IKubernetesClient>(sp => new KubernetesClient(KubernetesClientConfiguration.BuildConfigFromConfigFile()));
//} 
#endregion
// 注册 KubernetesService
//builder.Services.AddSingleton<Kubernetes>();
builder.Services.AddScoped<K8sService>();
builder.Services.AddSingleton<Kubernetes>(sp => {
    //var config = KubernetesClientConfiguration.BuildConfigFromConfigFile("path/to/kubeconfig");
    var config = new KubernetesClientConfiguration { Host = "http://192.168.59.1:8001/" };
    return new Kubernetes(config);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
