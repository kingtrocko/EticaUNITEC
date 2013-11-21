using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EticaUNITEC.Clases;

namespace EticaUNITEC
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            warnMsg.Visible = false;
            if(Request.Params["status"]!=null){
               string msj = Request.Params["status"];
               if (msj.Equals("1"))
               {
                   warnMsg.InnerText = "Acceda a su cuenta para poder ver el contenido de la pagina";
               }
               if (msj.Equals("2"))
               {
                   warnMsg.InnerText = "La combinación del correo/contraseña es incorrecta o la cuenta no existe";
               }
                warnMsg.Visible=true;
            }
            txt_correo.Focus();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Usuario u = new Usuario();
            if (u.Login(txt_correo.Text, txt_pass.Text))
            {
                Response.Redirect("../Inicio/Home.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx?status=2");
            }
            
        }
    }
}