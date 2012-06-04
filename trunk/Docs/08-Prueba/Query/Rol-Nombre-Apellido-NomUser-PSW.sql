
SELECT R.rolename as 'Rol', P.nombre as 'Nombre', P.apellido as 'Apellido', U.username as 'Nombre Usuario', M.password as 'Password'
FROM EDUAR_DEV.dbo.Personas as P 
INNER JOIN  EDUAR_DEV_aspnet_services.dbo.aspnet_Users as U ON P.username = U.UserName
INNER JOIN  EDUAR_DEV_aspnet_services.dbo.aspnet_Membership as M ON M.UserId=U.UserId
INNER JOIN EDUAR_DEV_aspnet_services.dbo.aspnet_UsersInRoles as UR on M.UserId=UR.UserId
INNER JOIN EDUAR_DEV_aspnet_services.dbo.aspnet_Roles as R on R.RoleId=UR.RoleId
ORDER BY  R.rolename

------------------------------------------------------


SELECT R.rolename, P.nombre, P.apellido, U.username, M.password
FROM EDUAR_DEV_aspnet_services.dbo.aspnet_Roles as R
INNER JOIN  EDUAR_DEV_aspnet_services.dbo.aspnet_UsersInRoles as UR on R.RoleId=UR.RoleId
INNER JOIN  EDUAR_DEV_aspnet_services.dbo.aspnet_Users as U ON UR.UserId = U.UserId
INNER JOIN  EDUAR_DEV_aspnet_services.dbo.aspnet_Membership as M ON M.UserId=U.UserId
INNER JOIN EDUAR_DEV.dbo.Personas as P on P.UserName=U.Username
ORDER BY  R.rolename
------------------------------------------------------------------------

SELECT R.rolename, U.username, M.password
FROM EDUAR_DEV_aspnet_services.dbo.aspnet_Roles as R
INNER JOIN  EDUAR_DEV_aspnet_services.dbo.aspnet_UsersInRoles as UR on R.RoleId=UR.RoleId
INNER JOIN  EDUAR_DEV_aspnet_services.dbo.aspnet_Users as U ON UR.UserId = U.UserId
INNER JOIN  EDUAR_DEV_aspnet_services.dbo.aspnet_Membership as M ON M.UserId=U.UserId
ORDER BY R.rolename