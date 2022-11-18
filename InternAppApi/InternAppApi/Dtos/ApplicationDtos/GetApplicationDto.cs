using InternAppApi.Dtos.SelectionDtos;

namespace InternAppApi.Dtos.ApplicationDtos
{
    public class GetApplicationDto
    {

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public EducationLevel EducationLevel { get; set; } = EducationLevel.HighSchool;
        public string CV { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Applied;
        public List<ApplicationComment> Comments { get; set; }
        public List<GetSelectionDetailsDto> Selections { get; set; }
    }
}