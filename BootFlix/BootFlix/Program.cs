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
            List<Cliente> listaClientes = new List<Cliente>();
            Menu(listaClientes);
        }
        static void Menu(List<Cliente> listaClientes)
        {
            int optMenu = 0;
            do
            {
                Console.WriteLine("Bienvenido a BooFlix\n-------------------");
                Console.WriteLine("1)Loguearse\n2)Registrarse\n3)Salir");
                optMenu = Convert.ToInt32(Console.ReadLine());

                switch (optMenu)
                {
                    case 1:
                        Loguearse();
                        Menu2();
                        break;
                    case 2:
                        Registrarse(listaClientes);
                        break;
                    case 3:
                        Console.WriteLine("Salir");
                        Console.ReadLine();
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
        static void Registrarse(List<Cliente> listaClientes)
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
            Cliente cliente = new Cliente(nombre, correoElectronico, idCliente, fechaNacimiento, contrasenia);
            listaClientes.Add(cliente);
            cliente.CrearCliente();                     
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
                    Menu2();
                    registros.Close();
                    contra = false;
                }
                else
                {
                    contra = false;
                    Console.WriteLine("No estas registrado, te invitamos a registrarte ;)");
                }

                conexion.Close();
                registros.Close();

            } while (contra != false);

        }
        static void Menu2()
        {
            int optMenu2;
            do
            {
                Console.WriteLine("\nBienvenido a BooFlix\n********************");
                Console.WriteLine("1)Consultar peliculas para su edad\n2)Consultar peliculas para su edad disponibles\n3)Mis peliculas\n4)Salir");
                optMenu2 = Convert.ToInt32(Console.ReadLine());

                switch (optMenu2)
                {
                    case 1://peliculas filtradas
                        //PeliculasFiltradas(); //crearla en la clase pelicula
                        break;
                    case 2://peliculas filtradas disponibles
                        //PeliculasFiltradasDisponibles(); //crearla en la clase pelicula
                        break;
                    case 3://mis peliculas
                        MisPeliculas();
                        Console.ReadLine();
                        break;
                    case 4://salir
                        Console.WriteLine("Salir");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Error Ingresa de nuevo");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
            while (optMenu2 != 4);
            Console.ReadLine();
        }
        static void MisPeliculas()
        {

        }
















    }
    
}
