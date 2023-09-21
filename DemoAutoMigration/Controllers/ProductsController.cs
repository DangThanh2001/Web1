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
        private IProductService service;
        public ProductsController()
        {
            service = new ProductService();
        }

        [HttpGet]
        public ResponseBody<List<Job>> getAllJob()
        {
            return new ResponseBody<List<Job>>()
            {
                statusCode = HttpStatusCode.OK,
                data = service.getAllProducts(),
                message = "Ok con de",
            };
        }

        [HttpPost]
        public ResponseBody<string> addNewJob(Job job)
        {
            try
            {
                service.addJob(job);
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
