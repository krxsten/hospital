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

        public DiagnoseService(HospitalDbContext context)
        {
            this.context = context;
        }

        public async Task<List<DiagnoseIndexDTO>> GetAllAsync()
        {
            return await context.Diagnoses.Select(x => new DiagnoseIndexDTO
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.ImageURL
            }).ToListAsync();
        }

        public async Task<DiagnoseIndexDTO?> GetByIdAsync(Guid id)
        {
            var diagnose = await context.Diagnoses.Where(x => x.ID == id).Select(x => new DiagnoseIndexDTO
            {
                ID = x.ID,
                Name = x.Name,
                Image = x.ImageURL
            }).FirstOrDefaultAsync();
            return diagnose;
        }

        public async Task CreateAsync(DiagnoseCreateDTO model)
        {
            var diagnose = new Diagnose
            {
                ID = Guid.NewGuid(),
                Name = model.Name,
                ImageURL = model.Image
            };
            await context.Diagnoses.AddAsync(diagnose);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DiagnoseIndexDTO model)
        {
            var diagnose = await context.Diagnoses.FindAsync(model.ID);
            if (diagnose == null)
            {
                return;
            }
            diagnose.Name = model.Name;
            diagnose.ImageURL = model.Image;
            //var uploadResult = await imageService.UploadImageAsync(model.ImageFile, model.ImageFile.FileName, "images");
            //imageURL = uploadResult.Url;
            //publicId = uploadResult.PublicId;
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
    }
}