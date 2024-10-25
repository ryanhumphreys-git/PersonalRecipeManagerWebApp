using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial class StorageBroker
    {
        public async ValueTask<User> SelectUserInformationByIdAsync(Guid userId) =>
            await SelectAsync<User>(userId);
        public async ValueTask<UserKitchen> SelectUserKitchenByIdAsync(Guid kitchenId) =>
            await SelectAsync<UserKitchen>(kitchenId);
        public async ValueTask<List<Kitchen>> SelectAllUserKitchensAsync(Guid id)
        {
            return await db.Kitchen
                .Include(k => k.UserKitchens.Where(uk => uk.UserId == id))
                .ToListAsync();
        }
        public async ValueTask UpdateUserInformationAsync(User user) =>
            await UpdateAsync(user);
    }
}

