using k8s;
using k8s.Models;
using K8sManagementApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApp.Controllers
{
    public class PodController : Controller
    {
        private readonly Kubernetes _kubernetesClient;
        private readonly IKubernetesService _kubernetesService;


        #region 直接实例化
        //private Kubernetes _client;

        //public PodController()
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
        #endregion
      
        //public PodController(IKubernetesService kubernetesService)
        //{
        //    _kubernetesService = kubernetesService;
        //}
        public PodController(Kubernetes kubernetesClient)
        {
            _kubernetesClient = kubernetesClient;
        }

        // 获取 Pod 列表
        public async Task<IActionResult> Index()
        {
            // 调用 KubernetesClient 获取 Pod 列表
            var podList = await _kubernetesClient.ListPodForAllNamespacesAsync();
            // 将 Pod 列表传递给视图
            return View(podList.Items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1Pod pod)
        {
            if (ModelState.IsValid)
            {
                // 调用 KubernetesClient 创建 Pod
                var createdPod = await _kubernetesClient.CreateNamespacedPodAsync(pod, pod.Namespace());
                // 重定向到 Pod 列表页面
                return RedirectToAction("Index");
            }
            return View(pod);
        }

        public async Task<IActionResult> Edit(string name, string ns)
        {
            var pod = await _kubernetesClient.ReadNamespacedPodAsync(name,ns);
            //var pod = (await _kubernetesClient.ListNamespacedPodAsync(ns)).Items.Select(x => x.Name() == name);
            return View(pod);
        }

        /// <summary>
        /// 更新 Pod
        /// </summary>
        /// <param name="pod"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(V1Pod pod)
        {
            if (ModelState.IsValid)
            {
                // 调用 KubernetesClient 更新 Pod
                var updatedPod = await _kubernetesClient.ReplaceNamespacedPodAsync(pod, pod.Metadata.Name, pod.Namespace());
                // 重定向到 Pod 列表页面
                return RedirectToAction("Index");
            }
            return View(pod);
        }

        /// <summary>
        ///  删除 Pod
        /// </summary>
        /// <param name="name"></param>
        /// <param name="ns">namespace</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string name, string ns)
        {
            // 调用 KubernetesClient 删除 Pod
            await _kubernetesClient.DeleteNamespacedPodAsync(name, ns);

            //await _kubernetesClient.DeleteNamespacedPodAsync(name, ns, new V1DeleteOptions());
            //_kubernetesClient.DeleteNamespacedPod(name, "your_namespace", new V1DeleteOptions());

            // 重定向到 Pod 列表页面
            return RedirectToAction("Index");
        }
    }
}