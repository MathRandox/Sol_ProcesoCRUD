using Npgsql;
using ProcesoCRUD.Entidades;
using System;
using System.Data;

namespace ProcesoCRUD.Datos
{
    public class D_Contactos
    {
        public DataTable Listado_ca()
        {
            NpgsqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            NpgsqlConnection SqlCon = new NpgsqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                NpgsqlCommand Comando =
                    new NpgsqlCommand("SELECT descripcion_ca, codigo_ca FROM tb_cargos WHERE activo=true;", SqlCon);
                Comando.CommandType = CommandType.Text;
                SqlCon.Open();
                Resultado = Comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public DataTable Listado_co(string cTexto)
        {
            NpgsqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            NpgsqlConnection SqlCon = new NpgsqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                NpgsqlCommand Comando =
                    new NpgsqlCommand("SELECT (func_listado_co('" + cTexto + "')).*;", SqlCon);
                Comando.CommandType = CommandType.Text;
                SqlCon.Open();
                Resultado = Comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
        }

        public string Guardar_co(int nOpcion, E_Contactos oPro)
        {
            string Rpta = "";
            string SQL = "";
            string SentenciaSQL01 = "INSERT INTO tb_contactos(nombre_co, nromovil_co, correo_co, fechanac_co, codigo_ca) " +
                " values('" + oPro.Nombre_co + "','" + oPro.Nromovil_co + "','" + oPro.Correo_co + "','" + oPro.FechaNac_co + "','" + oPro.Codigo_ca + "')";
            string SentenciaSQL02 = "UPDATE tb_contactos set " +
                " nombre_co='" + oPro.Nombre_co + "'," +
                " nromovil_co='" + oPro.Nromovil_co + "'," +
                " correo_co='" + oPro.Correo_co + "'," +
                " fechanac_co='" + oPro.FechaNac_co + "'," +
                " codigo_ca='" + oPro.Codigo_ca + "'" +
                " WHERE codigo_co='" + oPro.Codigo_co + "'";

            SQL = nOpcion == 1 ? SentenciaSQL01 : SentenciaSQL02;

            NpgsqlConnection SqlCon = new NpgsqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                NpgsqlCommand Comando = new NpgsqlCommand(SQL, SqlCon);
                Comando.CommandType = CommandType.Text;
                SqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo registrar la información";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if(SqlCon.State==ConnectionState.Open) SqlCon.Close();
            }

            return Rpta;
        }

        public string Eliminar_co(int nCodigo_co)
        {
            string Rpta = "";
            string SQL = "UPDATE tb_contactos SET activo=false WHERE codigo_co='"+nCodigo_co+"'";

            NpgsqlConnection SqlCon = new NpgsqlConnection();
            try
            {
                SqlCon = Conexion.getInstancia().CrearConexion();
                NpgsqlCommand Comando = new NpgsqlCommand(SQL, SqlCon);
                Comando.CommandType = CommandType.Text;
                SqlCon.Open();
                Rpta = Comando.ExecuteNonQuery() >= 1 ? "OK" : "No se pudo eliminar la información";
            }
            catch (Exception ex)
            {
                Rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }

            return Rpta;
        }
    }
}
