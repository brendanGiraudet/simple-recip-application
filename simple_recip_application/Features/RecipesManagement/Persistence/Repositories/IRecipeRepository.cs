using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using simple_recip_application.Data.Repository;
using simple_recip_application.Features.RecipesManagement.ApplicationCore;

namespace simple_recip_application.Features.RecipesManagement.Persistence.Repositories;

public interface IRecipeRepository : IRepository<IRecipeModel>
{

}
