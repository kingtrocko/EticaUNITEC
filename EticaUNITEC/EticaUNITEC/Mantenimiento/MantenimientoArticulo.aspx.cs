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
    public partial class MantenimientoArticulo : EticaPage
    {

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtNum.Focus();
                cbCateg.SelectedIndex = 0;

              
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                try
                {
                    CargarCombo(con);
                   
                }
                catch (Exception ex)
                {
                    Session["error"] = ex.StackTrace;
                    Response.Redirect("~/Seguridad/Error.aspx");
                }
                con.Close();
            }
            if (Request.Params["id"] != null && ViewState["ArtId"] == null)
            {
                ViewState["ArtId"] = Request.Params["id"].ToString();
                string id = ViewState["ArtId"].ToString();
                Modificar(id);
            }
            
        }
        void CargarCombo(SqlConnection con)
        {
            string sql = @"SELECT * FROM Categorias";

            SqlDataAdapter adpCat = new SqlDataAdapter(sql, con);
            DataTable dtCat = new DataTable();
            adpCat.Fill(dtCat);
            cbCateg.DataSource = dtCat;
            cbCateg.DataValueField = "CategoriaId";
            cbCateg.DataTextField = "CategoriaNombre";
            cbCateg.DataBind();
        }

        protected void btnReg_Click(object sender, EventArgs e)
        {
            Response.Redirect("Articulo.aspx");
        }
               
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!currentUser.ValidarPrivilegio("Insertar Articulos"))
                return;

            bool ver=true;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                guardarArticulos(con, trans);
                trans.Commit();
                Limpiar();

            }
            catch (Exception ex)
            {
                ver = false;
                trans.Rollback();
                Session["error"] = ex.StackTrace;

            }
            con.Close();

            if (!ver)
                Response.Redirect("~/Seguridad/Error.aspx");
            else
                Response.Redirect("Articulo.aspx");
        }
        
        void Limpiar()
        {
            txtNum.Text = "";
            cbCateg.SelectedIndex = 0;
        }
       
        void guardarArticulos( SqlConnection con,SqlTransaction trans)
        {
            if (ViewState["ArtId"] == null) //insert
            {
                string sql = @"INSERT INTO Articulos(ArticuloNumero,CategoriaId) 
                               VALUES ( @artnum, @catid)";

                SqlCommand cmd = new SqlCommand(sql, con);
              
                cmd.Parameters.Add(new SqlParameter("artnum", txtNum.Text));
                cmd.Parameters.Add(new SqlParameter("catid", cbCateg.SelectedValue));
           
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                string lol = "SELECT @@identity as Codigo";
                SqlDataAdapter adp = new SqlDataAdapter(lol, con);
                DataTable tab = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(tab);

                ViewState["ArtId"] = tab.Rows[0]["Codigo"].ToString();
            }
            else //update
            {
                string articuloID = ViewState["ArtId"].ToString();
                string sql = @"UPDATE Articulos
                               SET ArticuloNumero=@ArtNum,CategoriaId=@catId
                               WHERE ArticuloId=@artId ";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("ArtNum", txtNum.Text));
                cmd.Parameters.Add(new SqlParameter("catId", cbCateg.SelectedValue));
               
                cmd.Parameters.Add(new SqlParameter("artId", articuloID));
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
            }
        }
        
        void Modificar(string id)
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
                    txtNum.Text = dt.Rows[0]["ArticuloNumero"].ToString();
                    cbCateg.SelectedValue = dt.Rows[0]["CategoriaId"].ToString();

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

        void buscarArticulo()
        {
            if (txtNum.Text == "")
                return;

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            try
            {
                string sql = @"SELECT * 
                               FROM Articulos 
                               WHERE ArticuloNumero ='" + txtNum.Text + "'";
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.SelectCommand.Transaction = trans;
                adp.Fill(dt);

                if (dt.Rows.Count > 0) //existe
                {
                    ViewState["ArtId"] = dt.Rows[0]["ArticuloId"].ToString();
                   cbCateg.SelectedValue=dt.Rows[0]["CategoriaId"].ToString();
                  
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

        protected void txtNum_TextChanged(object sender, EventArgs e)
        {
            buscarArticulo();
           // cbCateg.SelectedIndex = 0;
            
           
        }
       
    }
}