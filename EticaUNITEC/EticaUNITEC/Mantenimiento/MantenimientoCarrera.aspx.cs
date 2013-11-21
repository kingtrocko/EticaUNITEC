using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EticaUNITEC;

namespace EticaUNITEC
{
    public partial class MantenimientoCarrera : EticaPage
    {
        string connect = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] != null && ViewState["CarId"] == null)
            {
                ViewState["CarId"] = Request.Params["id"].ToString();
                string id = ViewState["CarId"].ToString();
                Cargar(id);
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!currentUser.ValidarPrivilegio("Insertar Carreras"))
                return;


            SqlConnection con = new SqlConnection(connect);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            
            try
            {
                if (ViewState["CarId"] == null)
                {
                    string sql = @"INSERT INTO Carreras
                               (CarreraCodigo, CarreraNombre) 
                                VALUES (@CarCod, @CarNom)";

                    SqlCommand cmd = new SqlCommand(sql, con, trans);

                    cmd.Parameters.Add(new SqlParameter("CarCod", TextCodigo.Text));
                    cmd.Parameters.Add(new SqlParameter("CarNom", TextNombre.Text));

                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();

                    string lol = "SELECT @@identity as Codigo";
                    SqlDataAdapter adp = new SqlDataAdapter(lol, con);
                    DataTable tab = new DataTable();
                    adp.SelectCommand.Transaction = trans;
                    adp.Fill(tab);
                }
                else
                {
                    string CarID = ViewState["CarId"].ToString();
                    string sql = @"UPDATE Carreras
                               SET CarreraCodigo=@CarCod, CarreraNombre=@CarNom
                               WHERE CarreraId=@CarId ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("CarId", CarID));
                    cmd.Parameters.Add(new SqlParameter("CarCod", TextCodigo.Text));
                    cmd.Parameters.Add(new SqlParameter("CarNom", TextNombre.Text));

                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
                throw new ApplicationException(ex.Message);
            }
            finally
            {
                con.Close();
            }
            Response.Redirect("Carreras.aspx");
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Carreras.aspx");
        }

        void Cargar(string id)
        {
            SqlConnection con = new SqlConnection(connect);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                string sql = @"SELECT * 
                               FROM Carreras 
                               WHERE CarreraId ='" + id + "'";

                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable table = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(table);

                if (table.Rows.Count > 0)
                {
                    TextId.Text = table.Rows[0]["CarreraId"].ToString();
                    TextCodigo.Text = table.Rows[0]["CarreraCodigo"].ToString();
                    TextNombre.Text = table.Rows[0]["CarreraNombre"].ToString();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            finally
            {
                con.Close();
            }
        }
    }
}