using Microsoft.AspNetCore.Http;

namespace MIDIS.SGPVL.Utils.Helpers.FileManager
{
    public interface IStorageManager
    {
        string Base64ToFileBase64(string data, string type);
        void DeleteFile(string path);
        byte[] GetBytes(string path);
        string GetBase64(Stream stream);
        string LoadFileToBase64(string path);
        void SaveFile(string path, string base64, string type);
        Task<string> SaveFileFormCollection(string path, string fileNameSaved, IFormFile formCollection);
    }
}
