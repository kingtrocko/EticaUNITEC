using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using EticaUNITEC;

namespace EticaUNITEC.Faltas
{
    public partial class ListaFaltas : EticaPage
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                cmbCarrera.SelectedIndex = 0;
                cmbCategoria.SelectedIndex = 0;

                InicializarGrid();
            }
            SetPrivilegiosToActualUsuer();
            this.grid.Columns[2].Visible = false;
            privilegio = "Consultar Faltas";
        }

        private void SetPrivilegiosToActualUsuer()
        {
            if (!currentUser.TienePrivilegio("Modificar Faltas"))
                grid.Columns[0].Visible = false;
            if (!currentUser.TienePrivilegio("Eliminar Faltas"))
                grid.Columns[1].Visible = false;

        }

        void InicializarGrid()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            {
                string sql = @"SELECT FaltaId,AlumnoCuenta,AlumnoNombre,AlumnoGenero,CarreraNombre,CategoriaNombre,FaltaTitulo,ArticuloNumero,IncisoLetra,IncisoDescripcion,SancionNombre,FaltaSancionTiempo
                               FROM InformacionFaltas";

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

        void CargarCombos()
        { 
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            {
                string sql = @"SELECT *
                               FROM Carreras";
                SqlDataAdapter adpCarrera = new SqlDataAdapter(sql, con);
                DataTable dtCarrera = new DataTable();
                adpCarrera.Fill(dtCarrera);

                sql = @"SELECT *
                        FROM Categorias";
                SqlDataAdapter adpCategorias = new SqlDataAdapter(sql, con);
                DataTable dtCategorias = new DataTable();
                adpCategorias.Fill(dtCategorias);

                DataRow rowCarr = dtCarrera.NewRow();
                rowCarr["CarreraId"] = "-1";
                rowCarr["CarreraNombre"] = "Ninguno";
                dtCarrera.Rows.InsertAt(rowCarr,0);
                cmbCarrera.DataSource = dtCarrera;
                cmbCarrera.DataValueField = "CarreraId";
                cmbCarrera.DataTextField = "CarreraNombre";
                cmbCarrera.DataBind();

                DataRow rowCat = dtCategorias.NewRow();
                rowCat["CategoriaId"] = "-1";
                rowCat["CategoriaNombre"] = "Ninguno";
                dtCategorias.Rows.InsertAt(rowCat, 0);
                cmbCategoria.DataSource = dtCategorias;
                cmbCategoria.DataValueField = "CategoriaId";
                cmbCategoria.DataTextField = "CategoriaNombre";
                cmbCategoria.DataBind();
                
            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
        }
        
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> filtros = new Dictionary<string, string>();

            if (cmbCarrera.SelectedValue != "-1")
                filtros.Add("CarreraId", cmbCarrera.SelectedValue);
            if (txtCuenta.Text != "")
                filtros.Add("AlumnoCuenta",txtCuenta.Text);
            if (txtAlumnoNombre.Text != "")
                filtros.Add("AlumnoNombre",txtAlumnoNombre.Text);
            if (txtFaltaTitulo.Text != "")
                filtros.Add("FaltaTitulo",txtFaltaTitulo.Text);
            if (cmbCategoria.SelectedValue != "-1")
                filtros.Add("CategoriaId",cmbCategoria.SelectedValue);
            if(txtFechaIncio.Text!= "")
                filtros.Add("FaltaFechaInicio",txtFechaIncio.Text);
            if(txtFechaFin.Text != "")
                filtros.Add("FaltaFechaFin", txtFechaFin.Text);

            CargarReporte(filtros);
            filtros.Clear();
        }

        void CargarReporte(Dictionary<string, string> filtros)
        {
            if (filtros.Count == 0)
            {
                InicializarGrid();
                return;
            }

            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            {
                string sql = @"SELECT FaltaId,AlumnoCuenta,AlumnoNombre,AlumnoGenero,CarreraNombre,CategoriaNombre,FaltaTitulo,ArticuloNumero,IncisoLetra,IncisoDescripcion,SancionNombre,FaltaSancionTiempo
                               FROM InformacionFaltas
                               ";
                
                if(filtros.Count == 1)
                {
                    sql += @"WHERE " + filtros.ElementAt(0).Key + "='" + filtros.ElementAt(0).Value + "'";
                }
                else if (filtros.Count == 2)
                {
                    sql += @"WHERE " + filtros.ElementAt(0).Key + "='" + filtros.ElementAt(0).Value + "' and "
                            + filtros.ElementAt(1).Key + "='" + filtros.ElementAt(1).Value + "'";
                }
                else if (filtros.Count == 3)
                {
                    sql += @"WHERE " + filtros.ElementAt(0).Key + "='" + filtros.ElementAt(0).Value + "' and "
                            + filtros.ElementAt(1).Key + "='" + filtros.ElementAt(1).Value + "' and "
                            + filtros.ElementAt(2).Key + "='" + filtros.ElementAt(2).Value + "'";
                }
                else if (filtros.Count == 4)
                {
                    sql += @"WHERE " + filtros.ElementAt(0).Key + "='" + filtros.ElementAt(0).Value + "' and "
                            + filtros.ElementAt(1).Key + "='" + filtros.ElementAt(1).Value + "' and "
                            + filtros.ElementAt(2).Key + "='" + filtros.ElementAt(2).Value + "' and "
                            + filtros.ElementAt(3).Key + "='" + filtros.ElementAt(3).Value + "'";
                }
                else if (filtros.Count == 5)
                {
                    sql += @"WHERE " + filtros.ElementAt(0).Key + "='" + filtros.ElementAt(0).Value + "' and "
                            + filtros.ElementAt(1).Key + "='" + filtros.ElementAt(1).Value + "' and "
                            + filtros.ElementAt(2).Key + "='" + filtros.ElementAt(2).Value + "' and "
                            + filtros.ElementAt(3).Key + "='" + filtros.ElementAt(3).Value + "' and "
                            + filtros.ElementAt(4).Key + "='" + filtros.ElementAt(4).Value + "'";
                }
                else if (filtros.Count == 6)
                {
                    sql += @"WHERE " + filtros.ElementAt(0).Key + "='" + filtros.ElementAt(0).Value + "' and "
                            + filtros.ElementAt(1).Key + "='" + filtros.ElementAt(1).Value + "' and "
                            + filtros.ElementAt(2).Key + "='" + filtros.ElementAt(2).Value + "' and "
                            + filtros.ElementAt(3).Key + "='" + filtros.ElementAt(3).Value + "' and "
                            + filtros.ElementAt(4).Key + "='" + filtros.ElementAt(4).Value + "' and "
                            + filtros.ElementAt(5).Key + "='" + filtros.ElementAt(5).Value + "'";
                }
                else if (filtros.Count == 7)
                {
                    sql += @"WHERE " + filtros.ElementAt(0).Key + "='" + filtros.ElementAt(0).Value + "' and "
                            + filtros.ElementAt(1).Key + "='" + filtros.ElementAt(1).Value + "' and "
                            + filtros.ElementAt(2).Key + "='" + filtros.ElementAt(2).Value + "' and "
                            + filtros.ElementAt(3).Key + "='" + filtros.ElementAt(3).Value + "' and "
                            + filtros.ElementAt(4).Key + "='" + filtros.ElementAt(4).Value + "' and "
                            + filtros.ElementAt(5).Key + "='" + filtros.ElementAt(5).Value + "' and "
                            + filtros.ElementAt(6).Key + "='" + filtros.ElementAt(6).Value + "'";
                }
                    
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                                
                grid.DataSource = dt;
                grid.DataBind();
                
            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
        }

        protected void grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int i = e.NewEditIndex;
            GridViewRow row = grid.Rows[i];
            string faltaId = row.Cells[2].Text;           
            Response.Redirect("MantenimientoFaltas.aspx?id=" + faltaId);
        }

        protected void grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int i = e.RowIndex;
            GridViewRow row = grid.Rows[i];
            string faltaId = row.Cells[2].Text;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            try
            {
                string sql = "DELETE Faltas WHERE FaltaId='" + faltaId + "'";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
        }
    }
}
