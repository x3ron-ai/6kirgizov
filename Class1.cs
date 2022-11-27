using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace converter6practa
{
    public class Editor
    {
        public static string Edit(string text)
        {
            Console.Clear();
            string[] lines = text.Split('\n');
            ConsoleKey key = new ConsoleKey();
            int max, pos = 1;
            while (key != ConsoleKey.F1)
            {
                Console.Clear();
                Console.WriteLine("Для выхода нажать F1");
                max = 0;
                foreach(string line in lines)
                {
                    max++;
                    Console.WriteLine("    "+line);
                }
                Console.SetCursorPosition(0, pos);
                Console.Write("|>");
                key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (pos == 1)
                            pos = max;
                        else
                            pos--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (pos == max)
                            pos = 1;
                        else pos++;
                        break;
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(4, pos);
                        foreach(char c in lines[pos-1])
                            Console.Write(' ');
                        Console.SetCursorPosition(4, pos);
                        lines[pos - 1] = Console.ReadLine();
                        break;
                }
            }
            return string.Join("\n", lines);
        }
    }
    public class Figura
    {
        public string name;
        public int width;
        public int height;
        public Figura()
        {

        }
        public Figura(string Name, int Height, int Width)
        {
            name = Name;
            height = Height;
            width = Width;
        }
    }
    public class Converter
    {
        public static void GenerateFile(string openPath, string savePath)
        {
            List<Figura> figures = GenerateList(openPath);
            if (openPath.Contains(".txt"))
            {
                File.WriteAllText(savePath, GenerateString(openPath));
            }
            else if (openPath.Contains(".json"))
            {
                File.WriteAllText(savePath, JsonConvert.SerializeObject(figures));
            }
            else if (openPath.Contains(".xml"))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Figura>));
                using (FileStream fs = new FileStream(savePath, FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, figures);
                }
            }
        }
        public static string GenerateString(string path)
        {
            string response = "";
            foreach (Figura figura in GenerateList(path))
            {
                response += figura.name + '\n' + figura.width + '\n' + figura.height + '\n';
            }
            return response;
        }
        private static List<Figura> GenerateList(string path)
        {
            List<Figura> figures = new List<Figura>();
            if (path.Contains(".json"))
            {
                string text = File.ReadAllText(path);
                figures = JsonConvert.DeserializeObject<List<Figura>>(text);
            }
            else if (path.Contains(".xml"))
            {
                XmlSerializer xml = new XmlSerializer(typeof(List<Figura>));
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    figures = (List<Figura>)xml.Deserialize(fs);
                }
            }
            else if (path.Contains(".txt"))
            {
                string[] text = File.ReadAllLines(path);
                for (int i = 0; i < text.GetLength(0); i = i + 3)
                {
                    Figura figura = new Figura();
                    if (i+2 < text.Length )
                    {
                        figura.name = text[i];
                        figura.width = Convert.ToInt32(text[i + 1]);
                        figura.height = Convert.ToInt32(text[i + 2]);
                        figures.Add(figura);
                    }
                    else break;
                }
            }
            return figures;
        }
    }
}