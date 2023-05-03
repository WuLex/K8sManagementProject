using k8s.Models;
using k8s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace K8sManagementApp.Controllers
{
    // NetworkController MVC
    [Authorize(Roles = "admin")]
    public class NetworkController : Controller
    {
        private readonly Kubernetes _kubernetes;

        public NetworkController(Kubernetes kubernetes)
        {
            _kubernetes = kubernetes;
        }

        public async Task<IActionResult> Index()
        {
            var networkPolicies = await _kubernetes.ListNamespacedNetworkPolicyAsync("default");
            return View(networkPolicies.Items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1NetworkPolicy networkPolicy)
        {
            await _kubernetes.CreateNamespacedNetworkPolicyAsync(networkPolicy, "default");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string name)
        {
            var networkPolicy = await _kubernetes.ReadNamespacedNetworkPolicyAsync(name, "default");
            return View(networkPolicy);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string name, V1NetworkPolicy networkPolicy)
        {
            await _kubernetes.ReplaceNamespacedNetworkPolicyAsync(networkPolicy, name, "default");
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string name)
        {
            await _kubernetes.DeleteNamespacedNetworkPolicyAsync(name, "default");
            return RedirectToAction(nameof(Index));
        }
    }
}
