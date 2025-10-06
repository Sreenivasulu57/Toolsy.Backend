using VSC.Toolsy.Common.Interfaces;
namespace VSC.Toolsy.Services
{
    public class MediaService : IMediaService
    {
        private readonly IStorageService _storageService;

        public MediaService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<bool> DeleteByFileName(string fileName)
        {
            return await _storageService.DeleteMediaAsync(fileName);
        }

        public string GetByFileName(string fileName)
        {
            return _storageService.GetMediaUrl(fileName);
        }

        public async Task<string> SaveMediaAsync(Stream stream, string fileName, string contentType)
        {
            return await _storageService.SaveMediaAsync(stream, fileName, contentType);
        }
    }
}
