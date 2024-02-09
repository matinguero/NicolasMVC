using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace NicolasMVC.Models
{
    public class Noticia
    {

        public int id { get; set; }
        public string Titulo { get; set; }

        public string Copete { get; set; }

        public string Texto { get; set; }

        public int id_categoria { get; set; }

        public string Categoria { get; set; }

        public int iActivo { get; set; }

        public DateTime FechaAlta { get; set; }

        public string Foto { get; set; }

        public byte[] Foto2 { get; set; }




        /// <summary>
        /// Esta funcion hace trae las noticias de la base por categoria y activo
        /// </summary>
        /// <param name="iCategoria">Este el parametro Categoria</param>
        /// <param name="iActivo">Este es el actvo que permite mostrar los activos o todos</param>
        /// <param name="sResult">Parametro por refedrencia de resultado</param>
        /// <returns></returns>
        public static IEnumerable<Noticia> ObtenerNoticias(int iCategoria, int iActivo, ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerNoticias", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@activo", iActivo);
                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@categoria_id", iCategoria);


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);

                //ESTO ES UN COMENTARIO
                //TODO: me falta hacer equis cosa
                List<Models.Noticia> ListaNoticia = new List<Models.Noticia>();

                foreach (DataRow row in dt.Rows)
                {
                    var Noticia = new Noticia();
                    Noticia.id = int.Parse(row["id"].ToString());
                    Noticia.Titulo = row["titulo"].ToString();
                    Noticia.Copete = row["copete"].ToString();
                    Noticia.Foto = row["imagen"].ToString();
                    Noticia.iActivo = int.Parse(row["activo"].ToString());
                    Noticia.Categoria = row["categoria"].ToString();
                    Noticia.FechaAlta = System.DateTime.Parse(row["fecha"].ToString());


                    //LEO EL ARRAY DE BYTES
                    if (row["imagen2"] != DBNull.Value)
                    {
                        byte[] bytes = (byte[])row["imagen2"];
                        Noticia.Foto2 = bytes;
                    }
                    else
                    {
                        Noticia.Foto2 = null;
                    }

                    ListaNoticia.Add(Noticia);
                }


                sResult = "";
                return ListaNoticia;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }



        public static IEnumerable<Noticia> ObtenerNoticias(ref List<Models.Noticia> ListaNoticia, int iCategoria, int iActivo, ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerNoticias", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@activo", iActivo);
                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@categoria_id", iCategoria);


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);

                //ESTO ES UN COMENTARIO
                //TODO: me falta hacer equis cosa
                //List<Models.Noticia> ListaNoticia = new List<Models.Noticia>();

                foreach (DataRow row in dt.Rows)
                {
                    var Noticia = new Noticia();
                    Noticia.id = int.Parse(row["id"].ToString());
                    Noticia.Titulo = row["titulo"].ToString();
                    Noticia.Copete = row["copete"].ToString();
                    Noticia.Foto = row["imagen"].ToString();
                    Noticia.iActivo = int.Parse(row["activo"].ToString());
                    Noticia.Categoria = row["categoria"].ToString();
                    Noticia.FechaAlta = System.DateTime.Parse(row["fecha"].ToString());


                    //LEO EL ARRAY DE BYTES
                    if (row["imagen2"] != DBNull.Value)
                    {
                        byte[] bytes = (byte[])row["imagen2"];
                        Noticia.Foto2 = bytes;
                    }
                    else
                    {
                        Noticia.Foto2 = null;
                    }

                    ListaNoticia.Add(Noticia);
                }


                sResult = "";
                return ListaNoticia;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }



        public static Noticia ObtenerNoticia(int noticia_id, ref string sResult)
        {

            SqlConnection MyConnection = default(SqlConnection);
            SqlDataAdapter MyDataAdapter = default(SqlDataAdapter);

            try
            {
                MyConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSQL"].ConnectionString);
                MyDataAdapter = new SqlDataAdapter("spObtenerNoticia", MyConnection);
                MyDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;


                MyDataAdapter.SelectCommand.Parameters.AddWithValue("@id", noticia_id);


                DataTable dt = new DataTable();
                MyDataAdapter.Fill(dt);


                var Noticia = new Noticia();
                if (dt.Rows.Count == 1)
                {
                    Noticia.id = int.Parse(dt.Rows[0]["id"].ToString());
                    Noticia.Titulo = dt.Rows[0]["titulo"].ToString();
                    Noticia.iActivo = int.Parse(dt.Rows[0]["activo"].ToString());
                    Noticia.Copete = dt.Rows[0]["copete"].ToString();
                    Noticia.Texto = dt.Rows[0]["texto"].ToString();
                    Noticia.Categoria = dt.Rows[0]["categoria"].ToString();
                    Noticia.id_categoria = int.Parse(dt.Rows[0]["categoria_id"].ToString());
                    Noticia.FechaAlta = System.DateTime.Parse(dt.Rows[0]["fecha"].ToString());
                    Noticia.Foto = dt.Rows[0]["Imagen"].ToString();


                    //LEO EL ARRAY DE BYTES
                    if (dt.Rows[0]["imagen2"] != DBNull.Value)
                    {
                        byte[] bytes = (byte[])dt.Rows[0]["imagen2"];
                        Noticia.Foto2 = bytes;
                    }
                    else
                    {
                        Noticia.Foto2 = null;
                    }



                }



                sResult = "";
                return Noticia;
            }
            catch (Exception ex)
            {
                sResult = ex.Message;
                return null;
            }

        }



        public static string ModificarNoticia(Noticia noticia)
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
                MyCommand = new SqlCommand("spModificarNoticia", MyConnection);
                MyCommand.CommandType = CommandType.StoredProcedure;


                MyCommand.Parameters.AddWithValue("@id", noticia.id);
                MyCommand.Parameters.AddWithValue("@Titulo", noticia.Titulo);
                MyCommand.Parameters.AddWithValue("@copete", noticia.Copete);
                MyCommand.Parameters.AddWithValue("@Texto", noticia.Texto);
                MyCommand.Parameters.AddWithValue("@activo", noticia.iActivo);
                MyCommand.Parameters.AddWithValue("@categoria_id", noticia.id_categoria);
                MyCommand.Parameters.AddWithValue("@imagen", noticia.Foto);
                MyCommand.Parameters.AddWithValue("@imagen2", noticia.Foto2);



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




        public static string InsertarNoticia(Noticia noticia)
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
                MyCommand = new SqlCommand("spInsertarNoticia", MyConnection);
                MyCommand.CommandType = CommandType.StoredProcedure;


                //MyCommand.Parameters.AddWithValue("@id", noticia.id);
                MyCommand.Parameters.AddWithValue("@Titulo", noticia.Titulo);
                MyCommand.Parameters.AddWithValue("@copete", noticia.Copete);
                MyCommand.Parameters.AddWithValue("@Texto", noticia.Texto);
                MyCommand.Parameters.AddWithValue("@activo", noticia.iActivo);
                MyCommand.Parameters.AddWithValue("@categoria_id", noticia.id_categoria);
                MyCommand.Parameters.AddWithValue("@imagen", noticia.Foto);
                MyCommand.Parameters.AddWithValue("@imagen2", noticia.Foto2);



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