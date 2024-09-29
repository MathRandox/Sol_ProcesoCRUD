using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcesoCRUD.Datos
{
    public class Conexion
    {
        private string BaseDeDatos;
        private string Servidor;
        private string PuertoServidor;
        private string Usuario;
        private string Clave;
        private static Conexion Con = null;

        public Conexion()
        {
            this.BaseDeDatos = "bd_aprendizaje";
            this.Servidor = "localhost";
            this.PuertoServidor = "5432";
            this.Usuario = "user_crud";
            this.Clave= "1234567890";
        }

        public NpgsqlConnection CrearConexion()
        {
            NpgsqlConnection Cadena = new NpgsqlConnection();
            try
            {
                Cadena.ConnectionString = "Server=" + this.Servidor
                                        +";Port=" + this.PuertoServidor
                                        +";User id=" + this.Usuario
                                        +";Password=" + this.Clave
                                        +";Database=" + this.BaseDeDatos;
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
            }
            return Cadena;

        }

        public static Conexion getInstancia()
        {
            if(Con == null)
            {
                Con = new Conexion();
            }
            return Con;
        }
    }
}
