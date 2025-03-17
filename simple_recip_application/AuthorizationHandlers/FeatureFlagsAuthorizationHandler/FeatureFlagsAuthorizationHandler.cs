using Microsoft.AspNetCore.Authorization;
using Microsoft.FeatureManagement;

namespace simple_recip_application.AuthorizationHandlers.FeatureFlagsAuthorizationHandler;

public class FeatureFlagsAuthorizationHandler(IFeatureManager _featureManager) : AuthorizationHandler<FeatureFlagsAuthorizationRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, FeatureFlagsAuthorizationRequirement requirement)
    {
        if (await _featureManager.IsEnabledAsync(requirement.FeatureFlag))
        {
            context.Succeed(requirement);
            return;
        }

        context.Fail();
    }
}