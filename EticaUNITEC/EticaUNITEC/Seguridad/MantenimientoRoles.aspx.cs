using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EticaUNITEC;

namespace EticaUNITEC
{
    public partial class MantenimientoRoles1 : EticaPage
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            privilegio = "Consultar Roles";

            if (Request.Params["id"] == null)
            {
                Response.Redirect("~/Seguridad/ListaRoles.aspx");
                Response.Write("no indico el id");
            }

            if (!IsPostBack)
            {
                if(Request.Params["id"].ToString() != "-1")
                    txtRolId.Text = Request.Params["id"].ToString();
                cargarDatos();
                cargarPrivilegiosDisponibles();
                cargarPrivilegiosAsignados();
                Session["DispInicial"] = listPrivilegiosDisponibles;
                Session["AsigInicial"] = listPrivilegiosAsignados;
                compararPrivilegiosAsigConDispo();

              
            }
            txtRolId.Enabled = false;
            SetPrivilegiosToActualUsuer();
        }

        private void SetPrivilegiosToActualUsuer()
        {
            if (!currentUser.TienePrivilegio("Insertar Roles"))
            {
                btnGuardar.Visible = false;
                btnEnviarAAsignados.Enabled = false;
                btnEnviarAAsignadosTodos.Enabled = false;
                btnEnviarADisponibles.Enabled = false;
                btnEnviarADisponiblesTodos.Enabled = false;
            }
        }

        void cargarDatos()
        {            
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            string sql = @"SELECT RolNombre, RolDescripcion 
                           FROM Roles  
                           WHERE RolId=@Id";            
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add(new SqlParameter("Id", txtRolId.Text));
                SqlDataReader reader = cmd.ExecuteReader();
                if( reader.Read() )
                {
                    txtRolNombre.Text = reader.GetString(0);
                    txtRolDescripcion.Text = reader.GetString(1);
                }                
            }
            catch (Exception e)
            {
                //Response.Output.Write("Error: " + e.GetType().ToString());
                Session["error"] = e.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            
            con.Close();
        }

        void cargarPrivilegiosDisponibles()
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string sql = @"SELECT PrivilegioId, PrivilegioLlave
                            FROM Privilegios";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            con.Close();

            listPrivilegiosDisponibles.DataSource = dt;
            listPrivilegiosDisponibles.DataValueField = "PrivilegioId";
            listPrivilegiosDisponibles.DataTextField = "PrivilegioLlave";
            listPrivilegiosDisponibles.DataBind();
        }

        void cargarPrivilegiosAsignados()
        {            
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string sql = @"SELECT PrivilegiosXRolId, RolId, PrivilegioId
                           FROM PrivilegiosXRol
                           WHERE RolId=@Id";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.Add(new SqlParameter("Id", txtRolId.Text));
            SqlDataReader reader = cmd.ExecuteReader();
            
            DataTable dtPrivilegiosXUsuario = new DataTable();
            dtPrivilegiosXUsuario.Load(reader);

            DataTable dtPrivilegios = new DataTable();
            foreach (DataRow row in dtPrivilegiosXUsuario.Rows)
            {
                sql = @"Select PrivilegioId, PrivilegioLlave
                        from Privilegios
                        WHERE PrivilegioId=@Id";

                string rolId = row["PrivilegioId"].ToString();
                SqlCommand cmd2 = new SqlCommand(sql, con);
                cmd2.Parameters.Add(new SqlParameter("Id", rolId));
                SqlDataReader reader2 = cmd2.ExecuteReader();
                dtPrivilegios.Load(reader2);                
            }
            con.Close();

            listPrivilegiosAsignados.DataSource = dtPrivilegios;
            listPrivilegiosAsignados.DataValueField = "PrivilegioId";
            listPrivilegiosAsignados.DataTextField = "PrivilegioLlave";
            listPrivilegiosAsignados.DataBind();
            
        }

        void compararPrivilegiosAsigConDispo()
        {
            if (listPrivilegiosDisponibles.Items == null || listPrivilegiosAsignados.Items == null)
                return;

            List<ListItem> disp = new List<ListItem>();
            foreach (ListItem itemDisp in listPrivilegiosDisponibles.Items)
            {
                string dato = itemDisp.Text;
                bool existe = false;
                int size = listPrivilegiosAsignados.Items.Count;
                for (int i = 0; i < size; i++)
                {
                    listPrivilegiosAsignados.SelectedIndex = i;
                    string c = listPrivilegiosAsignados.SelectedItem.ToString();
                    if (dato.Equals(c))
                    {
                        existe = true;
                        break;
                    }
                }
                if (!existe)
                    disp.Add(itemDisp);
                else
                    listPrivilegiosAsignados.SelectedIndex = -1;
            }
            listPrivilegiosDisponibles.Items.Clear();
            for (int i = 0; i < disp.Count; i++)
            {
                listPrivilegiosDisponibles.Items.Add(disp[i]);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            
            try
            {
                guardarRoles(con,trans);
                guardarRolesXUsuario(con,trans);
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback(); 
                Session["error"] = ex.StackTrace;
                Response.Redirect("~/Seguridad/Error.aspx");
            }
            con.Close();
            Response.Redirect("~/Seguridad/ListaRoles.aspx");   
        }
        
        void guardarRoles(SqlConnection con, SqlTransaction trans)
        {
            if (txtRolId.Text == "")
            {
                string sql = @"INSERT INTO Roles(RolNombre,RolDescripcion)
                           VALUES(@Nom,@Desc)";
                SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("Nom", txtRolNombre.Text));
                    cmd.Parameters.Add(new SqlParameter("Desc", txtRolDescripcion.Text));
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();

                    string lol = "SELECT @@identity as Codigo";
                    SqlDataAdapter adp = new SqlDataAdapter(lol, con);
                    DataTable tab = new DataTable();
                    adp.SelectCommand.Transaction = trans;
                    adp.Fill(tab);
                    
                    txtRolId.Text = tab.Rows[0]["Codigo"].ToString();
                              
            }
            else
            {
                string sql = @"UPDATE Roles 
                           SET RolNombre=@Nom, RolDescripcion=@Desc  
                           WHERE RolId=@Id";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Transaction = trans;
                    cmd.Parameters.Add(new SqlParameter("Nom", txtRolNombre.Text));
                    cmd.Parameters.Add(new SqlParameter("Desc", txtRolDescripcion.Text));
                    cmd.Parameters.Add(new SqlParameter("Id", txtRolId.Text));
                    cmd.ExecuteNonQuery();
               
            }
        }

        void guardarRolesXUsuario(SqlConnection con, SqlTransaction trans)
        {
            ListBox itemsDispInicial = (ListBox)Session["DispInicial"];
            ListBox itemsAsigInicial = (ListBox)Session["AsigInicial"];

            if (listPrivilegiosAsignados.Items.Count > itemsAsigInicial.Items.Count) //hago un insert a PrivilegiosXUsuario
            {
                insertarPrivilegiosXUsuario(con,trans);
            }
            else if (listPrivilegiosAsignados.Items.Count < itemsAsigInicial.Items.Count)//hago un delete a PrivilegiosXUsuario 
            {
                eliminarPrivilegiosXUsuario(con,trans);
            }

        }

        void insertarPrivilegiosXUsuario(SqlConnection con, SqlTransaction trans)
        {
            ListBox itemsAsigInicial = (ListBox)Session["AsigInicial"];
            ListBox nuevosAsig = new ListBox();

            foreach (ListItem itemAsigFin in listPrivilegiosAsignados.Items)
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

          

            foreach (ListItem itemNuevo in nuevosAsig.Items)
            {
                string privId = itemNuevo.Value;
                string privNombre = itemNuevo.Text;

                string sql = @"INSERT INTO PrivilegiosXRol(RolId,PrivilegioId)
                                    VALUES(@RolId,@PrivilegioId)";
                SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("RolId", txtRolId.Text));
                    cmd.Parameters.Add(new SqlParameter("PrivilegioId", privId));
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                
            }
        }

        void eliminarPrivilegiosXUsuario(SqlConnection con, SqlTransaction trans)
        {
            ListBox itemsDispInicial = (ListBox)Session["DispInicial"];
            int countDispInicial = itemsDispInicial.Items.Count;
            ListBox nuevoDisp = new ListBox();

            foreach (ListItem itemDispFin in listPrivilegiosDisponibles.Items)
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
                string privId = itemNuevo.Value;
                string privNombre = itemNuevo.Text;

                string sql = @"DELETE PrivilegiosXRol
                                WHERE RolId=@RolId AND PrivilegioId=@PrivilegioId";
                SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.Add(new SqlParameter("RolId", txtRolId.Text));
                    cmd.Parameters.Add(new SqlParameter("PrivilegioId", privId));
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Seguridad/ListaRoles.aspx");
        }

        protected void btnEnviarAAsignados_Click(object sender, EventArgs e)
        {
            if (listPrivilegiosDisponibles.SelectedItem == null)
                return;
            string rol = listPrivilegiosDisponibles.SelectedItem.Text;
            listPrivilegiosAsignados.Items.Add(listPrivilegiosDisponibles.SelectedItem);
            listPrivilegiosDisponibles.Items.Remove(listPrivilegiosDisponibles.SelectedItem);
            listPrivilegiosAsignados.SelectedIndex = -1;
            listPrivilegiosDisponibles.SelectedIndex = -1;

        }

        protected void btnEnviarAAsignadosTodos_Click(object sender, EventArgs e)
        {
            foreach (ListItem rol in listPrivilegiosDisponibles.Items)
            {
                listPrivilegiosAsignados.Items.Add(rol);
            }
            listPrivilegiosDisponibles.Items.Clear();

            listPrivilegiosAsignados.SelectedIndex = -1;
            listPrivilegiosDisponibles.SelectedIndex = -1;
        }

        protected void btnEnviarADisponibles_Click(object sender, EventArgs e)
        {
            if (listPrivilegiosAsignados.SelectedItem == null)
                return;
            string rol = listPrivilegiosAsignados.SelectedItem.Text;
            listPrivilegiosDisponibles.Items.Add(listPrivilegiosAsignados.SelectedItem);
            listPrivilegiosAsignados.Items.Remove(listPrivilegiosAsignados.SelectedItem);

            listPrivilegiosAsignados.SelectedIndex = -1;
            listPrivilegiosDisponibles.SelectedIndex = -1;
        }

        protected void btnEnviarADisponiblesTodos_Click(object sender, EventArgs e)
        {
            foreach (ListItem rol in listPrivilegiosAsignados.Items)
            {
                listPrivilegiosDisponibles.Items.Add(rol);
            }
            listPrivilegiosAsignados.Items.Clear();

            listPrivilegiosAsignados.SelectedIndex = -1;
            listPrivilegiosDisponibles.SelectedIndex = -1;
        }

        
    }
}