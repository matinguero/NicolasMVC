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
    public class PerfilesUsuario
    {
        public int id { get; set; }

        [Required(ErrorMessage = "El campo Descripción es requerido")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }


        public static IEnumerable<PerfilesUsuario> ObtenerPerfiles(ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerPerfiles", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);


                List<Models.PerfilesUsuario> ListaPerfiles = new List<Models.PerfilesUsuario>();

                foreach (DataRow row in dt.Rows)
                {
                    var Perfil = new PerfilesUsuario();
                    Perfil.id = int.Parse(row["id"].ToString());
                    Perfil.Descripcion = row["descripcion"].ToString();


                    ListaPerfiles.Add(Perfil);
                }


                sResult = "";
                return ListaPerfiles;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }
    }
}