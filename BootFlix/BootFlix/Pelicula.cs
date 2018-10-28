using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace BootFlix
{
    class Pelicula
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["BootFlix"].ConnectionString;
        static SqlConnection conexion = new SqlConnection(connectionString);
        static string cadena;
        static SqlCommand comando;

        private string sinopsis, director, peliculas;
        private int edad;

        public Pelicula()
        {

        }
        public Pelicula(string sinopsis,string director,string peliculas,int edad)
        {
            this.sinopsis = sinopsis;
            this.director = director;
            this.peliculas = peliculas;
            this.edad = edad;
        }
        public Pelicula(string peliculas)
        {
            this.peliculas = peliculas;
            
        }
        
        public string GetSinopsis()
        {
            return sinopsis;
        }
        public void SetSinopsis(string sinopsis)
        {
            this.sinopsis=sinopsis;
        }
        public string GetDirector()
        {
            return director;
        }
        public void SetDirector(string director)
        {
            this.director=director;
        }
        public int GetEdad()
        {
            return edad;
        }
        public void SetEdad(int edad)
        {
            this.edad=edad;
        }
        public string GetPeliculas()
        {
            return peliculas;
        }
        public void SetPeliculas(string peliculas)
        {
            this.peliculas=peliculas;
        }

        public string MostrarPeliculas()
        {
            return peliculas;
        }


        //HACEMOS UNA CONSULTA A LA TABLA PELICULA DE LAS PELICULAS FILTRADAS POR EDAD Y LIBRES
        //public void ConsultaPeliculasFiltradasLibres()
        //{
        //    conexion.Open();
        //    cadena = "SELECT * FROM PELICULAS WHERE PELICULAS 'Edad' >=18 AND 'Estado' LIKE 'LIBRE' ";
        //    comando = new SqlCommand(cadena, conexion);
        //    SqlDataReader registros = comando.ExecuteReader();
        //    while (registros.Read())

        //    Console.WriteLine(registros["Peliculas"].ToString() + "\t" + registros["Edad"].ToString() + "\t" + registros["Estado"].ToString());
        //    Console.ReadLine();
        //    conexion.Close();
        //}








    }
}
