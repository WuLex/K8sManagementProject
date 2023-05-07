using k8s;
using k8s.Models;
using K8sManagementApp.Models;
using K8sManagementApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace K8sManagementApp.Controllers
{
    public class NetworkPolicyController : Controller
    {
        private readonly Kubernetes _client;

        public NetworkPolicyController(Kubernetes client)
        {
            _client = client;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var networkPolicies = await _client.ListNamespacedNetworkPolicyAsync("default");
            var networkPolicies = await _client.ListNetworkPolicyForAllNamespacesAsync();
            return View(networkPolicies.Items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NetworkPolicyViewModel model)
        {
            var networkPolicy = new V1NetworkPolicy
            {
                Metadata = new V1ObjectMeta
                {
                    Name = model.Name,
                    Labels = model.Labels
                },
                Spec = new V1NetworkPolicySpec
                {
                    PodSelector = new V1LabelSelector
                    {
                        MatchLabels = model.Labels
                    },
                    Ingress = model.Rules.Select(rule => new V1NetworkPolicyIngressRule
                    {
                        Ports = rule.Ports.Select(port => new V1NetworkPolicyPort
                        {
                            Port = port
                        }).ToList(),
                        FromProperty = rule.Sources.Select(source => new V1NetworkPolicyPeer
                        {
                            PodSelector = new V1LabelSelector
                            {
                                MatchLabels = new Dictionary<string, string>
                            {
                                { "app", source }
                            }
                            }
                        }).ToList()
                    }).ToList()
                }
            };

            await _client.CreateNamespacedNetworkPolicyAsync(networkPolicy, "default");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string name)
        {
            var networkPolicy = await _client.ReadNamespacedNetworkPolicyAsync(name, "default");

            var model = new NetworkPolicyViewModel
            {
                Name = networkPolicy.Metadata.Name,
                Labels = (Dictionary<string, string>)networkPolicy.Metadata.Labels,
                Rules = networkPolicy.Spec.Ingress.Select(rule => new NetworkPolicyRule
                {
                    Ports = rule.Ports.Select(port => port.Port.ToString()).ToList(),
                    Sources = rule.FromProperty.Select(peer => peer.PodSelector.MatchLabels["app"]).ToList(),
                    //Destinations = rule.To.Select(peer => peer.PodSelector.MatchLabels["app"]).ToList()
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string name, NetworkPolicyViewModel model)
        {
            var networkPolicy = await _client.ReadNamespacedNetworkPolicyAsync(name, "default");

            networkPolicy.Metadata.Labels = model.Labels;
            networkPolicy.Spec.PodSelector.MatchLabels = model.Labels;
            networkPolicy.Spec.Ingress = model.Rules.Select(rule => new V1NetworkPolicyIngressRule
            {
                Ports = rule.Ports.Select(port => new V1NetworkPolicyPort
                {
                    Port = int.Parse(port)
                }).ToList(),
                FromProperty = rule.Sources.Select(source => new V1NetworkPolicyPeer
                {
                    PodSelector = new V1LabelSelector
                    {
                        MatchLabels = new Dictionary<string, string>
                {
                    { "app", source }
                }
                    }
                }).ToList()
                ,
                //To = rule.Destinations.Select(destination => new V1NetworkPolicyPeer
                //{
                //    PodSelector = new V1LabelSelector
                //    {
                //        MatchLabels = new Dictionary<string, string>
                //        {
                //            { "app", destination }
                //        }
                //    }
                //}).ToList()
            }).ToList();

            await _client.ReplaceNamespacedNetworkPolicyAsync(networkPolicy, name, "default");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string name)
        {
            await _client.DeleteNamespacedNetworkPolicyAsync(name, "default");

            return RedirectToAction("Index");
        }
    }
}