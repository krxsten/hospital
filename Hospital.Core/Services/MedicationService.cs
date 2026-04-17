using Hospital.Core.Contracts;
using Hospital.Core.DTOs;
using Hospital.Data;
using Hospital.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Core.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly HospitalDbContext context;

        public MedicationService(HospitalDbContext context)
        {
            this.context = context;
        }
		public async Task<List<MedicationIndexDTO>> GetAllAsync()
		{
			return await context.Medications
				.Select(x => new MedicationIndexDTO
				{
					ID = x.ID,
					Name = x.Name,
					DiagnoseID = x.DiagnoseID,
					DiagnoseName = x.Diagnose.Name,
					Description = x.Description,
					SideEffects = x.SideEffects
				})
				.ToListAsync();
		}

		public async Task<MedicationIndexDTO?> GetByIdAsync(Guid id)
		{
			return await context.Medications
				.Where(x => x.ID == id)
				.Select(x => new MedicationIndexDTO
				{
					ID = x.ID,
					Name = x.Name,
					DiagnoseID = x.DiagnoseID,
					DiagnoseName = x.Diagnose.Name,
					Description = x.Description,
					SideEffects = x.SideEffects
				})
				.FirstOrDefaultAsync();
		}

		public async Task CreateAsync(MedicationCreateDTO model)
		{
			var medication = new Medication
			{
				ID = Guid.NewGuid(),
				Name = model.Name,
				DiagnoseID = model.DiagnoseID,
				Description = model.Description,
				SideEffects = model.SideEffects
			};

			await context.Medications.AddAsync(medication);
			await context.SaveChangesAsync();
		}

		public async Task UpdateAsync(MedicationEditDTO model)
		{
			var medication = await context.Medications.FindAsync(model.ID);
			if (medication == null)
			{
				return;
			}
			medication.Name = model.Name;
			medication.DiagnoseID = model.DiagnoseID;
			medication.Description = model.Description;
			medication.SideEffects = model.SideEffects;

			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var medication = await context.Medications.FindAsync(id);
			if (medication == null)
			{
				return;
			}

			context.Medications.Remove(medication);
			await context.SaveChangesAsync();
		}
        public async Task<List<MedicationIndexDTO>> GetMedicationsForSideEffect(string sideEffect)
        {
            if (string.IsNullOrWhiteSpace(sideEffect))
            {
                return new List<MedicationIndexDTO>();
            }

            var words = sideEffect
                .ToLower()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var query = context.Medications.AsQueryable();

            foreach (var word in words)
            {
                string pattern = $"%{word}%";

                query = query.Where(x =>
                    x.SideEffects != null &&
                    EF.Functions.Like(x.SideEffects.ToLower(), pattern));
            }

            return await query
                .Select(x => new MedicationIndexDTO
                {
                    ID = x.ID,
                    Name = x.Name,
                    DiagnoseID = x.DiagnoseID,
                    DiagnoseName = x.Diagnose != null ? x.Diagnose.Name : "No Diagnose",
                    Description = x.Description,
                    SideEffects = x.SideEffects
                })
                .ToListAsync();
        }
    }
}
