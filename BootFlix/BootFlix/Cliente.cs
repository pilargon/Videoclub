using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace BootFlix
{
    class Cliente
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["BootFlix"].ConnectionString;
        static SqlConnection conexion = new SqlConnection(connectionString);
        static string cadena;
        static SqlCommand comando;

        private string nombre,correoElectronico,contrasenia;
        private DateTime fechaNacimiento;
        private List<Cliente> listaClientes;
        private int idCliente;

        public Cliente()
        {

        }
        public Cliente(string nombre, string correoElectronico, int idCliente, DateTime fechaNacimiento, string contrasenia)
        {
            this.nombre = nombre;
            this.correoElectronico = correoElectronico;
            this.idCliente = idCliente;
            this.fechaNacimiento = fechaNacimiento;
            this.contrasenia = contrasenia;
        }

        public string GetNombre()
        {
            return nombre;
        }
        public void SetNombre(string nombre)
        {
            this.nombre = nombre;
        }
        public string GetCorreoElectronico()
        {
            return correoElectronico;
        }
        public void SetCorreoElectronico(string correoElectronico)
        {
            this.correoElectronico = correoElectronico;
        }
        public int GetIdCliente()
        {
            return idCliente;
        }
        public void SetIdCliente(int idCliente)
        {
            this.idCliente = idCliente;
        }
        public DateTime GetFechaNacimiento()
        {
            return fechaNacimiento;
        }
        public void SetFechaNacimiento(DateTime fechaNacimiento)
        {
            this.fechaNacimiento = fechaNacimiento;
        }
        public string GetContrasenia()
        {
            return contrasenia;
        }
        public void SetContrsenia(string contrasenia)
        {
            this.contrasenia = contrasenia;
        }


        public void CrearCliente()
        {
            conexion.Open();
            cadena = "INSERT INTO Clientes VALUES ('" + this.idCliente + "','" + this.correoElectronico + "','" + this.contrasenia + "','" + this.fechaNacimiento + "','" + this.nombre + "')";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();           
            conexion.Close();
        }



    }
}
