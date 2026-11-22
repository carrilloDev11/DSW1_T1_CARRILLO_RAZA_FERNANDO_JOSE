using DSW1_T1_CARRILLO_RAZA_FERNANDO_JOSE.Models;

namespace DSW1_T1_CARRILLO_RAZA_FERNANDO_JOSE.Repositories
{
    public interface ILibroRepository
    {
        IEnumerable<Libro> ListarLibros();

        Libro? ObtenerLibroPorId(int id);

        void RegistrarLibro(Libro libro);

        void ActualizarLibro(Libro libro);

        void EliminarLibro(int id);

        IEnumerable<Libro> ListarLibrosPorAutor(string autor);
    }
}