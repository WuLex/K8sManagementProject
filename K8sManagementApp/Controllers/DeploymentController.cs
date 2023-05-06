using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApp.Controllers
{
    public class DeploymentController : Controller
    {
        private readonly Kubernetes _client;

        public DeploymentController(Kubernetes client)
        {
            _client = client;
        }

        public IActionResult List()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDeployments(string ns= "default")
        {
            var deployments = await _client.ListNamespacedDeploymentAsync(ns);
            return Json(new { code = 0, data = deployments.Items });
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeployment([FromBody] V1Deployment deployment)
        {
            await _client.CreateNamespacedDeploymentAsync(deployment, "default");
            return Json(new { success = true });
        }

        [HttpGet]
        public async Task<IActionResult> EditDeployment(string name,string ns = "default")
        {
            var deployment = await _client.ReadNamespacedDeploymentAsync(name, ns);
            return View(deployment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeployment([FromBody] V1Deployment deployment)
        {
            await _client.ReplaceNamespacedDeploymentAsync(deployment, deployment.Metadata.Name, "default");
            return Json(new { success = true });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeployment(string name, string ns = "default")
        {
            var deleteOptions = new V1DeleteOptions
            {
                PropagationPolicy = "Foreground",
                GracePeriodSeconds = 5
            };
            await _client.DeleteNamespacedDeploymentAsync(name, ns, deleteOptions);
            return Json(new { success = true });
        }


    }
}
