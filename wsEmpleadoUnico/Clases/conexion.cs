using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

namespace wsEmpleadoUnico.Clases
{
    public class conexion
    {
        public DataSet GetConsulta(string query)
        {
            string cadena = ConfigurationManager.ConnectionStrings["conEmpUnico"].ToString();

            SqlConnection MiConexion = new SqlConnection(cadena);
            MiConexion.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, MiConexion);
            DataSet dsConsulta = new DataSet();
            adapter.Fill(dsConsulta);
            MiConexion.Close();

            return dsConsulta;
        }

    }
}
