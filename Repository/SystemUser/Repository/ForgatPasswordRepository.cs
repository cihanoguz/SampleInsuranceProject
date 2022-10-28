using DAL;
using Entity.SystemUsers;
using Repository.Base;
using Repository.SystemUser.Interface;

namespace Repository.SystemUser.Repository
{
    public class ForgatPasswordRepository : BaseRepository<ForgatPassword>, IForgatPasswordRepository
    {
        public ForgatPasswordRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
