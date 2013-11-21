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
    public partial class Articulo : EticaPage
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
            {
                InicioGrid();
                grid.Columns[3].Visible = false;
            }

             privilegio = "Consultar Articulos";
             SetPrivilegiosToActualUser();

        }

        protected void SetPrivilegiosToActualUser()
        {
            if (!currentUser.TienePrivilegio("Modificar Articulos"))
                grid.Columns[4].Visible = false;
            if (!currentUser.TienePrivilegio("Eliminar Articulos"))
                grid.Columns[5].Visible = false;
            if (!currentUser.TienePrivilegio("Insertar Articulos"))
                btnNuevo.Visible = false;
        }

        void InicioGrid()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            {
                string sql = @"SELECT ArticuloId,ArticuloNumero,CategoriaNombre
                               FROM InformacionArticulos";

                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                grid.DataSource = reader;
                grid.DataBind();

            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/MantenimientoArticulo.aspx");
        }

        protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int i = e.NewEditIndex;
            GridViewRow row = grid.Rows[i];
            string artId = row.Cells[3].Text;
           
                Response.Redirect("~/Mantenimiento/MantenimientoArticulo.aspx?id=" + artId);
            
          
        }
      

        protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            int i = e.RowIndex;
            GridViewRow row = grid.Rows[i];
            string artId = row.Cells[3].Text;
           // string num = row.Cells[4].Text;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
          

            try
            {
                
             
                string sql= @"DELETE Incisos FROM Incisos I INNER JOIN Articulos A 
                               ON I.[ArticuloId]=A.[ArticuloId] WHERE A.ArticuloId='" + artId + "'";
                sql += @"DELETE Articulos WHERE ArticuloId='" + artId + "'";
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

        protected void grid_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int i = e.NewSelectedIndex;
            GridViewRow row = grid.Rows[i];
            string artId = row.Cells[3].Text;

            Response.Redirect("~/Mantenimiento/Inciso.aspx?id=" + artId);
        }
    }
}