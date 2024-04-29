using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net.Http.Headers;

namespace MIDIS.SGPVL.Utils.Helpers.FileManager
{
    public class FileStorage : IStorageManager
    {
        public byte[] GetBytes(string path)
        {
            byte[] fileArray = File.ReadAllBytes(path);

            return fileArray;
        }

        public string GetBase64(Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
        public string LoadFileToBase64(string path)
        {
            byte[] fileArray = GetBytes(path);
            string base64ImageRepresentation = Convert.ToBase64String(fileArray);

            return base64ImageRepresentation;
        }

        public string Base64ToFileBase64(string data, string type)
        {
            string fileBase64 = $"data:{type};base64,{data}";
            return fileBase64;
        }

        public void SaveFile(string path, string base64, string type)
        {
            var bytes = Convert.FromBase64String(base64);

            using (var ms = new MemoryStream(bytes))
            {
                using (var fs = new FileStream(path, FileMode.Create))
                {
                    ms.WriteTo(fs);
                }
            }
        }

        public async Task<string> SaveFileFormCollection(string path, IFormCollection formCollection)
        {
            var file = formCollection.Files.First();
            var fileNameReturn = string.Empty;

            var extension = Path.GetExtension(file.FileName);
            if (file != null && file.Length > 0)
            {
                //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fileName = $"file_{Guid.NewGuid()}{extension}";

                var fullPath = Path.Combine(path, fileName);
                fileNameReturn = fileName;

                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    using (var fs = new FileStream(fullPath, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }
            }
            return fileNameReturn;
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
