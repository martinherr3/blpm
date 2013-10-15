USE [master]
GO
EXEC xp_instance_regwrite N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'LoginMode', REG_DWORD, 2
GO
GO
CREATE LOGIN [EduarAdmin] WITH PASSWORD=N'admin2013', DEFAULT_DATABASE=[EDUAR], DEFAULT_LANGUAGE=[Español], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO 

ALTER LOGIN [EduarAdmin] ENABLE
GO

ALTER SERVER ROLE [sysadmin] ADD MEMBER [EduarAdmin]
GO

ALTER SERVER ROLE [securityadmin] ADD MEMBER [EduarAdmin]
GO

ALTER SERVER ROLE [serveradmin] ADD MEMBER [EduarAdmin]
GO

ALTER SERVER ROLE [setupadmin] ADD MEMBER [EduarAdmin]
GO

ALTER SERVER ROLE [processadmin] ADD MEMBER [EduarAdmin]
GO

ALTER SERVER ROLE [diskadmin] ADD MEMBER [EduarAdmin]
GO

ALTER SERVER ROLE [dbcreator] ADD MEMBER [EduarAdmin]
GO

ALTER SERVER ROLE [bulkadmin] ADD MEMBER [EduarAdmin]
GO

USE [EDUAR]
GO
CREATE USER [EduarAdmin] FOR LOGIN [EduarAdmin]
GO 

-- Le asigna el rol de owner
EXEC sp_addrolemember 'db_owner', 'EduarAdmin';

-- Le asigna el owner creado a la base de datos
--EXEC [EDUAR].dbo.sp_changedbowner N'EduarAdmin'

USE [EDUAR_aspnet_services]
GO
CREATE USER [EduarAdmin] FOR LOGIN [EduarAdmin]
GO 

-- Le asigna el rol de owner
EXEC sp_addrolemember 'db_owner', 'EduarAdmin';

-- Le asigna el owner creado a la base de datos
--EXEC [EDUAR].dbo.sp_changedbowner N'EduarAdmin'