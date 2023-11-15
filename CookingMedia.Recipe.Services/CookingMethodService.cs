using System.Linq.Expressions;
using CookingMedia.Recipe.EntityModels.Dto.Requests;
using CookingMedia.Recipe.EntityModels.Dto.Responses;
using CookingMedia.Recipe.EntityModels.LookUp;
using CookingMedia.Recipe.Repositories;

namespace CookingMedia.Recipe.Services;

public class CookingMethodService
{
    private readonly UnitOfWork _unitOfWork;

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

    public PageResult<CookingMethod> Search(SearchCookingMethodRequest request)
    {
        return _unitOfWork.CookingMethods.Get(request,
            new Expression<Func<CookingMethod, bool>>[] { x => x.Name.Contains(request.Name) },
            o => o.OrderBy(e => e.Name));
    }

    public void Update(CookingMethod cookingMethod)
    {
        _unitOfWork.CookingMethods.Update(cookingMethod);
        _unitOfWork.Save();
    }

    public void Delete(int id)
    {
        _unitOfWork.CookingMethods.Delete(id);
        _unitOfWork.Save();
    }
}