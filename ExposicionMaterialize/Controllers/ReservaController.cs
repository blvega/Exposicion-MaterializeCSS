
using ExposicionMaterialize.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExposicionMaterialize.Controllers
{
    public class ReservaController : Controller
    {
        // GET: Reserva
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult createReserva(string name)
        {

            TempData["mssg4"] = name;
            ViewBag.mssg4 = TempData["mssg4"] as string;


            string correo = TempData["correo"] as string;
            try {
                using (var client = new HttpClient())
                {
                    var task = Task.Run(async () =>
                    {
                        return await client.GetAsync("https://tiusr7pl.cuc-carrera-ti.ac.cr/Api/Clientes/Para/?usuario=" + correo);
                    }
                    );
                    HttpResponseMessage message = task.Result;
                    if (message.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {

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
                            var ced = mens;
                            if (ced != "")
                            {
                                var ced2 = ced.Split('"');


                                var ced3 = ced2[3];


                                TempData["cli"] = ced3;
                                ViewBag.cli = TempData["cli"] as string;
                            }
                            else
                            {
                                TempData["cli"] = "";
                                ViewBag.cli = TempData["cli"] as string;
                            }


                            //TempData["idCli"] = estudiantes.ToString();



                        }
                        else
                        {

                        }

                    }

                }
            } catch (Exception es) { }
          
          
            return View();

        }

        [HttpPost]
        public ActionResult createReserva(Reserva reserva)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://tiusr7pl.cuc-carrera-ti.ac.cr/Api/Reservas");


                //HTTP POST
                var postTask = client.PostAsJsonAsync<Reserva>("Reservas", reserva);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["mssg3"] = "¡Reserva realizada con éxito!";
                    return RedirectToAction("Principal", new RouteValueDictionary(new { controller = "Cliente"}));
                }
                else {

                ModelState.AddModelError(string.Empty, "Error al realizar la reserva");
                }
            }

           

            return View(reserva);
        }
    }
}