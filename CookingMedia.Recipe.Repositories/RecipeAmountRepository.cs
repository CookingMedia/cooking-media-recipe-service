﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookingMedia.Recipe.EntityModels;

namespace CookingMedia.Recipe.Repositories;

public class RecipeAmountRepository : GenericRepository<RecipeAmount>
{
    public RecipeAmountRepository(CookingMediaRecipeDbContext context) : base(context)
    {
    }
}