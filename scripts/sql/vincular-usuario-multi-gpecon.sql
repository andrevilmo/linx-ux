/*
    Liga um usuario a um grupo economico (GPECON) para a tela Usuário (Server).

    Com PERMITE_MULTI_GPECON_USUARIO = true, o login devolve o grupo de origem
    e os grupos de LX_TCS.TCS_USUARIO_AUT_GPECON. Ao entrar em cada grupo, a
    grade lista os usuarios daquele grupo. Nao junta todos numa lista so.

    O indicador Multi Grupo Economico precisa estar marcado num acesso cujo
    ambiente pertence ao grupo de origem do usuario. Sem isso a sessao nao
    e tratada como multi-grupo.

    Rodar no catalogo do Portal / FrameworkAutorizacao
    (o banco de LX_TCS.TCS_USUARIO_AUTENTICACAO).

    Preencha @NomeUsuario (login ou nome) e @NomeGpecon (NOME_EMPRESA do grupo).
    A execucao grava. Para simular, mude @Confirmar para 0.
    Depois saia e entre de novo: a lista de grupos fica em cache no servico.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @NomeUsuario varchar(250) = 'jacqueline.dmaques';
DECLARE @NomeGpecon  varchar(250) = '';
DECLARE @Confirmar   bit = 1;

DECLARE @IdUsuario bigint;
DECLARE @Login varchar(250);
DECLARE @IdGpeconOrigem int;
DECLARE @NomeGpeconOrigem varchar(250);
DECLARE @IdGpeconAlvo int;
DECLARE @NomeGpeconAlvo varchar(250);
DECLARE @Acessos int = 0;
DECLARE @VinculoNovo bit = 0;

IF NULLIF(LTRIM(RTRIM(@NomeUsuario)), '') IS NULL
    THROW 50001, 'Informe @NomeUsuario.', 1;

IF NULLIF(LTRIM(RTRIM(@NomeGpecon)), '') IS NULL
    THROW 50001, 'Informe @NomeGpecon com o NOME_EMPRESA do grupo.', 1;

IF OBJECT_ID(N'LX_TCS.TCS_USUARIO_AUTENTICACAO', N'U') IS NULL
    OR OBJECT_ID(N'LX_TCS.TCS_USUARIO_AUT_GPECON', N'U') IS NULL
    OR OBJECT_ID(N'LX_TCS.TCS_EMPRESA_AUTENTICACAO', N'U') IS NULL
    THROW 50001, 'Tabelas do Portal nao encontradas. Conecte no catalogo FrameworkAutorizacao.', 1;

SELECT @IdUsuario = ID_USUARIO, @Login = NOME_AUTENTICACAO, @IdGpeconOrigem = ID_LINX_GPECON
FROM LX_TCS.TCS_USUARIO_AUTENTICACAO
WHERE NOME_AUTENTICACAO = @NomeUsuario;

IF @IdUsuario IS NULL AND (
    SELECT COUNT(*) FROM LX_TCS.TCS_USUARIO_AUTENTICACAO WHERE NOME_USUARIO = @NomeUsuario
) = 1
    SELECT @IdUsuario = ID_USUARIO, @Login = NOME_AUTENTICACAO, @IdGpeconOrigem = ID_LINX_GPECON
    FROM LX_TCS.TCS_USUARIO_AUTENTICACAO
    WHERE NOME_USUARIO = @NomeUsuario;

IF @IdUsuario IS NULL AND (
    SELECT COUNT(*) FROM LX_TCS.TCS_USUARIO_AUTENTICACAO WHERE UPPER(NOME_AUTENTICACAO) = UPPER(@NomeUsuario)
) = 1
    SELECT @IdUsuario = ID_USUARIO, @Login = NOME_AUTENTICACAO, @IdGpeconOrigem = ID_LINX_GPECON
    FROM LX_TCS.TCS_USUARIO_AUTENTICACAO
    WHERE UPPER(NOME_AUTENTICACAO) = UPPER(@NomeUsuario);

IF @IdUsuario IS NULL AND (
    SELECT COUNT(*) FROM LX_TCS.TCS_USUARIO_AUTENTICACAO WHERE UPPER(NOME_USUARIO) = UPPER(@NomeUsuario)
) = 1
    SELECT @IdUsuario = ID_USUARIO, @Login = NOME_AUTENTICACAO, @IdGpeconOrigem = ID_LINX_GPECON
    FROM LX_TCS.TCS_USUARIO_AUTENTICACAO
    WHERE UPPER(NOME_USUARIO) = UPPER(@NomeUsuario);

IF @IdUsuario IS NULL
    THROW 50002, 'Usuario nao encontrado, ou ha mais de um com esse nome. Use o NOME_AUTENTICACAO.', 1;

SELECT @NomeGpeconOrigem = NOME_EMPRESA
FROM LX_TCS.TCS_EMPRESA_AUTENTICACAO
WHERE ID_LINX = @IdGpeconOrigem;

SELECT @IdGpeconAlvo = ID_LINX, @NomeGpeconAlvo = NOME_EMPRESA
FROM LX_TCS.TCS_EMPRESA_AUTENTICACAO
WHERE NOME_EMPRESA = @NomeGpecon;

IF @IdGpeconAlvo IS NULL AND (
    SELECT COUNT(*) FROM LX_TCS.TCS_EMPRESA_AUTENTICACAO WHERE UPPER(NOME_EMPRESA) = UPPER(@NomeGpecon)
) = 1
    SELECT @IdGpeconAlvo = ID_LINX, @NomeGpeconAlvo = NOME_EMPRESA
    FROM LX_TCS.TCS_EMPRESA_AUTENTICACAO
    WHERE UPPER(NOME_EMPRESA) = UPPER(@NomeGpecon);

IF @IdGpeconAlvo IS NULL
    THROW 50003, 'GPECON nao encontrado, ou ha mais de uma empresa com esse nome.', 1;

BEGIN TRY
BEGIN TRAN;

IF @IdGpeconAlvo = @IdGpeconOrigem
    PRINT 'O grupo informado ja e o grupo de origem. Nao precisa de vinculo para ve-lo.';
ELSE IF EXISTS (
    SELECT 1
    FROM LX_TCS.TCS_USUARIO_AUT_GPECON
    WHERE ID_USUARIO = @IdUsuario AND ID_LINX = @IdGpeconAlvo
)
    PRINT 'Vinculo ja existia.';
ELSE
BEGIN
    INSERT INTO LX_TCS.TCS_USUARIO_AUT_GPECON (ID_USUARIO, ID_LINX)
    VALUES (@IdUsuario, @IdGpeconAlvo);
    SET @VinculoNovo = 1;
END

UPDATE a
SET INDICA_MULTI_GPECON = 1
FROM LX_TCS.TCS_USUARIO_ACESSO a
INNER JOIN LX_TCS.TCS_AMBIENTE amb ON amb.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE
WHERE a.ID_USUARIO = @IdUsuario
  AND amb.ID_LINX = @IdGpeconOrigem
  AND a.INDICA_MULTI_GPECON = 0;

SET @Acessos = @@ROWCOUNT;

IF NOT EXISTS (
    SELECT 1
    FROM LX_TCS.TCS_USUARIO_ACESSO a
    INNER JOIN LX_TCS.TCS_AMBIENTE amb ON amb.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE
    WHERE a.ID_USUARIO = @IdUsuario
      AND amb.ID_LINX = @IdGpeconOrigem
      AND a.INDICA_MULTI_GPECON = 1
)
    THROW 50004, 'Nenhum acesso deste usuario esta no ambiente do grupo de origem. Sem o indicador Multi Grupo Economico nesse acesso, a troca de grupo nao fica disponivel.', 1;

/*
    O parametro e lido no banco da aplicacao do ambiente.
    Atualiza o valor existente. Nao cria a definicao do parametro.
*/
DECLARE @Db varchar(250);
DECLARE @Sql nvarchar(max);
DECLARE @Alterados int;

DECLARE db_cursor CURSOR LOCAL FAST_FORWARD FOR
SELECT DISTINCT b.NOME_BANCO
FROM LX_TCS.TCS_USUARIO_ACESSO a
INNER JOIN LX_TCS.TCS_AMBIENTE amb ON amb.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE
INNER JOIN LX_TCS.TCS_AMBIENTE_CONEXAO c ON c.ID_TCS_AMBIENTE = a.ID_TCS_AMBIENTE
INNER JOIN LX_TCS.TCS_BANCO_SERVIDOR b ON b.ID_TCS_BANCO_SERVIDOR = c.ID_TCS_BANCO_SERVIDOR
WHERE a.ID_USUARIO = @IdUsuario
  AND amb.ID_LINX = @IdGpeconOrigem
  AND NULLIF(LTRIM(RTRIM(b.NOME_BANCO)), '') IS NOT NULL;

OPEN db_cursor;
FETCH NEXT FROM db_cursor INTO @Db;

WHILE @@FETCH_STATUS = 0
BEGIN
    IF DB_ID(@Db) IS NULL
        PRINT 'Banco fora desta instancia, parametro nao alterado: ' + @Db;
    ELSE
    BEGIN
        SET @Sql = N'
            DECLARE @n int = 0;
            IF OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_PARAMETRO_VALOR'', N''U'') IS NOT NULL
               AND OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_PARAMETRO'', N''U'') IS NOT NULL
            BEGIN
                UPDATE v
                SET VALOR_PARAMETRO = ''true''
                FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_PARAMETRO_VALOR v
                INNER JOIN ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_PARAMETRO p
                    ON p.ID_PARAMETRO = v.ID_PARAMETRO
                WHERE p.TITULO_PARAMETRO = N''PERMITE_MULTI_GPECON_USUARIO''
                  AND ISNULL(v.INATIVO, 0) = 0
                  AND LOWER(LTRIM(RTRIM(ISNULL(v.VALOR_PARAMETRO, N'''')))) <> N''true'';
                SET @n = @n + @@ROWCOUNT;
            END
            IF OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_PARAMETRO_VALOR'', N''U'') IS NOT NULL
               AND OBJECT_ID(N''' + QUOTENAME(@Db) + N'.LX_TCS.TCS_PARAMETRO_AUTORIZACAO'', N''U'') IS NOT NULL
            BEGIN
                UPDATE v
                SET VALOR_PARAMETRO = ''true''
                FROM ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_PARAMETRO_VALOR v
                INNER JOIN ' + QUOTENAME(@Db) + N'.LX_TCS.TCS_PARAMETRO_AUTORIZACAO p
                    ON p.ID_PARAMETRO = v.ID_PARAMETRO
                WHERE p.TITULO_PARAMETRO = N''PERMITE_MULTI_GPECON_USUARIO''
                  AND ISNULL(v.INATIVO, 0) = 0
                  AND LOWER(LTRIM(RTRIM(ISNULL(v.VALOR_PARAMETRO, N'''')))) <> N''true'';
                SET @n = @n + @@ROWCOUNT;
            END
            SET @AlteradosOut = @n;
        ';
        SET @Alterados = 0;
        EXEC sp_executesql @Sql, N'@AlteradosOut int OUTPUT', @AlteradosOut = @Alterados OUTPUT;
        PRINT 'Parametro em ' + @Db + ': valores ajustados para true = ' + CAST(@Alterados AS varchar(10));
        IF @Alterados = 0
            PRINT 'Se PERMITE_MULTI_GPECON_USUARIO ainda nao estiver true em ' + @Db + ', ligue o parametro na tela de parametros.';
    END

    FETCH NEXT FROM db_cursor INTO @Db;
END

CLOSE db_cursor;
DEALLOCATE db_cursor;

PRINT '----';
PRINT 'Usuario: ' + @Login + ' (ID_USUARIO=' + CAST(@IdUsuario AS varchar(20)) + ')';
PRINT 'Grupo de origem: ' + CAST(@IdGpeconOrigem AS varchar(20)) + ' - ' + ISNULL(@NomeGpeconOrigem, '');
PRINT 'Grupo ligado: ' + CAST(@IdGpeconAlvo AS varchar(20)) + ' - ' + @NomeGpeconAlvo
    + CASE WHEN @VinculoNovo = 1 THEN ' (vinculo criado)' ELSE '' END;
PRINT 'Acessos marcados como Multi Grupo Economico: ' + CAST(@Acessos AS varchar(10));

IF @Confirmar = 1
BEGIN
    COMMIT;
    PRINT 'Gravado. Saia e entre de novo, depois alterne para o grupo ' + @NomeGpeconAlvo + '.';
END
ELSE
BEGIN
    ROLLBACK;
    PRINT 'SIMULACAO: nada foi gravado. Execute de novo com @Confirmar = 1.';
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
