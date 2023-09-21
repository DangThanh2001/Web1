using DemoAutoMigration.Models;

namespace DemoAutoMigration.IService
{
    public interface IProductService
    {
        public List<Job> getAllProducts();
        public List<Job> findJobByName(string? input);
        public int updateJob(Job job);
        public void addJob(Job job);
        public void removeJob(); 
    }
}
