namespace Models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; //ustalamy domyślną wartość na pusty string, aby uniknąć wartości null
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; //ustalamy domyślną wartość na aktualny czas

    }
}
