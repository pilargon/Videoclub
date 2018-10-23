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


        ////HACEMOS UNA CONSULTA A LA TABLA LIBRERIA
        //  conexion.Open();
        //  cadena = "SELECT * FROM LIBRERIA";
        //  comando = new SqlCommand(cadena, conexion);
        //SqlDataReader registros = comando.ExecuteReader();
        //    while (registros.Read())
            

              
        //       Console.WriteLine(registros["TEMA"].ToString() + "\t" + registros["ESTANTE"].ToString() + "\t" + registros["EJEMPLARES"].ToString());
        //       Console.WriteLine();
               

        //   Console.ReadLine();
        //    conexion.Close();







    }
}
