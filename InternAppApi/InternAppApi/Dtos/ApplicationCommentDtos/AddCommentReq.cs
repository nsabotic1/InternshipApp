namespace InternAppApi.Dtos.ApplicationCommentDtos
{
    public class AddCommentReq
    {
        public int applicationId { get; set; }
        public string body { get; set; }
    }
}