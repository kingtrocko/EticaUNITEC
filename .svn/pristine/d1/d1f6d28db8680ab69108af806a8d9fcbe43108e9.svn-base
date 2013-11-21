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
    public partial class Alumnos : EticaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            privilegio = "Consultar Alumnos";
            SetPrivilegiosToActualUser();
        }

        protected void SetPrivilegiosToActualUser()
        {
            if (!currentUser.TienePrivilegio("Modificar Alumnos"))
                Grid.Columns[5].Visible = false;
            if (!currentUser.TienePrivilegio("Eliminar Alumnos"))
                Grid.Columns[6].Visible = false;
            if (!currentUser.TienePrivilegio("Insertar Alumnos"))
                BtnNuevo.Visible = false;
        }


        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("MantenimientoAlumno.aspx");
        }

        protected void Grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int i = e.NewEditIndex;
            GridViewRow row = Grid.Rows[i];
            string AlId = row.Cells[0].Text;
            Response.Redirect("~/Mantenimiento/MantenimientoAlumno.aspx?id=" + AlId);
        }
    }
}