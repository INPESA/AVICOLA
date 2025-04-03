using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AvicolaLaVida
{
    public class SqlConnectionClass
    {
        public static string SqlConnection = "Server = 192.168.2.2; Database = AvicolaINP; " +
                                             "User Id = INPESA; Password = INP2023$;";

        #region Procedimientos de Guardado

        public static void GuardarProc(string command, List<object> list)bios 
        {
            SqlConnection con = new SqlConnection(SqlConnection);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = command;
            con.Open();
            int i = 0;

            SqlCommandBuilder.DeriveParameters(cmd);
            foreach (SqlParameter p in cmd.Parameters)
            {
                if (p.ParameterName != "@RETURN_VALUE")
                {
                    p.Value = list[i];
                    i++;
                }
            }

            cmd.Connection = con;
            cmd.ExecuteNonQuery();
            con.Close();
        }


        #endregion

        #region Procedimientos de Carga
        public static DataTable CargarTablaProc(string command, List<object> list)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(SqlConnection);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = command;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            int i = 0;

            SqlCommandBuilder.DeriveParameters(cmd);
            foreach (SqlParameter p in cmd.Parameters)
            {
                if (p.ParameterName != "@RETURN_VALUE")
                {
                    cmd.Parameters.Add(new SqlParameter(p.ParameterName, list[i]));
                    i++;
                }
            }

            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable CargarTablaCommand(string command)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(SqlConnection);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = command;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public static DataTable CargarProcedimiento(string procedure)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(SqlConnection);
            SqlCommand cmd = con.CreateCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure;
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();

            return dt;
        }

        public static DataTable CargarTabla(string command)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(SqlConnection);
            SqlCommand cmd = con.CreateCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            cmd.CommandText = "select * from " + command;
            cmd.Connection = con;
            da.SelectCommand = cmd;
            da.Fill(dt);
            con.Close();
            return dt;
        }

        #endregion

        public static void Sql_Command(string command)
        {
            SqlConnection con = new SqlConnection(SqlConnection);
            SqlCommand cmd = con.CreateCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            con.Open();
            cmd.CommandText = command;
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
