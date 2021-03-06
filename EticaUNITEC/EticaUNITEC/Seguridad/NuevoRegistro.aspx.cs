﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using EticaUNITEC;

namespace Etica_Unitec
{
    public partial class NuevoRegistro : EticaPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetPrivilegiosToActualUsuer();
        }

        private void SetPrivilegiosToActualUsuer()
        {
            if (!currentUser.TienePrivilegio("Insertar Privilegios"))
                btnGuardar.Visible = false;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string cmstring = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(cmstring);
            con.Open();
            string sql = "INSERT INTO Privilegios(PrivilegioLlave,PrivilegioDescripcion) VALUES (@PreL, @PreD)";
            SqlCommand cmd = new SqlCommand(sql, con);

            string Llave, Desc;
            
            
                Llave = txtLlave.Text;
                Desc = txtDescripcion.Text;

                cmd.Parameters.Add(new SqlParameter("PreL", Llave));
                cmd.Parameters.Add(new SqlParameter("PreD", Desc));

                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            

            con.Close();
            if (txtLlave.Text != "" && txtDescripcion.Text != "")
            {
                Response.Redirect("~/Seguridad/PrivilegioInicio.aspx");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Seguridad/PrivilegioInicio.aspx");
        }
    }
}