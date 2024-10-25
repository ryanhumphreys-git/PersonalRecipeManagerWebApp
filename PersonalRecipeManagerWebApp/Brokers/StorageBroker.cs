using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Data;
using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers;

public partial class StorageBroker(RecipeContext recipeContext) : IStorageBroker
{
    private readonly RecipeContext db = recipeContext;

    private async ValueTask<T> InsertAsync<T>(T @object)
    {
        this.db.Entry(@object).State = EntityState.Added;
        await this.db.SaveChangesAsync();

        return @object;
    }

    private async ValueTask<List<T>> SelectAllAsync<T>() where T : class => this.db.Set<T>().ToList();

    private async ValueTask<T> SelectAsync<T>(params object[] @objectIds) where T : class =>
        await this.db.FindAsync<T>(objectIds);

    private async ValueTask<T> UpdateAsync<T>(T @object)
    {
        this.db.Entry(@object).State = EntityState.Modified;
        await this.db.SaveChangesAsync();

        return @object;
    }

    private async ValueTask<T> DeleteAsync<T>(T @object)
    {
        this.db.Entry(@object).State = EntityState.Deleted;
        await this.db.SaveChangesAsync();

        return @object;
    }
}