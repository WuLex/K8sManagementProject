using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace K8sManagementApp.Controllers
{
    //[Authorize(Roles = "admin")]
    public class IngressController : Controller
    {
        private readonly Kubernetes _kubernetes;

        public IngressController(Kubernetes kubernetes)
        {
            _kubernetes = kubernetes;
        }

        public async Task<IActionResult> Index()
        {
            //var ingresses = await _kubernetes.ListNamespacedIngressAsync("default");
            var ingresses = await _kubernetes.ListIngressForAllNamespacesAsync();
            return View(ingresses.Items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1Ingress ingress, string ns = "default")
        {
            await _kubernetes.CreateNamespacedIngressAsync(ingress, ns);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string name, string ns = "default")
        {
            var ingress = await _kubernetes.ReadNamespacedIngressAsync(name, ns);
            return View(ingress);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string name, V1Ingress ingress, string ns = "default")
        {
            await _kubernetes.ReplaceNamespacedIngressAsync(ingress, name, ns);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string name,string ns="default")
        {
            await _kubernetes.DeleteNamespacedIngressAsync(name, ns);
            return RedirectToAction(nameof(Index));
        }
    }
}
