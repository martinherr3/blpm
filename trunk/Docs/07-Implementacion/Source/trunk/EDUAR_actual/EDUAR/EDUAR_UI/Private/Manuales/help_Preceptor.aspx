<%@ Page Title="Ayuda para el Preceptor" Language="C#" MasterPageFile="~/Private/Manuales/Help.Master"
    AutoEventWireup="true" CodeBehind="help_Preceptor.aspx.cs" Inherits="EDUAR_UI.help_Preceptor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        El objetivo del siguiente manual es que un administrador pueda conocer las funcionalidades
        que puede ejecutar en <b>EDU@R 2.0</b>. El Administrador tiene acceso a todas las
        funcionalidades del sistema. A continuación se describirá la distribución de los
        principales elementos del sistema, a donde dirigirse para realizar ciertas funciones
        y como realizar las mismas.</p>
    <table class="tablaInterna" cellpadding="8" cellspacing="1">
        <tr>
            <td class="TD10" valign="top">
                <ol>
                    <li>
                        <asp:LinkButton ID="lnkPresentacion" Text="Presentación" runat="server" OnClick="CambiarPanel_Click"
                            CommandArgument="pnlPresentacion" />
                    </li>
                    <li>
                        <asp:LinkButton ID="lnkElementosBasicos" Text="Elementos Básicos" runat="server"
                            OnClick="CambiarPanel_Click" CommandArgument="pnlBasicos" /></li>
                    <li>
                        <asp:LinkButton ID="lnkCuenta" Text="Cuenta" runat="server" OnClick="CambiarPanel_Click"
                            CommandArgument="pnlCuenta" /></li>
                    <ol>
                        <li>
                            <asp:LinkButton ID="lnkPreguntaSecreta" Text="Pregunta Secreta" runat="server" OnClick="CambiarPanel_Click"
                                CommandArgument="pnlPreguntaSecreta" /></li>
                        <li>
                            <asp:LinkButton ID="lnkContrasenia" Text="Contraseña" runat="server" OnClick="CambiarPanel_Click"
                                CommandArgument="pnlContrasenia" /></li>
                        <li>
                            <asp:LinkButton ID="lnkEmail" Text="Email" runat="server" OnClick="CambiarPanel_Click"
                                CommandArgument="pnlEmail" /></li>
                    </ol>
                    <li>
                        <asp:LinkButton ID="lnkAdministracion" Text="Administración" runat="server" OnClick="CambiarPanel_Click"
                            CommandArgument="pnlAdministracion" /></li>
                    <ol>
                        <li>
                            <asp:LinkButton ID="lnkIndicadores" Text="Indicadores" runat="server" OnClick="CambiarPanel_Click"
                                CommandArgument="pnlIndicadores" /></li>
                        <li>
                            <asp:LinkButton ID="lnkRoles" Text="Roles" runat="server" OnClick="CambiarPanel_Click"
                                CommandArgument="pnlRoles" /></li>
                        <li>
                            <asp:LinkButton ID="lnkUsuarios" Text="Usuarios" runat="server" OnClick="CambiarPanel_Click"
                                CommandArgument="pnlUsuarios" /></li>
                    </ol>
                    <li><a href="#encabezado11">Administración de Usuarios</a></li>
                    <ol>
                        <li><a href="#encabezado13">Registrar un usuario</a></li>
                        <li><a href="#encabezado14">Editar el perfil de un usuario </a></li>
                    </ol>
                    <li><a href="#encabezado15">Reportes</a></li>
                    <ol>
                        <li><a href="#encabezado16">Accesos</a></li>
                        <li><a href="#encabezado17">Calificaciones</a></li>
                        <li><a href="#encabezado18">Inasistencias</a></li>
                        <li><a href="#encabezado19">Sanciones</a></li>
                        <li><a href="#encabezado20">Indicadores</a></li>
                    </ol>
                    <li><a href="#encabezado21">Históricos</a></li>
                    <ol>
                        <li><a href="#encabezado22">Rendimiento</a></li>
                        <li><a href="#encabezado5">Alumno</a></li>
                    </ol>
                    <li><a href="#encabezado7">Mensajes</a></li>
                    <li><a href="#encabezado7">Alumnos</a></li>
                    <li><a href="#encabezado7">Agenda</a></li>
                    <li><a href="#encabezado7">Planificación</a></li>
                    <li><a href="#encabezado7">Foro</a></li>
                </ol>
            </td>
            <td class="TD90" valign="top" style="padding-left: 20px">
                <asp:UpdatePanel ID="udpContenido" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="pnlPresentacion" runat="server" Visible="true">
                            <b>EDU@R 2.0</b> gestiona el sistema con diferentes perfiles de acuerdo a los roles
                            vinculados a la entidad educativa. El Perfil Administrador tiene acceso a la mayoría
                            de las funcionalidades del sistema a diferencia del resto de los perfiles que cuentan
                            con acceso restringido. El Administrador es el único usuario que puede registrar
                            nuevos usuarios asignándole un perfil al mismo. Puede conocer el nivel de acceso
                            al sistema generando reportes de acceso al mismo y a las distintas secciones con
                            que cuenta.
                        </asp:Panel>
                        <asp:Panel ID="pnlBasicos" runat="server" Visible="false">
                            <asp:Image ImageUrl="~/Private/Manuales/Images/elementosBasicos.png" runat="server" /><br />
                            <li>Ruta de Navegación: Aquí se muestra el camino de navegación seguido hasta la página
                                actual.</li>
                            <li>Menu de Opciones: En este sector se encuentran las diferentes opciones que el usuario
                                puede elegir para acceder a la información que brinda el sistema. La información
                                se organiza siguiendo el principio de árbol, esto quiere decir que un menú puede
                                tener menus hijos y menus padres. Los menus hijos se despliegan en una serie de
                                opciones, y al seleccionar la opción requerida, se ingresará a una nueva página.</li>
                            <li>Link de Ayuda: al ingresar en este link se accederá al manual de usuario correspondiente.</li>
                            <li>Cerrar sesión: al hacer click en Cerrar Sesión finaliza la sesión actual en el sistema.</li>
                        </asp:Panel>
                        <asp:Panel ID="pnlCuenta" runat="server" Visible="false">
                            <asp:Image ID="Image1" ImageUrl="~/Private/Manuales/Images/miCuenta.png" runat="server" /><br />
                            <li>En la opción Mi Cuenta del menu, se puede actualizar o cambiar los datos de su cuenta.
                                <b>EDU@R 2.0</b> brinda la posibilidad de modificar los datos para mantener actualizada
                                su cuenta: Contraseña, Email, Pregunta secreta.</li>
                        </asp:Panel>
                        <asp:Panel ID="pnlPreguntaSecreta" runat="server" Visible="false">
                            <asp:Image ID="Image2" ImageUrl="~/Private/Manuales/Images/preguntaSecreta.png" runat="server" /><br />
                            <li>Ingresar la nueva pregunta secreta, luego la respuesta correspondiente y por último,
                                confirme el cambio presionando el botón "Cambiar Pregunta".</li>
                            <li>Si desea omitir los cambios realizados, presione el botón "Cancelar", el cual lo
                                retornará a la página de inicio.</li>
                        </asp:Panel>
                        <asp:Panel ID="pnlContrasenia" runat="server" Visible="false">
                            <asp:Image ID="Image3" ImageUrl="~/Private/Manuales/Images/contrasenia.png" runat="server" /><br />
                            <li>Ingrese la nueva contraseña, recuerde que la misma debe ser alfanumérica, y poseer
                                más de 5 caracteres. Repita la contraseña para poder confirmar la misma. Luego de
                                completar los datos requeridos, presione el botón "Cambiar contraseña".</li>
                            <li>Si desea omitir los cambios realizados, presione el botón "Cancelar", el cual lo
                                retornará a la página de inicio.</li>
                        </asp:Panel>
                        <asp:Panel ID="pnlEmail" runat="server" Visible="false">
                            <asp:Image ID="Image4" ImageUrl="~/Private/Manuales/Images/email.png" runat="server" /><br />
                            <li>Ingrese la dirección de email, como por ejemplo xxx@gmail.com., que quiere asociar
                                a <b>EDU@R 2.0</b>, luego confirme el cambio presionando el botón "Cambiar Email".
                                A partir del momento en que se modifique el email, esta dirección será la utilizada
                                por <b>EDU@R 2.0</b> para enviar informes y/o notificaciones, así como también para
                                recuperar su contraseña en caso de olvido.</li>
                            <li>Si desea omitir los cambios realizados, presione el botón "Cancelar", el cual lo
                                retornará a la página de inicio.</li>
                        </asp:Panel>
                        <asp:Panel ID="pnlAdministracion" runat="server" Visible="false">
                            <asp:Image ID="Image5" ImageUrl="~/Private/Manuales/Images/administracion.png" runat="server" /><br />
                            <li>Al posicionar el puntero sobre el menu Administración, se despliegan 3 opciones
                                posibles: Indicadores, Roles y Usuarios. Desde cada uno de estos items se puede
                                acceder a las configuraciones respectivas para cada sección.</li>
                        </asp:Panel>
                        <asp:Panel ID="pnlIndicadores" runat="server" Visible="false">
                            <asp:Image ID="Image6" ImageUrl="~/Private/Manuales/Images/indicadores.png" runat="server" /><br />
                            <li>Al seleccionar la opción Indicadores se accede al listado de indicadores definidos,
                                y luego, seleccionando la opción "Editar" se pueden modificar los parámetros que
                                definen al mismo.</li><br />
                            <asp:Image ID="Image7" ImageUrl="~/Private/Manuales/Images/configurarIndicador.png"
                                runat="server" /><br />
                            <br />
                            <li>En la parte superior de la pantalla contamos con las opciones "Guardar", que guarda
                                las modificaciones realizadas y "Volver" el cual cierra la pantalla descartando
                                los cambios.</li>
                            <li>Cada indicador cuenta con la opción "Invertir Escala" el cual define si el valor
                                "Verde" se aplica para los valores más cercanos a 0 o a la inversa. Del mismo modo
                                aplica para los indicadores Rojo y Amarillo.</li>
                            <li>A su vez, cada indicador, posee 3 niveles de Análisis: <b>Principal</b>, <b>Intermedio</b>
                                y <b>Secundario</b>, los cuales determinan el nivel de análisis en cuanto a las
                                fechas para cada uno. El nivel <b>Principal</b> se utiliza para las fechas más recientes,
                                <b>Intermedio</b> para un periodo anterior y <b>Secundario</b> para fechas anteriores
                                al nivel Intermedio.</li>
                        </asp:Panel>
                        <asp:Panel ID="pnlRoles" runat="server" Visible="false">
                            <asp:Image ID="Image8" ImageUrl="~/Private/Manuales/Images/roles.png" runat="server" /><br />
                            <li>Cada usuario tendrá asociado un Rol o Perfil dependiendo de la naturaleza de su
                                relación con la entidad educativa. Cada Perfil tiene restricciones y permisos de
                                acceso diferentes. En <b>EDU@R 2.0</b> el usuario con Perfil Administrador sólo
                                puede modificar la descripción asociada a cada Perfil, para adecuarlo a cada institución.
                            </li>
                            <br />
                            <asp:Image ID="Image9" ImageUrl="~/Private/Manuales/Images/editarRol.png" runat="server" /><br />
                            <br />
                            <li>Una vez ingresada la descripción y presionado el botón "Guardar", el sistema solicitará
                                Confirmación para actualizar la información.</li>
                            <asp:Image ID="Image10" ImageUrl="~/Private/Manuales/Images/confirmar.png" runat="server" /><br />
                            <br />
                            <li>Una vez actualizada la información en el sistema, se muestra un mensaje informando
                                el resultado Satisfactorio.</li>
                            <asp:Image ID="Image11" ImageUrl="~/Private/Manuales/Images/satisfactorio.png" runat="server" /><br />
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
