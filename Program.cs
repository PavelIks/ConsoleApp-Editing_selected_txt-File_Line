using System;
using System.IO;

namespace EditTXTFileInFolder
{
    class CustomDirectory
    {
        private string Path = "";
        public CustomDirectory(string Path)
        {
            if (!Directory.Exists(Path)) throw new DirectoryNotFoundException();
            this.Path = Path;
        }

        private string GetFileSystemEntriesByNumber(int FileNumber)
        {
            return Directory.GetFileSystemEntries(Path)[FileNumber - 1];
        }

        public void ShowFiles()
        {
            if (Path == "")
            {
                throw new MethodAccessException("Path is empty.");
            }
            Console.WriteLine(Path);
            string[] FilesName = Directory.GetFileSystemEntries(Path);
            for (int i = 1; i <= FilesName.Length; i++)
            {
                Console.WriteLine($"{i}) {FilesName[i - 1]}");
            }
        }

        public void ShowFileContent(int Number)
        {
            string FileName = GetFileSystemEntriesByNumber(Number);
            //File.OpenWrite(FileName);
            Console.WriteLine(FileName + ":");
            string[] Content = File.ReadAllLines(FileName);
            for (int i = 1; i <= Content.Length; i++)
            {
                Console.WriteLine($"{i}){Content[i - 1]}");
            }
        }

        public bool isNumberStringExists(int FileNumber, int StringNumber)
        {
            try
            {
                var Try = File.ReadAllLines(GetFileSystemEntriesByNumber(FileNumber))[StringNumber - 1];
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void EditFileText(int Number, string Content, int NumberString)
        {
            string FileName = GetFileSystemEntriesByNumber(Number);
            string[] Strs = File.ReadAllLines(FileName);
            Strs[NumberString - 1] = Content;
            File.WriteAllLines(FileName, Strs);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CustomDirectory CD;
            while (true)
            {
                Console.Write("Введите путь до директории: ");
                try
                {
                    CD = new CustomDirectory(Console.ReadLine());
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Неверный путь.");
                    continue;
                }
                Console.WriteLine("Файлы данного пути");
                CD.ShowFiles();
                int number;
                while (true)
                {
                    Console.Write("Введите номер файла для записи: ");
                    try
                    {
                        CD.ShowFileContent(number = Convert.ToInt32(Console.ReadLine()));
                    }
                    catch (UnauthorizedAccessException exception)
                    {
                        Console.WriteLine("Данный файл заблокирован для записи.");
                        continue;
                    }
                    catch
                    {
                        Console.WriteLine("Неверный номер.");
                        continue;
                    }
                    break;
                }
                int str_num;
                while (true)
                {
                    Console.Write("Введите номер строки, который хотите редактировать: ");
                    if (!CD.isNumberStringExists(number, str_num = Convert.ToInt32(Console.ReadLine())))
                    {
                        Console.WriteLine("Неверная строка.");
                        continue;
                    }
                    break;
                }
                Console.Write("Введите текст, который хотите записать в файл: ");
                CD.EditFileText(number, Console.ReadLine(), str_num);
                Console.Clear();
                Console.WriteLine("Данные изменины.");
                Console.WriteLine("Нажмите любую кнопку, чтобы продолжить");
                Console.ReadKey(true);
                Console.Clear();
            }
        }
    }
}