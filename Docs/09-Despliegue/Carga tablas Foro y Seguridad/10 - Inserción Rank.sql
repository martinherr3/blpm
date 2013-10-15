USE [EDUAR_aspnet_services]
GO

SET IDENTITY_INSERT [EDUAR_aspnet_services].[dbo].[yaf_Rank] ON

INSERT INTO [yaf_Rank]([RankID],[BoardID],[Name],[MinPosts],[RankImage],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES (1,1,'Administración',NULL,'Administration.png',0,2147483647,'default!font-size: 8pt; color: #811334/yafpro!font-size: 8pt; color:blue',0,'',0,'','',0,0)
INSERT INTO [yaf_Rank]([RankID],[BoardID],[Name],[MinPosts],[RankImage],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES (2,1,'Invitado',NULL,'Guest.png',0,0,'',100,'',0,'','',0,0)
INSERT INTO [yaf_Rank]([RankID],[BoardID],[Name],[MinPosts],[RankImage],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES (3,1,'Novato',0,'Newbie.png',3,10,'',3,'',0,'','',0,0)
INSERT INTO [yaf_Rank]([RankID],[BoardID],[Name],[MinPosts],[RankImage],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES (4,1,'Miembro',10,'Member.png',2,30,'',2,'',0,'','',0,0)
INSERT INTO [yaf_Rank]([RankID],[BoardID],[Name],[MinPosts],[RankImage],[Flags],[PMLimit],[Style],[SortOrder],[Description],[UsrSigChars],[UsrSigBBCodes],[UsrSigHTMLTags],[UsrAlbums],[UsrAlbumImages]) VALUES (5,1,'Miembro Avanzado',30,'Advanced-Member.png',2,100,'',1,'',0,'','',0,0)

SET IDENTITY_INSERT [EDUAR_aspnet_services].[dbo].[yaf_Rank] OFF
