namespace InternAppApi.Models
{
    public class ApplicationComment
    {
        public int Id { get; set; }
        public string Body { get; set; } = string.Empty;
        public string UserName { get; set; }
        public string UserId { get; set; }
    }
}