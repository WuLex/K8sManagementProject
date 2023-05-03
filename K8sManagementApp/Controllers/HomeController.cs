using k8s;
using K8sManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace K8sManagementApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KubernetesConfig _config;
        private readonly Kubernetes _kubernetesClient;
        public HomeController(ILogger<HomeController> logger, IOptions<KubernetesConfig> config)
        {
            _logger = logger;
            _config = config.Value;

            //K8s连接认证数据配置
            var k8sConfig = new KubernetesClientConfiguration
            {
                Host = _config.ApiServerUrl,
                AccessToken = _config.AccessToken,
                SkipTlsVerify = _config.SkipTlsVerify,
                Namespace = _config.Namespace,
                ClientCertificateData = _config.ClientCertificateData,
                ClientCertificateKeyData = _config.ClientKeyData
            };

            _kubernetesClient = new Kubernetes(k8sConfig);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}