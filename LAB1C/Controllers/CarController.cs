
using LAB1C.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LAB1C.Controllers
{
    public class CarController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7039/api");

        private readonly HttpClient _client;
        
        public CarController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //HTTP client
            IEnumerable<Car>cars=Enumerable.Empty<Car>();
            HttpResponseMessage response=_client.GetAsync(_client.BaseAddress+"/Cars").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                cars = JsonConvert.DeserializeObject<IEnumerable<Car>>(data);
            }
            return View(cars);
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Create(Car carToAdd)
        {
            if (ModelState.IsValid)
            {
                //HTTP POST
                var postTask = _client.PostAsJsonAsync<Car>(_client.BaseAddress + "/Cars", carToAdd);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(carToAdd);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Car carByID = new Car();

            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Cars/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                carByID = JsonConvert.DeserializeObject<Car>(data);

                return View(carByID);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult Edit(Car updatedCar)
        {
            if (ModelState.IsValid)
            {                
                var putTask = _client.PutAsJsonAsync<Car>(_client.BaseAddress + "/Cars/"+updatedCar.SerialNumber, updatedCar);
                putTask.Wait();

                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(updatedCar);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Car carByID = new Car();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Cars/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                carByID = JsonConvert.DeserializeObject<Car>(data);

                return View(carByID);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult DeletePOST(int? SerialNumber)
        {
            
            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/Cars/"+ SerialNumber).Result;
            
            if ((int)response.StatusCode==StatusCodes.Status400BadRequest)
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }

    }
}
