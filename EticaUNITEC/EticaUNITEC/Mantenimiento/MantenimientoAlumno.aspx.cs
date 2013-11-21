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
    public partial class NnuevoAlumno : EticaPage
    {
        string connect = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["id"] != null && ViewState["AlId"] == null)
            {
                ViewState["AlId"] = Request.Params["id"].ToString();
                string id = ViewState["AlId"].ToString();
                Cargar(id);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Alumnos.aspx");
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!currentUser.ValidarPrivilegio("Insertar Alumnos"))
                return;

            SqlConnection con = new SqlConnection(connect);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            
            try
            {
                if (ViewState["AlId"] == null)
                {
                    string sql = @"INSERT INTO Alumnos
                               (AlumnoNombre,AlumnoCuenta,AlumnoGenero,CarreraId) 
                                VALUES (@AlNombre, @AlCuenta, @AlGenero, @CaId)";

                    SqlCommand cmd = new SqlCommand(sql, con, trans);

                    cmd.Parameters.Add(new SqlParameter("AlNombre", TextNombre.Text));
                    cmd.Parameters.Add(new SqlParameter("AlCuenta", TextCuenta.Text));
                    cmd.Parameters.Add(new SqlParameter("AlGenero", ComboGenero.SelectedItem.Value));
                    cmd.Parameters.Add(new SqlParameter("CaId", ComboCarrera.SelectedValue));
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
                    string AlID = ViewState["AlId"].ToString();
                    string sql = @"UPDATE Alumnos
                               SET AlumnoNombre=@AlNombre, AlumnoCuenta=@AlCuenta, AlumnoGenero=@AlGenero, CarreraId=@CaId
                               WHERE AlumnoId=@AlId ";

                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("AlId", AlID));
                    cmd.Parameters.Add(new SqlParameter("AlNombre", TextNombre.Text));
                    cmd.Parameters.Add(new SqlParameter("AlCuenta", TextCuenta.Text));
                    cmd.Parameters.Add(new SqlParameter("AlGenero", ComboGenero.SelectedItem.Value));
                    cmd.Parameters.Add(new SqlParameter("CaId", ComboCarrera.SelectedValue));

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
            Response.Redirect("Alumnos.aspx");
        }

        void Cargar(string id)
        {
            SqlConnection con = new SqlConnection(connect);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                string sql = @"SELECT * 
                               FROM Alumnos 
                               WHERE AlumnoId ='" + id + "'";

                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable table = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(table);

                if (table.Rows.Count > 0)
                {
                    TextId.Text = table.Rows[0]["AlumnoId"].ToString();
                    TextNombre.Text = table.Rows[0]["AlumnoNombre"].ToString();
                    TextCuenta.Text = table.Rows[0]["AlumnoCuenta"].ToString();
                    ComboGenero.SelectedValue = table.Rows[0]["AlumnoGenero"].ToString();
                    ComboCarrera.SelectedValue = table.Rows[0]["CarreraId"].ToString();
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