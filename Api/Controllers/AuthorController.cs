namespace Api.Controllers
{
    using AutoMapper;
    using business.Mediator.Author.Commands.Create;
    using business.Mediator.Author.Commands.Delete;
    using business.Mediatr.Author.Commands.Update;
    using business.Mediatr.Author.Queries.GetAll;
    using business.Mediatr.Author.Queries.GetById;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public AuthorController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new GetAuthorListQuery());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("GetAuthorById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetAuthorDetailedQuery{Id = id});
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        
        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> Create(CreateAuthorCommand model)
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
        
        [HttpPut("UpdateAuthor")]
        public async Task<IActionResult> Update(UpdateAuthorCommand model)
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
        
        [HttpDelete("DeleteAuthor")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteAuthorCommand{Id = id});
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
