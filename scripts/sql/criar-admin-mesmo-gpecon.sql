/*
    Cria um usuario administrador no mesmo grupo economico (GPECON) de um usuario alvo,
    para entrar no UX e gerenciar esse usuario na tela Usuário (Server).

    A grade dessa tela so lista usuarios cujo ID_LINX_GPECON e o grupo da sessao
    (ou quem esta ligado a esse grupo em TCS_USUARIO_AUTENTICACAO_GPECON).
    Por isso o novo admin nasce com o mesmo ID_LINX_GPECON da usuaria alvo.

    O script entra no catalogo qa-ux-portal-3-12-RT
    (Portal / FrameworkAutorizacao: LX_TCS.TCS_USUARIO_AUTENTICACAO e dbo.aspnet_Users).
    A conexao pode abrir em outro banco da mesma instancia; o USE abaixo troca para esse catalogo.

    Antes de executar:
      Ajuste @LoginAlvo se o alvo nao for jacqueline.dmaques.
      A senha do novo admin ja esta definida e o script grava ao executar.
      Para apenas simular, mude @Confirmar para 0.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

IF DB_ID(N'qa-ux-portal-3-12-RT') IS NULL
    THROW 50001, 'Catalogo qa-ux-portal-3-12-RT nao existe nesta instancia.', 1;

USE [qa-ux-portal-3-12-RT];

DECLARE @LoginAlvo   varchar(250) = 'jacqueline.dmaques';
DECLARE @LoginAdmin  varchar(250) = 'admin.jacqueline.dmaques';
DECLARE @NomeAdmin   varchar(250) = 'Admin Jacqueline DMAQUES';
DECLARE @EmailAdmin  varchar(250) = 'admin.jacqueline.dmaques@local';
DECLARE @Senha       nvarchar(128) = N'P@ssw0rd1920398';
DECLARE @Confirmar   bit = 1;

DECLARE @IdAlvo bigint;
DECLARE @IdGpecon int;
DECLARE @NomeGpecon varchar(250);
DECLARE @UidGpecon uniqueidentifier;
DECLARE @IdModelo bigint;
DECLARE @ModeloNome varchar(250);
DECLARE @IdNovo bigint;
DECLARE @UidNovo uniqueidentifier = NEWID();
DECLARE @UserId uniqueidentifier = NEWID();
DECLARE @ApplicationId uniqueidentifier;
DECLARE @Agora datetime = GETDATE();
DECLARE @SenhaGerada bit = 0;

IF OBJECT_ID(N'LX_TCS.TCS_USUARIO_AUTENTICACAO', N'U') IS NULL
    THROW 50001, 'Tabela LX_TCS.TCS_USUARIO_AUTENTICACAO nao encontrada em qa-ux-portal-3-12-RT.', 1;

IF OBJECT_ID(N'dbo.aspnet_Users', N'U') IS NULL OR OBJECT_ID(N'dbo.aspnet_Membership', N'U') IS NULL
    THROW 50001, 'Tabelas dbo.aspnet_Users / aspnet_Membership nao encontradas neste banco.', 1;

SELECT @ApplicationId = ApplicationId
FROM dbo.aspnet_Applications
WHERE LoweredApplicationName = '/';

IF @ApplicationId IS NULL
    THROW 50001, 'Aplicacao ASP.NET Membership "/" nao encontrada em dbo.aspnet_Applications.', 1;

SELECT TOP (1)
    @IdAlvo = ID_USUARIO,
    @IdGpecon = ID_LINX_GPECON
FROM LX_TCS.TCS_USUARIO_AUTENTICACAO
WHERE NOME_AUTENTICACAO = @LoginAlvo
ORDER BY INATIVO, ID_USUARIO;

IF @IdAlvo IS NULL
BEGIN
    SELECT TOP (1)
        @IdAlvo = ID_USUARIO,
        @IdGpecon = ID_LINX_GPECON
    FROM LX_TCS.TCS_USUARIO_AUTENTICACAO
    WHERE UPPER(NOME_AUTENTICACAO) = UPPER(@LoginAlvo)
    ORDER BY INATIVO, ID_USUARIO;
END

IF @IdAlvo IS NULL
    THROW 50002, 'Usuario alvo nao encontrado em TCS_USUARIO_AUTENTICACAO.', 1;

SELECT
    @NomeGpecon = NOME_EMPRESA,
    @UidGpecon = UID_EMPRESA
FROM LX_TCS.TCS_EMPRESA_AUTENTICACAO
WHERE ID_LINX = @IdGpecon;

IF @NomeGpecon IS NULL
    THROW 50002, 'GPECON do usuario alvo nao existe em TCS_EMPRESA_AUTENTICACAO.', 1;

IF EXISTS (
    SELECT 1
    FROM LX_TCS.TCS_USUARIO_AUTENTICACAO
    WHERE UPPER(NOME_AUTENTICACAO) = UPPER(@LoginAdmin)
)
    THROW 50003, 'O login do novo admin ja existe. Escolha outro @LoginAdmin.', 1;

IF EXISTS (
    SELECT 1
    FROM dbo.aspnet_Users
    WHERE ApplicationId = @ApplicationId
      AND LoweredUserName = LOWER(@LoginAdmin)
)
    THROW 50003, 'O login do novo admin ja existe no ASP.NET Membership.', 1;

IF @Senha IS NULL OR LTRIM(RTRIM(@Senha)) = N''
BEGIN
    DECLARE @Upper varchar(24) = 'ABCDEFGHJKLMNPQRSTUVWXYZ';
    DECLARE @Lower varchar(24) = 'abcdefghijkmnopqrstuvwxyz';
    DECLARE @Digit varchar(8) = '23456789';
    DECLARE @Special varchar(8) = '#@!%+*?&';
    DECLARE @n int = ABS(CHECKSUM(NEWID()));

    SET @Senha =
        SUBSTRING(@Upper, (@n % 24) + 1, 1)
        + SUBSTRING(@Lower, ((@n / 7) % 24) + 1, 1)
        + SUBSTRING(@Lower, ((@n / 11) % 24) + 1, 1)
        + SUBSTRING(@Digit, ((@n / 13) % 8) + 1, 1)
        + SUBSTRING(@Special, ((@n / 17) % 8) + 1, 1)
        + SUBSTRING(@Upper, ((@n / 19) % 24) + 1, 1)
        + SUBSTRING(@Lower, ((@n / 23) % 24) + 1, 1)
        + N'k'
        + SUBSTRING(@Digit, ((@n / 29) % 8) + 1, 1)
        + SUBSTRING(@Lower, ((@n / 31) % 24) + 1, 1)
        + SUBSTRING(@Special, ((@n / 37) % 8) + 1, 1)
        + SUBSTRING(@Upper, ((@n / 41) % 24) + 1, 1)
        + SUBSTRING(@Lower, ((@n / 43) % 24) + 1, 1)
        + N'q';
    SET @SenhaGerada = 1;
END

IF LEN(@Senha) < 12
    OR @Senha COLLATE Latin1_General_CS_AS NOT LIKE '%[A-Z]%'
    OR @Senha COLLATE Latin1_General_CS_AS NOT LIKE '%[a-z]%'
    OR @Senha NOT LIKE '%[0-9]%'
    OR @Senha NOT LIKE '%[^0-9A-Za-z]%'
    THROW 50004, 'Senha fora da regra do UX: minimo 12, com maiuscula, minuscula, numero e especial.', 1;

/* Ambientes do GPECON: a empresa do grupo e as empresas ligadas a ele. */
DECLARE @Ambiente TABLE (ID_TCS_AMBIENTE int PRIMARY KEY);

INSERT INTO @Ambiente (ID_TCS_AMBIENTE)
SELECT a.ID_TCS_AMBIENTE
FROM LX_TCS.TCS_AMBIENTE a
WHERE a.ID_LINX = @IdGpecon
   OR a.ID_LINX IN (
        SELECT g.ID_LINX
        FROM LX_TCS.TCS_EMPRESA_GPECON g
        WHERE g.ID_LINX_GPECON = @IdGpecon
   );

/*
    Modelo de acessos e perfis: um admin ativo deste GPECON que nao e suporte.
    Se nao houver, usa os acessos da propria usuaria alvo e marca administrador.
*/
SELECT TOP (1)
    @IdModelo = u.ID_USUARIO,
    @ModeloNome = u.NOME_AUTENTICACAO
FROM LX_TCS.TCS_USUARIO_AUTENTICACAO u
WHERE u.ID_LINX_GPECON = @IdGpecon
  AND u.INATIVO = 0
  AND u.INDICA_ACESSO_SUPORTE = 0
  AND u.ID_USUARIO <> @IdAlvo
  AND EXISTS (
        SELECT 1
        FROM LX_TCS.TCS_USUARIO_ACESSO a
        INNER JOIN @Ambiente amb ON amb.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE
        WHERE a.ID_USUARIO = u.ID_USUARIO
          AND a.INDICA_ADMINISTRADOR = 1
  )
ORDER BY u.ID_USUARIO;

DECLARE @Acesso TABLE (
    ID_TCS_AMBIENTE int NOT NULL PRIMARY KEY,
    INDICA_MULTI_GPECON bit NOT NULL,
    INDICA_ACESSO_PADRAO bit NOT NULL,
    ID_TCS_AMBIENTE_RELACIONADO int NULL
);

IF @IdModelo IS NOT NULL
BEGIN
    INSERT INTO @Acesso (ID_TCS_AMBIENTE, INDICA_MULTI_GPECON, INDICA_ACESSO_PADRAO, ID_TCS_AMBIENTE_RELACIONADO)
    SELECT
        a.ID_TCS_AMBIENTE,
        a.INDICA_MULTI_GPECON,
        a.INDICA_ACESSO_PADRAO,
        CASE WHEN rel.ID_TCS_AMBIENTE IS NULL THEN NULL ELSE a.ID_TCS_AMBIENTE_RELACIONADO END
    FROM LX_TCS.TCS_USUARIO_ACESSO a
    INNER JOIN @Ambiente amb ON amb.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE
    LEFT JOIN @Ambiente rel ON rel.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE_RELACIONADO
    WHERE a.ID_USUARIO = @IdModelo;
END

/* Completa com os ambientes em que a usuaria alvo ja entra. */
INSERT INTO @Acesso (ID_TCS_AMBIENTE, INDICA_MULTI_GPECON, INDICA_ACESSO_PADRAO, ID_TCS_AMBIENTE_RELACIONADO)
SELECT
    a.ID_TCS_AMBIENTE,
    0,
    a.INDICA_ACESSO_PADRAO,
    CASE WHEN rel.ID_TCS_AMBIENTE IS NULL THEN NULL ELSE a.ID_TCS_AMBIENTE_RELACIONADO END
FROM LX_TCS.TCS_USUARIO_ACESSO a
INNER JOIN @Ambiente amb ON amb.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE
LEFT JOIN @Ambiente rel ON rel.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE_RELACIONADO
WHERE a.ID_USUARIO = @IdAlvo
  AND NOT EXISTS (SELECT 1 FROM @Acesso x WHERE x.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE);

IF NOT EXISTS (SELECT 1 FROM @Acesso)
BEGIN
    INSERT INTO @Acesso (ID_TCS_AMBIENTE, INDICA_MULTI_GPECON, INDICA_ACESSO_PADRAO, ID_TCS_AMBIENTE_RELACIONADO)
    SELECT ID_TCS_AMBIENTE, 0, 0, NULL
    FROM @Ambiente;
END

IF NOT EXISTS (SELECT 1 FROM @Acesso)
    THROW 50005, 'Nenhum ambiente deste GPECON foi encontrado. O admin nao teria onde entrar.', 1;

IF (SELECT COUNT(*) FROM @Acesso WHERE INDICA_ACESSO_PADRAO = 1) <> 1
BEGIN
    UPDATE @Acesso SET INDICA_ACESSO_PADRAO = 0;

    UPDATE a
    SET INDICA_ACESSO_PADRAO = 1
    FROM @Acesso a
    WHERE a.ID_TCS_AMBIENTE = (SELECT MIN(ID_TCS_AMBIENTE) FROM @Acesso);
END

DECLARE @Salt varbinary(16) = CRYPT_GEN_RANDOM(16);
/* SqlMembershipProvider PasswordFormat=1: SHA1(salt + UTF-16LE da senha), sem padding. */
DECLARE @SenhaBytes varbinary(8000) = CONVERT(varbinary(8000), CONVERT(varbinary(max), @Senha));
IF DATALENGTH(@SenhaBytes) <> LEN(@Senha) * 2
    THROW 50007, 'Falha ao codificar a senha em UTF-16. O hash do Membership ficaria invalido.', 1;
DECLARE @Hash varbinary(20) = HASHBYTES('SHA1', @Salt + @SenhaBytes);
DECLARE @Password64 varchar(128);
DECLARE @Salt64 varchar(128);

SELECT @Password64 = CAST(N'' AS xml).value('xs:base64Binary(sql:variable("@Hash"))', 'varchar(128)');
SELECT @Salt64 = CAST(N'' AS xml).value('xs:base64Binary(sql:variable("@Salt"))', 'varchar(128)');

BEGIN TRY
BEGIN TRAN;

INSERT INTO LX_TCS.TCS_USUARIO_AUTENTICACAO (
    UID_USUARIO,
    NOME_AUTENTICACAO,
    NOME_CURTO_USUARIO,
    NOME_USUARIO,
    INATIVO,
    VIGENCIA_INICIAL,
    VIGENCIA_FINAL,
    DATA_EXPIRACAO_SENHA,
    EMAIL,
    ID_LINX_GPECON,
    LX_PFJ_FISICA_JURIDICA,
    DATA_CADASTRO,
    AUTENTICACAO_WINDOWS,
    INDICA_ACESSO_SUPORTE,
    INDICA_USUARIO_SERVICO
)
VALUES (
    @UidNovo,
    @LoginAdmin,
    LEFT(@LoginAdmin, 60),
    @NomeAdmin,
    0,
    CONVERT(datetime, '2000-01-01'),
    CONVERT(datetime, '2099-12-31'),
    CONVERT(datetime, '2099-12-31'),
    @EmailAdmin,
    @IdGpecon,
    1,
    @Agora,
    0,
    0,
    0
);

SELECT @IdNovo = ID_USUARIO
FROM LX_TCS.TCS_USUARIO_AUTENTICACAO
WHERE UID_USUARIO = @UidNovo;

IF @IdNovo IS NULL
    THROW 50006, 'Nao foi possivel ler o ID_USUARIO recem-criado.', 1;

IF COL_LENGTH(N'LX_TCS.TCS_USUARIO_AUTENTICACAO', N'INDICA_UTILIZA_SSO') IS NOT NULL
BEGIN
    DECLARE @SqlSso nvarchar(300) = N'
        UPDATE LX_TCS.TCS_USUARIO_AUTENTICACAO
        SET INDICA_UTILIZA_SSO = 0
        WHERE ID_USUARIO = @Id';
    EXEC sp_executesql @SqlSso, N'@Id bigint', @Id = @IdNovo;
END

IF COL_LENGTH(N'LX_TCS.TCS_USUARIO_AUTENTICACAO', N'INDICA_UTILIZA_MFA') IS NOT NULL
BEGIN
    /* Sem segredo TOTP o login com MFA ligado nao completa. Liga depois na propria tela, se a politica exigir. */
    DECLARE @SqlMfa nvarchar(300) = N'
        UPDATE LX_TCS.TCS_USUARIO_AUTENTICACAO
        SET INDICA_UTILIZA_MFA = 0
        WHERE ID_USUARIO = @Id';
    EXEC sp_executesql @SqlMfa, N'@Id bigint', @Id = @IdNovo;
END

INSERT INTO LX_TCS.TCS_USUARIO_ACESSO (
    INDICA_ADMINISTRADOR,
    INDICA_MULTI_GPECON,
    INDICA_ACESSO_PADRAO,
    ID_TCS_AMBIENTE,
    ID_USUARIO,
    ID_TCS_AMBIENTE_RELACIONADO
)
SELECT
    1,
    INDICA_MULTI_GPECON,
    INDICA_ACESSO_PADRAO,
    ID_TCS_AMBIENTE,
    @IdNovo,
    ID_TCS_AMBIENTE_RELACIONADO
FROM @Acesso;

INSERT INTO dbo.aspnet_Users (
    ApplicationId,
    UserId,
    UserName,
    LoweredUserName,
    MobileAlias,
    IsAnonymous,
    LastActivityDate
)
VALUES (
    @ApplicationId,
    @UserId,
    @LoginAdmin,
    LOWER(@LoginAdmin),
    NULL,
    0,
    @Agora
);

INSERT INTO dbo.aspnet_Membership (
    ApplicationId,
    UserId,
    Password,
    PasswordFormat,
    PasswordSalt,
    MobilePIN,
    Email,
    LoweredEmail,
    PasswordQuestion,
    PasswordAnswer,
    IsApproved,
    IsLockedOut,
    CreateDate,
    LastLoginDate,
    LastPasswordChangedDate,
    LastLockoutDate,
    FailedPasswordAttemptCount,
    FailedPasswordAttemptWindowStart,
    FailedPasswordAnswerAttemptCount,
    FailedPasswordAnswerAttemptWindowStart,
    Comment
)
VALUES (
    @ApplicationId,
    @UserId,
    @Password64,
    1,
    @Salt64,
    NULL,
    NULLIF(@EmailAdmin, ''),
    NULLIF(LOWER(@EmailAdmin), ''),
    NULL,
    NULL,
    1,
    0,
    @Agora,
    @Agora,
    @Agora,
    CONVERT(datetime, '17540101', 112),
    0,
    CONVERT(datetime, '17540101', 112),
    0,
    CONVERT(datetime, '17540101', 112),
    NULL
);

/*
    Libera para a empresa do admin todos os modulos do catalogo.
    A tela da empresa mostra isso em Modulos Permitidos.
*/
DECLARE @ModulosEmpresa int = 0;
IF OBJECT_ID(N'LX_TCS.TCS_EMPRESA_MODULO', N'U') IS NOT NULL
   AND OBJECT_ID(N'LX_TCS.TCS_MODULO_AUTORIZACAO', N'U') IS NOT NULL
BEGIN
    INSERT INTO LX_TCS.TCS_EMPRESA_MODULO (ID_MODULO, ID_LINX)
    SELECT m.ID_MODULO, @IdGpecon
    FROM LX_TCS.TCS_MODULO_AUTORIZACAO m
    WHERE NOT EXISTS (
        SELECT 1
        FROM LX_TCS.TCS_EMPRESA_MODULO e
        WHERE e.ID_MODULO = m.ID_MODULO
          AND e.ID_LINX = @IdGpecon
    );
    SET @ModulosEmpresa = @@ROWCOUNT;
END

/*
    Replica o usuario nos bancos de aplicacao deste GPECON que estao nesta instancia
    e copia perfil e filial do admin modelo (ou da usuaria alvo, se nao houver modelo).
*/
DECLARE @IdPerfilOrigem bigint = COALESCE(@IdModelo, @IdAlvo);
DECLARE @Db varchar(250);
DECLARE @Srv varchar(250);
DECLARE @Sql nvarchar(max);
DECLARE @Perfis int;
DECLARE @Filiais int;

DECLARE db_cursor CURSOR LOCAL FAST_FORWARD FOR
SELECT DISTINCT b.NOME_BANCO, b.NOME_SERVIDOR
FROM LX_TCS.TCS_USUARIO_ACESSO a
INNER JOIN LX_TCS.TCS_AMBIENTE_CONEXAO c ON c.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE
INNER JOIN LX_TCS.TCS_BANCO_SERVIDOR b ON b.ID_TCS_BANCO_SERVIDOR = c.ID_TCS_BANCO_SERVIDOR
WHERE a.ID_USUARIO = @IdNovo
  AND b.NOME_BANCO IS NOT NULL
  AND LTRIM(RTRIM(b.NOME_BANCO)) <> '';

OPEN db_cursor;
FETCH NEXT FROM db_cursor INTO @Db, @Srv;

WHILE @@FETCH_STATUS = 0
BEGIN
    IF DB_ID(@Db) IS NULL
    BEGIN
        PRINT 'Banco de aplicacao fora desta instancia, nao replicado: ' + @Db + ' @ ' + ISNULL(@Srv, '');
    END
    ELSE IF OBJECT_ID(QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO', N'U') IS NULL
    BEGIN
        PRINT 'Banco sem LX_TCS.TCS_USUARIO, ignorado: ' + @Db;
    END
    ELSE
    BEGIN
        SET @Sql = N'
            IF NOT EXISTS (SELECT 1 FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO WHERE ID_USUARIO = @IdNovo)
            INSERT INTO ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO (
                ID_USUARIO, UID_USUARIO, NOME_USUARIO, ID_LINX, EMAIL, LX_PFJ_FISICA_JURIDICA, DATA_CADASTRO
            )
            VALUES (@IdNovo, @UidNovo, @NomeAdmin, @IdGpecon, @EmailAdmin, 1, @Agora);

            INSERT INTO ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_PERFIL (ID_LINX, ID_PERFIL, ID_USUARIO)
            SELECT p.ID_LINX, p.ID_PERFIL, @IdNovo
            FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_PERFIL p
            WHERE p.ID_USUARIO = @IdOrigem
              AND NOT EXISTS (
                    SELECT 1
                    FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_PERFIL x
                    WHERE x.ID_USUARIO = @IdNovo
                      AND x.ID_PERFIL = p.ID_PERFIL
                      AND x.ID_LINX = p.ID_LINX
              );

            SET @PerfisOut = @@ROWCOUNT;

            IF OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_FILIAL'', N''U'') IS NOT NULL
            BEGIN
                INSERT INTO ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_FILIAL (ID_USUARIO, ID_FILIAL_PFJ, ID_LINX)
                SELECT @IdNovo, f.ID_FILIAL_PFJ, f.ID_LINX
                FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_FILIAL f
                WHERE f.ID_USUARIO = @IdOrigem
                  AND NOT EXISTS (
                        SELECT 1
                        FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_FILIAL x
                        WHERE x.ID_USUARIO = @IdNovo
                          AND x.ID_FILIAL_PFJ = f.ID_FILIAL_PFJ
                  );
                SET @FiliaisOut = @@ROWCOUNT;
            END
            ELSE
                SET @FiliaisOut = 0;

            IF OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_REGRA_TRANSACAO'', N''U'') IS NOT NULL
                INSERT INTO ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_REGRA_TRANSACAO
                    (ID_USUARIO, LX_REGRA_ACESSO_TRANSACAO, REGRA_TRANSACAO, ID_TRANSACAO, ID_LINX)
                SELECT @IdNovo, r.LX_REGRA_ACESSO_TRANSACAO, r.REGRA_TRANSACAO, r.ID_TRANSACAO, r.ID_LINX
                FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_REGRA_TRANSACAO r
                WHERE r.ID_USUARIO = @IdOrigem
                  AND NOT EXISTS (
                        SELECT 1
                        FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_REGRA_TRANSACAO x
                        WHERE x.ID_USUARIO = @IdNovo AND x.ID_TRANSACAO = r.ID_TRANSACAO AND x.ID_LINX = r.ID_LINX
                  );

            IF OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_REGRA_MODULO'', N''U'') IS NOT NULL
               AND OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_MODULO'', N''U'') IS NOT NULL
                INSERT INTO ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_REGRA_MODULO
                    (ID_USUARIO, LX_REGRA_ACESSO_MODULO, REGRA_TRANSACAO, ID_MODULO, ID_LINX)
                SELECT @IdNovo, 2, NULL, m.ID_MODULO, m.ID_LINX
                FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_MODULO m
                WHERE m.ID_LINX = @IdGpecon
                  AND m.INATIVO = 0
                  AND NOT EXISTS (
                        SELECT 1
                        FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_REGRA_MODULO x
                        WHERE x.ID_USUARIO = @IdNovo AND x.ID_MODULO = m.ID_MODULO AND x.ID_LINX = m.ID_LINX
                  );

            IF OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_GPECON'', N''U'') IS NOT NULL
                INSERT INTO ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_GPECON (ID_USUARIO, ID_GPECON, ID_LINX)
                SELECT @IdNovo, g.ID_GPECON, g.ID_LINX
                FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_GPECON g
                WHERE g.ID_USUARIO = @IdOrigem
                  AND NOT EXISTS (
                        SELECT 1
                        FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_USUARIO_GPECON x
                        WHERE x.ID_USUARIO = @IdNovo AND x.ID_GPECON = g.ID_GPECON AND x.ID_LINX = g.ID_LINX
                  );
        ';

        SET @Perfis = 0;
        SET @Filiais = 0;

        EXEC sp_executesql
            @Sql,
            N'@IdNovo bigint, @UidNovo uniqueidentifier, @NomeAdmin varchar(250), @IdGpecon int, @EmailAdmin varchar(250), @Agora datetime, @IdOrigem bigint, @PerfisOut int OUTPUT, @FiliaisOut int OUTPUT',
            @IdNovo = @IdNovo,
            @UidNovo = @UidNovo,
            @NomeAdmin = @NomeAdmin,
            @IdGpecon = @IdGpecon,
            @EmailAdmin = @EmailAdmin,
            @Agora = @Agora,
            @IdOrigem = @IdPerfilOrigem,
            @PerfisOut = @Perfis OUTPUT,
            @FiliaisOut = @Filiais OUTPUT;

        PRINT 'Catalogo de aplicacao ' + @Db
            + ': perfis copiados=' + CAST(@Perfis AS varchar(10))
            + ', filiais copiadas=' + CAST(@Filiais AS varchar(10))
            + ', origem=' + ISNULL(@ModeloNome, @LoginAlvo);
    END

    FETCH NEXT FROM db_cursor INTO @Db, @Srv;
END

CLOSE db_cursor;
DEALLOCATE db_cursor;

PRINT '----';
PRINT 'Alvo: ' + @LoginAlvo + ' (ID_USUARIO=' + CAST(@IdAlvo AS varchar(20)) + ')';
PRINT 'GPECON: ' + CAST(@IdGpecon AS varchar(20)) + ' - ' + ISNULL(@NomeGpecon, '') + ' (' + CONVERT(varchar(36), @UidGpecon) + ')';
PRINT 'Novo admin: ' + @LoginAdmin + ' (ID_USUARIO=' + CAST(@IdNovo AS varchar(20)) + ', UID=' + CONVERT(varchar(36), @UidNovo) + ')';
PRINT 'Modelo de acesso: ' + ISNULL(@ModeloNome, '(nenhum admin local; usados os acessos da usuaria alvo)');
DECLARE @QtdAcesso int = (SELECT COUNT(*) FROM @Acesso);
PRINT 'Acessos de administrador criados: ' + CAST(@QtdAcesso AS varchar(10));
PRINT 'Modulos novos liberados para a empresa: ' + CAST(@ModulosEmpresa AS varchar(10));
PRINT 'Senha' + CASE WHEN @SenhaGerada = 1 THEN ' gerada' ELSE '' END + ': ' + @Senha;

IF @Confirmar = 1
BEGIN
    COMMIT;
    PRINT 'Gravado. Entre com ' + @LoginAdmin + ' no ambiente deste GPECON e abra Usuário (Server). A usuaria ' + @LoginAlvo + ' aparece nessa lista.';
END
ELSE
BEGIN
    ROLLBACK;
    PRINT 'SIMULACAO: nada foi gravado. Revise as mensagens e execute de novo com @Confirmar = 1.';
    IF @SenhaGerada = 1
        PRINT 'Se quiser esta mesma senha na gravacao, copie o valor acima para @Senha. Uma nova execucao sem @Senha gera outra.';
END
END TRY
BEGIN CATCH
    IF CURSOR_STATUS('local', 'db_cursor') = 1
        CLOSE db_cursor;
    IF CURSOR_STATUS('local', 'db_cursor') >= -1
        DEALLOCATE db_cursor;
    IF @@TRANCOUNT > 0
        ROLLBACK;
    THROW;
END CATCH
