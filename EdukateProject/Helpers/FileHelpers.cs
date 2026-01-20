using System.Threading.Tasks;

namespace EdukateProject.Helpers
{
    public static class FileHelpers
    {
        public static bool CheckSize(this IFormFile file, int mb)
        {
            return file.Length <= mb * 1024 * 1024;
        }

        public static bool CheckType(this IFormFile file , string type)
        {
            return file.ContentType.Contains(type);
        }

        public static async Task<string> FileUpload(this IFormFile file , string folderPath)
        {
            string uniqueImagePath = Guid.NewGuid().ToString() + file.FileName;

            string path = Path.Combine(folderPath, uniqueImagePath);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await file.CopyToAsync(stream);

            return uniqueImagePath;
        }
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
