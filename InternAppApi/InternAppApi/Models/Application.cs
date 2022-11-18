namespace InternAppApi.Models
{
    public class Application
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public EducationLevel EducationLevel { get; set; } = EducationLevel.HighSchool;
        public string CV { get; set; } = string.Empty;
        public string CoverLetter { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Applied;
        public List<ApplicationComment> Comments { get; set; } = new List<ApplicationComment>();
        public List<Selection> Selections { get; set; }=new List<Selection>();
    }
}
