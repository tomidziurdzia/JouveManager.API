using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JouveManager.Domain.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ShipmentStatus
{
    [EnumMember(Value = "NotStarted")]
    NotStarted,
    [EnumMember(Value = "InProgress")]
    InProgress,
    [EnumMember(Value = "Delivered")]
    Delivered,
    [EnumMember(Value = "Cancelled")]
    Cancelled,
    [EnumMember(Value = "Reprogrammed")]
    Reprogrammed,
}

