﻿using System;
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
    public partial class UpdateUser : EticaPage
    {
        int uID = 0;
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.Params["userID"] != null)
                {
                    uID = Convert.ToInt32(Request.Params["userID"]);
                    cargarDatos();                    
                }
                else
                {
                    /*lblpass.Enabled=true;*/
                    txt_pass.Enabled= true;
                }
                cargarRolesDisponibles();
                cargarRolesAsignados();
                Session["DispInicial"] = listRolesDisponibles;
                Session["AsigInicial"] = listRolesAsignados;
                compararRolesAsigConDispo();
            }
            txt_id.Enabled = false;
            SetPrivilegiosToActualUsuer();
        }

        private void SetPrivilegiosToActualUsuer()
        {
            if (!currentUser.TienePrivilegio("Insertar Roles"))
            {
                btnEnviarAAsignados.Enabled = false;
                btnEnviarAAsignadosTodos.Enabled = false;
                btnEnviarADisponibles.Enabled = false;
                btnEnviarADisponiblesTodos.Enabled = false;
            }

        }

        private void cargarDatos()
        {
            if (uID != 0)
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = @"Select *
                               from Usuarios";
                SqlDataAdapter adp = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                con.Close();

                foreach(DataRow fila in dt.Rows)
                {
                    if (fila["UsuarioId"].ToString() == uID.ToString())
                    {
                        txt_id.Text = fila["UsuarioId"].ToString();
                        txt_nombre.Text = fila["UsuarioNombre"].ToString();
                        txt_correo.Text = fila["UsuarioCorreo"].ToString();
                        txt_tel.Text = fila["UsuarioTelefono"].ToString();
                        txt_dir.Text = fila["UsuarioDireccion"].ToString();
                    }
                }                
            }
        }

        void cargarRolesDisponibles()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string sql = @"Select RolId, RolNombre
                            from Roles";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();

            listRolesDisponibles.DataSource = dt;
            listRolesDisponibles.DataValueField = "RolId";
            listRolesDisponibles.DataTextField = "RolNombre";            
            listRolesDisponibles.DataBind();
        }

        void cargarRolesAsignados()
        {
            if (uID != 0)
            {
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = @"Select RolesXUsuarioId, UsuarioId, RolId
                            from RolesXUsuario
                            WHERE UsuarioId=@Id";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("Id", uID));
                SqlDataReader reader = cmd.ExecuteReader();
                
                DataTable dtRolesXUsuario = new DataTable();
                dtRolesXUsuario.Load(reader);

                DataTable dtRoles = new DataTable();
                foreach(DataRow row in dtRolesXUsuario.Rows)
                {
                    sql = @"Select RolId, RolNombre
                            from Roles
                            WHERE RolId=@Id";

                    string rolId = row["RolId"].ToString();
                    SqlCommand cmd2 = new SqlCommand(sql, con);
                    cmd2.Parameters.Add(new SqlParameter("Id", rolId));
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    dtRoles.Load(reader2);
                }
                con.Close();

                listRolesAsignados.DataSource = dtRoles;
                listRolesAsignados.DataValueField = "RolId";
                listRolesAsignados.DataTextField = "RolNombre";                
                listRolesAsignados.DataBind();
            }
        }

        void compararRolesAsigConDispo() 
        {
            if (listRolesDisponibles.Items == null || listRolesAsignados.Items == null)
                return;

            List<ListItem> disp = new List<ListItem>();
            foreach (ListItem itemDisp in listRolesDisponibles.Items)
            {
                string dato = itemDisp.Text;
                bool existe = false;
                int size = listRolesAsignados.Items.Count;
                for (int i = 0; i < size; i++)
                {
                    listRolesAsignados.SelectedIndex = i;
                    string c = listRolesAsignados.SelectedItem.ToString();
                    if (dato.Equals(c))
                    {
                        existe = true;
                        break;
                    }
                }
                if (!existe)
                    disp.Add(itemDisp);
                else
                    listRolesAsignados.SelectedIndex = -1;
            }
            listRolesDisponibles.Items.Clear();
            for(int i=0; i<disp.Count; i++)
            {
                listRolesDisponibles.Items.Add(disp[i]);
            }                
        }

        protected void bt_cancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Seguridad/Usuarios.aspx");
        }

        protected void br_guardar_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            bool exito = false;
            try
            {
                guardarUsuario(con,trans);
                guardarRolesXUsuario(con,trans);
                trans.Commit();
                exito = true;
              
            }
            catch (Exception ex)
            {
                trans.Rollback();
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
            if(exito)
                Response.Redirect("Usuarios.aspx");
        }

        
        void guardarUsuario(SqlConnection con, SqlTransaction trans)
        { 
            if(txt_id.Text!="")
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = @"UPDATE Usuarios
                                    SET UsuarioNombre=@nombre, UsuarioCorreo=@correo, UsuarioTelefono=@telefono, UsuarioDireccion=@direccion
                                    WHERE UsuarioId=@id";

                cmd.Parameters.Add(new SqlParameter("nombre", txt_nombre.Text));
                cmd.Parameters.Add(new SqlParameter("correo", txt_correo.Text));
                cmd.Parameters.Add(new SqlParameter("telefono", txt_tel.Text));
                cmd.Parameters.Add(new SqlParameter("direccion", txt_dir.Text));
                cmd.Parameters.Add(new SqlParameter("id", int.Parse(txt_id.Text)));
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();    
            }
            if (txt_id.Text == "")
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = @"INSERT INTO Usuarios (UsuarioNombre, UsuarioCorreo, UsuarioClave, UsuarioTelefono, UsuarioDireccion) 
                            VALUES (@nombre, @correo, @clave, @telefono, @direccion)";
                cmd.Parameters.Add(new SqlParameter("nombre", txt_nombre.Text));
                cmd.Parameters.Add(new SqlParameter("correo", txt_correo.Text));
                cmd.Parameters.Add(new SqlParameter("clave", Clases.Usuario.EncriptarClave(txt_pass.Text) ));
                cmd.Parameters.Add(new SqlParameter("telefono", txt_correo.Text));
                cmd.Parameters.Add(new SqlParameter("direccion", txt_dir.Text));
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();

                string lol = "SELECT @@identity as Codigo";
                SqlDataAdapter adp = new SqlDataAdapter(lol, con);
                adp.SelectCommand.Transaction = trans;
                DataTable tab = new DataTable();
                adp.Fill(tab);
                txt_id.Text = tab.Rows[0]["Codigo"].ToString();
            }
        }

        void guardarRolesXUsuario(SqlConnection con, SqlTransaction trans)
        {
            ListBox itemsDispInicial = (ListBox)Session["DispInicial"];
            ListBox itemsAsigInicial = (ListBox)Session["AsigInicial"];
                       
            if (listRolesAsignados.Items.Count > itemsAsigInicial.Items.Count ) //hago un insert a ROlesXUsuario
            {
                insertarRolesXUsuario(con,trans);   
            }            
            else if(listRolesAsignados.Items.Count < itemsAsigInicial.Items.Count)//hago un delete a RolesXUsuario 
            {
                eliminarRolesXUsuario(con,trans);
            }
            
        }

        void insertarRolesXUsuario(SqlConnection con, SqlTransaction trans)
        {
            ListBox itemsAsigInicial = (ListBox)Session["AsigInicial"];
            ListBox nuevosAsig = new ListBox();
                                 
            foreach (ListItem itemAsigFin in listRolesAsignados.Items)
            {
                string fin = itemAsigFin.Text;
                string idFin = itemAsigFin.Value;
                bool existe = false;
                int size = itemsAsigInicial.Items.Count;
                for (int i = 0; i < size; i++)
                {
                    itemsAsigInicial.SelectedIndex = i;
                    string inicio = itemsAsigInicial.SelectedItem.Text;
                    if (fin.Equals(inicio))
                    {
                        existe = true;
                        break;
                    }
                }
                if (!existe)
                {
                    nuevosAsig.Items.Add(itemAsigFin);
                }
            }

            foreach(ListItem itemNuevo in nuevosAsig.Items)
            {
                string rolId = itemNuevo.Value;
                string rolNombre = itemNuevo.Text;

                string sql = @"INSERT INTO RolesXUsuario(UsuarioId,RolId)
                                    VALUES(@UsuarioId,@RolId)";
                
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("UsuarioId", txt_id.Text));
                cmd.Parameters.Add(new SqlParameter("RolId", rolId));
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();                
            }           
        }

        void eliminarRolesXUsuario(SqlConnection con, SqlTransaction trans)
        {
            ListBox itemsDispInicial = (ListBox)Session["DispInicial"];
            int countDispInicial = itemsDispInicial.Items.Count;
            ListBox nuevoDisp = new ListBox();

            foreach (ListItem itemDispFin in listRolesDisponibles.Items)
            {
                string fin = itemDispFin.Text;
                string idFin = itemDispFin.Value;
                bool existe = false;
                int size = itemsDispInicial.Items.Count;
                for (int i = 0; i < size; i++)
                {
                    itemsDispInicial.SelectedIndex = i;
                    string inicio = itemsDispInicial.SelectedItem.Text;
                    if (fin.Equals(inicio))
                    {
                        existe = true;
                        break;
                    }
                }
                if (!existe)
                {
                    nuevoDisp.Items.Add(itemDispFin);
                }
            }

            foreach (ListItem itemNuevo in nuevoDisp.Items)
            {
                string rolId = itemNuevo.Value;
                string rolNombre = itemNuevo.Text;

                string sql = @"DELETE RolesXUsuario
                                WHERE UsuarioId=@UsuarioId AND RolId=@RolId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("UsuarioId", txt_id.Text));
                cmd.Parameters.Add(new SqlParameter("RolId", rolId));
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
            }           
        }

        protected void btnEnviarAAsignados_Click(object sender, EventArgs e)
        {
            if (listRolesDisponibles.SelectedItem == null)
                return;
            string rol = listRolesDisponibles.SelectedItem.Text;
            listRolesAsignados.Items.Add(listRolesDisponibles.SelectedItem);
            listRolesDisponibles.Items.Remove(listRolesDisponibles.SelectedItem);
            listRolesAsignados.SelectedIndex = -1;
            listRolesDisponibles.SelectedIndex = -1;
            
        }

        protected void btnEnviarAAsignadosTodos_Click(object sender, EventArgs e)
        {
            foreach(ListItem rol in listRolesDisponibles.Items)
            {
                listRolesAsignados.Items.Add(rol);
            }
            listRolesDisponibles.Items.Clear();

            listRolesAsignados.SelectedIndex = -1;
            listRolesDisponibles.SelectedIndex = -1;
        }

        protected void btnEnviarADisponibles_Click(object sender, EventArgs e)
        {
            if (listRolesAsignados.SelectedItem == null)
                return;
            string rol = listRolesAsignados.SelectedItem.Text;
            listRolesDisponibles.Items.Add(listRolesAsignados.SelectedItem);
            listRolesAsignados.Items.Remove(listRolesAsignados.SelectedItem);

            listRolesAsignados.SelectedIndex = -1;
            listRolesDisponibles.SelectedIndex = -1;
        }

        protected void btnEnviarADisponiblesTodos_Click(object sender, EventArgs e)
        {
            foreach (ListItem rol in listRolesAsignados.Items)
            {
                listRolesDisponibles.Items.Add(rol);
            }
            listRolesAsignados.Items.Clear();

            listRolesAsignados.SelectedIndex = -1;
            listRolesDisponibles.SelectedIndex = -1;
        }        
    }
}