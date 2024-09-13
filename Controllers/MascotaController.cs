using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using app_mascota.Data;
using app_mascota.Models;
using app_mascota.ViewModel;

namespace app_mascota.Controllers
{
    public class MascotaController : Controller
    {
        private readonly ILogger<MascotaController> _logger;
        private readonly ApplicationDbContext _context;

        public MascotaController(ILogger<MascotaController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var mismascotas = _context.DataMascota.ToList();
            var viewModel = new MascotaViewModel
            {
                FormularioMascota = new Mascota(),
                ListaMascota = mismascotas
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Enviar(MascotaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var mascota = new Mascota
                {
                    Nombre = viewModel.FormularioMascota.Nombre,
                    Raza = viewModel.FormularioMascota.Raza,
                    Color = viewModel.FormularioMascota.Color,
                    FechaNacimiento = viewModel.FormularioMascota.FechaNacimiento.ToUniversalTime() // Convertir a UTC
                };

                _context.Add(mascota);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(nameof(Index), viewModel);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var mascota = _context.DataMascota.Find(id);
            if (mascota == null)
            {
                return NotFound();
            }

            var viewModel = new MascotaViewModel
            {
                FormularioMascota = mascota,
                ListaMascota = _context.DataMascota.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(MascotaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var mascota = _context.DataMascota.Find(viewModel.FormularioMascota.Id);
                if (mascota == null)
                {
                    return NotFound();
                }

                mascota.Nombre = viewModel.FormularioMascota.Nombre;
                mascota.Raza = viewModel.FormularioMascota.Raza;
                mascota.Color = viewModel.FormularioMascota.Color;
                mascota.FechaNacimiento = viewModel.FormularioMascota.FechaNacimiento.ToUniversalTime(); // Convertir a UTC

                _context.Update(mascota);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var mascota = _context.DataMascota.Find(id);
            if (mascota == null)
            {
                return NotFound();
            }

            return View(mascota);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(long id)
        {
            var mascota = _context.DataMascota.Find(id);
            if (mascota != null)
            {
                _context.DataMascota.Remove(mascota);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
