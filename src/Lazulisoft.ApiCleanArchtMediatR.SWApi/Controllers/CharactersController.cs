using System;
using System.Threading.Tasks;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Commands;
using Lazulisoft.ApiCleanArchtMediatR.Application.Characters.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lazulisoft.ApiCleanArchtMediatR.SWApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class CharactersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var characters = await Mediator.Send(new GetAllCharactersQuery());
            return Ok(characters);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var character = await Mediator.Send(new GetCharacterByIdQuery() { Id = id });
            if (character == null)
                return NotFound();

            return Ok(character);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateCharacterCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await Mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCharacterCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != command.Id)
            {
                ModelState.AddModelError("Id", "The id property must be equal to id param.");
                return BadRequest(ModelState);
            }

            try
            {
                var character = await Mediator.Send(new GetCharacterByIdQuery() { Id = id });
                if (character == null)
                    return NotFound();

                await Mediator.Send(command);
                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var character = await Mediator.Send(new GetCharacterByIdQuery() { Id = id });
                if (character == null)
                    return NotFound();

                await Mediator.Send(new DeleteCharacterCommand() { Id = id });
                return Ok();
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}