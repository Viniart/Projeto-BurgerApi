using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerApi.Services
{
    public interface IUploadService
    {
        Task<string> UploadImagemAsync(IFormFile arquivo);

    }
}