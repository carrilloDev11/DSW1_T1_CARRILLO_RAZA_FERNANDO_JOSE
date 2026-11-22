using Microsoft.AspNetCore.Mvc;
using DSW1_T1_CARRILLO_RAZA_FERNANDO_JOSE.Models;
using DSW1_T1_CARRILLO_RAZA_FERNANDO_JOSE.Services;

namespace DSW1_T1_CARRILLO_RAZA_FERNANDO_JOSE.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroService _libroService;

        public LibroController(ILibroService libroService)
        {
            _libroService = libroService;
        }

        public IActionResult Index(string? autor)
        {
            var libros = string.IsNullOrWhiteSpace(autor)
                ? _libroService.ListarLibros()
                : _libroService.ListarLibrosPorAutor(autor);

            ViewBag.Autor = autor;
            ViewBag.Mensaje = TempData["Mensaje"];

            return View(libros);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(Libro libro)
        {
            if (!ModelState.IsValid)
                return View(libro);

            try
            {
                _libroService.RegistrarLibro(libro);
                TempData["Mensaje"] = "Libro registrado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(libro);
            }
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            if (id <= 0)
                return BadRequest();

            var libro = _libroService.ObtenerLibroPorId(id);

            if (libro == null)
                return NotFound();

            return View(libro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, Libro libro)
        {
            if (id != libro.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(libro);

            try
            {
                _libroService.ActualizarLibro(libro);
                TempData["Mensaje"] = "Libro actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(libro);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Eliminar(int id)
        {
            if (id <= 0)
                return BadRequest();

            var libro = _libroService.ObtenerLibroPorId(id);

            if (libro == null)
                return NotFound();

            return View(libro);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmarEliminar(int id)
        {
            try
            {
                _libroService.EliminarLibro(id);
                TempData["Mensaje"] = "Libro eliminado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
