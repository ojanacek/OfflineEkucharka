namespace OfflineKucharka
{
    public class Ingredience
    {
        public decimal Mnozstvi { get; set; }
        public string Jednotky { get; set; }
        public string Nazev { get; set; }

        public override string ToString()
        {
            if (Mnozstvi == 0)
                return Nazev;

            return string.Format("{0} {1} {2}", Mnozstvi, Jednotky, Nazev);
        }
    }
}
