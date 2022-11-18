namespace InternAppApi.Dtos.ApplicationDtos
{
    public class UpdateApplicationDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public EducationLevel EducationLevel { get; set; } = EducationLevel.HighSchool;
        public string CV { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Applied;
    }
}