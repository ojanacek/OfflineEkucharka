using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace OfflineKucharka
{
    static class PraceSeSoubory
    {
        const string SlozkaReceptu = "Recepty";

        public static IEnumerable<Recept> NacistRecepty()
        {
            string cestaAplikace = Assembly.GetExecutingAssembly().Location;
            string cestaSlozkyReceptu = Path.Combine(Path.GetDirectoryName(cestaAplikace), SlozkaReceptu);

            foreach (var receptSoubor in Directory.GetFiles(cestaSlozkyReceptu, "*.txt"))
            {
                Recept recept = null;

                try
                {
                    recept = ParsovatRecept(File.ReadAllLines(receptSoubor, Encoding.Default));
                    string jmenoSouboruReceptu = Path.GetFileNameWithoutExtension(receptSoubor);
                    recept.Obrazek = NajitObrazekReceptu(cestaSlozkyReceptu, jmenoSouboruReceptu);
                }
                catch { MessageBox.Show("spatne formovany recept " + receptSoubor); }

                if (recept != null)
                    yield return recept;
            }
        }

        private static Recept ParsovatRecept(string[] radkyTextu)
        {
            string nazev = radkyTextu[0];
            string popisek = radkyTextu[1];
            string[] kategorie = radkyTextu[2].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] ingredienceText = radkyTextu[3].Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            var ingredience = ingredienceText.Select(ParsovatIngredienci).ToArray();
            string postup = string.Join("\n", radkyTextu.Skip(4)).TrimEnd();

            return new Recept
            {
                Nazev = nazev,
                Popisek = popisek,
                Kategorie = kategorie,
                Ingredience = ingredience,
                Postup = postup
            };
        }

        private static Ingredience ParsovatIngredienci(string text)
        {
            string[] casti = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (casti.Length == 1)
                return new Ingredience { Nazev = casti[0] };

            decimal mnozstvi = ParsovatMnozstvi(casti[0]);

            if (casti.Length == 2)
                return new Ingredience { Mnozstvi = mnozstvi, Nazev = casti[1] };
            
            string jednotky = casti[1];
            string nazev = string.Join(" ", casti.Skip(2));
            return new Ingredience
            {
                Mnozstvi = mnozstvi,
                Jednotky = jednotky,
                Nazev = nazev
            };
        }

        private static decimal ParsovatMnozstvi(string text)
        {
            if (text.Contains("/"))
            {
                string[] casti = text.Split('/');
                decimal cast1 = decimal.Parse(casti[0]);
                decimal cast2 = decimal.Parse(casti[1]);
                return cast1 / cast2;
            }
            else
            {
                return decimal.Parse(text);
            }
        }

        private static BitmapImage NajitObrazekReceptu(string slozkaReceptu, string jmenoReceptu)
        {
            foreach (string cestaObrazku in Directory.GetFiles(slozkaReceptu, "*.jpg"))
            {
                string jmenoObrazkuBezPripony = Path.GetFileNameWithoutExtension(cestaObrazku);
                if (string.Equals(jmenoObrazkuBezPripony, jmenoReceptu, StringComparison.OrdinalIgnoreCase))
                {
                    return NacistObrazek(cestaObrazku);
                }
            }

            return null;
        }

        private static BitmapImage NacistObrazek(string cestaSouboru)
        {
            Uri uri = new Uri(cestaSouboru);
            return new BitmapImage(uri);
        }
    }
}
