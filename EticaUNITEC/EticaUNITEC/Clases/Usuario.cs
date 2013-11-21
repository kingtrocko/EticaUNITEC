using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
namespace EticaUNITEC.Clases
{
    public class Usuario
    {

        #region set/gets

        Dictionary<String, bool> permisos= new Dictionary<string,bool>();
        

        int usuarioID;

        public int UsuarioID
        {
            get { return usuarioID; }
            set { usuarioID = value; }
        }
        string usuarioNombre;

        public string UsuarioNombre
        {
            get { return usuarioNombre; }
            set { usuarioNombre = value; }
        }
        string usuarioCorreo;

        public string UsuarioCorreo
        {
            get { return usuarioCorreo; }
            set { usuarioCorreo = value; }
        }
        #endregion


        void CargarPrivilegios()
        {
            string sql = @"SELECT PrivilegioLlave 
                           FROM  PrivilegiosPorUsuario
                           WHERE  UsuarioId=@uid";
            
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(sql,con);
                adp.SelectCommand.Parameters.Add(new SqlParameter("uid", UsuarioID));
                DataTable dt = new DataTable();
                adp.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    if(!permisos.ContainsKey(row["PrivilegioLlave"].ToString()))
                        permisos.Add(row["PrivilegioLlave"].ToString(),true);
                }


            }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Session["error"] = ex.StackTrace;
                System.Web.HttpContext.Current.Response.Redirect("~/Seguridad/Error.aspx");
            }
            finally
            {
                con.Close();
            }

        }


       public bool Login(string correo,string clave)
        {
            string sql = @"SELECT * 
                           FROM  Usuarios
                           WHERE  UsuarioCorreo=@correo and UsuarioClave=@clave";

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                adp.SelectCommand.Parameters.Add(new SqlParameter("correo", correo));
                adp.SelectCommand.Parameters.Add(new SqlParameter("clave", clave ));//Usuario.EncriptarClave(clave)
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count == 0)
                    return false;
                UsuarioID = (int)dt.Rows[0]["UsuarioId"];
                UsuarioCorreo = dt.Rows[0]["UsuarioCorreo"].ToString();
                UsuarioNombre = dt.Rows[0]["UsuarioNombre"].ToString();
                CargarPrivilegios();
                System.Web.HttpContext.Current.Session["Usuario"] = this;
                return true;
             }
            catch (Exception ex)
            {
                System.Web.HttpContext.Current.Session["error"] = ex.Message+ ex.StackTrace;
                System.Web.HttpContext.Current.Response.Redirect("~/Seguridad/Error.aspx");
                return false;
            }
            finally
            {
                con.Close();
            }


        }




        public static string EncriptarClave(string originalPassword)
        {
          
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);            
            return BitConverter.ToString(encodedBytes);
        }



        public bool ValidarPrivilegio(string privilegioLLave)
        {
            bool t = TienePrivilegio(privilegioLLave);
            if (!t)
            {
                System.Web.HttpContext.Current.Session["error"] = "No tiene privilegios para esta operacion!!!";
                System.Web.HttpContext.Current.Response.Redirect("~/Seguridad/Error.aspx");
            }
            return t; 
        }

        public bool TienePrivilegio(string privilegioLlave)
        {

            return permisos.ContainsKey(privilegioLlave);

        }

        public static Usuario Current
        {
            get { return         (Usuario)System.Web.HttpContext.Current.Session["Usuario"]; }
        }

    }
}