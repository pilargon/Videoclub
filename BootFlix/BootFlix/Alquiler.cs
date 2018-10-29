using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace BootFlix
{
    class Alquiler
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["BootFlix"].ConnectionString;
        static SqlConnection conexion = new SqlConnection(connectionString);
        static string cadena;
        static SqlCommand comando;

        private DateTime fechaAlquiler, fechaDevolucion;
        private int idCliente, idPeliculas;

        public Alquiler()
        {

        }
        public Alquiler(DateTime fechaAlquiler,DateTime fechaDevolucion, int idCliente, int idPeliculas)
        {
            this.fechaAlquiler = fechaAlquiler;
            this.fechaDevolucion = fechaDevolucion;
            this.idCliente = idCliente;
            this.idPeliculas = idPeliculas;
        }
        public void MisPeliculas()
        {
            conexion.Open();
            cadena = "SELECT * FROM PELICULAS WHERE PELICULAS 'Edad' >=18 ";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())

                Console.WriteLine(registros["Peliculas"].ToString() + "\t" + registros["Edad"].ToString());
            Console.ReadLine();
            conexion.Close();
        }
        //public void CrearReserva()
        //{
        //    conexion.Open();
        //    cadena = "INSERT INTO Alquiler(FechaAlquiler,FechaDevolucion,IdCliente,IdPeliculas) VALUES ('" + DateTime.Today + "','" + DateTime.Today.AddDays(10) + "','" + cliente.GetIdCliente() + "','" + IdPelicula + "')";
        //    comando = new SqlCommand(cadena, conexion);
        //    comando.ExecuteNonQuery();
        //    conexion.Close();
        //    Console.WriteLine("Su pelicula ha sido alquilada");
        //}


    }
}
