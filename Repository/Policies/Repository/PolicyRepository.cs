using DAL;
using Entity.Policies;
using Repository.Base;
using Repository.Policies.Interface;

namespace Repository.Policies.Repository
{
    public class PolicyRepository : BaseRepository<Policy>, IPolicyRepository
    {
        public PolicyRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
