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

    public bool ExistById(int id)
    {
        return _unitOfWork.CookingMethods.ExistById(id);
    }

    public void Add(CookingMethod cookingMethod)
    {
        _unitOfWork.CookingMethods.Insert(cookingMethod);
        _unitOfWork.Save();
    }

    public IEnumerable<CookingMethod> Search(string name)
    {
        return _unitOfWork.CookingMethods.Get(x => x.Name.Contains(name));
    }

    public void Update(CookingMethod cookingMethod)
    {
        _unitOfWork.CookingMethods.Update(cookingMethod);
        _unitOfWork.Save();
    }

    public void Delete(int Id)
    {
        _unitOfWork.CookingMethods.Delete(Id);
        _unitOfWork.Save();
    }
}
