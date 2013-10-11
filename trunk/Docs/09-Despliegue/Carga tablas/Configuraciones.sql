INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (1, 'BaseDeDatosOrigen', 'Devuelve la cadena de conexión a la base de datos transaccional', 'Server=sofia;Database=alba;Uid=sa;Pwd=tesis;', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (3, 'PasswordInicial', 'Password Inicial para los usuarios generados por el administrador', 'h4d/w+B6jR76h4/XHzxtQw==', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (8, 'BaseDeDatosOrigenDesdeRemoto', 'Devuelve la cadena de conexión a la base de datos transaccional', 'Server=eduar.redirectme.net,44647;Database=alba;Uid=EduarUser;Pwd=u:n.~w5W1U;', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (11, 'emailFrom', 'Devuelve la dirección de email desde la que se envían mensajes de correo', 'proyecto.eduar2.0@gmail.com', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (12, 'servidorSMTP', 'Contiene el servidor smtp para el envio de mails', 'smtp.gmail.com', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (13, 'displayName', 'Nombre que se muestra al enviar mails', 'EDU@R 2.0', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (15, 'puertoSMTP', 'Puerto utilizado para el envio de mails', '587', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (16, 'enableSSL', 'Indica si el envío de mails se realiza por un canal seguro', 'true', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (18, 'SendUserName', 'Nombre de usuario para envío de emails', 'proyecto.eduar2.0@gmail.com', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (19, 'SendUserPass', 'Password de la cuenta de email', 'tesis1234.', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (20, 'PreguntaDefault', 'Pregunta secreta por defecto que se utiliza cuando el administrador crea un usuario', 'Mi Número de Documento es', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (26, 'BaseDeDatosOrigenDEV', 'Devuelve la cadena de conexión a la base de datos transaccional', 'Server=sofia;Database=eduar_transaccional;Uid=sa;Pwd=tesis;', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (27, 'BaseDeDatosOrigenDEVDesdeRemoto', 'Devuelve la cadena de conexión a la base de datos transaccional', 'Server=eduar.redirectme.net;Database=eduar_transaccional;Uid=sa;Pwd=tesis;', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (28, 'SancionesExpulsion', 'SancionesExpulsion', '15', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (29, 'LimiteInasistencias', 'LimiteInasistencias', '15', '1');
GO
INSERT INTO [Configuraciones] ([idConfiguracion], [nombre], [descripcion], [valor], [activo]) VALUES (31, 'AlertaAprobacion', 'Se utiliza para mostrar un mensaje cuando se aprueba una planificación con un porcentaje de cobertura menor al valor', '60', '1');
GO
