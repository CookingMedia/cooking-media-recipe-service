﻿using CookingMedia.Recipe.EntityModels;

namespace CookingMedia.Recipe.Repositories;

public class UnitOfWork : IDisposable
{
    private readonly CookingMediaRecipeDbContext _context = new();
    private RecipeRepository? _recipeRepository;
    public RecipeRepository Recipes => _recipeRepository ??= new RecipeRepository(_context);

    private CookingMethodRepository _cookingMethodRepository;
    public CookingMethodRepository CookingMethods =>
        _cookingMethodRepository ?? new CookingMethodRepository(_context);

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
