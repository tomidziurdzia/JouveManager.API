using System.ComponentModel;

namespace JouveManager.Domain;


public enum UserType
{
    [Description("Administrative")]
    Administrative = 1,

    [Description("Manager")]
    Manager = 2,

    [Description("Employee")]
    Employee = 3,
}