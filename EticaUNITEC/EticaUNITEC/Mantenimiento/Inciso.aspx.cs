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
    public partial class Inciso : EticaPage
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
        string id;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (Request.Params["id"] != null && ViewState["ArticulosId"] == null)
            {
                ViewState["ArticulosId"] = Request.Params["id"].ToString();
                id = ViewState["ArticulosId"].ToString();
                 VerificarArt(id);
            }
            if (!IsPostBack)
            {
                InicioGrid();
                gridInc.Columns[2].Visible = false;
                gridInc.Columns[3].Visible = false;
                
            }
            txtArt.ReadOnly = true;

            privilegio = "Consultar Incisos";
            SetPrivilegiosToActualUser();
        }

        protected void SetPrivilegiosToActualUser()
        {
            if (!currentUser.TienePrivilegio("Modificar Incisos"))
                gridInc.Columns[0].Visible = false;
            if (!currentUser.TienePrivilegio("Eliminar Incisos"))
                gridInc.Columns[1].Visible = false;
            if (!currentUser.TienePrivilegio("Insertar Incisos"))
                btnNuevo.Visible = false;
        }

        void VerificarArt(string id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                string sql = @"SELECT * 
                               FROM Articulos 
                               WHERE ArticuloId ='" + id + "'";
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(dt);

                if (dt.Rows.Count > 0) //existe
                {
                    txtArt.Text = dt.Rows[0]["ArticuloNumero"].ToString();
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

        void InicioGrid()
        {
            
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            {
                string sql = @"SELECT IncisoId,IncisoLetra,IncisoContenido,ArticuloId,IncisoDescripcion
                               FROM Incisos WHERE ArticuloId='" + id + "'";

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                gridInc.DataSource = reader;
                gridInc.DataBind();

            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
        }

        protected void btnReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("Articulo.aspx");
        }

        protected void gridInc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int i = e.NewEditIndex;
            GridViewRow row = gridInc.Rows[i];
            string incId = row.Cells[3].Text;
            
            Response.Redirect("~/Mantenimiento/MantenimientoInciso.aspx?id=" + incId);
        }

        protected void gridInc_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = e.RowIndex;
            GridViewRow row = gridInc.Rows[i];
            string incId = row.Cells[3].Text;
            SqlConnection con = new SqlConnection(connectionString);
           
            con.Open();
            
            try
            {
                string sql = "DELETE Incisos WHERE IncisoId='" + incId + "'";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                Response.Redirect("~Seguridad/Error.aspx");
            }
            
            con.Close();

            InicioGrid();            

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Session["ArticID"]=txtArt.Text;
       Response.Redirect("~/Mantenimiento/MantenimientoInciso.aspx" );
        }
    }
}