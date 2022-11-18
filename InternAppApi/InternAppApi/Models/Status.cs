using System.Text.Json.Serialization;

namespace InternAppApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Applied = 1,
        PreSelection = 2,
        InSelection = 3,
        Completed = 4, 
        Rejected = 5,
    }
}
