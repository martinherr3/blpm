USE [EDUAR_aspnet_services]
GO

SET IDENTITY_INSERT [EDUAR_aspnet_services].[dbo].[yaf_TopicStatus] ON

INSERT INTO [yaf_TopicStatus] ([TopicStatusID],[TopicStatusName],[BoardID],[DefaultDescription]) VALUES (6,'INFORMATIC',1,'Información')
INSERT INTO [yaf_TopicStatus] ([TopicStatusID],[TopicStatusName],[BoardID],[DefaultDescription]) VALUES (7,'SOLVED',1,'Resuelto')
INSERT INTO [yaf_TopicStatus] ([TopicStatusID],[TopicStatusName],[BoardID],[DefaultDescription]) VALUES (8,'QUESTION',1,'Pregunta')
INSERT INTO [yaf_TopicStatus] ([TopicStatusID],[TopicStatusName],[BoardID],[DefaultDescription]) VALUES (9,'ISSUE',1,'Problema')
INSERT INTO [yaf_TopicStatus] ([TopicStatusID],[TopicStatusName],[BoardID],[DefaultDescription]) VALUES (10,'FIXED',1,'Corregido')


SET IDENTITY_INSERT [EDUAR_aspnet_services].[dbo].[yaf_TopicStatus] OFF