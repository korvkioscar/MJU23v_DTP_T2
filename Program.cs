using System.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//Added these again since I became uncertain.

namespace MJU23v_DTP_T2
{
    internal class Program
    {
        static List<Link> links = new List<Link>();
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
            public string ToFormattedString()
            {
                return $"{category}|{group}|{name}|{descr}|{link}";
            }
        }
        static void Main(string[] args)
        {
            // 2. We change names and method variable names.
            string filepath = @"..\..\..\links\links.lis";
            // 4. We implement a ReadLinksFromFile to start with.
            ReadLinksFromFile(filePath);
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
                        LoadLinks(arg);
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
            static void ReadLinksFromFile(string filePath)
            {
                // 17. Creating streamreader which is a textreader.
                using (StreamReader sr = new StreamReader(filePath))
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
            static void LoadLinks(string[] arg)
            {
                if (arg.Length == 2)
                {
                    filePath = $@"..\..\..\links\{arg[1]}";
                }
                links = new List<Link>();
                ReadLinksFromFile(filePath);
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
            static void PrintHelp()
            {
                Console.WriteLine("Hjälp            - skriv ut den här hjälpen");
                Console.WriteLine("sluta            - avsluta programmet");
            }
        }

