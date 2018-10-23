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
        private static int idCliente = 0;

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
            //CREAR CODIGO DE RESERVA
            conexion.Open();
            cadena = "SELECT max(idCliente) AS 'EntryCount' FROM CLIENTES;";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                idCliente = Convert.ToInt32(registros["EntryCount"])+1;
            }           
            conexion.Close();
            registros.Close();


            string nombre, correoElectronico, contrasenia;
            DateTime fechaNacimiento;
            Console.WriteLine("Registro de Clientes\n");
            Console.WriteLine("Ingrese Nombre: ");
            nombre = Console.ReadLine();
            Console.WriteLine("Ingrese Correo electronico : ");
            correoElectronico = Console.ReadLine();
            Console.WriteLine("Contraseña: ");
            contrasenia = Console.ReadLine();
            Console.WriteLine("Año de nacimiento(yyyy): ");
            int year = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Mes de nacimiento(mm): ");
            int month = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Dia de nacimiento(dd): ");
            int day = Convert.ToInt32(Console.ReadLine());           
            fechaNacimiento = new DateTime(year, month, day);


            conexion.Open();
            cadena = "INSERT INTO Clientes VALUES ('" +idCliente+ "','" + correoElectronico + "','" + contrasenia + "','" + fechaNacimiento + "','" + nombre+ "')";
            comando = new SqlCommand(cadena,conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            
        }
        static void Loguearse()
        {
            bool contra = true;
            do
            {   //pide al loguearse nombre y contrase;a y busca si coinciden ambas en la BBDD
                Console.WriteLine("Dime tu nombre: ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Dime tu contraseña: ");
                string contrasenia = Console.ReadLine();
                conexion.Open();
                cadena = "SELECT * FROM Clientes WHERE nombre = '" + nombre + "' AND contraseña= '" + contrasenia+"'";
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
