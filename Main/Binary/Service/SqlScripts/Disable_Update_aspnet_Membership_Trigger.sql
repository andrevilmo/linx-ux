/*
  Problem:
    Trigger dbo.Update_aspnet_Membership resets IsLockedOut and FailedPasswordAttemptCount
    on EVERY update to aspnet_Membership (unless Comment is updated in the same statement).
    That prevents ASP.NET Membership lockout after invalid password attempts.

  Original trigger body (for reference):
    AFTER UPDATE -> IF NOT UPDATE(COMMENT)
      SET IsLockedOut = 0, FailedPasswordAttemptCount = 0, Comment = 'Unlocked'
      WHERE FailedPasswordAttemptCount != 0

  Fix:
    Disable (or drop) the trigger so SqlMembershipProvider can lock accounts normally.
*/

DISABLE TRIGGER [dbo].[Update_aspnet_Membership] ON [dbo].[aspnet_Membership];
GO

-- Optional verification:
-- SELECT name, is_disabled
-- FROM sys.triggers
-- WHERE parent_id = OBJECT_ID('dbo.aspnet_Membership')
--   AND name = 'Update_aspnet_Membership';
