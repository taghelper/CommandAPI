using System.Collections.Generic;
using AutoMapper;
using CommandAPI.Data;
using CommandAPI.Dtos;
using CommandAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;

namespace CommandAPI.Controllers
{
    // [Route("api/[controller]")]
    [Route("api/commands")]
    [ApiController]
    [EnableCors("WeatherForecastCorsPolicy")] //needed in order for us to be able to send ajax requests from projects having 'locallhost'
    public class CommandsController : ControllerBase
    {
        private readonly ICommandAPIRepo _repository;

        private readonly IMapper _mapper;

        public CommandsController(ICommandAPIRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // [HttpGet]
        // public ActionResult<IEnumerable<string>> Get()
        // {
        //     return new string[] { "this", "is", "hard", "coded" };
        // }
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDto>(commandItem));
        }

        [HttpPost]
        public ActionResult<CommandReadDto>
        CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand (commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            // var commandReadDto = new CommandReadDto() {};
            // commandReadDto.Id = commandModel.Id;
            // commandReadDto.HowTo = commandModel.HowTo;
            // commandReadDto.Platform = commandModel.Platform;
            // commandReadDto.CommandLine = commandModel.CommandLine;
            //There is no differnce in using either commandModel.Id or commandReadDto.Id when assigning
            //the value of our Id to the Id at below inside "new{Id=commandModel.Id}" :)
            //===>   //return CreatedAtRoute(nameof(GetCommandById),new{Id=commandModel.Id},commandCreateDto);
            return CreatedAtRoute(nameof(GetCommandById),
            new { Id = commandReadDto.Id },
            commandCreateDto);
        }

        [HttpPut("{id}")]
        public ActionResult
        UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }

            //the  following 3 lines works instead of using _mapper.Map(), as well! :)
            commandModelFromRepo.HowTo = commandUpdateDto.HowTo;
            commandModelFromRepo.CommandLine = commandUpdateDto.CommandLine;
            commandModelFromRepo.Platform = commandUpdateDto.Platform;

            // _mapper.Map(commandUpdateDto,commandModelFromRepo);
            _repository.UpdateCommand (commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDto> patchDoc){
            var commandModelFromRepo=_repository.GetCommandById(id);

            if (commandModelFromRepo==null)
            {
                return NotFound();
            }
            var commandToPatch=_mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch,ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch,commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id){
            var commandFromRepo=_repository.GetCommandById(id);
            if (commandFromRepo==null)
            {
                return NotFound();
            }

            _repository.DeleteCommand(commandFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
