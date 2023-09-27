using DemoAutoMigration.Common;
using DemoAutoMigration.IRepository;
using DemoAutoMigration.IService;
using DemoAutoMigration.Models;

namespace DemoAutoMigration.Service
{
    public class JobService : IJobService
    {
        private readonly IJobRepository repository;

        public JobService(IJobRepository repository)
        {
            this.repository = repository;
        }

        public int Add(Job data)
        {
            if (Validation.checkStringIsEmpty(data.salary, data.description))
            {
                Console.WriteLine("abc");
            }
            data.dateCreated = DateTime.Now;
            data.lastUpdate = DateTime.Now;
            data.isActive = true;
            data.isDelete = false;
            return repository.createObject(data);
        }

        public int DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Job> findJobByName(string? input)
        {
            throw new NotImplementedException();
        }

        public List<Job> GetAll()
        {
            return repository.getAllObject();
        }

        public Job GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Job data)
        {
            throw new NotImplementedException();
        }
    }
}
