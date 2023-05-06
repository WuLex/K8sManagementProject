using k8s.Models;
using k8s;
using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApp.Controllers
{
    public class ReplicaSetController : Controller
    {
        private readonly Kubernetes _kubernetes;

        public ReplicaSetController(Kubernetes kubernetes)
        {
            _kubernetes = kubernetes;
        }
        //public IActionResult List()
        //{
        //    return View();
        //}

        //public IActionResult Add()
        //{
        //    return View();
        //}
        //public IActionResult Edit()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            var replicaSets = await _kubernetes.ListNamespacedReplicaSetAsync("default");
            return View(replicaSets.Items);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1ReplicaSet replicaSet)
        {
            await _kubernetes.CreateNamespacedReplicaSetAsync(replicaSet, "default");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string name)
        {
            var replicaSet = await _kubernetes.ReadNamespacedReplicaSetAsync(name, "default");
            return View(replicaSet);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(V1ReplicaSet replicaSet)
        {
            await _kubernetes.ReplaceNamespacedReplicaSetAsync(replicaSet, replicaSet.Metadata.Name, "default");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string name)
        {
            await _kubernetes.DeleteNamespacedReplicaSetAsync(name, "default");
            return RedirectToAction("Index");
        }
      
    }
}
