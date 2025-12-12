namespace Models.Serialization
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public PersonalData Personal { get; set; }
        public Address HomeAddress { get; set; }
        public AccountSettings Settings { get; set; }
        public List<string> Permissions { get; set; }
    }
}
