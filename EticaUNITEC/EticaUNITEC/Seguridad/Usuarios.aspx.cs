using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using EticaUNITEC;


namespace EticaUNITEC
{
    public partial class Usuarios : EticaPage
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int c = GridView1.Rows.Count;
                int f = GridView1.Columns.Count;
                Session["gridInicial"] = GridView1;
            }

            privilegio = "Consultar Usuarios";
            SetPrivilegiosToActualUsuer();
        }

        private void SetPrivilegiosToActualUsuer()
        {
            if (!currentUser.TienePrivilegio("Insertar Usuarios"))
                Button1.Enabled = false;
            if (!currentUser.TienePrivilegio("Modificar Usuarios"))
                GridView1.Columns[5].Visible = false;
            if (!currentUser.TienePrivilegio("Eliminar Usuarios"))
                GridView1.Columns[6].Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (!currentUser.ValidarPrivilegio("Insertar Usuarios"))
                return;
            Response.Redirect("~/Seguridad/UpdateUser.aspx");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int a = e.RowIndex;
            GridViewRow r = GridView1.Rows[a];
            string  x = r.Cells[1].Text;
           

            string q = "SELECT UsuarioId FROM Usuarios WHERE UsuarioNombre='"+x+"'";

            SqlDataAdapter adp = new SqlDataAdapter(q, con);
            DataTable tb = new DataTable();
            adp.Fill(tb);

            string id="";
            foreach (DataRow row in tb.Rows)
            {
                id = row[0].ToString();
            }

            q = "DELETE RolesXUsuario WHERE UsuarioId='" + id + "'";

            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();


            con.Close();


            
        }

    }
}