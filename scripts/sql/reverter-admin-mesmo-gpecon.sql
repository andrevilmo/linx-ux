/*
    Desfaz a criacao feita por criar-admin-mesmo-gpecon.sql.

    Apaga o login informado em @LoginAdmin no catalogo qa-ux-portal-3-12-RT:
    acessos, vinculos de GPECON, Membership e o usuario.
    Nos bancos de aplicacao desta mesma instancia, apaga perfil, filial,
    regras e a replica de TCS_USUARIO. Banco de outro servidor e apenas avisado.

    Nao mexe nos modulos da empresa (TCS_EMPRESA_MODULO): eles sao da empresa,
    nao deste login.

    Para apenas simular, mude @Confirmar para 0.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @LoginAdmin varchar(250) = 'admin.teste.franqueado';
DECLARE @Confirmar bit = 1;

IF DB_ID(N'qa-ux-portal-3-12-RT') IS NULL
    THROW 50001, 'Catalogo qa-ux-portal-3-12-RT nao existe nesta instancia.', 1;

USE [qa-ux-portal-3-12-RT];

DECLARE @Id bigint;
DECLARE @Uid uniqueidentifier;
DECLARE @ApplicationId uniqueidentifier;
DECLARE @UserId uniqueidentifier;

SELECT @ApplicationId = ApplicationId
FROM dbo.aspnet_Applications
WHERE LoweredApplicationName = '/';

SELECT TOP (1)
    @Id = ID_USUARIO,
    @Uid = UID_USUARIO
FROM LX_TCS.TCS_USUARIO_AUTENTICACAO
WHERE UPPER(NOME_AUTENTICACAO) = UPPER(@LoginAdmin)
ORDER BY ID_USUARIO;

IF @Id IS NULL
    THROW 50002, 'Login informado nao existe em TCS_USUARIO_AUTENTICACAO.', 1;

IF @ApplicationId IS NOT NULL
    SELECT @UserId = UserId
    FROM dbo.aspnet_Users
    WHERE ApplicationId = @ApplicationId
      AND LoweredUserName = LOWER(@LoginAdmin);

PRINT 'Login: ' + @LoginAdmin + ' (ID_USUARIO=' + CAST(@Id AS varchar(20)) + ')';

BEGIN TRY
BEGIN TRAN;

DECLARE @Db varchar(250);
DECLARE @Srv varchar(250);
DECLARE @Sql nvarchar(max);

DECLARE db_cursor CURSOR LOCAL FAST_FORWARD FOR
SELECT DISTINCT b.NOME_BANCO, b.NOME_SERVIDOR
FROM LX_TCS.TCS_USUARIO_ACESSO a
INNER JOIN LX_TCS.TCS_AMBIENTE_CONEXAO c ON c.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE
INNER JOIN LX_TCS.TCS_BANCO_SERVIDOR b ON b.ID_TCS_BANCO_SERVIDOR = c.ID_TCS_BANCO_SERVIDOR
WHERE a.ID_USUARIO = @Id
  AND b.NOME_BANCO IS NOT NULL
  AND LTRIM(RTRIM(b.NOME_BANCO)) <> '';

OPEN db_cursor;
FETCH NEXT FROM db_cursor INTO @Db, @Srv;

WHILE @@FETCH_STATUS = 0
BEGIN
    IF DB_ID(@Db) IS NULL
        PRINT 'Banco de aplicacao fora desta instancia, nao revertido: ' + @Db + ' @ ' + ISNULL(@Srv, '');
    ELSE IF OBJECT_ID(QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO', N'U') IS NULL
        PRINT 'Banco sem LX_TCS.TCS_USUARIO, ignorado: ' + @Db;
    ELSE
    BEGIN
        SET @Sql = N'
            IF OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_REGRA_MODULO'', N''U'') IS NOT NULL
                DELETE FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_REGRA_MODULO WHERE ID_USUARIO = @Id;
            IF OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_REGRA_TRANSACAO'', N''U'') IS NOT NULL
                DELETE FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_REGRA_TRANSACAO WHERE ID_USUARIO = @Id;
            IF OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_FILIAL'', N''U'') IS NOT NULL
                DELETE FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_FILIAL WHERE ID_USUARIO = @Id;
            IF OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_PERFIL'', N''U'') IS NOT NULL
                DELETE FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_PERFIL WHERE ID_USUARIO = @Id;
            IF OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_GPECON'', N''U'') IS NOT NULL
                DELETE FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_GPECON WHERE ID_USUARIO = @Id;
            DELETE FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO WHERE ID_USUARIO = @Id;
        ';
        EXEC sp_executesql @Sql, N'@Id bigint', @Id = @Id;
        PRINT 'Catalogo de aplicacao revertido: ' + @Db;
    END

    FETCH NEXT FROM db_cursor INTO @Db, @Srv;
END

CLOSE db_cursor;
DEALLOCATE db_cursor;

DECLARE @FkSchema sysname;
DECLARE @FkTabela sysname;
DECLARE @FkColuna sysname;
DECLARE fk_cursor CURSOR LOCAL FAST_FORWARD FOR
SELECT OBJECT_SCHEMA_NAME(f.parent_object_id), OBJECT_NAME(f.parent_object_id), COL_NAME(fc.parent_object_id, fc.parent_column_id)
FROM sys.foreign_keys f
INNER JOIN sys.foreign_key_columns fc ON fc.constraint_object_id = f.object_id
WHERE f.referenced_object_id = OBJECT_ID(N'LX_TCS.TCS_USUARIO_AUTENTICACAO');

OPEN fk_cursor;
FETCH NEXT FROM fk_cursor INTO @FkSchema, @FkTabela, @FkColuna;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @Sql = N'DELETE FROM ' + QUOTENAME(@FkSchema) + N'.' + QUOTENAME(@FkTabela)
        + N' WHERE ' + QUOTENAME(@FkColuna) + N' = @Id';
    EXEC sp_executesql @Sql, N'@Id bigint', @Id = @Id;
    FETCH NEXT FROM fk_cursor INTO @FkSchema, @FkTabela, @FkColuna;
END

CLOSE fk_cursor;
DEALLOCATE fk_cursor;

IF @UserId IS NOT NULL
BEGIN
    DELETE FROM dbo.aspnet_Membership WHERE UserId = @UserId;
    IF OBJECT_ID(N'dbo.aspnet_UsersInRoles', N'U') IS NOT NULL
        DELETE FROM dbo.aspnet_UsersInRoles WHERE UserId = @UserId;
    IF OBJECT_ID(N'dbo.aspnet_Profile', N'U') IS NOT NULL
        DELETE FROM dbo.aspnet_Profile WHERE UserId = @UserId;
    IF OBJECT_ID(N'dbo.aspnet_PersonalizationPerUser', N'U') IS NOT NULL
        DELETE FROM dbo.aspnet_PersonalizationPerUser WHERE UserId = @UserId;
    DELETE FROM dbo.aspnet_Users WHERE UserId = @UserId;
END

DELETE FROM LX_TCS.TCS_USUARIO_AUTENTICACAO WHERE ID_USUARIO = @Id;

IF @Confirmar = 1
BEGIN
    COMMIT;
    PRINT 'Excluido: ' + @LoginAdmin;
END
ELSE
BEGIN
    ROLLBACK;
    PRINT 'SIMULACAO: nada foi excluido. Execute de novo com @Confirmar = 1.';
END
END TRY
BEGIN CATCH
    IF CURSOR_STATUS('local', 'db_cursor') = 1
        CLOSE db_cursor;
    IF CURSOR_STATUS('local', 'db_cursor') >= -1
        DEALLOCATE db_cursor;
    IF CURSOR_STATUS('local', 'fk_cursor') = 1
        CLOSE fk_cursor;
    IF CURSOR_STATUS('local', 'fk_cursor') >= -1
        DEALLOCATE fk_cursor;
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
