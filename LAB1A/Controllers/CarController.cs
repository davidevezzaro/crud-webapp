using LAB1.Data;
using LAB1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LAB1.Controllers
{
    public class CarController : Controller
    {
        //dependency injection example since the connection is already made in the program.cs
        //DEPENDENCY INJECTION HERE!
        private readonly ApplicationDbContext _db;
        public CarController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Car> cars = _db.Cars;

            return View(cars);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//to prevent cross site request forgery 
        public IActionResult Create(Car obj)
        {
            if (ModelState.IsValid)
            {
                _db.Cars.Add(obj);
                //this instruction send it to the db
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(obj);
            
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var carFromDb = _db.Cars.Find(id);
            if (carFromDb == null)
            {
                return NotFound();
            }
            return View(carFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//to prevent cross site request forgery 
        public IActionResult Edit(Car obj)
        {
			if (ModelState.IsValid)
            {
                _db.Cars.Update(obj);
                //this instruction send it to the db
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
                return View(obj);

        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var carFromDb = _db.Cars.Find(id);
            if (carFromDb == null)
            {
                return NotFound();
            }
            return View(carFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]//to prevent cross site request forgery 
        public IActionResult DeletePOST(int? SerialNumber)
        {
            var obj = _db.Cars.Find(SerialNumber);
            if (obj == null) {
                return NotFound();
            }
            _db.Cars.Remove(obj);
            //this instruction send it to the db
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
