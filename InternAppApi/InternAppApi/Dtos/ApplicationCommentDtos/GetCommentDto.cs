namespace InternAppApi.Dtos.ApplicationCommentDtos
{
    public class GetCommentDto
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }

    }
}