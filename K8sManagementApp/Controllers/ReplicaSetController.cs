using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApp.Controllers
{
    public class ReplicaSetController : Controller
    {
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
    }
}
