namespace Interfaces
{
    using Microsoft.AspNetCore.Http;

    public interface IFileEncoderService
    {
        public string Encode(IFormFile file);
    }
}
