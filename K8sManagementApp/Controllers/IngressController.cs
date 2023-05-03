using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace K8sManagementApp.Controllers
{
    // IngressController MVC
    [Authorize(Roles = "admin")]
    public class IngressController : Controller
    {
        private readonly Kubernetes _kubernetes;

        public IngressController(Kubernetes kubernetes)
        {
            _kubernetes = kubernetes;
        }

        public async Task<IActionResult> Index()
        {
            var ingresses = await _kubernetes.ListNamespacedIngressAsync("default");
            return View(ingresses.Items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1Ingress ingress)
        {
            await _kubernetes.CreateNamespacedIngressAsync(ingress, "default");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string name)
        {
            var ingress = await _kubernetes.ReadNamespacedIngressAsync(name, "default");
            return View(ingress);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string name, V1Ingress ingress)
        {
            await _kubernetes.ReplaceNamespacedIngressAsync(ingress, name, "default");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string name)
        {
            await _kubernetes.DeleteNamespacedIngressAsync(name, "default");
            return RedirectToAction(nameof(Index));
        }
    }
}
