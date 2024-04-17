using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Ksiegarnia
{
    class Program
    {
        static List<Ksiazka> ksiazki = new List<Ksiazka>();
        static string filePath = "ksiazki.json";

        static void Main(string[] args)
        {
            WczytajKsiazkiZPliku();

            while (true)
            {
                Console.WriteLine("Wybierz opcję:");
                Console.WriteLine("1. Dodaj nową książkę");
                Console.WriteLine("2. Wyświetl listę książek");
                Console.WriteLine("3. Wyświetl dane książki");
                Console.WriteLine("4. Usuń książkę");
                Console.WriteLine("5. Zakończ program");

                string opcja = Console.ReadLine();

                switch (opcja)
                {
                    case "1":
                        DodajNowaKsiazke();
                        break;
                    case "2":
                        WyswietlListeKsiazek();
                        break;
                    case "3":
                        WyswietlDaneKsiazki();
                        break;
                    case "4":
                        UsunKsiazke();
                        break;
                    case "5":
                        ZapiszKsiazkiDoPliku();
                        return;
                    default:
                        Console.WriteLine("Niepoprawna opcja.");
                        break;
                }
            }
        }

        static void WczytajKsiazkiZPliku()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                ksiazki = JsonConvert.DeserializeObject<List<Ksiazka>>(json);
            }
            else
            {
                Console.WriteLine("Plik z danymi nie istnieje. Utworzono nowy.");
            }
        }

        static void ZapiszKsiazkiDoPliku()
        {
            string json = JsonConvert.SerializeObject(ksiazki, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        static void DodajNowaKsiazke()
        {
            Console.WriteLine("Dodawanie nowej książki:");

            Console.Write("Tytuł: ");
            string tytul = Console.ReadLine();

            Console.Write("Autor: ");
            string autor = Console.ReadLine();

            Console.Write("Rok wydania: ");
            int rokWydania = Convert.ToInt32(Console.ReadLine());

            Console.Write("Gatunek: ");
            string gatunek = Console.ReadLine();

            int noweId = ksiazki.Count + 1;

            ksiazki.Add(new Ksiazka(noweId, tytul, autor, rokWydania, gatunek));

            Console.WriteLine("Książka została dodana.");
        }

        static void WyswietlListeKsiazek()
        {
            Console.WriteLine("Lista książek:");

            foreach (var ksiazka in ksiazki)
            {
                Console.WriteLine($"ID: {ksiazka.Id}, Tytuł: {ksiazka.Tytul}");
            }
        }

        static void WyswietlDaneKsiazki()
        {
            Console.Write("Podaj ID książki: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Ksiazka ksiazka = ksiazki.Find(k => k.Id == id);

            if (ksiazka != null)
            {
                Console.WriteLine($"ID: {ksiazka.Id}");
                Console.WriteLine($"Tytuł: {ksiazka.Tytul}");
                Console.WriteLine($"Autor: {ksiazka.Autor}");
                Console.WriteLine($"Rok wydania: {ksiazka.RokWydania}");
                Console.WriteLine($"Gatunek: {ksiazka.Gatunek}");
            }
            else
            {
                Console.WriteLine("Książka o podanym ID nie została znaleziona.");
            }
        }

        static void UsunKsiazke()
        {
            Console.Write("Podaj ID książki do usunięcia: ");
            int id = Convert.ToInt32(Console.ReadLine());

            Ksiazka ksiazka = ksiazki.Find(k => k.Id == id);

            if (ksiazka != null)
            {
                ksiazki.Remove(ksiazka);
                Console.WriteLine("Książka została usunięta.");
            }
            else
            {
                Console.WriteLine("Książka o podanym ID nie została znaleziona.");
            }
        }
    }

    /*
     * Klasa: Ksiazka
     * 
     * Opis: Program reprezentuje id które dodają się same, tytuł, autor, rok_wydania, gatunek.
     * 
     * Pola: int Id
     *       string Tytul - przechowuje Tytuł
     *       string Autor - przechowuje Autora
     *       int RokWydania - przechowuje RokWydania
     *       string Gatunek - przechowuje gatunek
     */

    class Ksiazka
    {
        public int Id { get; set; }
        public string Tytul { get; set; }
        public string Autor { get; set; }
        public int RokWydania { get; set; }
        public string Gatunek { get; set; }

        public Ksiazka(int id, string tytul, string autor, int rokWydania, string gatunek)
        {
            Id = id;
            Tytul = tytul;
            Autor = autor;
            RokWydania = rokWydania;
            Gatunek = gatunek;
        }
    }
}
