namespace business.Services
{
    using Interfaces;
    using Microsoft.AspNetCore.Http;

    public class FileBase64Service : IFileEncoderService
    {
        public string Encode(IFormFile file)
        {
            byte[] bytes = null!;
            var binaryReader = new BinaryReader(file.OpenReadStream());
            bytes = binaryReader.ReadBytes((int)file.Length);
            
            return Convert.ToBase64String(bytes);
        }
    }
}
