using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NicolasMVC.Models
{
    
        public class Cuenta
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "El campo usuario es requerido")]
            public string Usuario { get; set; }

            [Required(ErrorMessage = "El campo clave es requerido")]
            public string Clave { get; set; }

            public string Nombre { get; set; }


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


                    //dt = new DataTable();
                    MyDataAdapter.Fill(dt);
                    return "";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }



            public static string BuscarUsuarioPorMail(string sUsuario, ref DataTable dt)
            {
                SqlConnection MyConnection = default(SqlConnection);
                SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

                try
                {
                    MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                    MyDataAdapter = new SqlDataAdapter("spObtenerMail2", MyConnection);
                    MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


                    MyDataAdapter.SelectCommand.Parameters.Add("@mail", SqlDbType.VarChar, 50);
                    MyDataAdapter.SelectCommand.Parameters["@mail"].Value = sUsuario;


                    //dt = new DataTable();
                    MyDataAdapter.Fill(dt);
                    return "";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }






        }

    
}