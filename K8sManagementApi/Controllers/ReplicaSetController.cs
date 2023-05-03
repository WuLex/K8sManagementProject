using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeploymentController : ControllerBase
    {
        private readonly Kubernetes _client;

        public DeploymentController(Kubernetes client)
        {
            _client = client;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var deployment = await _client.ReadNamespacedDeploymentAsync(name, "default");
            return Ok(deployment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1Deployment deployment)
        {
            var result = await _client.CreateNamespacedDeploymentAsync(deployment, "default");
            return Ok(result);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name, V1Deployment deployment)
        {
            var result = await _client.ReplaceNamespacedDeploymentAsync(deployment, name, "default");
            return Ok(result);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await _client.DeleteNamespacedDeploymentAsync(name, "default");
            return Ok();
        }
    }
}
