BEGIN TRANSACTION

DECLARE @ApplicationId INT
 
SELECT @ApplicationId = ApplicationId
FROM [dbo].[Applications]
WHERE ApplicationName = 'Ed-Fi ODS API'

INSERT INTO dbo.ResourceClaims (
	Application_ApplicationId, 
	DisplayName, 
	ResourceName, 
	ClaimName)
VALUES (@ApplicationId, 'snapshot', 'snapshot', 'http://ed-fi.org/ods/identity/claims/publishing/snapshot')

DECLARE @SnapshotResourceClaimId INT

SELECT @SnapshotResourceClaimId = SCOPE_IDENTITY()

/*
INSERT INTO dbo.AuthorizationStrategies (DisplayName, AuthorizationStrategyName, Application_ApplicationId)
VALUES ('Key Based Ownership', 'KeyBasedOwnership', @ApplicationId)

SELECT @AuthorizationStrategyId = SCOPE_IDENTITY()
*/

DECLARE @AuthorizationStrategyId INT


SELECT @AuthorizationStrategyId = AuthorizationStrategyId
FROM dbo.AuthorizationStrategies s
WHERE s.AuthorizationStrategyName = 'NoFurtherAuthorizationRequired'

DECLARE @ReadActionId INT

SELECT @ReadActionId = ActionId
FROM dbo.Actions
WHERE ActionName = 'Read'

INSERT INTO dbo.ResourceClaimAuthorizationMetadatas (
    Action_ActionId, 
    AuthorizationStrategy_AuthorizationStrategyId,
    ResourceClaim_ResourceClaimId)
VALUES (@ReadActionId, @AuthorizationStrategyId, @SnapshotResourceClaimId)

DECLARE @ClaimSetId INT

-- Get the Ed-Fi Sandbox claim set
SELECT @ClaimSetId = ClaimSetId
FROM dbo.ClaimSets
WHERE ClaimSetName = 'Ed-Fi Sandbox'

-- Add claim set claims for read permissions...
INSERT INTO dbo.ClaimSetResourceClaims(
    ClaimSet_ClaimSetId,
    ResourceClaim_ResourceClaimId,
    Action_ActionId)
-- To the sandbox claim set
VALUES (@ClaimSetId, @SnapshotResourceClaimId, @ReadActionId)
-- To all claim sets
--SELECT ClaimSetId, @SnapshotResourceClaimId, @ReadActionId
--FROM	dbo.ClaimSets

COMMIT TRANSACTION
