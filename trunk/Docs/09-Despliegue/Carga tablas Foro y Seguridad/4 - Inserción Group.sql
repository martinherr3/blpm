USE [EDUAR_aspnet_services]
GO

SET IDENTITY_INSERT [EDUAR_aspnet_services].[dbo].[yaf_Board] ON
INSERT INTO [yaf_Board]([BoardID],[Name],[AllowThreaded],[MembershipAppName],[RolesAppName]) VALUES (1,'EDU@R',0,'','')
SET IDENTITY_INSERT [EDUAR_aspnet_services].[dbo].[yaf_Board] OFF

SET IDENTITY_INSERT [EDUAR_aspnet_services].[dbo].[yaf_Group] ON

INSERT INTO [yaf_Group]([GroupID],[BoardID],[Name],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES(2,1,'Guests',2,0,'default!font-size: 8pt; font-style: italic; font-weight: bold; color: #0c7333/yafpro!font-size: 8pt; color: #6e1987',1,NULL,0,NULL,NULL,0,0)
INSERT INTO [yaf_Group]([GroupID],[BoardID],[Name],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES(4,1,'Administrador',1,2147483647,'default!font-size: 8pt; color: red/yafpro!font-size: 8pt; color:blue',0,'',256,'URL,IMG,SPOILER,QUOTE','',10,120)
INSERT INTO [yaf_Group]([GroupID],[BoardID],[Name],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES(5,1,'Alumno',0,0,'',0,'',0,'','',0,0)
INSERT INTO [yaf_Group]([GroupID],[BoardID],[Name],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES(6,1,'Docente',8,0,'',0,'',0,'','',0,0)
INSERT INTO [yaf_Group]([GroupID],[BoardID],[Name],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES(7,1,'Tutor',0,0,NULL,0,NULL,0,NULL,NULL,0,0)
INSERT INTO [yaf_Group]([GroupID],[BoardID],[Name],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES(8,1,'Director',8,0,'',0,'',0,'','',0,0)
INSERT INTO [yaf_Group]([GroupID],[BoardID],[Name],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES(9,1,'Psicopedagogo',8,0,'',0,'',0,'','',0,0)
INSERT INTO [yaf_Group]([GroupID],[BoardID],[Name],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES(10,1,'Preceptor',8,0,'',0,'',0,'','',0,0)
INSERT INTO [yaf_Group]([GroupID],[BoardID],[Name],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES(11,1,'Administrativo',8,0,'',0,'',0,'','',0,0)

SET IDENTITY_INSERT [EDUAR_aspnet_services].[dbo].[yaf_Group] OFF