﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using EticaUNITEC;

namespace Etica_Unitec
{
    public partial class PrivilegioInicio : EticaPage
    {
        string cmstring = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
                CargarGrid();

            privilegio = "Consultar Privilegios";
            SetPrivilegiosToActualUsuer();
        }

        private void SetPrivilegiosToActualUsuer()
        {
            if (!currentUser.TienePrivilegio("Insertar Privilegios"))
                btnAdd.Visible = false;
            if (!currentUser.TienePrivilegio("Modificar Privilegios"))
                gridPrivilegio.Columns[4].Visible = false;
            if (!currentUser.TienePrivilegio("Eliminar Privilegios"))
                gridPrivilegio.Columns[3].Visible = false;

        }



        void CargarGrid()
        {

            SqlConnection con = new SqlConnection(cmstring);
            con.Open();
            string sql = "SELECT PrivilegioId,PrivilegioLlave,PrivilegioDescripcion FROM Privilegios";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con); 
            DataTable dt = new DataTable();
            adap.Fill(dt);
            con.Close();

           gridPrivilegio.DataSource = dt;
           gridPrivilegio.DataBind();
            
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!currentUser.ValidarPrivilegio("Insertar Privilegios"))
                return;
            Response.Redirect("~/Seguridad/NuevoRegistro.aspx");
        }

        protected void gridPrivilegio_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            int idSelect = Convert.ToInt32(gridPrivilegio.DataKeys[e.RowIndex].Values[0]);

            Eliminar(idSelect);
            CargarGrid();
        }

        void Eliminar(int id)
        {

            SqlConnection con = new SqlConnection(cmstring);
            try{
            StringBuilder sql = new StringBuilder("");
            sql.Append("DELETE FROM Privilegios ");
            sql.Append("WHERE PrivilegioId="+id);
             
            SqlCommand cmd = new SqlCommand(sql.ToString(), con);
            con.Open();
            int IdSelect=cmd.ExecuteNonQuery();
            if(IdSelect!=1){
                throw new ApplicationException("No Se Elimino El Registro!!");
            }
            }catch(Exception e){
                con.Close();
                Session["error"] = e.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
                throw new ApplicationException(e.Message);

            }
            finally
            {
                con.Close();
            }
        }

       

        protected void gridPrivilegio_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                LinkButton lb = (LinkButton)e.CommandSource;
                // gridPrivilegio.EditIndex = e.NewEditIndex;
                Response.Redirect("~/Seguridad/ModificarRegistro.aspx?userid=" + lb.Text + " ");
                //CargarGrid();
            }
        }

        protected void gridPrivilegio_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.Header || e.Row.RowType != DataControlRowType.Header)
            {
                e.Row.Attributes.Add("onmouseout", "CssClass='normalRow'");
                e.Row.Attributes.Add("onmouseover","CssClass='highlightRow'");
            }
        }

        protected void gridPrivilegio_PageIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gridPrivilegio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridPrivilegio.PageIndex = e.NewPageIndex;
            CargarGrid();

        }

       

    }
}