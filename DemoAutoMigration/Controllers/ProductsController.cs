using DemoAutoMigration.Common;
using DemoAutoMigration.IService;
using DemoAutoMigration.Models;
using DemoAutoMigration.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DemoAutoMigration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IJobService service;

        public ProductsController(IJobService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ResponseBody<List<Job>> getAllJob()
        {
            return new ResponseBody<List<Job>>()
            {
                statusCode = HttpStatusCode.OK,
                data = service.GetAll(),
                message = "Ok con de",
            };
        }

        [HttpPost]
        public ResponseBody<string> addNewJob(Job job)
        {
            try
            {
                service.Add(job);
                return new ResponseBody<string>()
                {
                    statusCode = HttpStatusCode.OK,
                    data = null,
                    message = "Ok",
                };
            }
            catch (Exception ex)
            {
                return new ResponseBody<string>()
                {
                    statusCode = HttpStatusCode.BadRequest,
                    data = null,
                    message = ex.Message,
                };
            }
        }
    }
}
