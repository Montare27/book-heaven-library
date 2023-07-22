namespace Api.Controllers
{
    using business.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "SaveList")]
    public class SaveBookController : ControllerBase
    {
        private readonly SaveBookService _saveBookService;

        public SaveBookController(SaveBookService saveBookService)
        {
            _saveBookService = saveBookService;
        }
        
        [HttpGet("GetAllBooksFromSavedList")]
        public async Task<IActionResult> GetAllBooksFromSavedList()
        {
            try
            {
                var result =  await _saveBookService.GetALlBooksFromSavedList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("SaveBook/{id:int}")]
        public async Task<IActionResult> SaveBook(int id)
        {
            try
            {
                await _saveBookService.SaveBook(id);
                return Ok("Saved");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("RemoveBookFromSaved/{id:int}")]
        public async Task<IActionResult> RemoveBookFromSaved(int id)
        {
            try
            {
                await _saveBookService.RemoveBookFromSavedList(id);
                return Ok("Removed");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
