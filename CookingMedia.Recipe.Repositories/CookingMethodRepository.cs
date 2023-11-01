using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.EntityModels.LookUp;

namespace CookingMedia.Recipe.Repositories;

public class CookingMethodRepository : GenericRepository<CookingMethod>
{
    public CookingMethodRepository(CookingMediaRecipeDbContext context)
        : base(context) { }
}
