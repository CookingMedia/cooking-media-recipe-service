using CookingMedia.Recipe.EntityModels;
using CookingMedia.Recipe.EntityModels.Enum;
using CookingMedia.Recipe.EntityModels.LookUp;

namespace CookingMedia.Recipe.Repositories;

public class CookingMethodRepository : GenericRepository<CookingMethod>
{
    public CookingMethodRepository(CookingMediaRecipeDbContext context)
        : base(context)
    {
    }

    public override void Delete(object id)
    {
        var cookingMethod = GetById(id);
        if (cookingMethod != null) cookingMethod.Status = CookingMethodStatus.Disable;
    }
}