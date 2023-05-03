using k8s;
using k8s.Models;
using K8sManagementApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApp.Controllers
{
    public class ServiceController : Controller
    {
        private readonly ILogger<ServiceController> _logger;

        //使用自定义服务层(非必须)
        //private readonly IKubernetesService _kubernetesService;

        //k8s提供的客户端类
        private readonly Kubernetes _kubernetesClient;

        #region 直接实例化

        //private Kubernetes _client;
        //public ServiceController()
        //{
        //    // 初始化 KubernetesClient
        //    _client = new Kubernetes(new KubernetesClientConfiguration
        //    {
        //        // 设置 Kubernetes 集群的配置信息，例如 API 地址、认证信息等
        //        Host = "https://kubernetes.api.url",
        //        AccessToken = "your_access_token",
        //        ClientCertificateData = "your_client_certificate_data",
        //        ClientCertificateKeyData = "your_client_key_data",
        //        SkipTlsVerify = true
        //    });
        //}

        #endregion 直接实例化

        public ServiceController(ILogger<ServiceController> logger, Kubernetes kubernetesClient/*, IKubernetesService kubernetesService*/)
        {
            _logger = logger;
            //_kubernetesService = kubernetesService;
            _kubernetesClient = kubernetesClient;
        }

        // Service 列表页面
        public async Task<IActionResult> Index()
        {
            //var services = await _kubernetesService.GetServicesAsync();
            var services = await _kubernetesClient.ListServiceForAllNamespacesAsync();
            return View(services.Items);
        }

        // 创建 Service 页面
        public IActionResult Create()
        {
            return View();
        }

        // 提交创建 Service 请求
        [HttpPost]
        public async Task<IActionResult> Create(V1Service service)
        {
            if (ModelState.IsValid)
            {
                await _kubernetesClient.CreateNamespacedServiceAsync(service, service.Namespace());
                //await _kubernetesService.CreateServiceAsync(service);
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // 编辑 Service 页面
        public async Task<IActionResult> Edit(string name, string namespaceProperty)
        {
            var service = await _kubernetesClient.ReadNamespacedServiceAsync(name, namespaceProperty);
            //var service = await _kubernetesService.GetServiceAsync(name, ns);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // 提交编辑 Service 请求
        [HttpPost]
        public async Task<IActionResult> Edit(V1Service service)
        {
            if (ModelState.IsValid)
            {
                await _kubernetesClient.ReplaceNamespacedServiceAsync(service, service.Name(), service.Namespace());
                //await _kubernetesService.UpdateServiceAsync(service);
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // 删除 Service
        [HttpPost]
        public async Task<IActionResult> Delete(string name, string namespaceProperty)
        {
            await _kubernetesClient.DeleteNamespacedServiceAsync(name, namespaceProperty);
            //await _kubernetesService.DeleteServiceAsync(name, namespaceProperty);
            return RedirectToAction("Index");
        }
    }
}