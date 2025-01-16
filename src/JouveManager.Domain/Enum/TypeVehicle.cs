using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JouveManager.Domain.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TypeVehicle
{
    [EnumMember(Value = "Chassis")]
    Chassis,
    [EnumMember(Value = "Truck")]
    Truck,
    [EnumMember(Value = "Van")]
    Van,
    [EnumMember(Value = "Pickup")]
    Pickup,
    [EnumMember(Value = "TractorUnit")]
    TractorUnit
}