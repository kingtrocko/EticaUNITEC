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
    public partial class Carreras : EticaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            privilegio = "Consultar Carreras";
            SetPrivilegiosToActualUser();
        }

        protected void SetPrivilegiosToActualUser()
        {
            if (!currentUser.TienePrivilegio("Modificar Carreras"))
                Grid.Columns[3].Visible = false;
            if (!currentUser.TienePrivilegio("Eliminar Carreras"))
                Grid.Columns[4].Visible = false;
            if (!currentUser.TienePrivilegio("Insertar Carreras"))
                BtnNuevo.Visible = false;

        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("MantenimientoCarrera.aspx");
        }

        protected void Grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int i = e.NewEditIndex;
            GridViewRow row = Grid.Rows[i];
            string CarId = row.Cells[0].Text;
            Response.Redirect("~/Mantenimiento/MantenimientoCarrera.aspx?id=" + CarId);
        }
    }
}