using CookingMedia.Recipe.EntityModels.LookUp;
using CookingMedia.Recipe.Repositories;

namespace CookingMedia.Recipe.Services;

public class CookingMethodService
{
    private UnitOfWork _unitOfWork;

    public CookingMethodService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public CookingMethod? GetById(int id)
    {
        return _unitOfWork.CookingMethods.GetById(id);
    }

    public void Add(CookingMethod cookingMethod)
    {
        _unitOfWork.CookingMethods.Insert(cookingMethod);
    }
}
