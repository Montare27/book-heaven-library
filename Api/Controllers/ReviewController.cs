namespace Api.Controllers
{
    using AutoMapper;
    using business.Mediator.Review.Commands.Create;
    using business.Mediator.Review.Commands.Delete;
    using business.Mediatr.Review.Commands.Update;
    using business.Mediatr.Review.Queries.GetAll;
    using business.Mediatr.Review.Queries.GetById;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "Reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ReviewController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("GetAllReviews")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result  =await _mediator.Send(new GetReviewListQuery());
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("GetReviewById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result  =await _mediator.Send(new GetReviewDetailedQuery{Id = id});
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost("WriteReview")]
        public async Task<IActionResult> Create(CreateReviewCommand model)
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
        
        [HttpPut("UpdateReview")]
        public async Task<IActionResult> Update(UpdateReviewCommand model)
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
        
        [HttpDelete("DeleteReview/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteReviewCommand{Id = id});
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
