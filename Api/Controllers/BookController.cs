namespace Api.Controllers
{
    using AutoMapper;
    using business.Mediator.Book.Commands.Create;
    using business.Mediator.Book.Query.GetAll;
    using business.Mediatr.Book.Commands.Delete;
    using business.Mediatr.Book.Commands.Update;
    using business.Mediatr.Book.Query.GetById;
    using Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Books")]
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<BookController> _logger;
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _userService;

        public BookController( IMapper mapper, IMediator mediator, ICurrentUserService userService, ILogger<BookController> logger)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("CheckAuth")]
        public IActionResult CheckAuth()
        {
            var result = new{
                isAdmin = User.IsInRole("Admin"),
                username = User.Identity?.Name ?? "empty",
                id = User.Claims.FirstOrDefault(x=>x.Type.Equals("Id"))?.Value ?? "empty",
            };
            return Ok(result);
        }
        
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new GetBookListQuery());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

        [HttpGet("GetBookById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetBookDetailedQuery{Id = id});
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("GetBooksByUserId/{id:guid}")]
        public async Task<IActionResult> GetBooksByUserId()
        {
            try
            {
                var id = _userService.Id;
                var books = await _mediator.Send(new GetBookListQuery());
                var result = books.Where(x => x.PublisherId == id);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreateBook")]
        public async Task<IActionResult> Create( CreateBookCommand model)   
        {
            _logger.LogInformation("Controller was activated: ");
            try
            {
                var result = await _mediator.Send(model);
                _logger.LogInformation("Book was created: ");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: " + e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("UpdateBook")]
        public async Task<IActionResult> Update(UpdateBookCommand model)
        {
            try
            {
                var result = await _mediator.Send(model);
                _logger.LogInformation("Book was updated: ");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Error: " + e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("DeleteBook/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteBookCommand{Id = id});
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
