using System.Text;
using System.Windows.Media.Imaging;

namespace OfflineKucharka
{
    public class Recept
    {
        public string Nazev { get; set; }
        public BitmapImage Obrazek { get; set; }
        public string Popisek { get; set; }
        public string[] Kategorie { get; set; }
        public Ingredience[] Ingredience { get; set; }
        public string IngredienceText { get { return string.Join<Ingredience>("\n", Ingredience); } }
        public string Postup { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Nazev);
            sb.AppendLine(Popisek);
            sb.AppendLine(string.Join(",", Kategorie));
            sb.AppendLine(string.Join<Ingredience>(";", Ingredience));
            sb.Append(Postup);
            return sb.ToString();
        }
    }
}
