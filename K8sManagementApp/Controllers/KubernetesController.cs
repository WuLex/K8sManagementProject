using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApp.Controllers
{
    public class KubernetesController : Controller
    {
        private readonly Kubernetes _kubernetesClient;

        public KubernetesController(Kubernetes kubernetesClient)
        {
            _kubernetesClient = kubernetesClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        // 显示 Pod 列表
        public async Task<IActionResult> PodList()
        {
            var pods = await _kubernetesClient.ListPodForAllNamespacesAsync();
            return View(pods.Items);
        }

        // 显示 Service 列表
        public async Task<IActionResult> ServiceList()
        {
            var services = await _kubernetesClient.ListServiceForAllNamespacesAsync();
            return View(services.Items);
        }

        // 创建 Pod
        [HttpGet]
        public IActionResult CreatePod()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePod(V1Pod pod)
        {
            await _kubernetesClient.CreateNamespacedPodAsync(pod, pod.Namespace());
            return RedirectToAction("PodList");
        }

        // 创建 Service
        [HttpGet]
        public IActionResult CreateService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateService(V1Service service)
        {
            await _kubernetesClient.CreateNamespacedServiceAsync(service, service.Namespace());
            return RedirectToAction("ServiceList");
        }

        // 更新 Pod
        [HttpGet]
        public async Task<IActionResult> UpdatePod(string name, string ns)
        {
            var pod = await _kubernetesClient.ReadNamespacedPodAsync(name, ns);
            return View(pod);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePod(V1Pod pod)
        {
            await _kubernetesClient.ReplaceNamespacedPodAsync(pod, pod.Metadata.Name, pod.Namespace());
            return RedirectToAction("PodList");
        }

        // 更新 Service
        [HttpGet]
        public async Task<IActionResult> UpdateService(string name, string ns)
        {
            var service = await _kubernetesClient.ReadNamespacedServiceAsync(name, ns);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateService(V1Service service)
        {
            if (ModelState.IsValid)
            {
                await _kubernetesClient.ReplaceNamespacedServiceAsync(service, service.Name(), service.Namespace());
                return RedirectToAction("ServiceList");
            }

            return View(service);
        }

        // 删除 Pod
        [HttpGet]
        public async Task<IActionResult> DeletePod(string name, string ns)
        {
            await _kubernetesClient.DeleteNamespacedPodAsync(name, ns);
            return RedirectToAction("PodList");
        }

        // 删除 Service
        [HttpGet]
        public async Task<IActionResult> DeleteService(string name, string ns)
        {
            await _kubernetesClient.DeleteNamespacedServiceAsync(name, ns);
            return RedirectToAction("ServiceList");
        }
    }
}