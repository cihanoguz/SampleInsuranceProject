using DAL;
using Entity.Customers;
using Repository.Base;
using Repository.Customers.Interface;

namespace Repository.Customers.Repository
{
    public class AgentRepository : BaseRepository<Agent>, IAgentRepository
    {
        public AgentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
