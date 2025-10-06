
namespace VSC.Toolsy.Common.Interfaces
{
    public interface IMediaService
    {
        Task<bool> DeleteByFileName(string fileName);
        string GetByFileName(string fileName);
        Task<string> SaveMediaAsync(Stream stream, string fileName, string contentType);
    }
}
