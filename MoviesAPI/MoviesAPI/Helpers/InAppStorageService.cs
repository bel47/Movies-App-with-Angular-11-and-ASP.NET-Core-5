using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MoviesAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Helpers
{
    public class InAppStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _emv;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InAppStorageService(IWebHostEnvironment emv, IHttpContextAccessor httpContextAccessor)
        {
            _emv = emv;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task DeleteFIle(string fileRoute, string containerName)
        {
            if (string.IsNullOrEmpty(fileRoute)) {
                return Task.CompletedTask;
            }
            var fileName = Path.GetFileName(fileRoute);
            var fileDirectory = Path.Combine(_emv.WebRootPath, containerName, fileName);
            if (File.Exists(fileDirectory)) {
                File.Delete(fileDirectory);
            }
            return Task.CompletedTask;
        }

        public async Task<string> EditFile(string containerName, IFormFile file, string fileRoute)
        {
            await DeleteFIle(fileRoute, containerName);
            return await SaveFile(containerName, file);
        }

        public async Task<string> SaveFile(string containerName, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(_emv.WebRootPath, containerName);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string route = Path.Combine(folder, fileName);
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var content = ms.ToArray();
                await File.WriteAllBytesAsync(route, content);
            }
            var url = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var routForDB = Path.Combine(url, containerName, fileName).Replace("\\", "/");
            return routForDB;
        }
    }
}
