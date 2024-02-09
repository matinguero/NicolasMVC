using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace ProyectoNoticiasNicolas.Utilidades
{
    public class datos
    {

        #region "Categorias"


        public static string ObtenerCategoriasNoticias(ref DataTable dt)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerTodasLasCategorias", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


                //dt = new DataTable();
                MyDataAdapter.Fill(dt);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region "Noticias"

        public static string ObtenerNoticias(int iActivoId, int iCategoria, ref DataTable dt)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerNoticias", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


                MyDataAdapter.SelectCommand.Parameters.Add("@activo", SqlDbType.Int);
                MyDataAdapter.SelectCommand.Parameters["@activo"].Value = iActivoId;

                MyDataAdapter.SelectCommand.Parameters.Add("@categoria_id", SqlDbType.Int);
                MyDataAdapter.SelectCommand.Parameters["@categoria_id"].Value = iCategoria;


                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static string ObtenerNoticia(int id, ref DataTable dt)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerNoticia", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


                MyDataAdapter.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                MyDataAdapter.SelectCommand.Parameters["@id"].Value = id;



                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string EliminarNoticia(int iId)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MyCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyCommand = new SqlCommand("spEliminarNoticia", MyConnection);
                MyCommand.CommandType = CommandType.StoredProcedure;

                //AGREGO EL PARAMETRO CON EL VALOR DEL PARAMETRO AL COMANDO
                MyCommand.Parameters.AddWithValue("@id", iId);

                //ACCIONES A MANO
                MyConnection.Open(); //ABRO CONEXION
                MyCommand.ExecuteNonQuery(); //EJECUTO COMANDO
                MyConnection.Close(); //CIERRO CONEXION
                MyConnection.Dispose(); //DESCARTO CONEXION

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public static string InsertarNoticia(string sTitulo, string sCopete, string sTexto, int iActivo, int icategoria_id, String sImagen)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MyCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyCommand = new SqlCommand("spInsertarNoticia", MyConnection);
                MyCommand.CommandType = CommandType.StoredProcedure;

                //AGREGO EL PARAMETRO CON EL VALOR DEL PARAMETRO AL COMANDO
                MyCommand.Parameters.AddWithValue("@Titulo", sTitulo);
                MyCommand.Parameters.AddWithValue("@copete", sCopete);

                MyCommand.Parameters.AddWithValue("@texto", sTexto);
                MyCommand.Parameters.AddWithValue("@activo", iActivo);

                MyCommand.Parameters.AddWithValue("@categoria_id", icategoria_id);

                MyCommand.Parameters.AddWithValue("@imagen", sImagen);

                MyCommand.Parameters.AddWithValue("@fecha", DBNull.Value);

                //ACCIONES A MANO
                MyConnection.Open(); //ABRO CONEXION
                MyCommand.ExecuteNonQuery(); //EJECUTO COMANDO
                MyConnection.Close(); //CIERRO CONEXION
                MyConnection.Dispose(); //DESCARTO CONEXION

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public static string ModificarNoticia(int iId, string sTitulo, string sCopete, string sTexto, int iActivo, int icategoria_id)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MyCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyCommand = new SqlCommand("spModificarNoticia", MyConnection);
                MyCommand.CommandType = CommandType.StoredProcedure;

                //AGREGO EL PARAMETRO CON EL VALOR DEL PARAMETRO AL COMANDO

                MyCommand.Parameters.AddWithValue("@Id", iId);

                MyCommand.Parameters.AddWithValue("@Titulo", sTitulo);
                MyCommand.Parameters.AddWithValue("@Copete", sCopete);

                MyCommand.Parameters.AddWithValue("@Texto", sTexto);
                MyCommand.Parameters.AddWithValue("@activo", iActivo);

                MyCommand.Parameters.AddWithValue("@categoria_id", icategoria_id);

                MyCommand.Parameters.AddWithValue("fecha", DBNull.Value);

                //ACCIONES A MANO
                MyConnection.Open(); //ABRO CONEXION
                MyCommand.ExecuteNonQuery(); //EJECUTO COMANDO
                MyConnection.Close(); //CIERRO CONEXION
                MyConnection.Dispose(); //DESCARTO CONEXION

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        #endregion

        #region "Usuarios"
        public static string LoginUsuario(string sUsuario, string sClave, ref DataTable dt)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spLogin", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


                MyDataAdapter.SelectCommand.Parameters.Add("@usuario", SqlDbType.VarChar, 50);
                MyDataAdapter.SelectCommand.Parameters["@usuario"].Value = sUsuario;

                MyDataAdapter.SelectCommand.Parameters.Add("@clave", SqlDbType.VarChar, 50);
                MyDataAdapter.SelectCommand.Parameters["@clave"].Value = sClave;



                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string ObtenerUsuariosRegistrados(ref DataTable dt)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerUsuarios", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string ObtenerCategoriasUsuarios(ref DataTable dt)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerPerfiles", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                MyDataAdapter.Fill(dt);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        

        public static string ObtenerUsuario(int iId, ref DataTable dt)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerUsuario", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


                MyDataAdapter.SelectCommand.Parameters.Add("@id", SqlDbType.Int);
                MyDataAdapter.SelectCommand.Parameters["@id"].Value = iId;


                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



      



        public static string ModificarUsuario(string sEmail, string sClave, string sNombre, string sApellido, int iPerfil_id, int iId)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MyCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyCommand = new SqlCommand("spActualizarUsuario", MyConnection);
                MyCommand.CommandType = CommandType.StoredProcedure;

                //AGREGO EL PARAMETRO CON EL VALOR DEL PARAMETRO AL COMANDO
                MyCommand.Parameters.AddWithValue("@email", sEmail);

                MyCommand.Parameters.AddWithValue("@clave", sClave);

                MyCommand.Parameters.AddWithValue("@nombre", sNombre);

                MyCommand.Parameters.AddWithValue("@apellido", sApellido);

                MyCommand.Parameters.AddWithValue("@perfil_id", iPerfil_id);

                MyCommand.Parameters.AddWithValue("@id", iId);


                
                //MyCommand.Parameters.AddWithValue("@foto", sFoto);

                //ACCIONES A MANO
                MyConnection.Open(); //ABRO CONEXION
                MyCommand.ExecuteNonQuery(); //EJECUTO COMANDO
                MyConnection.Close(); //CIERRO CONEXION
                MyConnection.Dispose(); //DESCARTO CONEXION

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public static string ObtenerPerfiles(ref DataTable dt)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerPerfiles", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                //dt = new DataTable();
                MyDataAdapter.Fill(dt);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string InsertarUsuario(string sEmail, string sClave, string sNombre, string sApellido, string sFecha, int iPerfil_id)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MyCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyCommand = new SqlCommand("spInsertarUsuario", MyConnection);
                MyCommand.CommandType = CommandType.StoredProcedure;

                //AGREGO EL PARAMETRO CON EL VALOR DEL PARAMETRO AL COMANDO
                //MyCommand.Parameters.AddWithValue("@id", iId);

                MyCommand.Parameters.AddWithValue("@email", sEmail);

                MyCommand.Parameters.AddWithValue("@clave", sClave);

                MyCommand.Parameters.AddWithValue("@nombre", sNombre);

                MyCommand.Parameters.AddWithValue("@apellido", sApellido);

                MyCommand.Parameters.AddWithValue("@fecha", DBNull.Value);

                MyCommand.Parameters.AddWithValue("@perfil_id", iPerfil_id);

                


                //ACCIONES A MANO
                MyConnection.Open(); //ABRO CONEXION
                MyCommand.ExecuteNonQuery(); //EJECUTO COMANDO
                MyConnection.Close(); //CIERRO CONEXION
                MyConnection.Dispose(); //DESCARTO CONEXION

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


        public static string EliminarUsuario(int iId)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MyCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyCommand = new SqlCommand("spEliminarUsuario", MyConnection);
                MyCommand.CommandType = CommandType.StoredProcedure;

                //AGREGO EL PARAMETRO CON EL VALOR DEL PARAMETRO AL COMANDO
                MyCommand.Parameters.AddWithValue("@id", iId);

                //ACCIONES A MANO
                MyConnection.Open(); //ABRO CONEXION
                MyCommand.ExecuteNonQuery(); //EJECUTO COMANDO
                MyConnection.Close(); //CIERRO CONEXION
                MyConnection.Dispose(); //DESCARTO CONEXION

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        

        //public static string ActualizarUsuarioAlternativoSP(int iId, string sNombre, string sApellido, string sMail, int iPerfilID)
        //{
        //    SqlConnection MyConnection = default(SqlConnection);
        //    SqlCommand MyCommand = default(SqlCommand);

        //    try
        //    {
        //        MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
        //        MyCommand = new SqlCommand("spActualizarusuariosinpass", MyConnection);
        //        MyCommand.CommandType = CommandType.StoredProcedure;

        //        //AGREGO EL PARAMETRO CON EL VALOR DEL PARAMETRO AL COMANDO
        //        MyCommand.Parameters.AddWithValue("@Email", sMail);

               

        //        MyCommand.Parameters.AddWithValue("@nombre", sNombre);

        //        MyCommand.Parameters.AddWithValue("@apellido", sApellido);

               

        //        MyCommand.Parameters.AddWithValue("@Perfil_id", iPerfilID);

        //        MyCommand.Parameters.AddWithValue("@id", iId);

        //        //ACCIONES A MANO
        //        MyConnection.Open(); //ABRO CONEXION
        //        MyCommand.ExecuteNonQuery(); //EJECUTO COMANDO
        //        MyConnection.Close(); //CIERRO CONEXION
        //        MyConnection.Dispose(); //DESCARTO CONEXION

        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        // }

        


        public static string ActualizarUsuario(string sEmail, string sClave, string sNombre, string sApellido, int iPerfil_id, int iId)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MyCommand = default(SqlCommand);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyCommand = new SqlCommand("spActualizarUsuario", MyConnection);
                MyCommand.CommandType = CommandType.StoredProcedure;

                //AGREGO EL PARAMETRO CON EL VALOR DEL PARAMETRO AL COMANDO
                MyCommand.Parameters.AddWithValue("@id", iId);

                MyCommand.Parameters.AddWithValue("@nombre", sNombre);

                MyCommand.Parameters.AddWithValue("@apellido", sApellido);

                MyCommand.Parameters.AddWithValue("@Email", sEmail);

                MyCommand.Parameters.AddWithValue("@Clave", sClave);

                MyCommand.Parameters.AddWithValue("@Perfil_id", iPerfil_id);

                //ACCIONES A MANO
                MyConnection.Open(); //ABRO CONEXION
                MyCommand.ExecuteNonQuery(); //EJECUTO COMANDO
                MyConnection.Close(); //CIERRO CONEXION
                MyConnection.Dispose(); //DESCARTO CONEXION

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


        #endregion

        #region "SORTING / PAGING"
        public static void SortGridView(ref string sSortExpression, ref string sDirection)
            {

                if (!string.IsNullOrEmpty(sSortExpression) | !string.IsNullOrEmpty(sDirection))
                {
                    string myDirection = sDirection.Trim();

                    if (string.IsNullOrEmpty(myDirection))
                    {
                        myDirection = System.Web.UI.WebControls.SortDirection.Descending.ToString();
                    }
                    else
                    {
                        if (myDirection == "ASC")
                        {
                            myDirection = System.Web.UI.WebControls.SortDirection.Descending.ToString();
                        }
                        else
                        {
                            myDirection = System.Web.UI.WebControls.SortDirection.Ascending.ToString();
                        }
                    }

                    switch (myDirection.ToUpper().Trim())
                    {
                        case "ASCENDING":
                            sDirection = " ASC";
                            break;
                        case "DESCENDING":
                            sDirection = " DESC";
                            break;
                    }


                }

            }



        #endregion
        //TODO: FRON-END
    }
}
