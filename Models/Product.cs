using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Models
{
    public class Product : Entity
    {
        public float Price { get; set; }

        [JsonIgnore] //ignorujemy to pole podczas serializacji do JSON
        [XmlIgnore] //ignorujemy to pole podczas serializacji do XML
        public DateTime CreatedAt { get; set; } = DateTime.Now; //ustalamy domyślną wartość na aktualny czas

        public string FullInfo => base.ToString() + $" - {Price} - {CreatedAt}";

        public override string ToString()
        {
            return FullInfo;
        }
    }
}
