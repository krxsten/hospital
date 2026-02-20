using Hospital.Core.Contracts;
using Hospital.Core.Services;
using Hospital.Data;
using Hospital.Data.Entities;
using Hospital.Entities;
using Hospital.WebProject.Controllers;
using Hospital.WebProject.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;

})
.AddRoles<IdentityRole<Guid>>()
.AddEntityFrameworkStores<HospitalDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IDiagnoseService, DiagnoseService>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    await IdentitySeeder.SeedRolesAsync(roleManager);
    await IdentitySeeder.SeedAdminAsync(userManager);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();

    context.Database.EnsureCreated();

   
    if (!context.Specializations.Any())
    {
        context.Specializations.AddRange(
            new Specialization { ID = Guid.NewGuid(), SpecializationName = "Cardiology", Image = "cardiology.jpg" },
            new Specialization { ID = Guid.NewGuid(), SpecializationName = "Neurology", Image = "neurology.jpg" },
            new Specialization { ID = Guid.NewGuid(), SpecializationName = "Pediatrics", Image = "pediatrics.jpg" },
            new Specialization { ID = Guid.NewGuid(), SpecializationName = "Orthopedics", Image = "orthopedics.jpg" },
            new Specialization { ID = Guid.NewGuid(), SpecializationName = "Dermatology", Image = "dermatology.jpg" }
        );
        context.SaveChanges();
    }

   
    if (!context.Shifts.Any())
    {
        context.Shifts.AddRange(
            new Shift { ID = Guid.NewGuid(), Type = "Morning", StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(14) },
            new Shift { ID = Guid.NewGuid(), Type = "Afternoon", StartTime = TimeSpan.FromHours(14), EndTime = TimeSpan.FromHours(20) },
            new Shift { ID = Guid.NewGuid(), Type = "Night", StartTime = TimeSpan.FromHours(20), EndTime = TimeSpan.FromHours(8) }
        );
        context.SaveChanges();
    }

    
    if (!context.Rooms.Any())
    {
        context.Rooms.AddRange(
            new Room { ID = Guid.NewGuid(), RoomNumber = 101, IsTaken = false },
            new Room { ID = Guid.NewGuid(), RoomNumber = 102, IsTaken = false },
            new Room { ID = Guid.NewGuid(), RoomNumber = 201, IsTaken = false },
            new Room { ID = Guid.NewGuid(), RoomNumber = 202, IsTaken = false },
            new Room { ID = Guid.NewGuid(), RoomNumber = 301, IsTaken = false }
        );
        context.SaveChanges();
    }

    
    if (!context.Diagnoses.Any())
    {
        var hypertension = new Diagnose { ID = Guid.NewGuid(), Name = "Hypertension", Image = "hypertension.jpg" };
        var diabetes = new Diagnose { ID = Guid.NewGuid(), Name = "Diabetes", Image = "diabetes.jpg" };
        var asthma = new Diagnose { ID = Guid.NewGuid(), Name = "Asthma", Image = "asthma.jpg" };
        var pneumonia = new Diagnose { ID = Guid.NewGuid(), Name = "Pneumonia", Image = "pneumonia.jpg" };

        context.Diagnoses.AddRange(hypertension, diabetes, asthma, pneumonia);
        context.SaveChanges();

        
        context.Medications.AddRange(
            new Medication
            {
                ID = Guid.NewGuid(),
                Name = "Lisinopril",
                Description = "Blood pressure medication",
                SideEffects = "Dizziness, cough",
                DiagnoseID = hypertension.ID
            },
            new Medication
            {
                ID = Guid.NewGuid(),
                Name = "Metformin",
                Description = "Blood sugar control",
                SideEffects = "Nausea, diarrhea",
                DiagnoseID = diabetes.ID
            },
            new Medication
            {
                ID = Guid.NewGuid(),
                Name = "Salbutamol",
                Description = "Asthma inhaler",
                SideEffects = "Tremor, headache",
                DiagnoseID = asthma.ID
            },
            new Medication
            {
                ID = Guid.NewGuid(),
                Name = "Azithromycin",
                Description = "Antibiotic for pneumonia",
                SideEffects = "Stomach pain",
                DiagnoseID = pneumonia.ID
            }
        );

        context.SaveChanges();
    }
}
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
