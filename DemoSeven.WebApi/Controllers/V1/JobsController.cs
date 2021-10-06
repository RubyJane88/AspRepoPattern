using DemoSeven.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace DemoSeven.WebApi.Controllers.V1
{
    public sealed class JobsController : ApiController
    {
        [HttpGet("fire-and-forget-job")]
        public ActionResult CreateFireAndForgetJob() => Ok(new {message="a sample response from version 1"});

        [HttpGet("delayed-job")]
        public ActionResult CreateDelayedJob() => Ok(new {message="a sample response from version 1"});

        [HttpGet("recurring-job")]
        public ActionResult CreateRecurringJob() => Ok(new {message="a sample response from version 1"});

        [HttpGet("continuation-job")]
        public ActionResult CreateContinuationJob() => Ok(new {message="a sample response from version 1"});
    }
}