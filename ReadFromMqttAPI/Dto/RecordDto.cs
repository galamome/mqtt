using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ReadFromMqttAPI.Dto;

/// <summary>
/// Record Data Transfer Object
/// </summary>
public sealed class RecordDto
{
    /// <summary>
    /// Creation Date and Time of the record
    /// </summary>
    /// <example>2023-05-02T10:30:00Z</example>
    [Required]
    public DateTime? CreationDateTime { get; init; }

    /// <summary>
    /// Topic
    /// </summary>
    /// <example>18T_Press_Temperature</example>
    [Required]
    public string Topic { get; init; }

    /// <summary>
    /// Value
    /// </summary>
    /// <example>18°C</example>
    [Required]
    public string Value { get; init; }
}