using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BurgerApi.Services
{
    public class UploadService : IUploadService
    {
        public async Task<string> UploadImagemAsync(IFormFile arquivo)
        {
            // 1. Verificar o Arquivo Enviado
            
            // Arquivo Vazio
            if (arquivo == null || arquivo.Length == 0)
                throw new Exception("Erro: arquivo vazio");

            // Arquivo é muito grande
            if (arquivo.Length > 1024 * 1024 * 2)
                throw new Exception("Erro: Arquivo excede o limite de 2MB");

            // Extensão do arquivo
            var extensao = Path.GetExtension(arquivo.FileName).ToLower();
            var permitidas = new[] {".jpg", ".jpeg", ".png"};

            if (!permitidas.Contains(extensao))
                throw new Exception("Erro: Extensão de arquivo inválida!");


            var novoNome = Guid.NewGuid().ToString() + extensao;

            var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), 
            "Uploads");

            if (!Directory.Exists(pastaDestino))
            {
                Directory.CreateDirectory(pastaDestino);
            }

            var caminhoFinal = Path.Combine(pastaDestino, novoNome);

            using (var stream = new FileStream(caminhoFinal, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return "/Uploads/" + novoNome;
        }
    }
}