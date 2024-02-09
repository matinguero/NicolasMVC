using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NicolasMVC.Models
{
    public class Usuarios
    {
        public int id { get; set; }

        //[Required(ErrorMessage = "El campo email es requerido")]
        //[Display(Name = "Mail")]
        public string email { get; set; }

        //[Required(ErrorMessage = "El campo clave es requerido")]
        public string clave { get; set; }
 
       //[Required(ErrorMessage = "El campo apellido es requerido")]
        public string apellido { get; set; }

        //[Required(ErrorMessage = "El campo nombre es requerido")]
        public string nombre { get; set; }

        public string fechaAlta { get; set; }

        //[Required(ErrorMessage = "El campo Perfil es requerido")]
        public int perfil_id { get; set; }

        public static IEnumerable<Usuarios> ObtenerUsuarios(ref List<Models.Usuarios> ListaUsuarios, int iPerfil, ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerUsuarios", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@perfil_id", iPerfil);


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    var Usuario = new Usuarios();

                    Usuario.id = int.Parse(row["id"].ToString());
                    Usuario.apellido = row["apellido"].ToString();
                    Usuario.nombre = row["nombre"].ToString();
                    Usuario.email = row["email"].ToString();
                    Usuario.fechaAlta = row["FechaAlta"].ToString();
                    Usuario.perfil_id = int.Parse(row["perfil_id"].ToString());

                    //Usuario.foto = row["foto"].ToString();

                    //if (row["foto_bytes"] != DBNull.Value)
                    //{
                    //    Usuario.FotoBytes = (byte[])row["foto_bytes"];
                    //}

                    ListaUsuarios.Add(Usuario);
                }


                sResult = "";
                return ListaUsuarios;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }

        public static bool TieneAccesoAdministracion(int iPerfilID, string sVista)
        {
            bool bRet = false;

            switch (iPerfilID)
            {
                case 1:
                    bRet = true;

                    break;
                case 2:
                    if (sVista.ToString().Contains("Usuarios/"))
                    {
                        bRet = false;
                    }

                    break;
            }



            return bRet;
        }

        public static Usuarios ObtenerDetalleUsuario(int iUsuarioID, ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerUsuario", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@id", iUsuarioID);


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);


                if (dt.Rows.Count == 1)
                {
                    var Usuario = new Usuarios();

                    Usuario.id = int.Parse(dt.Rows[0]["id"].ToString());
                    Usuario.apellido = dt.Rows[0]["apellido"].ToString();
                    Usuario.nombre = dt.Rows[0]["nombre"].ToString();
                    Usuario.email = dt.Rows[0]["email"].ToString();
                    Usuario.fechaAlta = dt.Rows[0]["FechaAlta"].ToString();
                    Usuario.clave = dt.Rows[0]["clave"].ToString();
                    Usuario.perfil_id = int.Parse(dt.Rows[0]["perfil_id"].ToString());

                    sResult = "";
                    return Usuario;
                }
                else
                {
                    throw new Exception("No existe el Usuario");
                }


            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }

        public static string InsertarUsuario(Usuarios usuario)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MyCommand = default(SqlCommand);


            string sRetValid = "";

            //sRetValid = ValidarFormulario(usuario);

            if (sRetValid != "")
            {
                return sRetValid;
            }



            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyCommand = new SqlCommand("spInsertarUsuario", MyConnection);
                MyCommand.CommandType = CommandType.StoredProcedure;


                //MyCommand.Parameters.AddWithValue("@id", usuario.id);
                MyCommand.Parameters.AddWithValue("@nombre", usuario.nombre);
                MyCommand.Parameters.AddWithValue("@apellido", usuario.apellido);
                MyCommand.Parameters.AddWithValue("@fechaAlta", System.DateTime.Now);
                MyCommand.Parameters.AddWithValue("@email", usuario.email);
                MyCommand.Parameters.AddWithValue("@clave", usuario.clave);
                MyCommand.Parameters.AddWithValue("@perfil_id", usuario.perfil_id);



                MyConnection.Open();
                int rowsAffected = MyCommand.ExecuteNonQuery();
                MyConnection.Close();
                MyConnection.Dispose();

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public static string ModificarUsuario(Usuarios usuario)
        {
            SqlConnection MyConnection = default(SqlConnection);
            SqlCommand MyCommand = default(SqlCommand);


            string sRetValid = "";

            //sRetValid = ValidarFormulario(usuario);

            if (sRetValid != "")
            {
                return sRetValid;
            }



            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyCommand = new SqlCommand("spActualizarUsuario", MyConnection);
                MyCommand.CommandType = CommandType.StoredProcedure;


                MyCommand.Parameters.AddWithValue("@id", usuario.id);
                MyCommand.Parameters.AddWithValue("@nombre", usuario.nombre);
                MyCommand.Parameters.AddWithValue("@apellido", usuario.apellido);
                MyCommand.Parameters.AddWithValue("@fechaAlta", System.DateTime.Now);
                MyCommand.Parameters.AddWithValue("@email", usuario.email);
                MyCommand.Parameters.AddWithValue("@clave", usuario.clave);
                MyCommand.Parameters.AddWithValue("@perfil_id", usuario.perfil_id);



                MyConnection.Open();
                int rowsAffected = MyCommand.ExecuteNonQuery();
                MyConnection.Close();
                MyConnection.Dispose();

                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


    }
}