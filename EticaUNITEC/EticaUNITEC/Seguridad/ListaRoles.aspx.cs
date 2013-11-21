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
    public partial class MantenimientoRoles : EticaPage
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            privilegio = "Consultar Roles";
            SetPrivilegiosToActualUsuer();
        }

        private void SetPrivilegiosToActualUsuer()
        {
            if (!currentUser.TienePrivilegio("Insertar Roles"))
                btnAgregar.Visible = false;
            if (!currentUser.TienePrivilegio("Modificar Roles"))
                gridRoles.Columns[3].Visible = false;
            if (!currentUser.TienePrivilegio("Eliminar Roles"))
                gridRoles.Columns[4].Visible = false;
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!currentUser.ValidarPrivilegio("Insertar Roles"))
                return;
            Response.Redirect("~/Seguridad/MantenimientoRoles.aspx?id=-1");
        }

        protected void gridRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            int a = e.RowIndex;
            GridViewRow r = gridRoles.Rows[a];
            string x = r.Cells[1].Text;


            string q = "SELECT RolId FROM Roles WHERE RolNombre='" + x + "'";

            SqlDataAdapter adp = new SqlDataAdapter(q, con);
            DataTable tb = new DataTable();
            adp.Fill(tb);

            string id = "";
            foreach (DataRow row in tb.Rows)
            {
                id = row[0].ToString();
            }

            q = "DELETE PrivilegiosXRol WHERE RolId='" + id + "'";

            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();


            con.Close();


            
        }       
        
    }
}