namespace Models
{
    public class Person : Entity
    {
        public DateTime BirthDate { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" - {BirthDate}";
        }
    }
}
