using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Core.Services
{
    public class DiagnoseService : IDiagnoseService
    {
        private readonly HospitalDbContext context;
        private readonly IImageService imageService;

        public DiagnoseService(HospitalDbContext context, IImageService imageService)
        {
            this.context = context;
            this.imageService = imageService;
        }

        public async Task<List<DiagnoseIndexDTO>> GetAllAsync()
        {
            return await context.Diagnoses.Select(x => new DiagnoseIndexDTO
            {
                ID = x.ID,
                Name = x.Name,
                ImageURL = x.ImageURL
            }).ToListAsync();
        }

        public async Task<DiagnoseIndexDTO?> GetByIdAsync(Guid id)
        {
            var diagnose = await context.Diagnoses.Where(x => x.ID == id).Select(x => new DiagnoseIndexDTO
            {
                ID = x.ID,
                Name = x.Name,
                ImageURL = x.ImageURL
            }).FirstOrDefaultAsync();
            return diagnose;
        }

        public async Task CreateAsync(DiagnoseCreateDTO model)
        {
            var uploadResult = await imageService.UploadImageAsync(model.ImageFile);
            var diagnose = new Diagnose
            {
                ID = Guid.NewGuid(),
                Name = model.Name,
                ImageURL = uploadResult.Url,
                PublicID = uploadResult.PublicId
            };
            await context.Diagnoses.AddAsync(diagnose);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DiagnoseIndexDTO dto)
        {
            var diagnose = await context.Diagnoses.FindAsync(dto.ID);

            if (diagnose == null)
                throw new Exception("Diagnose not found");

            diagnose.Name = dto.Name;

            if (dto.NewImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.NewImageFile.FileName);

                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.NewImageFile.CopyToAsync(stream);
                }

                diagnose.ImageURL = "/images/" + fileName;
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var diagnose = await context.Diagnoses.FindAsync(id);

            if (diagnose == null)
            {
                return;
            }
            context.Diagnoses.Remove(diagnose);
            await context.SaveChangesAsync();
        }
        public async Task<List<DiagnoseIndexDTO>> GetDiagnose(string diagnose)
        {
            if (string.IsNullOrWhiteSpace(diagnose))
            {
                return new List<DiagnoseIndexDTO>();
            }
            string pattern = $"%{diagnose}%";
            return await context.Diagnoses
                .Where(x => EF.Functions.Like(x.Name, pattern))
                .Select(x => new DiagnoseIndexDTO
                {
                    ID = x.ID,
                    Name = x.Name,
                    ImageURL = x.ImageURL
                })
                .ToListAsync();
        }
    }
}