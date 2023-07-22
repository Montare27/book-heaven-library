namespace Api.Controllers
{
    using AutoMapper;
    using business.Mediatr.Genre.Commands.Create;
    using business.Mediatr.Genre.Commands.Delete;
    using business.Mediatr.Genre.Commands.Update;
    using business.Mediatr.Genre.Queries.GetAll;
    using business.Mediatr.Genre.Queries.GetById;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Genres")]
    public class GenreController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public GenreController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetAllGenres")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new GetGenreListQuery());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("GetGenreById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetGenreQuery() {Id = id});
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost("CreateGenre")]
        public async Task<IActionResult> Create(CreateGenreCommand model)
        {
            try
            {
                var result = await _mediator.Send(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }
        
        [HttpPut("UpdateGenre")]
        public async Task<IActionResult> Update(UpdateGenreCommand model)
        {
            try
            {
                var result = await _mediator.Send(model);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("DeleteGenre/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteGenreCommand{Id = id});
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}