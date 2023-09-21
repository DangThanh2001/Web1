using DemoAutoMigration.Models;

namespace DemoAutoMigration.Repository
{
    public class JobDAO
    {
        private static JobContext context;
        public JobDAO()
        {
            context = new JobContext();
        }
    
        public static List<Job> getAllJob()
        {
            var lst = new List<Job>();
            //lst = context.jobs.ToList();
            return lst;
        }

        public static void addJob(Job job)
        {
            //context.jobs.Add(job);
            //context.SaveChanges();
        }
    }
}
