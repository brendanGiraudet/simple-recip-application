using Microsoft.AspNetCore.Authorization;
using simple_recip_application.Constants;

namespace simple_recip_application.AuthorizationHandlers.FeatureFlagsAuthorizationHandler;

public record class FeatureFlagsAuthorizationRequirement(string FeatureFlag) : IAuthorizationRequirement
{

}