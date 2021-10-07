using DemoSeven.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DemoSeven.WebApi.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = true)]
    public sealed class CustomersController : ApiController
    {
        //GET: api/customers
        [HttpGet]
        public ActionResult GetAllCustomersUsingRedisCache() => Ok(new {message="a sample response from customers version 1"});

    }
}