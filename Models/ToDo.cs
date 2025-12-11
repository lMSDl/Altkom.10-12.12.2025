namespace Models
{
    public class ToDo : Entity
    {
        public DateTime DueDate { get; set; }

        public override string ToString()
        {
            return base.ToString() + $" - {DueDate}";
        }
    }
}
