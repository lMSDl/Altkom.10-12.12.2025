namespace Models
{
    public class Product : Entity
    {
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; //ustalamy domyślną wartość na aktualny czas

        public override string ToString()
        {
            return base.ToString() + $" - {Price} - {CreatedAt}";
        }
    }
}
