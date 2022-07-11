using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Proyecto3C.DTOs;
using Proyecto3C.Entities;
using ProyectoVista3C.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;

namespace ProyectoVista3C.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
   
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:7088/api/clientes");
            var clientesList = JsonConvert.DeserializeObject<List<Cliente>>(json);

            return View(clientesList);

        }

        public async Task<ActionResult> Privacy()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync("https://localhost:7088/api/contacto");
            var contactoList = JsonConvert.DeserializeObject<List<Contacto>>(json);

            return View(contactoList);
        }

        public async Task<ActionResult> ClienteId(int idCliente)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"https://localhost:7088/api/clientes/{idCliente}");
            //var json_resonse = await json.Content.ReadAsStringAsync();
            var cliente = JsonConvert.DeserializeObject<Cliente>(json);

            return View(cliente);
        }

        public async Task<ActionResult> ContactoId(int idContacto)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"https://localhost:7088/api/contacto/{idContacto}");
            var contacto = JsonConvert.DeserializeObject<Contacto>(json);

            return View(contacto);
        }

       

        public async Task<ActionResult> BorrarClienteId(int idCliente)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.DeleteAsync($"https://localhost:7088/api/clientes/{idCliente}");

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> BorrarContactoId(int idContacto)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.DeleteAsync($"https://localhost:7088/api/contacto/{idContacto}");

            return RedirectToAction("Privacy");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}