using k8s;
using k8s.Models;
using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReplicaSetController : ControllerBase
    {
        private readonly Kubernetes _client;

        public ReplicaSetController(Kubernetes client)
        {
            _client = client;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var replicaSet = await _client.ReadNamespacedReplicaSetAsync(name, "default");
            return Ok(replicaSet);
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1ReplicaSet replicaSet)
        {
            var result = await _client.CreateNamespacedReplicaSetAsync(replicaSet, "default");
            return Ok(result);
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name, V1ReplicaSet replicaSet)
        {
            var result = await _client.ReplaceNamespacedReplicaSetAsync(replicaSet, name, "default");
            return Ok(result);
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await _client.DeleteNamespacedReplicaSetAsync(name, "default");
            return Ok();
        }
    }
}
