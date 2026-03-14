namespace Hospital.WebProject.ViewModels.Doctor
{
    public class DoctorIndexViewModel
    {
        public Guid SpecializationId { get; set; }
        public string SpecializationName { get; set; } = null!;

        public Guid ShiftId { get; set; }
        public string ShiftName { get; set; } = null!;

        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;

        public bool IsAccepted { get; set; }
        public string Image { get; set; } = null!;

        public List<Guid> PatientIDs { get; set; } = new();
        public List<string> PatientNames { get; set; } = new();

        public List<Guid> NurseIDs { get; set; } = new();
        public List<string> NurseNames { get; set; } = new();

        public List<Guid> CheckupIDs { get; set; } = new();
    }
}