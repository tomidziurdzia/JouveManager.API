using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserType
{
    Administrative,
    Manager,
    Employee,
    Generic
}