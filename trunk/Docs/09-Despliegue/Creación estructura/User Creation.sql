-- Crea al login EduarAdmin con password 'admin2013'.
CREATE LOGIN EduarAdmin 
    WITH PASSWORD = 'admin2013';
GO

-- Crea un usuario de la BD para el login creado anteriormente.
CREATE USER EduarAdmin FOR LOGIN EduarAdmin;
GO

-- Le asigna el rol de owner
EXEC sp_addrolemember 'db_owner', 'UsuarioAdmin';

-- Le asigna el owner creado a la base de datos
EXEC [EDUAR].dbo.sp_changedbowner N'UsuarioAdmin'

