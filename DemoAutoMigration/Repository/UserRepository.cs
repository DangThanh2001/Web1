using DemoAutoMigration.IRepository;
using DemoAutoMigration.Models;

namespace DemoAutoMigration.Repository
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly JobContext jobContext;

        public UserRepository(JobContext jobContext)
        {
            this.jobContext = jobContext;
        }

        public int countObject()
        {
            throw new NotImplementedException();
        }

        public int createObject(User data)
        {
            throw new NotImplementedException();
        }

        public int deleteObject(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> getAllObject()
        {
            throw new NotImplementedException();
        }

        public User getById(int id)
        {
            throw new NotImplementedException();
        }

        public int updateObject(User data)
        {
            jobContext.users.Update(data);
            return jobContext.SaveChanges();
        }
    }
}
