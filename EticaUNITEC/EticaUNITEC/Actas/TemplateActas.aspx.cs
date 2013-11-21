using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using FreeTextBoxControls;
using EticaUNITEC;

namespace EticaUNITEC.Seguridad
{
    public partial class Editor : EticaPage
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
       
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarComboCategoria();
            if (IsPostBack && ViewState["TemplateId"] == null)
            {
                buscarTemplate();
            }
            if (Request.Params["id"] != null && ViewState["TemplateId"] == null)
            {
                ViewState["TemplateId"] = Request.Params["id"].ToString();
                CargarDatos();               
            }
            SetPrivilegiosToActualUsuer();
        }

        private void SetPrivilegiosToActualUsuer()
        {
            if (!currentUser.TienePrivilegio("Insertar Plantillas"))
                btnGuardar.Visible = false;
            /*if (!currentUser.TienePrivilegio("Eliminar Usuarios"))
                btnEliminar.Visible = false;*/
        }

        void CargarComboCategoria()
        {
            bool exito = true;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            { 
                string sql = @"SELECT *
                                FROM Categorias";

                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                cmbCategoria.DataSource = dt;
                cmbCategoria.DataTextField = "CategoriaNombre";
                cmbCategoria.DataValueField = "CategoriaId";            
                cmbCategoria.DataBind();
            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                exito = false;
            }
            con.Close();
            if (!exito)
                Response.Redirect("~/Seguridad/Error.aspx");
        }

        void CargarDatos()
        {
            string templateId = ViewState["TemplateId"].ToString();
            
            bool exito = true;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            {
                string sql = @"SELECT *
                            FROM Templates
                            WHERE TemplateId='" + templateId + "'";

                SqlDataAdapter adp= new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                txtTitulo.Text = dt.Rows[0]["TemplateNombre"].ToString();
                cmbCategoria.SelectedValue = dt.Rows[0]["CategoriaId"].ToString();
                txtDescripcion.Text = dt.Rows[0]["TemplateDescripcion"].ToString();
                FreeTextBox1.Text = dt.Rows[0]["TemplateHTML"].ToString();
            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                exito = false;
            }
            con.Close();
            if (!exito)
                Response.Redirect("~/Seguridad/Error.aspx");
        }

        void buscarTemplate()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                string sql = @"SELECT * 
                               FROM Templates 
                               WHERE TemplateNombre ='" + txtTitulo.Text + "'";
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(dt);

                if (dt.Rows.Count > 0) //existe
                {
                    ViewState["TemplateId"] = dt.Rows[0][0].ToString();
                    FreeTextBox1.Text = dt.Rows[0][2].ToString();
                    txtDescripcion.Text = dt.Rows[0][3].ToString();
                    cmbCategoria.SelectedValue = dt.Rows[0][4].ToString();
                    //btnEliminar.Enabled = true;
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

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            bool exito = true;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                guardarTemplate(con, trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Session["error"] = ex.StackTrace;
                exito = false;                
            }
            con.Close();
            if (exito)   
                Response.Redirect("ListaTemplateActas.aspx");
            else
                Response.Redirect("~/Seguridad/Error.aspx");
        }

        void guardarTemplate(SqlConnection con, SqlTransaction trans)
        {
            if (ViewState["TemplateId"] == null) //hacer un insert
            {
                string sql = @"INSERT INTO Templates(TemplateNombre,TemplateHTML,TemplateDescripcion,CategoriaId) 
                               VALUES (@nombre, @html, @descripcion,@catId)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("nombre", txtTitulo.Text));
                cmd.Parameters.Add(new SqlParameter("html", FreeTextBox1.Text));
                cmd.Parameters.Add(new SqlParameter("descripcion", txtDescripcion.Text));
                cmd.Parameters.Add(new SqlParameter("catId", cmbCategoria.SelectedValue));
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
            }
            else //hacer update
            {
                string templateID = ViewState["TemplateId"].ToString();
                string sql = @"UPDATE Templates
                               SET TemplateNombre=@nombre,TemplateHTML=@html,TemplateDescripcion=@descripcion, CategoriaId=@catId
                               WHERE TemplateId=@id ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("nombre", txtTitulo.Text));
                cmd.Parameters.Add(new SqlParameter("html", FreeTextBox1.Text));
                cmd.Parameters.Add(new SqlParameter("descripcion", txtDescripcion.Text));
                cmd.Parameters.Add(new SqlParameter("id", templateID));
                cmd.Parameters.Add(new SqlParameter("catId", cmbCategoria.SelectedValue));
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
            }
        }
      
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ViewState["TemplateId"] = null;
            txtTitulo.Text = "";
            txtDescripcion.Text = "";
            FreeTextBox1.Text = "";
            cmbCategoria.SelectedIndex = 0;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaTemplateActas.aspx");
        }
        
    }
}