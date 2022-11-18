using System.Text.Json.Serialization;

namespace InternAppApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EducationLevel
    {
        HighSchool = 1,
        Bachelor = 2,
        Masters = 3,
        PHD = 4
    }
}