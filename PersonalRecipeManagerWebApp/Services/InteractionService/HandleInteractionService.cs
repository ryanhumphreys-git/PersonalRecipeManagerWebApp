using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Brokers;
using PersonalRecipeManagerWebApp.Data;
using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Services
{
    public partial class HandleInteractionService(IStorageBroker dataBroker) : IHandleInteractionService
    {
        private readonly IStorageBroker _broker = dataBroker;
    }
}
