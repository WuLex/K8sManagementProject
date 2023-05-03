using Microsoft.AspNetCore.Mvc;

namespace K8sManagementApp.Controllers
{
    public class DeploymentController : Controller
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
