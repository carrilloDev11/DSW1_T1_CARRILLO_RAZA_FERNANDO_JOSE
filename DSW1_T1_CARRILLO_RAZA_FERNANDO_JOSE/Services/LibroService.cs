using DSW1_T1_CARRILLO_RAZA_FERNANDO_JOSE.Models;
using DSW1_T1_CARRILLO_RAZA_FERNANDO_JOSE.Repositories;

namespace DSW1_T1_CARRILLO_RAZA_FERNANDO_JOSE.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;

        public LibroService(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        public IEnumerable<Libro> ListarLibros()
        {
            return _libroRepository.ListarLibros();
        }

        public Libro? ObtenerLibroPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del libro no es válido.");

            return _libroRepository.ObtenerLibroPorId(id);
        }

        public void RegistrarLibro(Libro libro)
        {
            ValidarLibro(libro);

            _libroRepository.RegistrarLibro(libro);
        }

        public void ActualizarLibro(Libro libro)
        {
            if (libro.Id <= 0)
                throw new ArgumentException("El ID del libro no es válido.");

            ValidarLibro(libro);

            if (_libroRepository.ObtenerLibroPorId(libro.Id) == null)
                throw new KeyNotFoundException("El libro no existe.");

            _libroRepository.ActualizarLibro(libro);
        }

        public void EliminarLibro(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID del libro no es válido.");

            if (_libroRepository.ObtenerLibroPorId(id) == null)
                throw new KeyNotFoundException("El libro no existe.");

            _libroRepository.EliminarLibro(id);
        }

        public IEnumerable<Libro> ListarLibrosPorAutor(string autor)
        {
            if (string.IsNullOrWhiteSpace(autor))
                return _libroRepository.ListarLibros();

            return _libroRepository.ListarLibrosPorAutor(autor.Trim());
        }

        private static void ValidarLibro(Libro libro)
        {
            ArgumentNullException.ThrowIfNull(libro);

            if (string.IsNullOrWhiteSpace(libro.NombreLibro) ||
                string.IsNullOrWhiteSpace(libro.Autor) ||
                string.IsNullOrWhiteSpace(libro.NombreEditorial) ||
                string.IsNullOrWhiteSpace(libro.ISDN))
            {
                throw new ArgumentException(
                    "Todos los campos del libro son obligatorios.");
            }

            if (libro.NombreLibro.Length > 120 ||
                libro.Autor.Length > 100 ||
                libro.NombreEditorial.Length > 100 ||
                libro.ISDN.Length > 20)
            {
                throw new ArgumentException(
                    "Uno o más campos superan la longitud permitida.");
            }
        }
    }
}
