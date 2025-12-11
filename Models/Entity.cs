namespace Models
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; //ustalamy domyślną wartość na pusty string, aby uniknąć wartości null

        public override string ToString()
        {
            return $"{Id}. {Name}";
        }
    }
}
