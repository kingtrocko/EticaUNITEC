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
    public partial class InsertarEvidencia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO Evidencias (FaltaId,EvidenciaContenido,EvidenciaNombreArchivo,EvidenciaMime,EvidenciaDescripcion)
                                       VALUES(@id,@contenido,@nombre,@mime,@descripcion)";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;


            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                byte [] buffer = new byte[ FileUpload1.PostedFile.ContentLength];
                FileUpload1.PostedFile.InputStream.Read(buffer, 0, buffer.Length);

                SqlCommand cmd = new SqlCommand(sql, con, trans);
                cmd.Parameters.Add(  new SqlParameter( "id",TextBox1.Text));
                cmd.Parameters.Add(new SqlParameter("contenido", buffer));
                cmd.Parameters.Add(new SqlParameter("nombre", FileUpload1.PostedFile.FileName));
                cmd.Parameters.Add(new SqlParameter("mime", FileUpload1.PostedFile.ContentType));
                cmd.Parameters.Add(new SqlParameter("descripcion", TextBox2.Text));
                cmd.ExecuteNonQuery();
                trans.Commit();
                        }
            catch (Exception ex)
            {
                trans.Rollback();
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
        }
    }
}