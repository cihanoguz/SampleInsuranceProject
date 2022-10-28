using DAL;
using Entity.Customers;
using Repository.Base;
using Repository.Customers.Interface;

namespace Repository.Customers.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
