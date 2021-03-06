﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace EticaUNITEC
{
    public partial class SetupDB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] comandos = {@"If EXISTS(select * FROM sys.views where name = 'AgrupadoAlumnos')
                     Drop View  AgrupadoAlumnos",@"If EXISTS(select * FROM sys.views where name = 'AgrupadoAlumnos')
                     Drop View  AgrupadoCarreras",@"If EXISTS(select * FROM sys.views where name = 'AgrupadoAlumnos')
                     Drop View  AgrupadoCategorias",@"
                            CREATE VIEW AgrupadoAlumnos AS
                        Select Faltas.FaltaId, Faltas.FaltaTitulo, Faltas.FaltaDescripcion, Alumnos.AlumnoNombre, Alumnos.AlumnoCuenta, Carreras.CarreraNombre, Faltas.FaltaFecha, Incisos.IncisoLetra, Articulos.ArticuloNumero
                        from ((((alumnos inner join carreras on (Alumnos.CarreraId = Carreras.CarreraId)) inner join faltas on (Alumnos.AlumnoId= Faltas.AlumnoId)) inner join Incisos on  (Incisos.IncisoId= Faltas.IncisoId)) inner join Articulos on (Incisos.ArticuloId = Articulos.ArticuloId))
                         ",@"Create VIEW AgrupadoCarreras AS SELECT        dbo.Carreras.CarreraCodigo, dbo.Carreras.CarreraNombre,
dbo.Categorias.CategoriaNombre, dbo.Articulos.ArticuloNumero,
dbo.Incisos.IncisoLetra,
                         dbo.Faltas.FaltaFecha
FROM            dbo.Categorias INNER JOIN
                         dbo.Faltas ON dbo.Categorias.CategoriaId =
dbo.Faltas.CategoriaId INNER JOIN
                         dbo.Articulos ON dbo.Categorias.CategoriaId =
dbo.Articulos.CategoriaId INNER JOIN
                         dbo.Incisos ON dbo.Faltas.IncisoId =
dbo.Incisos.IncisoId AND dbo.Articulos.ArticuloId =
dbo.Incisos.ArticuloId CROSS JOIN
                         dbo.Carreras", @"Create VIEW AgrupadoCategorias AS SELECT        dbo.Categorias.CategoriaNombre,
dbo.Articulos.ArticuloNumero, dbo.Incisos.IncisoLetra,
dbo.Incisos.IncisoContenido,
                         dbo.Incisos.IncisoDescripcion,
dbo.Categorias.CategoriaId
FROM            dbo.Categorias INNER JOIN
                         dbo.Articulos ON dbo.Categorias.CategoriaId =
dbo.Articulos.CategoriaId INNER JOIN
                         dbo.Incisos ON dbo.Articulos.ArticuloId =
dbo.Incisos.ArticuloId
"};

            string conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBLocalConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            SqlCommand cmd;
            foreach (var c in comandos)
            {
                cmd = new SqlCommand(c, con);
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
    }
}