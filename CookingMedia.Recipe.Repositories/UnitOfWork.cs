using CookingMedia.Recipe.EntityModels;

namespace CookingMedia.Recipe.Repositories;

public class UnitOfWork : IDisposable
{
    private readonly CookingMediaRecipeDbContext _context = new();

    private RecipeRepository? _recipeRepository;
    public RecipeRepository Recipes => _recipeRepository ??= new RecipeRepository(_context);

    private CookingMethodRepository? _cookingMethodRepository;
    public CookingMethodRepository CookingMethods =>
        _cookingMethodRepository ??= new CookingMethodRepository(_context);

    private RecipeStepRepository? _recipeStepRepository;
    public RecipeStepRepository RecipeSteps => _recipeStepRepository ??= new RecipeStepRepository(_context);

    private RecipeAmountRepository? _recipeAmountRepository;
    public RecipeAmountRepository RecipeAmounts => _recipeAmountRepository ??= new RecipeAmountRepository(_context);

    private RecipeCategoryRepository? _recipeCategoryRepository;
    public RecipeCategoryRepository RecipeCategories =>
        _recipeCategoryRepository ??= new RecipeCategoryRepository(_context);

    private RecipeStyleRepository? _recipeStyleRepository;
    public RecipeStyleRepository RecipeStyles => _recipeStyleRepository ??= new RecipeStyleRepository(_context);

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
