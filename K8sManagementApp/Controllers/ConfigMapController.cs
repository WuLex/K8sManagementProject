using k8s;
using k8s.Models;
using K8sManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApp.Controllers
{
    public class ConfigMapController : Controller
    {
        private readonly Kubernetes _kubernetes;

        public ConfigMapController(Kubernetes kubernetes)
        {
            _kubernetes = kubernetes;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<PageDataResult<V1ConfigMap>> GetList(int page = 1, int limit = 10)
        {
            int offsetNum = 0;
            offsetNum = (page - 1) * limit;

            var configMaps = await _kubernetes.ListNamespacedConfigMapAsync("default");

            return new PageDataResult<V1ConfigMap>()
            {
                Msg = "success",
                Code = 0,
                Count = configMaps.Items.Count(),
                Data = configMaps.Items.Take(limit).Skip(offsetNum).ToList()
            };
            //return Ok(configMaps.Items.Take(limit).Skip(offsetNum));
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var configMap = await _kubernetes.ReadNamespacedConfigMapAsync(name, "default");
            return Ok(configMap);
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1ConfigMap configMap)
        {
            await _kubernetes.CreateNamespacedConfigMapAsync(configMap, "default");
            return Ok();
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> Update(string name, V1ConfigMap configMap)
        {
            await _kubernetes.ReplaceNamespacedConfigMapAsync(configMap, name, configMap.Namespace());
            return Ok();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await _kubernetes.DeleteNamespacedConfigMapAsync(name, "default");
            return Ok();
        }
    }
}