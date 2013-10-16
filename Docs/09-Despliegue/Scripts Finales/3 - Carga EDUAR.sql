USE [EDUAR]
INSERT INTO [AmbitoEncuesta] ([idAmbito], [nombre], [descripcion]) VALUES ('1', 'Institucional', 'Los padres usan este tipo de encuestas para evaluar a la institución');
GO
INSERT INTO [AmbitoEncuesta] ([idAmbito], [nombre], [descripcion]) VALUES ('2', 'Asignatura', 'Los alumnos usan este tipo de encuestas para evaluar el dictado de una asignatura en general');
GO
USE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoEventoInstitucional] ON

INSERT INTO [TipoEventoInstitucional] ([idTipoEventoInstitucional], [descripcion], [activo]) VALUES ('1', 'Campaña Solidaria', '1');
GO
INSERT INTO [TipoEventoInstitucional] ([idTipoEventoInstitucional], [descripcion], [activo]) VALUES ('2', 'Charla Informativa', '1');
GO
INSERT INTO [TipoEventoInstitucional] ([idTipoEventoInstitucional], [descripcion], [activo]) VALUES ('3', 'Acto Patrio', '1');
GO
INSERT INTO [TipoEventoInstitucional] ([idTipoEventoInstitucional], [descripcion], [activo]) VALUES ('4', 'Cierre Ciclo Lectivo', '1');
GO
INSERT INTO [TipoEventoInstitucional] ([idTipoEventoInstitucional], [descripcion], [activo]) VALUES ('5', 'Social', '1');
GO
INSERT INTO [TipoEventoInstitucional] ([idTipoEventoInstitucional], [descripcion], [activo]) VALUES ('6', 'Actividad Extracurricular', '1');
GO
INSERT INTO [TipoEventoInstitucional] ([idTipoEventoInstitucional], [descripcion], [activo]) VALUES ('7', 'Otro', '1');
GO

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoEventoInstitucional] OFF
USE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoNovedadAulica] ON

INSERT INTO [TipoNovedadAulica] ([idTipoNovedadAulica], [descripcion]) VALUES ('1', 'Disciplina');
GO
INSERT INTO [TipoNovedadAulica] ([idTipoNovedadAulica], [descripcion]) VALUES ('2', 'Comportamiento');
GO
INSERT INTO [TipoNovedadAulica] ([idTipoNovedadAulica], [descripcion]) VALUES ('3', 'Otros');
GO

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoNovedadAulica] OFF
USE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoPersona] ON

INSERT INTO [TipoPersona] ([idTipoPersona], [nombre], [descripcion], [activo]) VALUES ('1', 'Personal', 'Personal de la institución, docente como administrativo', '1');
GO
INSERT INTO [TipoPersona] ([idTipoPersona], [nombre], [descripcion], [activo]) VALUES ('2', 'Alumno', 'Alumnos de la institución', '1');
GO
INSERT INTO [TipoPersona] ([idTipoPersona], [nombre], [descripcion], [activo]) VALUES ('3', 'Tutor', 'Padres o tutores de los alumnos', '1');
GO
INSERT INTO [TipoPersona] ([idTipoPersona], [nombre], [descripcion], [activo]) VALUES ('4', 'Administrador', 'Administrador del sistema', '1');
GO

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoPersona] OFF
USE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoPersonaRol] ON

INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('1', '1', 'Docente');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('2', '1', 'Psicopedagogo');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('3', '1', 'Preceptor');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('4', '1', 'Director');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('5', '2', 'Alumno');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('6', '3', 'Tutor');
GO
INSERT INTO [TipoPersonaRol] ([idTipoPersonaRol], [idTipoPersona], [nombreRol]) VALUES ('7', '4', 'Administrador');
GO

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoPersonaRol] OFFUSE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoRegistroClases] ON

INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('1', 'Clase Normal');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('2', 'Ausencia Docente Con Aviso');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('3', 'Ausencia Docente Sin Aviso');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('4', 'Ausencia Alumnos');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('5', 'Paro Docente');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('6', 'Taller Docente');
GO
INSERT INTO [TipoRegistroClases] ([idTipoRegistroClases], [nombre]) VALUES ('7', 'Feriado');
GO

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoRegistroClases] OFFUSE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[Configuraciones] ON

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

SET IDENTITY_INSERT [EDUAR].[dbo].[Configuraciones] OFFUSE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[ConfiguracionesEstablecimiento] ON

INSERT INTO [ConfiguracionesEstablecimiento] ([idConfiguracionEstablecimiento], [nombre], [descripcion], [valor], [activo]) VALUES (1, 'Nombre', 'Nombre Completo del Establecimiento', 'Ingeniero Cassaffousth', '1');
GO
INSERT INTO [ConfiguracionesEstablecimiento] ([idConfiguracionEstablecimiento], [nombre], [descripcion], [valor], [activo]) VALUES (2, 'Dirreccion', 'Direccion Postal Completa del establecimiento educativo', 'Dean Funes 1808', '1');
GO
INSERT INTO [ConfiguracionesEstablecimiento] ([idConfiguracionEstablecimiento], [nombre], [descripcion], [valor], [activo]) VALUES (3, 'HorarioInicio', 'Horario de Inicio de Actividades en el establecimiento', '7:00', '1');
GO
INSERT INTO [ConfiguracionesEstablecimiento] ([idConfiguracionEstablecimiento], [nombre], [descripcion], [valor], [activo]) VALUES (4, 'HorarioFinalizacion', 'Horario de Finalizacion de Actividades', '23:00', '1');
GO

SET IDENTITY_INSERT [EDUAR].[dbo].[ConfiguracionesEstablecimiento] OFFUSE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[EstadoNovedadAulica] ON

INSERT INTO [EstadoNovedadAulica] ([idEstadoNovedadAulica], [nombre], [esFinal]) VALUES ('3', 'Abierta', '0');
GO
INSERT INTO [EstadoNovedadAulica] ([idEstadoNovedadAulica], [nombre], [esFinal]) VALUES ('4', 'Cerrada', '1');
GO
INSERT INTO [EstadoNovedadAulica] ([idEstadoNovedadAulica], [nombre], [esFinal]) VALUES ('5', 'En Estudio', '0');
GO
INSERT INTO [EstadoNovedadAulica] ([idEstadoNovedadAulica], [nombre], [esFinal]) VALUES ('6', 'Descartada', '1');
GO

SET IDENTITY_INSERT [EDUAR].[dbo].[EstadoNovedadAulica] OFFUSE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[Indicadores] ON

INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (1, '% De Cobertura de la Planificación', 'Planificacion', '1', 0, 30, 40, 400, 85, 50, 50, 50, 25, 10);
GO
INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (2, 'Alumnos con más de X faltas', 'Inasistencias', '0', 1, 60, 150, 365, 1, 2, 3, 12, 7, 5);
GO
INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (3, 'Ausencias Docentes', 'InasistenciasDocentes', '0', 0, 200, 300, 400, 0, 3, 3, 15, 10, 15);
GO
INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (4, 'Alumnos con más de X Amonestaciones', 'Sanciones', '0', 4, 30, 60, 365, 3, 2, 3, 10, 7, 10);
GO
INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (5, 'Promedio General', 'PromedioGeneral', '1', 0, 90, 190, 365, 6, 6, 6, 4, 4, 4);
GO
INSERT INTO [Indicadores] ([idIndicador], [nombre], [nombreSP], [invertirEscala], [parametroCantidad], [diasHastaPrincipal], [diasHastaIntermedio], [diasHastaSecundario], [verdeNivelPrincipal], [verdeNivelIntermedio], [verdeNivelSecundario], [rojoNivelPrincipal], [rojoNivelIntermedio], [rojoNivelSecundario]) VALUES (6, 'Alumnos con X Asignaturas Reprobadas', 'Reprobadas', '0', 5, 90, 150, 365, 2, 2, 2, 4, 6, 8);
GO


SET IDENTITY_INSERT [EDUAR].[dbo].[Indicadores] OFFUSE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[MotivoCitacion] ON

INSERT INTO [MotivoCitacion] ([idMotivoCitacion], [descripcion]) VALUES ('1', 'Ausencias reiteradas');
GO
INSERT INTO [MotivoCitacion] ([idMotivoCitacion], [descripcion]) VALUES ('2', 'Disciplina');
GO


SET IDENTITY_INSERT [EDUAR].[dbo].[MotivoCitacion] OFFUSE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[ProcesosAutomaticos] ON

INSERT INTO [ProcesosAutomaticos] ([idProcesoAutomatico], [nombre], [descripcion], [activo], [emailNotificacion]) VALUES ('1', 'InformeInasistencias', 'Informe que se envia a los tutores semanalmente', '1', 'tesis_blpm@googlegroups.com');
GO
INSERT INTO [ProcesosAutomaticos] ([idProcesoAutomatico], [nombre], [descripcion], [activo], [emailNotificacion]) VALUES ('4', 'InformeSanciones', 'Informe que se envia a los tutores semanalmente', '1', 'tesis_blpm@googlegroups.com');
GO
INSERT INTO [ProcesosAutomaticos] ([idProcesoAutomatico], [nombre], [descripcion], [activo], [emailNotificacion]) VALUES ('5', 'ImportarTablasAuxiliares', 'Ejecuta la importación de estas 3 entidades', '1', 'tesis_blpm@googlegroups.com');
GO
INSERT INTO [ProcesosAutomaticos] ([idProcesoAutomatico], [nombre], [descripcion], [activo], [emailNotificacion]) VALUES ('7', 'ImportarAlumnos', 'Ejecuta el proceso de importación de Alumnos', '1', 'tesis_blpm@googlegroups.com');
GO
INSERT INTO [ProcesosAutomaticos] ([idProcesoAutomatico], [nombre], [descripcion], [activo], [emailNotificacion]) VALUES ('8', 'ImportarTutores', 'Ejecuta el proceso de importación de Tutores, Tipo de Tutor y relación con Alumnos', '1', 'tesis_blpm@googlegroups.com');
GO
INSERT INTO [ProcesosAutomaticos] ([idProcesoAutomatico], [nombre], [descripcion], [activo], [emailNotificacion]) VALUES ('9', 'ImportarPersonal', 'Ejecuta el proceso de importación de Docentes y Personal Institucional', '1', 'tesis_blpm@googlegroups.com');
GO
INSERT INTO [ProcesosAutomaticos] ([idProcesoAutomatico], [nombre], [descripcion], [activo], [emailNotificacion]) VALUES ('11', 'ImportarConfiguracionAcademica', 'Ejecuta el proceso de importación de Asignaturas, Cursos, Ciclos Lectivos', '1', 'tesis_blpm@googlegroups.com');
GO
INSERT INTO [ProcesosAutomaticos] ([idProcesoAutomatico], [nombre], [descripcion], [activo], [emailNotificacion]) VALUES ('13', 'ImportarCalificaciones', 'Ejecuta el proceso de importación de Calificaciones Parciales', '1', 'tesis_blpm@googlegroups.com');
GO
INSERT INTO [ProcesosAutomaticos] ([idProcesoAutomatico], [nombre], [descripcion], [activo], [emailNotificacion]) VALUES ('14', 'ImportarAsistencia', 'Ejecuta el proceso de importación de Asistencia', '1', 'tesis_blpm@googlegroups.com');
GO
INSERT INTO [ProcesosAutomaticos] ([idProcesoAutomatico], [nombre], [descripcion], [activo], [emailNotificacion]) VALUES ('15', 'ImportarSanciones', 'Ejecuta el proceso de importación de Sanciones', '1', 'tesis_blpm@googlegroups.com');
GO

SET IDENTITY_INSERT [EDUAR].[dbo].[ProcesosAutomaticos] OFF
USE [EDUAR]

--SET IDENTITY_INSERT [EDUAR].[dbo].[RelAmbitoRol] ON

INSERT INTO [RelAmbitoRol] ([idAmbito], [rolName]) VALUES ('1', 'Tutor');
GO
INSERT INTO [RelAmbitoRol] ([idAmbito], [rolName]) VALUES ('1', 'Alumno');
GO
INSERT INTO [RelAmbitoRol] ([idAmbito], [rolName]) VALUES ('2', 'Alumno');
GO


--SET IDENTITY_INSERT [EDUAR].[dbo].[RelAmbitoRol] OFF
USE [EDUAR]

SET IDENTITY_INSERT [EDUAR].[dbo].[TipoEvento] ON

INSERT INTO [TipoEvento] ([idTipoEvento], [descripcion]) VALUES ('1', 'Evaluación');
GO
INSERT INTO [TipoEvento] ([idTipoEvento], [descripcion]) VALUES ('2', 'Reunión');
GO
INSERT INTO [TipoEvento] ([idTipoEvento], [descripcion]) VALUES ('3', 'Excursión');
GO
INSERT INTO [TipoEvento] ([idTipoEvento], [descripcion]) VALUES ('4', 'Clase Diaria');
GO


SET IDENTITY_INSERT [EDUAR].[dbo].[TipoEvento] OFF
