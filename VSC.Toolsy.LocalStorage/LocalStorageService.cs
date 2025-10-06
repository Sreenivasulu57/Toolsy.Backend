using Microsoft.Extensions.Configuration;
using VSC.Toolsy.Common.Interfaces;

namespace VSC.Toolsy.LocalStorage
{
    public class LocalStorageService : IStorageService
    {
        private readonly IConfiguration _configuration;

        private readonly string _mediaRootFolder;
        private readonly string _path;


        public LocalStorageService(IConfiguration configuration)
        {
            _configuration = configuration;

            _mediaRootFolder = _configuration["BlobStorage:MediaRootFolder"] ?? throw new Exception("MediaRootFolder is null");
            _path = _configuration["BlobStorage:Path"] ?? throw new Exception("path is null");
        }


        public string GetMediaUrl(string fileName)
        {
            return _path + @$"\{_mediaRootFolder}\{fileName}";
        }

        public async Task<string> SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null)
        {
            string basePath = _path;

            string dirPath = Path.Combine(basePath, _mediaRootFolder);

            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string filePath = Path.Combine(dirPath, fileName);

            using (FileStream output = new FileStream(filePath, FileMode.Create))
            {
                await mediaBinaryStream.CopyToAsync(output);
            }

            return filePath;
        }

        public async Task<bool> DeleteMediaAsync(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("Filename cannot be null, empty, or whitespace.", nameof(fileName));
            }

            string filePath = Path.Combine(_path, _mediaRootFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
            return true;
        }
    }
}
