using ExposicionMaterialize.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ExposicionMaterialize.Controllers
{
    public class ClienteController : Controller
    {

        private const string URL = "https://tiusr7pl.cuc-carrera-ti.ac.cr/Api/Clientes";
        // GET: Cliente
        public ActionResult Index()
        {


            List<Cliente> estudiantes;
            using (var client = new HttpClient())
            {
                var task = Task.Run(async () =>
                {
                    return await client.GetAsync("https://tiusr7pl.cuc-carrera-ti.ac.cr/Api/Clientes");
                }
                );
                HttpResponseMessage message = task.Result;
                if (message.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    estudiantes = null;
                }
                else
                {
                    var task2 = Task.Run(async () =>
                    {
                        return await message.Content.ReadAsStringAsync();
                    });
                    string mens = task2.Result;
                    if (!string.IsNullOrEmpty(mens))
                    {
                        estudiantes = JsonConvert.DeserializeObject<List<Cliente>>(mens);
                    }
                    else
                    {
                        estudiantes = null;
                    }

                }
                return View(estudiantes);
            }
        }


        public ActionResult create()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Login log)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://tiusr7pl.cuc-carrera-ti.ac.cr/Api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Login>("Clientes/Login", log);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["mssg"] = "¡Bienvenido!";

                    TempData["correo"] = log.Usuario.ToString();
                    return RedirectToAction("Principal");
                }
            }

            ModelState.AddModelError(string.Empty, "Correo y/o contraseña incorrecto");

            return View(log);
        }

        public ActionResult Welcome()
        {
            ViewBag.mssg2 = TempData["mssg2"] as string;
            return View();
        }

        public ActionResult Principal()
        {
           
            ViewBag.mssg = TempData["mssg"] as string;
            ViewBag.mssg3 = TempData["mssg3"] as string;
            return View();
        }

        [HttpPost]
        public ActionResult create(Cliente cliente)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://tiusr7pl.cuc-carrera-ti.ac.cr/Api/Clientes");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Cliente>("Clientes", cliente);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["mssg2"] = "¡Registro exitoso!";
                    return RedirectToAction("Welcome");
                }
                else {
                    ModelState.AddModelError(string.Empty, "Error, la identificación ingresada ya existe.");
                }
            }

      

            return View(cliente);
        }


     
       
        

    }
}
        



    
