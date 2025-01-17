﻿
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
//Added these again since I became uncertain.

namespace MJU23v_DTP_T2
{
    internal class Program
    {
        static List<Link> links = new List<Link>();
        //FIXME Creating a static string made on error out of two disappear.
        static string filepath = @"..\..\..\links\links.lis";
        class Link
        {
            public string category, group, name, descr, link;
            public Link(string category, string group, string name, string descr, string link)
            {
                this.category = category;
                this.group = group;
                this.name = name;
                this.descr = descr;
                this.link = link;
            }

            public Link(string line)
            {
                string[] part = line.Split('|');
                category = part[0];
                group = part[1];
                name = part[2];
                descr = part[3];
                link = part[4];
            }
            public void Print(int i)
            {
                Console.WriteLine($"|{i,-2}|{category,-10}|{group,-10}|{name,-20}|{descr,-40}|");
            }
            public void OpenLink()
            {
                Process application = new Process();
                application.StartInfo.UseShellExecute = true;
                application.StartInfo.FileName = link;
                application.Start();
            }
            //. 1 We need to override the ToString implementation.
            public override string ToString()
            {
                return $"{category}|{group}|{name}|{descr}|{link}";
            }
        }
        static void Main(string[] args)
        {
            // 2. We change names and method variable names.
            string filepath = @"..\..\..\links\links.lis";
            // 4. We implement a ReadLinksFromFile to start with.
            ReadLinksFromFile(filepath);
            // 3. We commit a break out static method.
            Console.WriteLine("Välkommen till länklistan! Skriv 'hjälp' för hjälp!");

            do
            {
                Console.Write("> ");
                string cmd = Console.ReadLine().Trim();
                string[] arg = cmd.Split();
                string command = arg[0];

                // 5. I'll implement a switch here instead.
                switch (command)
                {
                    // 7. Case Sluta
                    case "sluta":
                        Console.WriteLine("Hej då! Välkommen åter");
                        break;
                    // 8. Case Hjälp
                    case "hjälp":
                        PrintHelp();
                        break;
                    // 9. Case Ladda
                    case "ladda":
                        LoadLinks(arg, ref filepath);
                        break;
                    // 10. Case Lista
                    case "lista":
                        PrintLinks();
                        break;
                    // 11. Case Ny
                    case "ny":
                        CreateNewLink();
                        break;
                    // 12. Case Spara
                    case "spara":
                        SaveLinks(arg);
                        break;
                    // 13. Case Ta
                    case "ta":
                        OpenLink(arg);
                        break;
                    // 14. Case Öppna
                    case "öppna":
                        OpenLink(arg);
                        break;

                    // 15. We create the default if the command is unknown.
                    default:
                        Console.WriteLine($"Okänt kommando: '{command}'");
                        break;
                }
            }
            // 6. Usually having problems with curly braces so I'll put the curly braces here before I'll start working on the switch.
            while (true);
            // 16. We create a static void to make the ReadLinksFromFile work.
            static void ReadLinksFromFile(string filepath)
            {
                // 17. Creating streamreader which is a textreader.
                using (StreamReader sr = new StreamReader(filepath))
                {
                    int i = 0;
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        Console.WriteLine(line);
                        Link L = new Link(line);
                        L.Print(i++);
                        links.Add(L);
                        line = sr.ReadLine();
                    }
                }
            }
            // 18. Since we've already implemented a switch we don't need the else if.
            // 19. Created a static void to load links.

            //FIXME "Something about the filepath here is wrong. 
            static void LoadLinks(string[] arg, ref string loadFilePath)
            //FIX ME The LoadLinks(Arg) needed to add ref filepath because it's an returing reference.
            {
                if (arg.Length == 2)
                {
                    loadFilePath = $@"..\..\..\links\{arg[1]}";
                }
                links = new List<Link>();
                ReadLinksFromFile(loadFilePath);
            }
            // 21. Created a static void that print the links.
            static void PrintLinks()
            {
                int i = 0;
                foreach (Link L in links)
                    L.Print(i++);
            }
            // 20. We no longer need the if else:s.
            // 22. We create a static void that Prints help.
            // 
            static void PrintHelp()
            // 27. Now we need to make the print help actually help the user.
            {
                Console.WriteLine("sluta = Avslutar programmet ");
                Console.WriteLine("ladda = Laddar länkar från en fil");
                Console.WriteLine("lista = Visa alla länkarna");
                Console.WriteLine("ny = Lägg till en ny länk");
                Console.WriteLine("spara = Spara länkar till en fil");
                Console.WriteLine("ta = öppna länken med numret");
            }
            // 23. We create a static void that creates new links
            static void CreateNewLink()
            {
                Console.Write("Ange Kategori: ");
                string category = Console.ReadLine();

                Console.Write("Ange grupp: ");
                string group = Console.ReadLine();

                Console.Write("Ange namn: ");
                string name = Console.ReadLine();

                Console.Write("Ange beskrivning: ");
                string descr = Console.ReadLine();

                Console.Write("Ange länk ");
                string link = Console.ReadLine();

                Link newLink = new Link(category, group, name, descr, link);
                links.Add(newLink);

                Console.WriteLine("Länken har lagts till.");
            }
            // 24. Created a static void that saves links
            static void SaveLinks(string[] arg)
            {
                string saveFilePath = (arg.Length == 2) ? $@"..\..\..\links\{arg[1]}" : @"..\..\..\links\savedLinks.lis";
                using (StreamWriter sw = new StreamWriter(saveFilePath))
                {
                    foreach (Link L in links)
                    {
                        sw.WriteLine(L.ToString());
                    }
                }
                Console.WriteLine("Länkarna har sparats");
            }
            // 25. Created a static void that removes links.
            static void RemoveLink(string[] arg)
            {
                if (arg.Length != 2)
                {
                    Console.WriteLine("Felaktigt argument för att ta kommandot");
                    return;
                }

                int linkIndex;
                if (int.TryParse(arg[1], out linkIndex) && linkIndex >= 0 && linkIndex < links.Count)
                {
                    links.RemoveAt(linkIndex);
                    Console.WriteLine($"Länk {linkIndex} har tagits bort.");
                }
                else
                {
                    Console.WriteLine($"Ogiligt länknummer: {arg[1]}");
                }
            }
            // 26. Created a static void that opens links.
            static void OpenLink(string[] args)
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("Felaktigt argument för att öppna kommandot.");
                    return;
                }

                int linkIndex;
                if (int.TryParse(args[1], out linkIndex) && (linkIndex >= 0 && linkIndex < links.Count))
                {
                    Link selectedLink = links[linkIndex];
                    selectedLink.OpenLink();
                }
                else
                {
                    Console.WriteLine($"Ogiltigt länknummer: {args[1]}");
                }
            }
        }
    }
}

