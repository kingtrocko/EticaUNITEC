using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace EticaUNITEC
{
    public partial class Imprimir : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (Request.Params["id"] == null)
            {
                Response.Output.Write(" Envieme un parametro xD");
            }
            else
            {
                if (!IsPostBack)
                {
                    if (Request.Params["id"] != "-1")
                    {
                        cargarDatos();
                    }
                }
            }




        }

        public void cargarDatos() 
        {
            SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();
            string querry = @"SELECT * FROM Faltas ";
            SqlDataAdapter adp = new SqlDataAdapter(querry, conexion);
            DataTable table = new DataTable();
            adp.Fill(table);

            foreach (DataRow x in table.Rows)
            {
                if (x[0].ToString() == Request.Params["id"].ToString())
                {
                    txtTituloFalta.Text = x["FaltaTitulo"].ToString();
                    txtNumeroActa.Text = x["FaltaNumeroActa"].ToString();
                    if (x["CategoriaId"].ToString() == "1")
                    {
                        txtCategoria.Text = "Leve";
                    }

                    if (x["CategoriaId"].ToString() == "2")
                    {
                        txtCategoria.Text = "Grave";
                    }
                    else
                    {
                        txtCategoria.Text = "MuyGrave";
                    }
                }
            }

            conexion.Close();

        
        }

        
        protected void cmbPlantilla_TextChanged(object sender, EventArgs e)
        {
            FreeTextBox1.Text = "";
            string valuePlantilla = cmbPlantilla.SelectedValue;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string sql = @"SELECT * FROM Templates WHERE TemplateId='" + valuePlantilla + "'";

            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            FreeTextBox1.Text = dt.Rows[0]["TemplateHTML"].ToString();
            con.Close();
            RemplazarDatos();

        }

        public void RemplazarDatos() 
        {
            string contenido = " ";
            SqlConnection conexion = new SqlConnection(connectionString);
            conexion.Open();
            string querry = @"SELECT * FROM InformacionFaltas";
            SqlDataAdapter adp = new SqlDataAdapter(querry, conexion);
            DataTable Faltas = new DataTable();
            adp.Fill(Faltas);
            foreach ( DataRow x in Faltas.Rows){

                if (x["FaltaId"].ToString() == Request.Params["id"].ToString())
                {
                    contenido = FreeTextBox1.Text;
                    contenido = contenido.Replace("{AlumnoNombre}", x["AlumnoNombre"].ToString());
                    contenido = contenido.Replace("{AlumnoCuenta}", x["AlumnoCuenta"].ToString());
                    contenido = contenido.Replace("{AlumnoGenero}", x["AlumnoGenero"].ToString());
                    contenido = contenido.Replace("{ArticuloNumero}", x["ArticuloNumero"].ToString());
                    contenido = contenido.Replace("{CarreraId}", x["CarreraId"].ToString());
                    contenido = contenido.Replace("{CarreraCodigo}", x["CarreraCodigo"].ToString());
                    contenido = contenido.Replace("{CarreraNombre}", x["CarreraNombre"].ToString());
                    contenido = contenido.Replace("{CategoriaNombre}", x["CategoriaNombre"].ToString());
                    contenido = contenido.Replace("{FaltaFecha}", x["FaltaFecha"].ToString());
                    contenido = contenido.Replace("{FaltaNumeroActa}", x["FaltaNumeroActa"].ToString());
                    contenido = contenido.Replace("{FaltaSancionDescripcion}", x["FaltaSancionDescripcion"].ToString());
                    contenido = contenido.Replace("{FaltaSancionTiempo}", x["FaltaSancionTiempo"].ToString());
                    contenido = contenido.Replace("{SancionNombre}", x["SancionNombre"].ToString());
                    contenido = contenido.Replace("{IncisoLetra}", x["IncisoLetra"].ToString());
                    contenido = contenido.Replace("{IncisoContenido}", x["IncisoContenido"].ToString());
                    contenido = contenido.Replace("{IncisoDescripcion}", x["IncisoDescripcion"].ToString());
                    contenido = contenido.Replace("{FaltaTitulo}", x["FaltaTitulo"].ToString());
                }
          
        
            }


            conexion.Close();

            FreeTextBox1.Text = " ";
            FreeTextBox1.Text = contenido;
        }

        protected void FreeTextBox1_TextChanged(object sender, EventArgs e)
        {
            
            RemplazarDatos();
            //Response.Output.Write("Se ha cambiado");
        }

        protected void txtCambios_Click(object sender, EventArgs e)
        {
            RemplazarDatos();
        }

        
    }
}