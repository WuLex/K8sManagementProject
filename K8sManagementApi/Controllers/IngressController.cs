using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngressController : ControllerBase
    {
        private readonly Kubernetes _kubernetes;

        public IngressController(Kubernetes kubernetes)
        {
            _kubernetes = kubernetes;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<V1Ingress>>> Get()
        {
            var ingresses = await _kubernetes.ListNamespacedIngressAsync("default");
            return Ok(ingresses.Items);
        }

        [HttpPost]
        public async Task<ActionResult<V1Ingress>> Create(V1Ingress ingress)
        {
            var result = await _kubernetes.CreateNamespacedIngressAsync(ingress, "default");
            return CreatedAtAction(nameof(Get), new { id = result.Metadata.Name }, result);
        }

        [HttpPut("{name}")]
        public async Task<ActionResult<V1Ingress>> Update(string name, V1Ingress ingress)
        {
            var result = await _kubernetes.ReplaceNamespacedIngressAsync(ingress, name, "default");
            return Ok(result);
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult> Delete(string name)
        {
            await _kubernetes.DeleteNamespacedIngressAsync(name, "default");
            return NoContent();
        }
    }
}
