using k8s.Models;
using k8s;
using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApi.Controllers
{
    // NetworkController API
    [ApiController]
    [Route("api/[controller]")]
    public class NetworkController : ControllerBase
    {
        private readonly Kubernetes _kubernetes;

        public NetworkController(Kubernetes kubernetes)
        {
            _kubernetes = kubernetes;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1NetworkPolicy>>> Get()
        {
            var networkPolicies = await _kubernetes.ListNamespacedNetworkPolicyAsync("default");
            return Ok(networkPolicies.Items);
        }

        [HttpPost]
        public async Task<ActionResult<V1NetworkPolicy>> Create(V1NetworkPolicy networkPolicy)
        {
            var result = await _kubernetes.CreateNamespacedNetworkPolicyAsync(networkPolicy, "default");
            return CreatedAtAction(nameof(Get), new { id = result.Metadata.Name }, result);
        }

        [HttpPut("{name}")]
        public async Task<ActionResult<V1NetworkPolicy>> Update(string name, V1NetworkPolicy networkPolicy)
        {
            var result = await _kubernetes.ReplaceNamespacedNetworkPolicyAsync(networkPolicy, name, "default");
            return Ok(result);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name)
        {
            await _kubernetes.DeleteNamespacedNetworkPolicyAsync(name, "default");
            return NoContent();
        }
    }
}
