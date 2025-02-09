using simple_recip_application.Data;
using simple_recip_application.Data.Repository;
using simple_recip_application.Features.IngredientsManagement.Persistence.Entities;

namespace simple_recip_application.Features.IngredientsManagement.Persistence.Repositories;

public class IngredientRepository : Repository<IngredientModel>
{
    public IngredientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
