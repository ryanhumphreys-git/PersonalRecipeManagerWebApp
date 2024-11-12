using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers
{
    public partial interface IStorageBroker
    {
        public ValueTask<User> SelectUserInformationByIdAsync(Guid id);
        public ValueTask<UserKitchen> SelectUserKitchenByIdAsync(Guid id);
        public ValueTask<List<Kitchen>> SelectAllUserKitchensAsync(Guid id);
        public ValueTask<User> UpdateUserInformationAsync(User user);
    }
}
