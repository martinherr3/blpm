USE [EDUAR_aspnet_services]

SET IDENTITY_INSERT [EDUAR_aspnet_services].[dbo].[yaf_AccessMask] ON

INSERT INTO [yaf_AccessMask]([AccessMaskID],[BoardID],[Name],[Flags],[SortOrder]) VALUES(1,1,'Acceso de Administrador',2047,4)
INSERT INTO [yaf_AccessMask]([AccessMaskID],[BoardID],[Name],[Flags],[SortOrder]) VALUES(2,1,'Acceso de Moderador',1511,3)
INSERT INTO [yaf_AccessMask]([AccessMaskID],[BoardID],[Name],[Flags],[SortOrder]) VALUES(3,1,'Acceso de Miembro',1189,2)
INSERT INTO [yaf_AccessMask]([AccessMaskID],[BoardID],[Name],[Flags],[SortOrder]) VALUES(4,1,'Acceso de Sólo Lectura',1,1)
INSERT INTO [yaf_AccessMask]([AccessMaskID],[BoardID],[Name],[Flags],[SortOrder]) VALUES(5,1,'Sin Acceso',0,0)

SET IDENTITY_INSERT [EDUAR_aspnet_services].[dbo].[yaf_AccessMask] OFF

