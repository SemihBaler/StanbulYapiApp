using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StanbulYapiApp_WEB.Models;
using System.Diagnostics;

namespace StanbulYapiApp_WEB.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
     
        public async Task<IActionResult> Index()
        {
            return  View();
        }

       
    }
}