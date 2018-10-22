using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace BootFlix
{
    class Program
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["BootFlix"].ConnectionString;
        static SqlConnection conexion = new SqlConnection(connectionString);
        static string cadena;
        static SqlCommand comando;
        private static string contrasenia;

        static void Main(string[] args)
        {
            Menu();
        }
        static void Menu()
        {
            int optMenu = 0;
            do
            {
                Console.WriteLine("Bienvenido a BooFlix \n");
                Console.WriteLine("1)Loguearse\n2)Registrarse\n3)Salir");
                optMenu = Convert.ToInt32(Console.ReadLine());

                switch (optMenu)
                {
                    case 1:
                        Loguearse();
                        break;
                    case 2:
                        Registrarse();
                        break;
                    case 3:
                        Console.WriteLine("Salir");
                        break;
                    default:
                        Console.WriteLine("Error Ingresa de nuevo");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            } while (optMenu != 3);

            Console.ReadLine();
        }

        static void Registrarse()
        {
            string nombre, correoElectronico, contrasenia;
            int fechaNacimiento;
            Console.WriteLine("Registro de Clientes\n");
            Console.WriteLine("Ingrese Nombre: ");
            nombre = Console.ReadLine();
            Console.WriteLine("Ingrese Correo electronico : ");
            correoElectronico = Console.ReadLine();
            Console.WriteLine("Contraseña: ");
            contrasenia = Console.ReadLine();
            Console.WriteLine("Año de nacimiento: ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Mes de nacimiento: ");
            int month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Dia de nacimiento: ");
            int day = Convert.ToInt32(Console.ReadLine());
            new DateTime(year, month, day);
            fechaNacimiento = Convert.ToInt32(Console.ReadLine());


            conexion.Open();
            cadena = "INSERT INTO Clientes VALUES ('" + nombre + "','" + correoElectronico + "','" + contrasenia + "','" + fechaNacimiento+ "')";
            comando = new SqlCommand(cadena,conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        // conexion.Open();
        //cadena = "INSERT INTO  VALUES (dni,nombre,apellido)";
        //cadena = "INSERT INTO  VALUES (NHab,CheckIn)";
        //comando = new SqlCommand(cadena, conexion);
        //comando.ExecuteNonQuery();
        //conexion.Close();

        static void Loguearse()
        {
            bool contra = true;
            do
            {
                conexion.Open();
                cadena = "SELECT * FROM Clientes WHERE contraseña = '" + contrasenia + "'";
                comando = new SqlCommand(cadena, conexion);
                SqlDataReader registros = comando.ExecuteReader();

                if (registros.Read())
                {
                    Console.WriteLine("Le aparecera un menu con las opciones");
                    registros.Close();
                    contra = false;
                }
                else
                {
                    contra = false;
                    Console.WriteLine("No estas registrado , te invitamos a registrarte ;)");
                }

                conexion.Close();
                registros.Close();

            } while (contra != false);

        }




    }
    
}
