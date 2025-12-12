namespace Models.Serialization
{
    public class AccountSettings
    {
        public bool TwoFactorEnabled { get; set; }
        public string Theme { get; set; }
        public string Language { get; set; }
    }
}