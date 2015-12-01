using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Configuration;

namespace wsEmpleadoUnico
{
    /// <summary>
    /// Summary description for wsInfoEmpleado
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class wsInfoEmpleado : System.Web.Services.WebService
    {
        [WebMethod(Description = "Regresa el DataSet de los Empleados de Nuevo Ingreso. Enviar el número de mes.(02)")]
        public DataSet dsRegresaEmpleadosNuevoIngreso(int MesCon)
        {
            DataSet dsEmpNuevoIngr = new DataSet();
            string consultaNuevoIngreso = string.Empty;

            consultaNuevoIngreso = "EXEC [dbo].[spDSWS_SelectEmpleadosNuevoIngreso] " + MesCon;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaNuevoIngreso, conexionEmpU());
            adapter.Fill(dsEmpNuevoIngr);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsEmpNuevoIngr;
        }

        [WebMethod(Description = "Regresa el DataSet de los Empleados que cumplen años del mes solicitado.Enviar el número de mes.(02)")]
        public DataSet dsRegresaEmpleadosCumpleMes(int MesCon)
        {
            DataSet dsEmpCumpMes = new DataSet();
            string consultaCumpMes = string.Empty;

            consultaCumpMes = "EXEC [dbo].[spDSWS_SelectEmpleadosCumpleanosMes] " + MesCon;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaCumpMes, conexionEmpU());
            adapter.Fill(dsEmpCumpMes);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsEmpCumpMes;
        }

        [WebMethod(Description = "Regresa el DataSet de las Empresas")]
        public DataSet dsRegresaEmpresas()
        {
            DataSet dsEmpresas = new DataSet();
            string consultaEmpresas = string.Empty;

            consultaEmpresas = "EXEC [dbo].[spDSWS_SelectEmpresas]";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaEmpresas, conexionEmpU());
            adapter.Fill(dsEmpresas);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsEmpresas;
        }

        [WebMethod(Description = "Regresa los Empleados de una Empresa en Especifico. Enviar el Id de la Empresa.")]
        public DataSet dsRegresaEmpleadosxEmpresa(int IdEmpresa)
        {
            DataSet dsEmpleadosxEmp = new DataSet();
            string consultaEmpleadosxEmp = string.Empty;

            consultaEmpleadosxEmp = "EXEC [dbo].[spDSWS_SelectEmpleadosxEmp] " + IdEmpresa;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaEmpleadosxEmp, conexionEmpU());
            adapter.Fill(dsEmpleadosxEmp);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsEmpleadosxEmp;
        }

        [WebMethod(Description = "Regresa a los empleados inactivos por empresa solicitada.")]
        public DataSet dsRegresaSelectEmpleadosxEmpInactivos(int idEmpresa)
        {
            DataSet ds = new DataSet();

            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter("spDSWS_SelectEmpleadosxEmpInactivos", conexionEmpU());
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = idEmpresa;
            adapter.Fill(ds);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return ds;
        }

        [WebMethod(Description="Regresa los empleados activos que tienen gente a su cargo. Enviar Id de la empresa a consultar, si envía 0, regresa de todas las empresas.")]
        public DataSet dsRegresaJefes(int idEmpresa)
        {
            DataSet dsJefes = new DataSet();

            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter("spDSWS_SelectJefesEmpresa", conexionEmpU());
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.SelectCommand.Parameters.Add("@IdEmpresa", SqlDbType.Int).Value = idEmpresa;
            adapter.Fill(dsJefes);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsJefes;
        }

        [WebMethod(Description = "Regresa los colaboradores enviando el ID del jefe directo.")]
        public DataSet dsRegresaColaboradoresIdJefe(int idJefe)
        {
            DataSet dsEmpleadosIDs = new DataSet();
            string consultaEmpleadosIds = string.Empty;

            consultaEmpleadosIds = "EXEC spDSWS_SelectColaboradoresIdJefe " + idJefe;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaEmpleadosIds, conexionEmpU());
            adapter.Fill(dsEmpleadosIDs);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsEmpleadosIDs;
        }

        [WebMethod(Description = "Regresa los Empleados enviando sus IDs. Se envian separados por coma(,). Ej. 1,2,3,n")]
        public DataSet dsRegresaEmpleadosIDs(string IdsEmpleados)
        {
            DataSet dsEmpleadosIDs = new DataSet();
            string consultaEmpleadosIds = string.Empty;

            consultaEmpleadosIds = "EXEC spDSWS_SelectEmpleadosxIDS '" + IdsEmpleados + "'";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaEmpleadosIds, conexionEmpU());
            adapter.Fill(dsEmpleadosIDs);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsEmpleadosIDs;
        }

        [WebMethod(Description = "Regresa los Empleados enviando sus IDs. Se envian separados por coma(,). Ej. 1,2,3,n. Tambien lo filtra por Oficina. 1 MLaurent, 2 Viena, 3 Mty.")]
        public DataSet dsRegresaEmpleadosIDsFiltro(string IdsEmpleados, int sucursal)
        {
            DataSet dsEmpleadosIDs = new DataSet();
            string consultaEmpleadosIds = string.Empty;

            consultaEmpleadosIds = "EXEC spDSWS_SelectEmpleadosxIDSFiltro '" + IdsEmpleados + "', " + sucursal;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaEmpleadosIds, conexionEmpU());
            adapter.Fill(dsEmpleadosIDs);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();
            return dsEmpleadosIDs;
        }

        [WebMethod(Description = "Regresa TODOS los Empleados enviando su ID.(Comedor)")]
        public DataSet dsRegresaTodosEmpleadosIDs(int IdsEmpleado)
        {
            DataSet dsEmpleadoTodosIds = new DataSet();
            string consultaEmpleadoTodosIds = string.Empty;

            consultaEmpleadoTodosIds = "EXEC spDSWS_SelectEmpleadosxIDSTodos " + IdsEmpleado;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaEmpleadoTodosIds, conexionEmpU());
            adapter.Fill(dsEmpleadoTodosIds);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsEmpleadoTodosIds;
        }

        [WebMethod(Description = "Regresa el Id_SiaNom enviando el Id_Empleado.")]
        public DataSet dsRegresaIdSiaNom(int idEmpleado)
        {
            DataSet dsIdSiaNom = new DataSet();
            string consultaIdSiaNom = string.Empty;

            consultaIdSiaNom = "SELECT [dbo].[fnDSWSRegresaIdSiaNom] (" + idEmpleado + ") AS Id_SiaNom";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaIdSiaNom, conexionEmpU());
            adapter.Fill(dsIdSiaNom);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsIdSiaNom;
        }

        [WebMethod(Description = "Regresa el Id_Empleado enviando el Id_SiaNom.")]
        public DataSet dsRegresaIdEmpleado(int IdSiaNom)
        {
            DataSet dsIdEmpleado = new DataSet();
            string consultaIdEmpleado = string.Empty;

            consultaIdEmpleado = "SELECT Id_Empleado FROM Nominas_Emp WHERE Id_SiaNomSIC = " + IdSiaNom;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaIdEmpleado, conexionEmpU());
            adapter.Fill(dsIdEmpleado);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsIdEmpleado;
        }


        [WebMethod(Description = "Regresa el catalogo de las Empresas que Pagan (Empresa de Nominas).")]
        public DataSet dsRegresaEmpresasNominas()
        {
            DataSet dsNominas = new DataSet();
            string consultaNominas = string.Empty;

            consultaNominas = "EXEC spDSWS_SelectNominas";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaNominas, conexionEmpU());
            adapter.Fill(dsNominas);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsNominas;
        }

        [WebMethod(Description = "Regresa los Cumpleaños de la Semana.")]
        public DataSet dsRegresaCumpleEmp()
        {
            DataSet dsCumpleanos = new DataSet();
            string consultaCumpleanos = string.Empty;

            consultaCumpleanos = "EXEC [dbo].[DSWS_SelectCumpleEmpleados]";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaCumpleanos, conexionEmpU());
            adapter.Fill(dsCumpleanos);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsCumpleanos;
        }

        [WebMethod(Description = "Regresa el Directorio de un Empleado. Se envia el ID del Empleado.")]
        public DataSet dsRegresaDirectorioxEmp(int IdEmpleado)//YA ESTUBO
        {
            DataSet dsDirectorio = new DataSet();
            string consultaDirectorio = string.Empty;

            consultaDirectorio = "EXEC [dbo].[spDSWS_SelectDirectorioSCANDA] " + IdEmpleado;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaDirectorio, conexionEmpU());
            adapter.Fill(dsDirectorio);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsDirectorio;
        }

        [WebMethod(Description = "Regresa una Lista de Empleados que Coincidan con el Nombre o Apellido. Enviar de Preferencia Mayuscula.")]
        public DataSet dsRegresaEmpleadoxNombre(string Nombre, int id_Empresa)
        {
            DataSet dsNombreEmpleado = new DataSet();
            DataSet dsReturn = new DataSet();
            string consultaEmpleadoNombre = string.Empty;

            consultaEmpleadoNombre = "EXEC [dbo].[spDSWS_SelectEmpleadosxEmprBuscador] " + id_Empresa;
            
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaEmpleadoNombre, conexionEmpU());
            adapter.Fill(dsNombreEmpleado);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            /****************empieza el filtro**************************/

            DataTable DataTableObject = new DataTable();
            DataColumn dcId_Empleado = new DataColumn("Id_Empleado");
            DataColumn dcNombre = new DataColumn("Nombre");

            DataTableObject.Columns.Add(dcId_Empleado);
            DataTableObject.Columns.Add(dcNombre);

            ArrayList renglonBorrar = new ArrayList();
            try
            {
                string[] s = Nombre.Split(' ');
               
                foreach (DataRow dr in dsNombreEmpleado.Tables[0].Rows)
                {
                    int cont = 0;

                    for (int x = 0; x <= s.Length - 1; x++)
                    {
                        if (dr["Nombre"].ToString().ToUpper().Contains(s[x].ToString().ToUpper()))
                            cont = cont + 1;
                        else
                            break;
                    }

                    if (cont == s.Length)
                    {
                        DataRow dataRowObject = DataTableObject.NewRow();
                        dataRowObject["Id_Empleado"] = dr["Id_Empleado"].ToString();
                        dataRowObject["Nombre"] = dr["Nombre"].ToString();
                        
                        DataTableObject.Rows.Add(dataRowObject);
                    }
                }
            }

            catch (Exception e)
            {
            }

            dsReturn.Tables.Add(DataTableObject);
           
           /**********************************************/

         return dsReturn;
        }

        [WebMethod(Description = "Regresa los directores de todas las empresas internas.")]
        public DataSet dtRegresaDirectores(int idEmpresa)
        {
            DataSet dsDirectores = new DataSet();
            string consultaDirectorio = string.Empty;

            consultaDirectorio = "EXEC [dbo].[spDSWS_SelectDirectores] " + idEmpresa;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaDirectorio, conexionEmpU());
            adapter.Fill(dsDirectores);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsDirectores;
        }

        [WebMethod(Description = "Ejecuta la actualizacion del SIC a Empleado Único.")]
        public Boolean dsActualizaEmpleadoUnico()
        {
            bool Actualizacion = true;
            string ejecutaActualizacion = string.Empty;

            try
            {
                SqlConnection conexionBD = new SqlConnection();
                conexionBD = MiConexion();
                conexionBD.Open();
                SqlCommand comando = new SqlCommand("EXEC  dbo.spUpdBaseEmpleadoUnicoJob", conexionBD);
                comando.ExecuteReader();
                SqlConnection.ClearAllPools();
                conexionBD.Close();
            }

            catch
            {
                Actualizacion = false;
            }

            return Actualizacion;
        }

        [WebMethod(Description = "Actualiza las Huellas Digitales del Empleado.")]
        public void UpdateHuellasDigitales(int IdEmpleado, string HuellaDerecha, string HuellaIzquierda)
        {
            string ActualizaHuellas = string.Empty;

            ActualizaHuellas = "EXEC [dbo].[spDSWS_UpdateHuellaDigital] " + IdEmpleado + ", '" + HuellaIzquierda + "', '" + HuellaDerecha + "'";
            
            SqlConnection conexionBD = new SqlConnection();
            conexionBD = MiConexion();
            conexionBD.Open();
            SqlCommand comando = new SqlCommand(ActualizaHuellas, conexionBD);
            comando.ExecuteNonQuery();
            SqlConnection.ClearAllPools();
            conexionBD.Close();
        }

        [WebMethod(Description = "Actualiza las huellas digitales para la versión 10 del lector.")]
        public void UpdateHuellasDigitalesV10(int IdEmpleado, string HuellaDerecha, string HuellaIzquierda)
        {
            string ActualizaHuellas = string.Empty;
            
            ActualizaHuellas = "EXEC [dbo].[spDSWS_UpdateHuellaDigitalV10] " + IdEmpleado + ", '" + HuellaIzquierda + "', '" + HuellaDerecha + "'";

            SqlConnection conexionBD = new SqlConnection();
            conexionBD = MiConexion();
            conexionBD.Open();
            SqlCommand comando = new SqlCommand(ActualizaHuellas, conexionBD);
            comando.ExecuteNonQuery();
            SqlConnection.ClearAllPools();
            conexionBD.Close();
        }

        [WebMethod(Description = "Regresa el DataSet de las Huellas de los Empleados Activos.")]
        public DataSet dsRegresaHuellasEmpleados()
        {
            DataSet dsHuellasEmpleados = new DataSet();
            string consultaHuellasEmp = string.Empty;

            consultaHuellasEmp = "EXEC [dbo].[spDSWS_SelectHuellasEmpleados]";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaHuellasEmp, conexionEmpU());
            adapter.Fill(dsHuellasEmpleados);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsHuellasEmpleados;
        }

        [WebMethod(Description = "Regresa el DataSet de las Huellas de los Empleados Activos. Tambien lo filtra por Oficina. 1 MLaurent, 2 Viena, 3 Mty.")]
        public DataSet dsRegresaHuellasEmpleadosFiltro(int sucursal)
        {
            DataSet dsHuellasEmpleados = new DataSet();
            string consultaHuellasEmp = string.Empty;

            consultaHuellasEmp = "EXEC [dbo].[spDSWS_SelectHuellasEmpleadosFiltro] " + sucursal;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaHuellasEmp, conexionEmpU());
            adapter.Fill(dsHuellasEmpleados);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsHuellasEmpleados;
        }

        [WebMethod(Description = "Regresa los registros de los empleados con sus huellas de versión 10. Se filtran por acceso a oficinas. Si se envía 0, regresa a todos.")]
        public DataSet dsSelectHuellasV10PorAcceso(int IdAccOficina)
        {
            DataSet ds = new DataSet();
            string consultaHuellasEmp = string.Empty;

            consultaHuellasEmp = "EXEC [dbo].[spDSWS_SelectHuellasV10PorAcceso] " + IdAccOficina;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaHuellasEmp, conexionEmpU());
            adapter.Fill(ds);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return ds;
        }

        [WebMethod(Description = "Regresa el Catalogo de Centro de Costos")]
        public DataSet dsRegresaCatalogoCentroCostos()
        {
            DataSet ds = new DataSet();
            string consultaCentroCosto = string.Empty;

            consultaCentroCosto = "EXEC [dbo].[spDSWS_SelectCentroCostos]";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaCentroCosto, conexionEmpU());
            adapter.Fill(ds);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return ds;
        }

        [WebMethod(Description="Regresa la Información Personal del Empleado. Se Envia el Alias del Empleado.")]
        public DataSet dsRegresaInfoPersonalEmp(string alias)
        {
            DataSet dsInfoPersonal = new DataSet();
            string consultaInfoPersonalEmpleado = string.Empty;

            consultaInfoPersonalEmpleado = "EXEC   [dbo].[spDSWS_SelectInfoPersonalEmpleado] '" + alias + "'";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaInfoPersonalEmpleado, conexionEmpU());
            adapter.Fill(dsInfoPersonal);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsInfoPersonal;
        }

        [WebMethod(Description = "Regresa la Lista de los Empleados para el Quiz. Se Envia el Número de Empleados que va a Regresar.")]
        public DataSet dsRegresaEmpleadosQuiz(int CantEmp,int Empresa, int EmpExc)
        {
            DataSet dsEmpleadosQuiz = new DataSet();
            string consultaInfoEmpQuiz = string.Empty;
            string FiltroEmpresa = (Empresa == 0) ? "" : " AND Empresas.Id_Empresa = " + Empresa.ToString() + " " ;
            string filtroEmpresaExc = (EmpExc == 0) ? "" : " AND Empresas.Id_Empresa <> " + EmpExc.ToString() + " ";

            try
            {
                consultaInfoEmpQuiz = "SELECT TOP " + CantEmp + " Empresas.Empresa, Empleados.Id_Empleado, Empleados.Nombre, Empleados.Ap_Paterno, " +
                                            "Empleados.Ap_Materno, Areas.Area, Puestos.Puesto, " +
                                            "ISNULL(dbo.fnRegresaNombreJefe(Empleados.Id_JefeDirecto), '') AS Jefe_Inmediato, " +
                                            "YEAR(Nominas_Emp.Fecha_Antiguedad) AS Fecha_Antiguedad, Regiones.Region," +
                                            "ISNULL(Empleados.No_Hijos, 0) AS No_Hijos, dbo.fnRegresaEdoCivil(Empleados.Id_Edo_Civil) AS Edo_Civil," +
                                            "Empleados.Fec_Nac,Empleados.Foto " +
                                       "FROM Regiones INNER JOIN Empleados INNER JOIN Nominas_Emp ON Empleados.Id_Empleado = Nominas_Emp.Id_Empleado " +
                                            "AND Nominas_Emp.Activo = 1 INNER JOIN Empresas ON Empleados.Id_Empresa = Empresas.Id_Empresa INNER JOIN " +
                                            "Puestos ON Nominas_Emp.Id_Puesto = Puestos.Id_Puesto INNER JOIN Areas ON Nominas_Emp.Id_Area = Areas.Id_Area " +
                                            "ON Regiones.Id_Region = Nominas_Emp.id_Region " +
                                       "WHERE Empleados.Foto <> '' " + FiltroEmpresa + filtroEmpresaExc +
                                       "GROUP BY Empleados.Id_Empleado, Empresas.Empresa, Empleados.Nombre, Empleados.Ap_Paterno, Empleados.Ap_Materno," +
                                                "Puestos.Puesto, Areas.Area,Empleados.Id_JefeDirecto, Nominas_Emp.Fecha_Antiguedad, Regiones.Region," +
                                                "Empleados.No_Hijos, Empleados.Id_Edo_Civil, Empleados.Fec_Nac,Empleados.Foto ORDER BY NEWID()";

                conexionEmpU().Open();
                SqlDataAdapter adapter = new SqlDataAdapter(consultaInfoEmpQuiz, conexionEmpU());
                adapter.Fill(dsEmpleadosQuiz);
                SqlConnection.ClearAllPools();
                conexionEmpU().Close();
            }
            catch (Exception ex)
            {
                
            }
            return dsEmpleadosQuiz;
        }

        [WebMethod(Description = "Regresa el DataSet de las Regiones.")]
        public DataSet dsRegresaRegiones()
        {
            DataSet dsRegiones = new DataSet();
            string consultaRegiones = string.Empty;

            consultaRegiones = "EXEC    [dbo].[spDSWS_SelectRegiones]";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaRegiones, conexionEmpU());
            adapter.Fill(dsRegiones);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsRegiones;
        }

        [WebMethod(Description = "Regresa las huellas de los empleados activos filtrando por Oficina. 1 México - MLaurent, 2 México - Viena, 3 México - Proyecto, 4 México - Modelo, 5 Alía, 6 Monterrey, 7 Bajio.")]
        public DataSet dsSelectHuellasEmpleadoOficina(int idOficina)
        {
            DataSet dsOficinasEmpleados = new DataSet();

            string consultaRegiones = string.Empty;

            consultaRegiones = "EXEC    [dbo].[spDSWS_SelectHuellasEmpleadoOficina] " + idOficina;
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaRegiones, conexionEmpU());
            adapter.Fill(dsOficinasEmpleados);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsOficinasEmpleados;
        }

        [WebMethod(Description = "Regresa el catalogo de proyectos.")]
        public DataSet dsSelectProyectos()
        {
            DataSet dsProyectos = new DataSet();
            string consultaProyectos = string.Empty;

            consultaProyectos = "EXEC	[dbo].[spDSWS_SelectProyectos]";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaProyectos, conexionEmpU());
            adapter.Fill(dsProyectos);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsProyectos;
        }

        [WebMethod(Description = "Regresa el catálogo de los niveles de los empleados.")]
        public DataSet dsSelectNiveles()
        {
            DataSet dsNiveles = new DataSet();
            string consulta = string.Empty;

            consulta = "EXEC [dbo].[spDSWS_SelectNivelesEmpleados]";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consulta, conexionEmpU());
            adapter.Fill(dsNiveles);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsNiveles;
        }

        [WebMethod(Description = "Regresa el catálogo de los Consejos.")]
        public DataSet dsSelectConsejos()
        {
            DataSet dsConsejos = new DataSet();
            string consulta = string.Empty;

            consulta = "EXEC [dbo].[spDSWS_SelectConsejos]";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consulta, conexionEmpU());
            adapter.Fill(dsConsejos);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsConsejos;
        }

        [WebMethod(Description = "Regresa las Oficinas Regionales.")]
        public DataSet dsSelectOficinas()
        {
            DataSet dsOficinas = new DataSet();
            string consultaProyectos = string.Empty;

            consultaProyectos = "EXEC	[dbo].[spDSWS_SelectOficinas]";
            conexionEmpU().Open();
            SqlDataAdapter adapter = new SqlDataAdapter(consultaProyectos, conexionEmpU());
            adapter.Fill(dsOficinas);
            SqlConnection.ClearAllPools();
            conexionEmpU().Close();

            return dsOficinas;
        }

        private SqlConnection conexionEmpU()
        {
            string conEmpUni = string.Empty;
            conEmpUni = ConfigurationManager.ConnectionStrings["conEmpUnico"].ConnectionString;
            SqlConnection MiConexion = new SqlConnection(conEmpUni);
            
            return MiConexion;
        }

        private SqlConnection MiConexion()
        {
            string cadena = ConfigurationManager.ConnectionStrings["conEmpUnico"].ConnectionString;
            SqlConnection conexionBD = new SqlConnection(cadena);

            return conexionBD;
        }
    }
}
