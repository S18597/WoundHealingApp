using System.ComponentModel;

namespace WoundHealingDb.Enums
{
    public enum TreatmentStatus
    {
        [Description("In progress")]
        InProgress = 1,
        [Description("Healed")]
        Healed = 2
    }
}