using Microsoft.AspNetCore.Mvc;
using WorkingWithRabbitMq.Application;
using WorkingWithRabbitMq.Application.Model;

namespace ProjectWorkingWithRabbitMq.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkingWithRabbitMqController : ControllerBase
    {        
        private readonly IWorkingWithRabbitMqService _workingWithRabbitMqService;        

        public WorkingWithRabbitMqController(      
            IWorkingWithRabbitMqService workingWithRabbitMqService)
        {            
            _workingWithRabbitMqService = workingWithRabbitMqService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        public IActionResult Set(RabbitMqTask task, CancellationToken cancellationToken)
        {            
            if (task.IsValid())
                return BadRequest();

            var response = _workingWithRabbitMqService.SendMessage(task, cancellationToken);

            if (!response)
                throw new ArgumentException ("Unable to insert in queue");

            return Created("", "");
        }

        [HttpGet]
        [ProducesResponseType(typeof(RabbitMqTask), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IActionResult), StatusCodes.Status404NotFound)]
        public IActionResult Get(CancellationToken cancellationToken)
        {
            var response = _workingWithRabbitMqService.GetMessage(cancellationToken);

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}