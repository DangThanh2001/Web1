using DemoAutoMigration.IService;
using DemoAutoMigration.Models;
using DemoAutoMigration.Repository;

namespace DemoAutoMigration.Service
{
    public class ProductService : IProductService
    {
        public void addJob(Job job)
        {
            JobDAO.addJob(job);
        }

        public List<Job> findJobByName(string? input)
        {
            throw new NotImplementedException();
        }

        public List<Job> getAllProducts()
        {
            return JobDAO.getAllJob();
        }

        public void removeJob()
        {
            throw new NotImplementedException();
        }

        public int updateJob(Job job)
        {
            throw new NotImplementedException();
        }
    }
}
