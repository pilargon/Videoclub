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



    }
}
