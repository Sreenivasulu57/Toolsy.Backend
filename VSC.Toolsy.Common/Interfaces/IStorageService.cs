using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSC.Toolsy.Common.Interfaces
{
    public interface IStorageService
    {
        string GetMediaUrl(string fileName);

        Task<string> SaveMediaAsync(Stream mediaBinaryStream, string fileName, string mimeType = null);

        Task<bool> DeleteMediaAsync(string fileName);
    }
}
