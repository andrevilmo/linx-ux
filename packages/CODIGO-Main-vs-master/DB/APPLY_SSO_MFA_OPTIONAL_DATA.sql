-- =============================================================================
-- OPTIONAL data for SSO / MFA (edit the variables, then run selected batches)
-- Database: same Portal catalog as APPLY_SSO_MFA.sql
-- Does not run automatically — uncomment / set names, then execute.
-- =============================================================================

SET NOCOUNT ON;
GO

-- -----------------------------------------------------------------------------
-- A) Enable Microsoft SSO button for one login (Utiliza SSO)
--    NomeAutenticacao must equal the UPN prefix before @
-- -----------------------------------------------------------------------------
DECLARE @NomeSso NVARCHAR(256) = N'andrevilmo'; -- <- change

UPDATE [LX_TCS].[TCS_USUARIO_AUTENTICACAO]
SET [INDICA_UTILIZA_SSO] = 1
WHERE UPPER(LTRIM(RTRIM([NOME_AUTENTICACAO]))) = UPPER(LTRIM(RTRIM(@NomeSso)));

SELECT [UID_USUARIO], [NOME_AUTENTICACAO], [INDICA_UTILIZA_SSO], [INDICA_UTILIZA_MFA]
FROM [LX_TCS].[TCS_USUARIO_AUTENTICACAO]
WHERE UPPER(LTRIM(RTRIM([NOME_AUTENTICACAO]))) = UPPER(LTRIM(RTRIM(@NomeSso)));
GO

-- -----------------------------------------------------------------------------
-- B) Turn user MFA off (skip TOTP for that user). Default NULL/1 = MFA on.
-- -----------------------------------------------------------------------------
/*
DECLARE @NomeMfaOff NVARCHAR(256) = N'andrevilmo';

UPDATE [LX_TCS].[TCS_USUARIO_AUTENTICACAO]
SET [INDICA_UTILIZA_MFA] = 0
WHERE UPPER(LTRIM(RTRIM([NOME_AUTENTICACAO]))) = UPPER(LTRIM(RTRIM(@NomeMfaOff)));
*/

-- Turn user MFA back on:
/*
UPDATE [LX_TCS].[TCS_USUARIO_AUTENTICACAO]
SET [INDICA_UTILIZA_MFA] = 1
WHERE UPPER(LTRIM(RTRIM([NOME_AUTENTICACAO]))) = UPPER(LTRIM(RTRIM(N'andrevilmo')));
*/
GO

-- -----------------------------------------------------------------------------
-- C) Company MFA policy (ID_GPCON = IdLinxGpecon of the environment)
--    No row = MFA enabled. Row with INDICA_MFA_HABILITADO = 0 = company MFA off.
-- -----------------------------------------------------------------------------
/*
DECLARE @IdGpecon INT = 0; -- <- set IdLinxGpecon

-- MFA on for the company (or delete the row — same effect as "no row")
IF EXISTS (SELECT 1 FROM [LX_TCS].[TCS_GPECON_MFA] WHERE [ID_GPCON] = @IdGpecon)
    UPDATE [LX_TCS].[TCS_GPECON_MFA]
    SET [INDICA_MFA_HABILITADO] = 1, [UPDATED_AT] = GETDATE()
    WHERE [ID_GPCON] = @IdGpecon;
ELSE
    INSERT INTO [LX_TCS].[TCS_GPECON_MFA]
        ([ID_GPCON], [INDICA_MFA_HABILITADO], [INDICA_DISPOSITIVO_CONFIAVEL], [QTD_DIAS_CONFIANCA], [CREATED_AT], [UPDATED_AT])
    VALUES (@IdGpecon, 1, 0, 0, GETDATE(), GETDATE());

-- MFA off for the company:
-- UPDATE [LX_TCS].[TCS_GPECON_MFA] SET [INDICA_MFA_HABILITADO] = 0, [UPDATED_AT] = GETDATE() WHERE [ID_GPCON] = @IdGpecon;
*/

SELECT * FROM [LX_TCS].[TCS_GPECON_MFA];
GO

-- -----------------------------------------------------------------------------
-- D) Clear TOTP lockout for a user (5 fails / 15 min). UX key is ID_USUARIO.
-- -----------------------------------------------------------------------------
/*
DECLARE @NomeUnlock NVARCHAR(256) = N'andrevilmo';

UPDATE M
SET [QTD_TENTATIVAS_TOTP] = 0, [DATA_BLOQUEIO_ATE] = NULL, [UPDATED_AT] = GETDATE()
FROM [LX_TCS].[TCS_USUARIO_MFA] M
INNER JOIN [LX_TCS].[TCS_USUARIO_AUTENTICACAO] A ON A.[ID_USUARIO] = M.[ID_USER_MFA]
WHERE UPPER(LTRIM(RTRIM(A.[NOME_AUTENTICACAO]))) = UPPER(LTRIM(RTRIM(@NomeUnlock)))
  AND M.[TABLE_ORIGIN] = 'UX';
*/
GO

-- -----------------------------------------------------------------------------
-- E) Lookup helpers
-- -----------------------------------------------------------------------------
SELECT TOP 50
    [NOME_AUTENTICACAO],
    [UID_USUARIO],
    [INDICA_UTILIZA_SSO],
    [INDICA_UTILIZA_MFA],
    [INDICA_USUARIO_SERVICO]
FROM [LX_TCS].[TCS_USUARIO_AUTENTICACAO]
ORDER BY [NOME_AUTENTICACAO];

SELECT TOP 50 * FROM [LX_TCS].[TCS_USUARIO_MFA] ORDER BY [UPDATED_AT] DESC;
GO
