<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false"  CodeBehind="Imprimir.aspx.cs" Inherits="EticaUNITEC.Imprimir" %>

<%@ Register assembly="FreeTextBox"  namespace="FreeTextBoxControls" tagprefix="FTB"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        body
        {
            height:auto;
            width: 700px;
            min-height:75%;
        }
        .FreeTextBox1_OuterTable {
            background-color: #FFFFFF;
            width: 760px;
        }
        .FreeTextBox1_EndTab {
            background-color: #FFFFFF;
            border-width: 0px;
        }
    </style>
    
    <link href="../Css/forms/signup.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style1.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style2.css" rel="stylesheet" type="text/css" />
    <link href="../Css/forms/style3.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
            if (navigator.userAgent.toLowerCase().indexOf('chrome') != -1) {
                document.write('<link href="../Css/forchrome.css" rel="stylesheet" type="text/css" />');
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
            <div id="reg-falta" class="signuponepage main content clearfix">
                <div class="clearfix" >
                    <div class="side-content" style="margin:5px;">
                        <div class="signup-box" style="padding:15px;">
                        <h2 class="redtext">Imprimir Plantilla</h2>
                            <div id="fault-form-element" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:30%;">
                                            <strong>Titulo de la falta</strong>
                                            <asp:TextBox ID="txtTituloFalta" runat="server"></asp:TextBox>
                                    </label>
                                    
                                    <label class="column2" style="width:30%;">
                                            <strong>Categoria</strong>
                                            <asp:TextBox ID="txtCategoria" runat="server"></asp:TextBox>
                                    </label>
                                </fieldset>
                            </div>
                            <div id="theme-form-element" class="form-element multi-field id">
                                <fieldset>
                                    <label class="column1" style="width:20%;">
                                            <strong>N. de Acta</strong>
                                            <asp:TextBox ID="txtNumeroActa" runat="server"></asp:TextBox>
                                    </label>
                                    <label class="column2" style="width:30%;">
                                            <strong>Plantilla</strong>
                                            <div class="goog-inline-block goog-flat-menu-button jfk-select styled-select">
                                                <asp:DropDownList ID="cmbPlantilla" runat="server" 
                                                    DataSourceID="SqlDataSourceCmbPlantilla" DataTextField="TemplateNombre" 
                                                    DataValueField="TemplateId" ontextchanged="cmbPlantilla_TextChanged" 
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                                <asp:SqlDataSource ID="SqlDataSourceCmbPlantilla" runat="server" 
                                                    ConnectionString="<%$ ConnectionStrings:DBConnectionString %>" 
                                                    DeleteCommand="DELETE FROM [Templates] WHERE [TemplateId] = @TemplateId" 
                                                    InsertCommand="INSERT INTO [Templates] ([TemplateNombre]) VALUES (@TemplateNombre)" 
                                                    SelectCommand="SELECT [TemplateId], [TemplateNombre] FROM [Templates]" 
                                                    UpdateCommand="UPDATE [Templates] SET [TemplateNombre] = @TemplateNombre WHERE [TemplateId] = @TemplateId">
                                                    <DeleteParameters>
                                                        <asp:Parameter Name="TemplateId" Type="Int32" />
                                                    </DeleteParameters>
                                                    <InsertParameters>
                                                        <asp:Parameter Name="TemplateNombre" Type="String" />
                                                    </InsertParameters>
                                                    <UpdateParameters>
                                                        <asp:Parameter Name="TemplateNombre" Type="String" />
                                                        <asp:Parameter Name="TemplateId" Type="Int32" />
                                                    </UpdateParameters>
                                                </asp:SqlDataSource>
                                            </div>
                                    </label>
                                </fieldset>
                            </div><!--Aki termina-->
                            <div>
                                <FTB:FreeTextBox ID="FreeTextBox1" runat="server" ButtonSet="OfficeMac" 
                                    Width="760" BackColor="White">
                                </FTB:FreeTextBox>
                                <asp:Button ID="txtCambios" class="g-button" runat="server" onclick="txtCambios_Click" style="margin-top:15px;"
                                Text="Ver Cambios" />
                                <style type="text/css">
                                    .FreeTextBox1_OuterTable {
                                        background-color: #FFFFFF;
                                        width: 760px;
                                    }
                                    .FreeTextBox1_EndTab {
                                        background-color: #FFFFFF;
                                        border-width: 0px;
                                    }
                                    .FreeTextBox1_DesignBox {
                                        background-color: #FFFFFF;
                                        border: 1px solid #808080;
                                        border-radius:5px;
                                    }
                                    .FreeTextBox1_StartTabOn {
                                        background-color: #FFFFFF;
                                        border-width: 0;
                                        padding: 0;
                                    }
                                </style>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
