using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Queues;

//using AZ204ContainerApp.Models;

namespace AZ204ContainerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        public WorkController()
        {
        }

        [HttpGet("queue")]
        public async Task<ActionResult<string>> TestQueue()
        {
            string connection = Environment.GetEnvironmentVariable("QUEUE_CONNECTION");
            string queue = Environment.GetEnvironmentVariable("QUEUE_QUEUE");
            string result = "";
            try {
                var client = new QueueClient(connection, queue);
                var receipt = await client.SendMessageAsync("This is a test");
                result = $"You have succeeded. Message info: {receipt.Value.MessageId}";
            } catch (Exception ex){
                result = $"You have failed. The error you generated: {ex.Message}";
            }
            return result;
        }

        [HttpGet("environment")]
        public ActionResult<string> TestEnvironment(int id)
        {
            // TODO: Your code here
            string envVar = Environment.GetEnvironmentVariable("PASSCODE");
            return envVar == "containerama" ? "Congratulations. You have succeeded." : $"You have failed. The environment variable PASSCODE is incorrect.";
        }
    }
}