using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EticaUNITEC;

namespace EticaUNITEC.Mantenimiento
{
    public partial class MantenimientoInciso : EticaPage
    {

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
        string id;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["id"] != null && ViewState["IncisoId"] == null)
            {
                ViewState["IncisoId"] = Request.Params["id"].ToString();
                id = ViewState["IncisoId"].ToString(); 
                buscarInciso();
            }
           
            
        }

        void buscarLetra()
        {
            string art = Convert.ToString(getArticulos());
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                string sql = @"SELECT * 
                               FROM Incisos 
                               WHERE IncisoLetra ='" + txtLetra.Text + "' AND ArticuloId='" + art + "'";

                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(dt);

                if (dt.Rows.Count > 0) //existe
                {
                    ViewState["ArticuloId"] = dt.Rows[0]["ArticuloId"].ToString();
                   
                    txtCont.Text = dt.Rows[0]["IncisoContenido"].ToString();
                    txtDesc.Text = dt.Rows[0]["IncisoDescripcion"].ToString();

                }

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
       
        void buscarInciso()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                string sql = @"SELECT * 
                               FROM Incisos 
                               WHERE IncisoId ='" + id + "'";

                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(dt);

                if (dt.Rows.Count > 0) //existe
                {
                    ViewState["ArticuloId"] = dt.Rows[0]["ArticuloId"].ToString();
                    txtLetra.Text = dt.Rows[0]["IncisoLetra"].ToString();
                    txtCont.Text = dt.Rows[0]["IncisoContenido"].ToString();
                    txtDesc.Text = dt.Rows[0]["IncisoDescripcion"].ToString();

                }

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

        int getArticulos()
        {
            int articuloID = 0;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                string sql = @"SELECT * 
                               FROM Articulos
                               WHERE ArticuloNumero ='" + Session["ArticID"].ToString() +"'";

                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(dt);

                foreach (DataRow row in dt.Rows) //existe
                {
                    articuloID = Convert.ToInt32( row["ArticuloId"].ToString());
                  

                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
            return articuloID;
        }
       
        void guardarIncisos(SqlConnection con, SqlTransaction trans)
        {
            string artID = Convert.ToString(getArticulos());
           

            if (ViewState["IncisoId"] == null) //insert
            {
                
                string sql = @"INSERT INTO Incisos(IncisoLetra,IncisoContenido,ArticuloId,IncisoDescripcion) 
                                           VALUES ( @inl, @inc,@artid, @ind) ";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.Add(new SqlParameter("inl", txtLetra.Text));
                cmd.Parameters.Add(new SqlParameter("inc", txtCont.Text));
                cmd.Parameters.Add(new SqlParameter("artid",artID));
                cmd.Parameters.Add(new SqlParameter("ind", txtDesc.Text));

                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                string lol = "SELECT @@identity as Codigo";
                SqlDataAdapter adp = new SqlDataAdapter(lol, con);
                DataTable tab = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(tab);

                ViewState["IncisoId"] = tab.Rows[0]["Codigo"].ToString();
            }
            else //update
            {
                string incID = ViewState["IncisoId"].ToString();

                string sql = @"UPDATE Incisos
                               SET IncisoLetra=@inl,
                                   IncisoContenido=@inc,
                                   ArticuloId=@artid,
                                   IncisoDescripcion=@ind
                               WHERE IncisoId=@incId ";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("inl", txtLetra.Text));
                cmd.Parameters.Add(new SqlParameter("inc", txtCont.Text));
                cmd.Parameters.Add(new SqlParameter("artid",artID ));
                cmd.Parameters.Add(new SqlParameter("ind", txtDesc.Text));
                cmd.Parameters.Add(new SqlParameter("incId", incID));

                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            if (!currentUser.ValidarPrivilegio("Insertar Incisos"))
                return;

            bool ver = true;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                guardarIncisos(con, trans);
                trans.Commit();
              

            }
            catch (Exception ex)
            {
                ver = false;
                trans.Rollback();
                Session["error"] = ex.StackTrace;

            }
            con.Close();
             string aid = Convert.ToString(getArticulos());
            if (!ver)
                Response.Redirect("~/Seguridad/Error.aspx");
            else
                Response.Redirect("Inciso.aspx?id=" + aid);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string arid = Convert.ToString(getArticulos());
            Response.Redirect("Inciso.aspx?id="+arid);
        }

        protected void txtLetra_TextChanged(object sender, EventArgs e)
        {
            buscarLetra();
        }


    }
}