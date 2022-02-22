namespace WoundHealingWebApi.Configs
{
    public class AppConfig
    {
        public bool IsTest { get; set; }

        public override string ToString()
        {
            return $"{nameof(IsTest)}: {IsTest}";
        }
    }
}