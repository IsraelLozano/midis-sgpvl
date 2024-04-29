using Microsoft.AspNetCore.Http;

namespace MIDIS.SGPVL.Utils.Helpers.FileManager
{
    public class AzureBlobStorage : IStorageManager
    {
        public string Base64ToFileBase64(string data, string type)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string path)
        {
            throw new NotImplementedException();
        }

        public string GetBase64(Stream stream)
        {
            throw new NotImplementedException();
        }

        public byte[] GetBytes(string path)
        {
            throw new NotImplementedException();
        }

        public string LoadFileToBase64(string path)
        {
            throw new NotImplementedException();
        }

        public void SaveFile(string path, string base64, string type)
        {
            throw new NotImplementedException();
        }

        public Task<string> SaveFileFormCollection(string path, IFormCollection formCollection)
        {
            throw new NotImplementedException();
        }
    }
}
