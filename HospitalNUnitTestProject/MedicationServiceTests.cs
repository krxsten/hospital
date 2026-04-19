using Hospital.Core.DTOs;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Hospital.Tests.Services
{
    [TestFixture]
    public class MedicationServiceTests
    {
        private HospitalDbContext context;
        private MedicationService service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new HospitalDbContext(options);
            service = new MedicationService(context);
        }

        [TearDown]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        private async Task<Diagnose> SeedDiagnoseAsync(string name = "Flu")
        {
            var diagnose = new Diagnose
            {
                ID = Guid.NewGuid(),
                Name = name,
                ImageURL = "/images/test.jpg",
                PublicID = "public_id"
            };

            context.Diagnoses.Add(diagnose);
            await context.SaveChangesAsync();

            return diagnose;
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllMedications()
        {
            var diagnose1 = await SeedDiagnoseAsync("Flu");
            var diagnose2 = await SeedDiagnoseAsync("Asthma");

            context.Medications.AddRange(
                new Medication
                {
                    ID = Guid.NewGuid(),
                    Name = "Paracetamol",
                    DiagnoseID = diagnose1.ID,
                    Diagnose = diagnose1,
                    Description = "For fever",
                    SideEffects = "Nausea"
                },
                new Medication
                {
                    ID = Guid.NewGuid(),
                    Name = "Ventolin",
                    DiagnoseID = diagnose2.ID,
                    Diagnose = diagnose2,
                    Description = "For breathing",
                    SideEffects = "Headache"
                });

            await context.SaveChangesAsync();

            var result = await service.GetAllAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Any(x => x.Name == "Paracetamol"), Is.True);
            Assert.That(result.Any(x => x.Name == "Ventolin"), Is.True);
            Assert.That(result.Any(x => x.DiagnoseName == "Flu"), Is.True);
            Assert.That(result.Any(x => x.DiagnoseName == "Asthma"), Is.True);
        }

        [Test]
        public async Task GetByIdAsync_WhenMedicationExists_ReturnsMedication()
        {
            var diagnose = await SeedDiagnoseAsync("Diabetes");

            var medication = new Medication
            {
                ID = Guid.NewGuid(),
                Name = "Insulin",
                DiagnoseID = diagnose.ID,
                Diagnose = diagnose,
                Description = "Controls blood sugar",
                SideEffects = "Dizziness"
            };

            context.Medications.Add(medication);
            await context.SaveChangesAsync();

            var result = await service.GetByIdAsync(medication.ID);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.ID, Is.EqualTo(medication.ID));
            Assert.That(result.Name, Is.EqualTo("Insulin"));
            Assert.That(result.DiagnoseName, Is.EqualTo("Diabetes"));
            Assert.That(result.Description, Is.EqualTo("Controls blood sugar"));
            Assert.That(result.SideEffects, Is.EqualTo("Dizziness"));
        }

        [Test]
        public async Task GetByIdAsync_WhenMedicationMissing_ReturnsNull()
        {
            var result = await service.GetByIdAsync(Guid.NewGuid());

            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task CreateAsync_AddsMedicationToDatabase()
        {
            var diagnose = await SeedDiagnoseAsync("Cold");

            var model = new MedicationCreateDTO
            {
                Name = "Aspirin",
                DiagnoseID = diagnose.ID,
                Description = "Pain relief",
                SideEffects = "Stomach pain"
            };

            await service.CreateAsync(model);

            var medication = await context.Medications.FirstOrDefaultAsync();

            Assert.That(medication, Is.Not.Null);
            Assert.That(medication!.Name, Is.EqualTo("Aspirin"));
            Assert.That(medication.DiagnoseID, Is.EqualTo(diagnose.ID));
            Assert.That(medication.Description, Is.EqualTo("Pain relief"));
            Assert.That(medication.SideEffects, Is.EqualTo("Stomach pain"));
        }

        [Test]
        public async Task UpdateAsync_WhenMedicationExists_UpdatesMedication()
        {
            var diagnose1 = await SeedDiagnoseAsync("Flu");
            var diagnose2 = await SeedDiagnoseAsync("Migraine");

            var medication = new Medication
            {
                ID = Guid.NewGuid(),
                Name = "OldMed",
                DiagnoseID = diagnose1.ID,
                Description = "Old description",
                SideEffects = "Old effects"
            };

            context.Medications.Add(medication);
            await context.SaveChangesAsync();

            var model = new MedicationEditDTO
            {
                ID = medication.ID,
                Name = "NewMed",
                DiagnoseID = diagnose2.ID,
                Description = "New description",
                SideEffects = "New effects"
            };

            await service.UpdateAsync(model);

            var updated = await context.Medications.FindAsync(medication.ID);

            Assert.That(updated, Is.Not.Null);
            Assert.That(updated!.Name, Is.EqualTo("NewMed"));
            Assert.That(updated.DiagnoseID, Is.EqualTo(diagnose2.ID));
            Assert.That(updated.Description, Is.EqualTo("New description"));
            Assert.That(updated.SideEffects, Is.EqualTo("New effects"));
        }

        [Test]
        public async Task UpdateAsync_WhenMedicationMissing_DoesNothing()
        {
            var diagnose = await SeedDiagnoseAsync("Flu");

            var model = new MedicationEditDTO
            {
                ID = Guid.NewGuid(),
                Name = "Missing",
                DiagnoseID = diagnose.ID,
                Description = "Desc",
                SideEffects = "Effects"
            };

            await service.UpdateAsync(model);

            Assert.That(await context.Medications.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteAsync_WhenMedicationExists_RemovesMedication()
        {
            var diagnose = await SeedDiagnoseAsync("Flu");

            var medication = new Medication
            {
                ID = Guid.NewGuid(),
                Name = "DeleteMe",
                DiagnoseID = diagnose.ID,
                Description = "Desc",
                SideEffects = "Effects"
            };

            context.Medications.Add(medication);
            await context.SaveChangesAsync();

            await service.DeleteAsync(medication.ID);

            var deleted = await context.Medications.FindAsync(medication.ID);
            Assert.That(deleted, Is.Null);
        }

        [Test]
        public async Task DeleteAsync_WhenMedicationMissing_DoesNothing()
        {
            await service.DeleteAsync(Guid.NewGuid());

            Assert.That(await context.Medications.CountAsync(), Is.EqualTo(0));
        }

        [Test]
        public async Task GetMedicationsForSideEffect_WhenSideEffectIsNull_ReturnsEmptyList()
        {
            var result = await service.GetMedicationsForSideEffect(null!);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetMedicationsForSideEffect_WhenSideEffectIsWhitespace_ReturnsEmptyList()
        {
            var result = await service.GetMedicationsForSideEffect("   ");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetMedicationsForSideEffect_WhenSingleWordMatches_ReturnsMatchingMedications()
        {
            var diagnose = await SeedDiagnoseAsync("Flu");

            context.Medications.AddRange(
                new Medication
                {
                    ID = Guid.NewGuid(),
                    Name = "Med1",
                    DiagnoseID = diagnose.ID,
                    Diagnose = diagnose,
                    Description = "Desc1",
                    SideEffects = "nausea and headache"
                },
                new Medication
                {
                    ID = Guid.NewGuid(),
                    Name = "Med2",
                    DiagnoseID = diagnose.ID,
                    Diagnose = diagnose,
                    Description = "Desc2",
                    SideEffects = "dry mouth"
                });

            await context.SaveChangesAsync();

            var result = await service.GetMedicationsForSideEffect("nausea");

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Med1"));
        }

        [Test]
        public async Task GetMedicationsForSideEffect_WhenMultipleWordsMatch_ReturnsCorrectMedications()
        {
            var diagnose = await SeedDiagnoseAsync("Asthma");

            context.Medications.AddRange(
                new Medication
                {
                    ID = Guid.NewGuid(),
                    Name = "Med1",
                    DiagnoseID = diagnose.ID,
                    Diagnose = diagnose,
                    Description = "Desc1",
                    SideEffects = "mild nausea and headache"
                },
                new Medication
                {
                    ID = Guid.NewGuid(),
                    Name = "Med2",
                    DiagnoseID = diagnose.ID,
                    Diagnose = diagnose,
                    Description = "Desc2",
                    SideEffects = "headache only"
                },
                new Medication
                {
                    ID = Guid.NewGuid(),
                    Name = "Med3",
                    DiagnoseID = diagnose.ID,
                    Diagnose = diagnose,
                    Description = "Desc3",
                    SideEffects = "nausea only"
                });

            await context.SaveChangesAsync();

            var result = await service.GetMedicationsForSideEffect("nausea headache");

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Name, Is.EqualTo("Med1"));
        }

        [Test]
        public async Task GetMedicationsForSideEffect_WhenNoMatches_ReturnsEmptyList()
        {
            var diagnose = await SeedDiagnoseAsync("Cold");

            context.Medications.Add(
                new Medication
                {
                    ID = Guid.NewGuid(),
                    Name = "Med1",
                    DiagnoseID = diagnose.ID,
                    Diagnose = diagnose,
                    Description = "Desc1",
                    SideEffects = "dizziness"
                });

            await context.SaveChangesAsync();

            var result = await service.GetMedicationsForSideEffect("rash");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public async Task GetMedicationsForSideEffect_WhenDiagnoseIsNull_ReturnsNoDiagnoseText()
        {
            var medication = new Medication
            {
                ID = Guid.NewGuid(),
                Name = "StandaloneMed",
                DiagnoseID = Guid.NewGuid(),
                Diagnose = null,
                Description = "Desc",
                SideEffects = "nausea"
            };

            context.Medications.Add(medication);
            await context.SaveChangesAsync();

            var result = await service.GetMedicationsForSideEffect("nausea");

            Assert.That(result.Count, Is.EqualTo(0));
        }
    }
}