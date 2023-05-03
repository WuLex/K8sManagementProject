using k8s;
using k8s.Models;
using K8sManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace K8sManagementApp.Controllers
{
    public class NodeController : Controller
    {
        private readonly Kubernetes _kubernetes;

        public NodeController(Kubernetes kubernetes)
        {
            _kubernetes = kubernetes;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

       [HttpGet]
        public IActionResult CreateNode()
        {
            return View();
        }


        [HttpGet]
        public async Task<PageDataResult<V1Node>> GetList(int page = 1, int limit = 10)
        {
            int offsetNum = 0;
            offsetNum = (page - 1) * limit;
            var nodes = await _kubernetes.ListNodeAsync();
            return new PageDataResult<V1Node>()
            {
                Msg = "success",
                Code = 0,
                Count = nodes.Items.Count(),
                Data = nodes.Items.Take(limit).Skip(offsetNum).ToList()
            };
            //return Ok(nodes.Items);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var node = await _kubernetes.ReadNodeAsync(name);
            return Ok(node);
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1Node node)
        {
            await _kubernetes.CreateNodeAsync(node);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(string name, V1Node node)
        {
            await _kubernetes.ReplaceNodeAsync(node,name);
            return Ok();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            await _kubernetes.DeleteNodeAsync(name);
            return Ok();
        }
    }
}
