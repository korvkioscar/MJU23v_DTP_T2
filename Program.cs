using System.Diagnostics;
//Removed System IO, Net and Xml. They are not used in the code.

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





                }

             }
                // 6. Usually having problems with curly braces so I'll put the curly braces here before I'll start working on the switch.
                while (true) ;
                {
                    int i = 0;
                    foreach (Link L in links)
                        L.Print(i++);
                }
                else if (command == "ny")
                {
                    Console.WriteLine("Skapa en ny länk:");
                    Console.Write("  ange kategori: ");
                    string category = Console.ReadLine();
                    Console.Write("  ange grupp: ");
                    string group = Console.ReadLine();
                    Console.Write("  ange namn: ");
                    string name = Console.ReadLine();
                    Console.Write("  ange beskrivning: ");
                    string descr = Console.ReadLine();
                    Console.Write("  ange länk: ");
                    string link = Console.ReadLine();
                    Link newLink = new Link(category, group, name, descr, link);
                    links.Add(newLink);
                }
                else if (command == "spara")
                {
                    if (arg.Length == 2)
                    {
                        filename = $@"..\..\..\links\{arg[1]}";
                    }
                    using (StreamWriter sr = new StreamWriter(filename))
                    {
                        foreach(Link L in links)
                        {
                            sr.WriteLine(L.ToString());
                        }
                    }
                }
                else if (command == "ta")
                {
                    if (arg[1] == "bort")
                    {
                        links.RemoveAt(Int32.Parse(arg[2]));
                    }
                }
                else if (command == "öppna")
                {
                    if (arg[1] == "grupp")
                    {
                        foreach (Link L in links)
                        {
                            if (L.group == arg[2])
                            {
                                L.OpenLink();
                            }
                        }
                    }
                    else if (arg[1] == "länk")
                    {
                        int ix = Int32.Parse(arg[2]);
                        links[ix].OpenLink();
                    }
                }
                else
                {
                    Console.WriteLine("Okänt kommando: '{command}'");
                }
            } while (true);
        }
    }
}