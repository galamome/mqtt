using ReadFromMqttAPI.Model;
using ReadFromMqttAPI.Extensions;

namespace ReadFromMqttAPI.Dto;

public static class RecortDtoExtensions
{
    public static IRecord ToInterface(this RecordDto dto)
    {
        return new Record()
        {
            CreationDateTime = dto.CreationDateTime,
            Topic = dto.Topic,
            Value =  dto.Value            
        };
    }

    public static RecordDto ToDto(this IRecord record)
    {
        return new RecordDto()
        {
            CreationDateTime = record.CreationDateTime,
            Topic = record.Topic,
            Value = record.Value
        };
    }
}