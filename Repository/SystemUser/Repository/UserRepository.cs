using DAL;
using Entity.SystemUsers;
using Repository.Base;
using Repository.SystemUser.Interface;


namespace Repository.SystemUser.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {


        }
    }
}
