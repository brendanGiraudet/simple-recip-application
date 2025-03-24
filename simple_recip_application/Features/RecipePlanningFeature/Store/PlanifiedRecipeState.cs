using Fluxor;
using simple_recip_application.Features.RecipePlanningFeature.ApplicationCore.Entities;
using simple_recip_application.Store;

namespace simple_recip_application.Features.RecipePlanningFeature.Store;

[FeatureState]
public record class PlanifiedRecipeState : BaseState<IPlanifiedRecipeModel>
{
    public Dictionary<DayOfWeek, List<IPlanifiedRecipeModel>> RecipesGroupedByDay { get; set; } = [];
}
