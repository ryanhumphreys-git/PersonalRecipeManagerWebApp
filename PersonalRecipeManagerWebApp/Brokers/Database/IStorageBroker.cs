using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Brokers;

public partial interface IStorageBroker
{
    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();

}