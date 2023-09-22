using DemoAutoMigration.IRepository;
using DemoAutoMigration.IService;
using DemoAutoMigration.Models;

namespace DemoAutoMigration.Repository
{
    public class JobRepository : IJobRepository
    {
        private JobContext context;

        public JobRepository(JobContext context)
        {
            this.context = context;
        }

        public int createObject(Job data)
        {
            if(data == null) 
                throw new ArgumentNullException("job cannot null");
            context.jobs.Add(data);
            var rs = context.SaveChanges();
            return rs > 0 ? rs : throw new Exception("data not inserted yet");
        }

        public int deleteObject(int id)
        {
            throw new NotImplementedException();
        }

        public List<Job> getAllObject()
        {
            return context.jobs.ToList();
        }

        public Job getById(int id)
        {
            throw new NotImplementedException();
        }

        public int updateObject(Job data)
        {
            throw new NotImplementedException();
        }
    }
}
