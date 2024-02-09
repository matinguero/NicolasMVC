using NicolasMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NicolasMVC.Controllers
{
    public class CategoriaNoticia
    {
        public int id { get; set; }
        public string descripcion { get; set; }




        public static IEnumerable<CategoriasNoticia> ObtenerCategoriasNoticias(ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerTodasLasCategorias", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);


                List<Models.CategoriasNoticia> ListaCategorias = new List<Models.CategoriasNoticia>();

                foreach (DataRow row in dt.Rows)
                {
                    var CatNoticia = new CategoriasNoticia();
                    CatNoticia.id = int.Parse(row["id"].ToString());
                    CatNoticia.descripcion = row["Categoria"].ToString();



                    ListaCategorias.Add(CatNoticia);
                }


                sResult = "";
                return ListaCategorias;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }

    }
}