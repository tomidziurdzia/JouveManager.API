using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace JouveManager.Domain.Enum;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TypeSemiTrailer
{
    [EnumMember(Value = "Sider")]
    Sider,
    [EnumMember(Value = "DropSide")]
    DropSide,
}
