using System.ComponentModel;

namespace WoundHealingWebApi.Enums
{
    public enum AccountType
    {
        [Description("Patient")]
        Patient = 1,
        [Description("Doctor")]
        Doctor = 2
    }
}