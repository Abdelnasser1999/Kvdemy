using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Kvdemy.Web.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<byte[]> GetFile(string folderName, string fileName)
        {
            var filePath = Path.Combine(_env.WebRootPath, folderName, fileName);
            return await File.ReadAllBytesAsync(filePath);
        }
        
        public async Task<string> GetFileBase64(string folderName, string fileName)
        {
            var file = await GetFile(folderName, fileName);
            return Convert.ToBase64String(file);
        }

        public async Task<string> SaveFile(IFormFile file, string folderName)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                await using var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }

        public async Task<string> SaveFile(byte[] file, string folderName, string extension)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), file);
            }

            return fileName;
        }

        public async Task<string> SaveFile(string file, string folderName, string extension)
        {
            string fileName = null;
            if (!string.IsNullOrWhiteSpace(file))
            {
                file = file.Substring(file.IndexOf(",", StringComparison.Ordinal) + 1);
                var bytes = Convert.FromBase64String(file);
                var uploads = Path.Combine(_env.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;
                await File.WriteAllBytesAsync(Path.Combine(uploads, fileName), bytes);
            }

            return fileName;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            // إنشاء اسم ملف فريد بناءً على الـ GUID وإضافة امتداد الملف الأصلي
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // تحديد المسار الكامل حيث سيتم تخزين الملف
            var folderPath = Path.Combine(_env.WebRootPath, "Chats", folderName);

            // التحقق مما إذا كان المجلد موجودًا، وإذا لم يكن موجودًا، يتم إنشاؤه
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // إنشاء المسار الكامل للملف
            var filePath = Path.Combine(folderPath, fileName);

            // نسخ الملف إلى المسار المحدد
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // إرجاع اسم الملف
            return fileName;
        }

    }
}
