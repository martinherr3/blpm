USE [EDUAR_aspnet_services]
GO

INSERT INTO [vw_aspnet_Applications] ([ApplicationName],[LoweredApplicationName],[ApplicationId],[Description])
VALUES ('EDUAR_UI','eduar_ui','340DD3A1-F04E-427C-8851-07EBECA2417A',NULL)
PRINT 'INSERT vw_aspnet_Applications'

INSERT INTO [aspnet_Roles]([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','2D3E43AD-60F7-4604-97D4-051A0058877F','Psicopedagogo','psicopedagogo','Rol de descripcion')
INSERT INTO [aspnet_Roles]([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','09FB2298-B8B7-4107-A2FD-FA3793B5CC78','Preceptor','preceptor','Rol asignado al Preceptores de la institución')
INSERT INTO [aspnet_Roles]([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','D3C57399-B73F-4F64-BCCE-EC5340318C3E','Administrativo','administrativo','Rol asignado al personal administrativo')
INSERT INTO [aspnet_Roles]([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','41AA295B-7655-4D6D-8ED2-919F6EE6A02A','Administrador','administrador','Rol todopoderoso del sistema')
INSERT INTO [aspnet_Roles]([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','BF650D66-DF58-4FB5-97A5-467541E37BB9','Alumno','alumno','Rol que se asigna a los alumnos de la institución')
INSERT INTO [aspnet_Roles]([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','8B3F6A0E-4C63-4107-8EEB-72BF2BBE1CE6','Director','director','Rol asignado al Director de la institución')
INSERT INTO [aspnet_Roles]([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','1D71CC3B-BB8E-44A6-988E-AF05C41F4B98','Docente','docente','Rol asignado a los docentes')
INSERT INTO [aspnet_Roles]([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','21949D8D-0EB9-496E-9B29-86A5370EA802','Tutor','tutor','Rol asignado a los tutores de alumnos')

INSERT INTO [vw_aspnet_Roles] ([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','41AA295B-7655-4D6D-8ED2-919F6EE6A02A','Administrador','administrador','Rol todopoderoso del sistema')
INSERT INTO [vw_aspnet_Roles] ([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','F6990D49-9F17-4C73-A325-40764ECF7EE8','Administrativo','administrativo','Rol asignado al personal administrativo')
INSERT INTO [vw_aspnet_Roles] ([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','BF650D66-DF58-4FB5-97A5-467541E37BB9','Alumno','alumno','Rol que se asigna a los alumnos de la institución')
INSERT INTO [vw_aspnet_Roles] ([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','8B3F6A0E-4C63-4107-8EEB-72BF2BBE1CE6','Director','director','Rol asignado al Director de la institución')
INSERT INTO [vw_aspnet_Roles] ([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','1D71CC3B-BB8E-44A6-988E-AF05C41F4B98','Docente','docente','Rol asignado a los docentes')
INSERT INTO [vw_aspnet_Roles] ([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','F6B45E7E-E655-44A7-833A-872112CBEB28','Preceptor','preceptor','Rol asignado a los preceptores para darles acceso al sistema')
INSERT INTO [vw_aspnet_Roles] ([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','D875CE96-F628-4ECB-8BF2-3A7B6F6BFA05','Psicopedagogo','psicopedagogo','NULL')
INSERT INTO [vw_aspnet_Roles] ([ApplicationId],[RoleId],[RoleName],[LoweredRoleName],[Description]) VALUES ('340DD3A1-F04E-427C-8851-07EBECA2417A','21949D8D-0EB9-496E-9B29-86A5370EA802','Tutor','tutor','NULL')