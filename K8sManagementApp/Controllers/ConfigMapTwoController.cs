using k8s.Models;
using k8s;
using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApp.Controllers
{
    public class ConfigMapTwoController : Controller
    {
        private readonly Kubernetes _kubernetes;

        public ConfigMapTwoController(Kubernetes kubernetes)
        {
            _kubernetes = kubernetes;
        }

        // GET: ConfigMap
        public async Task<IActionResult> Index()
        {
            var configMaps = await _kubernetes.ListNamespacedConfigMapAsync("default");
            return View(configMaps.Items);
        }

        // GET: ConfigMap/Create
        public IActionResult Create()
        {
            return View();
        }

        
        public async Task<IActionResult> Details(string name)
        {
            var configMap = await _kubernetes.ReadNamespacedConfigMapAsync(name, "default");
            return View(configMap);
        }

        // POST: ConfigMap/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Metadata.Name,Data")] V1ConfigMap configMap)
        {
            if (ModelState.IsValid)
            {
                await _kubernetes.CreateNamespacedConfigMapAsync(configMap, "default");
                return RedirectToAction(nameof(Index));
            }
            return View(configMap);
        }

        // GET: ConfigMap/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var configMap = await _kubernetes.ReadNamespacedConfigMapAsync(id, "default");
            return View(configMap);
        }

        // POST: ConfigMap/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Metadata.Name,Data")] V1ConfigMap configMap)
        {
            if (id != configMap.Metadata.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _kubernetes.ReplaceNamespacedConfigMapAsync(configMap, id, "default");
                return RedirectToAction(nameof(Index));
            }

            return View(configMap);
        }

        // GET: ConfigMap/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var configMap = await _kubernetes.ReadNamespacedConfigMapAsync(id, "default");
            return View(configMap);
        }

        // POST: ConfigMap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _kubernetes.DeleteNamespacedConfigMapAsync(id, "default");
            return RedirectToAction(nameof(Index));
        }
    }
}
