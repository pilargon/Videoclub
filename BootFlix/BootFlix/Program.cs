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
        static Cliente cliente;
        static bool salir;
        static int optMenu = 0;

        static void Main(string[] args)
        {
            List<Cliente> listaClientes = new List<Cliente>();
            Cliente c = new Cliente();

            Pelicula p = new Pelicula();
            salir = false;
            Menu();
        }

        static void Menu()
        {
            do
            {
                Console.WriteLine("Bienvenido a BootFlix\n-------------------");
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
                        break;
                }
            } while (optMenu != 3);

            Console.ReadLine();
        }
        static void Registrarse()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            conexion.Open();
            cadena = "SELECT max(idCliente) AS 'EntryCount' FROM CLIENTES;";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            while (registros.Read())
            {
                idCliente = Convert.ToInt32(registros["EntryCount"]) + 1;
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
            Console.WriteLine("Inserte fecha de nacimiento(dd/mm/yyyy):");
            fechaNacimiento = Convert.ToDateTime(Console.ReadLine());

            Cliente cliente = new Cliente(nombre, correoElectronico, idCliente, fechaNacimiento, contrasenia);
            listaClientes.Add(cliente);
            cliente.CrearCliente();
        }
        static void Loguearse()
        {
            string nombre = "";
            int idCliente;
            DateTime fechaNacimiento = new DateTime();
            bool contra = true;
            do
            {   //pide al loguearse nombre y contrase;a y busca si coinciden ambas en la BBDD
                Console.WriteLine("Dime tu correo electronico: ");
                string correoElectronico = Console.ReadLine();
                Console.WriteLine("Dime tu contraseña: ");
                string contrasenia = Console.ReadLine();
                conexion.Open();
                cadena = "SELECT * FROM Clientes WHERE correoElectronico = '" + correoElectronico + "' " +
                    "AND contraseña= '" + contrasenia + "'";
                comando = new SqlCommand(cadena, conexion);
                SqlDataReader registros = comando.ExecuteReader();


                if (registros.Read())
                {
                    conexion.Close();
                    conexion.Open();
                    cadena = "SELECT * FROM Clientes WHERE correoElectronico = '" + correoElectronico + "'" +
                        " AND contraseña= '" + contrasenia + "'";
                    comando = new SqlCommand(cadena, conexion);
                    registros = comando.ExecuteReader();

                    while (registros.Read())
                    {
                        nombre = registros["Nombre"].ToString();
                        idCliente = Convert.ToInt32(registros["IdCliente"]);
                        fechaNacimiento = Convert.ToDateTime(registros["FechaNacimiento"]);

                        cliente = new Cliente(nombre, correoElectronico, idCliente, fechaNacimiento, contrasenia);
                        conexion.Close();
                        registros.Close();

                        Menu2(cliente);

                        //funcion salir
                        if (salir)
                        {
                            optMenu = 3;
                            contra = false;
                            break;
                        }
                    }
                }
                else
                {
                    contra = false;
                    Console.WriteLine("No estas registrado, te invitamos a registrarte ");
                }
                conexion.Close();
                registros.Close();

            } while (contra != false);

        }

        public static void Menu2(Cliente cliente)
        {
            int optMenu2;
            do
            {
                Console.WriteLine("\nBienvenido a BooFlix\n********************");
                Console.WriteLine("1)Consultar peliculas para su edad\n2)Consultar peliculas para su edad disponibles\n3)Reservar\n4)Mis peliculas\n5)Salir");
                optMenu2 = Convert.ToInt32(Console.ReadLine());

                switch (optMenu2)
                {
                    case 1://peliculas filtradas
                        ConsultaPeliculasFiltradas(cliente);
                        break;
                    case 2://peliculas filtradas disponibles
                        ConsultaPeliculasFiltradasDisponibles(cliente);
                        break;
                    case 3://reservas
                        Reservar();
                        Console.ReadLine();
                        break;
                    case 4:
                        //MisPeliculas(); 
                        Console.ReadLine();
                        break;
                    case 5://salir
                        Console.WriteLine("Salir");
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Error Ingresa de nuevo");
                        break;
                }

            }
            while (optMenu2 != 5);
        }

        public static void ConsultaPeliculasFiltradasDisponibles(Cliente cliente)
        {
            List<Pelicula> listaPeliculas = new List<Pelicula>();
            conexion.Close();
            conexion.Open();
            cadena = "select DATEDIFF(YEAR, FechaNacimiento, GETDATE()) AS 'EDAD' " +
                "FROM Clientes where CorreoElectronico = '" + cliente.GetCorreoElectronico() + "'";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader edadRec = comando.ExecuteReader();
            edadRec.Read();
            int edad = Convert.ToInt32(edadRec[0].ToString());
            conexion.Close();

            conexion.Open();
            cadena = "SELECT * FROM PELICULAS WHERE Estado like 'LIBRE' AND Edad <= '" + edad + "' ";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            //Console.WriteLine(registros["Peliculas"].ToString() + "\t" + registros["Edad"].ToString());
            while (registros.Read())
            {
                Pelicula p = new Pelicula(registros["Peliculas"].ToString());
                listaPeliculas.Add(p);
            }
            foreach (Pelicula pelis in listaPeliculas)
            {
                Console.WriteLine(pelis.MostrarPeliculas());
            }
            conexion.Close();
            registros.Close();
        }

        public static void ConsultaPeliculasFiltradas(Cliente cliente)
        {
            List<Pelicula> listaPeliculas = new List<Pelicula>();
            conexion.Close();
            conexion.Open();
            cadena = "select DATEDIFF(YEAR, FechaNacimiento, GETDATE()) AS 'EDAD' " +
                "FROM Clientes where CorreoElectronico = '" + cliente.GetCorreoElectronico() + "'";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader edadRec = comando.ExecuteReader();
            edadRec.Read();
            int edad = Convert.ToInt32(edadRec[0].ToString());
            conexion.Close();


            conexion.Open();
            cadena = "SELECT * FROM PELICULAS WHERE EDAD <= '" + edad + "' ";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            Console.ReadLine();
            while (registros.Read())
            {
                Console.WriteLine(registros["Peliculas"].ToString());
                Console.WriteLine();
            }
            foreach (Pelicula pelis in listaPeliculas)
            {
                Console.WriteLine(pelis.MostrarPeliculas());
            }
            conexion.Close();
        }

        public static void Reservar()
        {
            List<Alquiler> listaAlquiler = new List<Alquiler>();
            Console.WriteLine("Escribe el titulo de la pelicula a reservar");
            string titulo = Console.ReadLine();

            //PONER PELI ELEGIDA COMO ALQUILADA
            conexion.Open();
            cadena = "UPDATE Peliculas SET Estado = 'ALQUILADA' WHERE Peliculas LIKE'" + titulo + "'";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader registros = comando.ExecuteReader();
            conexion.Close();

            //TOMAMOS ID PELICULA
            conexion.Open();
            cadena = "SELECT IdPeliculas FROM Peliculas WHERE Peliculas LIKE'" + titulo + "'";
            comando = new SqlCommand(cadena, conexion);
            registros = comando.ExecuteReader();
            int IdPelicula = 0;
            while (registros.Read())
            {
                IdPelicula = Convert.ToInt32(registros["IdPeliculas"]);
            }
            conexion.Close();
            registros.Close();

            //CREAR IDALQUILER
            conexion.Open();
            cadena = "SELECT max(IdAlquiler) AS 'maximo' FROM Alquiler";
            comando = new SqlCommand(cadena, conexion);
            registros = comando.ExecuteReader();
            int IdAlquiler = 0;
            while (registros.Read())
            {
                if (registros["maximo"].Equals(DBNull.Value))
                {
                    IdAlquiler = 1;
                }
                else
                {
                    IdAlquiler = Convert.ToInt32(registros["maximo"]) + 1;
                }

            }
            conexion.Close();
            registros.Close();

            //ACTUALIZAR ALQUILERES

            //Alquiler alquiler = new Alquiler(fechaAlquiler, fechaDevolucion, idCliente, idPeliculas);
            //listaAlquiler.Add(alquiler);
            //alquiler.CrearReserva();

            conexion.Open();
            cadena = "INSERT INTO Alquiler(FechaAlquiler,FechaDevolucion,IdCliente,IdPeliculas) VALUES ('" + DateTime.Today + "','" + DateTime.Today.AddDays(10) + "','" + cliente.GetIdCliente() + "','" + IdPelicula + "')";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            Console.WriteLine("Su pelicula ha sido alquilada");
        }
    }
}
//static void MisPeliculas()
//{
//    int optMisPeliculas;
//    do
//    {
//        Console.WriteLine("\nMis Peliculas\n********************" + "(Pulsar pelicula para opciones)\n");
//        conexion.Open();
//        cadena = "SELECT * FROM ALQUILERES";
//        comando = new SqlCommand(cadena, conexion);
//        SqlDataReader registros = comando.ExecuteReader();
//        Console.WriteLine(registros["IdPeliculaAlquilada"].ToString());
//        conexion.Close();

//        optMisPeliculas = Convert.ToInt32(Console.ReadLine());

//        switch (optMisPeliculas)
//        {
//            case 1://
//                break;
//            case 2://
//                break;
//            case 3://
//                break;
//            case 4://salir
//                break;
//            default:
//                break;
//        }
//    }
//    while (optMisPeliculas != 4);
//    Console.ReadLine();
//}

    














//}


