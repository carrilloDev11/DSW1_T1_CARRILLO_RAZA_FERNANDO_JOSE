using Microsoft.Data.SqlClient;
using System.Data;
using DSW1_T1_CARRILLO_RAZA_FERNANDO_JOSE.Models;

namespace DSW1_T1_CARRILLO_RAZA_FERNANDO_JOSE.Repositories
{
    public class LibroRepository : ILibroRepository
    {
        private readonly string _cadenaConexion;

        public LibroRepository(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("cn")
                ?? throw new InvalidOperationException(
                    "No se encontró la cadena de conexión 'cn'.");
        }

        public IEnumerable<Libro> ListarLibros()
        {
            List<Libro> libros = new List<Libro>();

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("usp_ListarLibros", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            libros.Add(MapearLibro(dr));
                        }
                    }
                }
            }

            return libros;
        }

        public Libro? ObtenerLibroPorId(int id)
        {
            Libro? libro = null;

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("usp_ObtenerLibroPorId", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            libro = MapearLibro(dr);
                        }
                    }
                }
            }

            return libro;
        }

        public void RegistrarLibro(Libro libro)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("usp_RegistrarLibro", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    AgregarParametrosLibro(cmd, libro);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarLibro(Libro libro)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("usp_ActualizarLibro", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = libro.Id;

                    AgregarParametrosLibro(cmd, libro);

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void EliminarLibro(int id)
        {
            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("usp_EliminarLibro", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Libro> ListarLibrosPorAutor(string autor)
        {
            List<Libro> libros = new List<Libro>();

            using (SqlConnection cn = new SqlConnection(_cadenaConexion))
            {
                using (SqlCommand cmd = new SqlCommand("usp_ListarLibrosPorAutor", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Autor", SqlDbType.VarChar, 100).Value = autor;

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            libros.Add(MapearLibro(dr));
                        }
                    }
                }
            }

            return libros;
        }

        private static Libro MapearLibro(SqlDataReader dr)
        {
            return new Libro
            {
                Id = dr.GetInt32(dr.GetOrdinal("Id")),
                NombreLibro = dr.GetString(dr.GetOrdinal("NombreLibro")),
                Autor = dr.GetString(dr.GetOrdinal("Autor")),
                NombreEditorial = dr.GetString(dr.GetOrdinal("NombreEditorial")),
                ISDN = dr.GetString(dr.GetOrdinal("ISDN"))
            };
        }

        private static void AgregarParametrosLibro(SqlCommand cmd, Libro libro)
        {
            cmd.Parameters.Add("@NombreLibro", SqlDbType.VarChar, 120).Value =
                libro.NombreLibro;

            cmd.Parameters.Add("@Autor", SqlDbType.VarChar, 100).Value =
                libro.Autor;

            cmd.Parameters.Add("@NombreEditorial", SqlDbType.VarChar, 100).Value =
                libro.NombreEditorial;

            cmd.Parameters.Add("@ISDN", SqlDbType.VarChar, 20).Value =
                libro.ISDN;
        }
    }
}