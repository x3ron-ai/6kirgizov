using converter6practa;
using System;

namespace kirgiz6stroika
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Путь до файла который надо открыть: ");
            string firstPath = Console.ReadLine();
            string text = Editor.Edit(Converter.GenerateString(firstPath));
            File.WriteAllText(firstPath, text);
            Console.Clear();
            Console.Write("Путь до файла в который надо сохранить: ");
            string secondPath = Console.ReadLine();
            Converter.GenerateFile(firstPath, secondPath);
            Console.WriteLine("Тут мои полномочия все");
        }
    }
}