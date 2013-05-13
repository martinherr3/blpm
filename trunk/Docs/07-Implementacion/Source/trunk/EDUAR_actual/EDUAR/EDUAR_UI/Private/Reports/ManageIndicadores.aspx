<%@ Page Title="Administrar Evaluaciones" Language="C#" MasterPageFile="~/EDUARMaster.Master"
    AutoEventWireup="true" CodeBehind="ManageIndicadores.aspx.cs" Inherits="EDUAR_UI.ManageIndicadores"
    Theme="Tema" StylesheetTheme="Tema" %>

<%@ MasterType VirtualPath="~/EDUARMaster.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <h2>
                    Indicadores
                    <asp:Label Text="" runat="server" ID="lblTitulo" /></h2>
                <br />
            </td>
            <td align="right">
                <asp:UpdatePanel ID="udpBotonera" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:ImageButton ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" ToolTip="Guardar"
                            ImageUrl="~/Images/botonGuardar.png" CausesValidation="true" ValidationGroup="vldErrores" />
                        <asp:ImageButton ID="btnVolver" OnClick="btnVolver_Click" runat="server" ToolTip="Volver"
                            ImageUrl="~/Images/botonVolver.png" />
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvwReporte" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table class="tablaInterna" cellpadding="0" cellspacing="0">
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="udpGrilla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="gvwReporte" runat="server" CssClass="DatosLista" SkinID="gridviewSkinPagerListado"
                AutoGenerateColumns="false" AllowPaging="false" Width="100%" DataKeyNames="idIndicador"
                OnRowCommand="gvwReporte_RowCommand" OnPageIndexChanging="gvwReporte_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Acciones">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:ImageButton ID="editar" runat="server" CommandName="Editar" CommandArgument='<%# Bind("idIndicador") %>'
                                ToolTip="Editar Indicador" ImageUrl="~/Images/Grillas/action_edit.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Indicador">
                        <HeaderStyle HorizontalAlign="center" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblNombreGrilla" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Peso">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPesoDefault" runat="server" Text='<%# Bind("pesoDefault") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Peso Mínimo">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPesoMinimo" runat="server" Text='<%# Bind("pesoMinimo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Peso Máximo">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="center" />
                        <ItemTemplate>
                            <asp:Label ID="lblPesoMaximo" runat="server" Text='<%# Bind("pesoMaximo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Escala">
                        <HeaderStyle HorizontalAlign="center" Width="20%" />
                        <ItemStyle HorizontalAlign="left" />
                        <ItemTemplate>
                            <asp:Label ID="lblOrganizador" runat="server" Text='<%# Bind("escala") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Maximiza">
                        <HeaderStyle HorizontalAlign="center" Width="10%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblMaximiza" runat="server" Text='<%# Boolean.Parse(Eval("maximiza").ToString()) ? "Sí" : "No"  %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:UpdatePanel ID="udpEdit" runat="server" UpdateMode="Conditional" Visible="false">
                <ContentTemplate>
                    <table class="tablaInternaSinBorde" cellpadding="1" cellspacing="5">
                        <tr>
                            <td>
                                <h3>
                                    Editar Indicador
                                    <asp:Literal ID="litEditar" runat="server" Text=""></asp:Literal>
                                </h3>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ValidationSummary runat="server" ID="vldErrores" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" cellpadding="1" cellspacing="5">
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label ID="lblPesoDefault" runat="server" Text="Peso Predeterminado:"></asp:Label>
                            </td>
                            <td valign="top" colspan="3">
                                <asp:TextBox ID="txtPesoDefault" runat="server" CssClass="EstiloTxtCorto50" ValidationGroup="validarEdit" />
                                <asp:CompareValidator ID="valCriterioMax" ErrorMessage="Peso Default Menor al Mínimo"
                                    ControlToValidate="txtPesoDefault" runat="server" Type="Double" Operator="GreaterThan"
                                    ForeColor="Red" Display="Dynamic" ControlToCompare="txtPesoMinimo" ValidationGroup="vldErrores">*
                                </asp:CompareValidator>
                                <asp:CompareValidator ID="valCriterioMin" ErrorMessage="Peso Default Mayor al Máximo"
                                    ControlToValidate="txtPesoDefault" runat="server" Type="Double" Operator="LessThan"
                                    ForeColor="Red" Display="Dynamic" ControlToCompare="txtPesoMaximo" ValidationGroup="vldErrores">*
                                </asp:CompareValidator>
                                <cc1:MaskedEditExtender runat="server" ID="maskCriterio" Mask="99.99" MaskType="Number"
                                    InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtPesoDefault" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label ID="lblPesoMinimo" runat="server" Text="Peso Mínimo:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios15">
                                <asp:TextBox ID="txtPesoMinimo" runat="server" CssClass="EstiloTxtCorto50" />
                                <asp:CompareValidator ID="CompareValidator1" ErrorMessage="Peso Mínimo Mayor al Peso Default"
                                    ControlToValidate="txtPesoMinimo" runat="server" Type="Double" Operator="LessThan"
                                    ForeColor="Red" Display="Dynamic" ControlToCompare="txtPesoDefault" ValidationGroup="vldErrores">*
                                </asp:CompareValidator>
                                <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender1" Mask="99.99" MaskType="Number"
                                    InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtPesoMinimo" />
                            </td>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label ID="Label1" runat="server" Text="Peso Máximo:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtPesoMaximo" runat="server" CssClass="EstiloTxtCorto50" ValidationGroup="validarEdit" />
                                <asp:CompareValidator ID="CompareValidator2" ErrorMessage="Peso Máximo Menor al Peso Default"
                                    ControlToValidate="txtPesoMaximo" runat="server" Type="Double" Operator="GreaterThan"
                                    ForeColor="Red" Display="Dynamic" ControlToCompare="txtPesoDefault" ValidationGroup="vldErrores">*
                                </asp:CompareValidator>
                                <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender2" Mask="99.99" MaskType="Number"
                                    InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtPesoMaximo" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label ID="lblMaximiza" runat="server" Text="Maximizante:"></asp:Label>
                            </td>
                            <td valign="top" class="TDCriterios15">
                                <asp:CheckBox ID="chkMaximiza" runat="server" Checked="true" />
                            </td>
                            <td valign="top" class="TDCriterios15">
                            </td>
                            <td valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" class="TDCriterios15">
                                <asp:Label runat="server" ID="Label9" Text="Escala:"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:TextBox runat="server" ID="txtEscala" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                    </table><br />
                    <table class="tablaInterna" border="0" cellpadding="1" cellspacing="5">
                        <tr>
                            <td colspan="2">
                                <h3>
                                    Configuración de Funciones de Preferencia
                                </h3>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDCriterios50" valign="top">
                                <cc1:Accordion ID="MyAccordion" runat="Server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                    AutoSize="Limit" Height="300px" FadeTransitions="true" TransitionDuration="250"
                                    FramesPerSecond="50" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                    <Panes>
                                        <cc1:AccordionPane ID="acpCuasiCriterio" runat="server" HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                            <Header>
                                                Cuasi Criterio
                                            </Header>
                                            <Content>
                                                <table border="0" cellpadding="1" cellspacing="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Límite de Indiferencia (q):"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtCCLimIndiferencia" />
                                                            <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender6" Mask="99.99" MaskType="Number"
                                                                InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtCCLimIndiferencia" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </Content>
                                        </cc1:AccordionPane>
                                        <cc1:AccordionPane ID="acpPseudoCriterio" runat="server" HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                            <Header>
                                                Pseudo Criterio con Preferencia Lineal
                                            </Header>
                                            <Content>
                                                <table border="0" cellpadding="1" cellspacing="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Límite de Preferencia (p):"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtPCLimPreferencia" />
                                                            <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender3" Mask="99.99" MaskType="Number"
                                                                InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtPCLimPreferencia" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </Content>
                                        </cc1:AccordionPane>
                                        <cc1:AccordionPane ID="acpLevelCriterio" runat="server" HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                            <Header>
                                                Level Criterio
                                            </Header>
                                            <Content>
                                                <table border="0" cellpadding="1" cellspacing="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label51" runat="server" Text="Límite de Indiferencia (q):"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtLCLimIndiferencia" />
                                                            <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender5" Mask="99.99" MaskType="Number"
                                                                InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtLCLimIndiferencia" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Límite de Preferencia (p):"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtLCLimPreferencia" />
                                                            <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender4" Mask="99.99" MaskType="Number"
                                                                InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtLCLimPreferencia" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </Content>
                                        </cc1:AccordionPane>
                                    </Panes>
                                    <HeaderTemplate>
                                        HeaderTemplate...</HeaderTemplate>
                                    <ContentTemplate>
                                        ContentTemplate...</ContentTemplate>
                                </cc1:Accordion>
                            </td>
                            <td class="TDCriterios50" valign="top">
                                <cc1:Accordion ID="Accordion1" runat="Server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
                                    AutoSize="Limit" Height="300px" FadeTransitions="true" TransitionDuration="250"
                                    FramesPerSecond="40" RequireOpenedPane="false" SuppressHeaderPostbacks="true">
                                    <Panes>
                                        <cc1:AccordionPane ID="acpPrefLineal" runat="server" HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                            <Header>
                                                Preferencia Lineal y Área de Indiferencia
                                            </Header>
                                            <Content>
                                                <table border="0" cellpadding="1" cellspacing="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Límite de Indiferencia (q):"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtPLLimIndiferencia" />
                                                            <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender7" Mask="99.99" MaskType="Number"
                                                                InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtPLLimIndiferencia" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label6" runat="server" Text="Límite de Preferencia (p):"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtPLLimPreferencia" />
                                                            <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender8" Mask="99.99" MaskType="Number"
                                                                InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtPLLimPreferencia" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </Content>
                                        </cc1:AccordionPane>
                                        <cc1:AccordionPane ID="acpGaussiano" runat="server" HeaderCssClass="accordionHeader"
                                            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
                                            <Header>
                                                Gaussiano
                                            </Header>
                                            <Content>
                                                <table border="0" cellpadding="1" cellspacing="5">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label7" runat="server" Text="Sigma (s):"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" ID="txtGSigma" />
                                                            <cc1:MaskedEditExtender runat="server" ID="MaskedEditExtender9" Mask="99.99" MaskType="Number"
                                                                InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" TargetControlID="txtGSigma" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br />
                                                            <br />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </Content>
                                        </cc1:AccordionPane>
                                    </Panes>
                                    <HeaderTemplate>
                                        HeaderTemplate...</HeaderTemplate>
                                    <ContentTemplate>
                                        ContentTemplate...</ContentTemplate>
                                </cc1:Accordion>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="gvwReporte" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
