using System;

namespace Lab5App
{
    class Program
    {
        static void Main()
        {
            Logger.Init(); // создаёт logs/app.log

            Console.WriteLine("Lab #5: IEnumerable, arrays and collections demo");
            Logger.Log("Program started.");

            int rows = ReadInt("Enter number of rows (m): ", 2);
            int cols = ReadInt("Enter number of columns (n): ", 3);
            int min = ReadInt("Enter min random value: ", 1);
            int max = ReadInt("Enter max random value: ", 9);

            var matrix = new MyMatrix(rows, cols, min, max);
            Logger.Log($"Created MyMatrix {rows}x{cols} range [{min},{max}]");

            Console.WriteLine("Full matrix:");
            matrix.Show();

            Console.WriteLine("\nPartial matrix (0..min(1,rows-1), 0..min(1,cols-1)):");
            matrix.ShowPartialy(0, Math.Min(1, matrix.Rows - 1), 0, Math.Min(1, matrix.Cols - 1));

            Console.WriteLine("\nChange size to +1 row and +2 cols:");
            matrix.ChangeSize(matrix.Rows + 1, matrix.Cols + 2);
            Logger.Log($"Changed size to {matrix.Rows}x{matrix.Cols}");
            matrix.Show();

            Console.WriteLine("\nDemonstrating MyList<int> with collection initializer:");
            var myList = new MyList<int> { 10, 20, 30 };
            Logger.Log("Created MyList and added 10,20,30");
            for (int i = 0; i < myList.Count; i++)
                Console.WriteLine($"myList[{i}] = {myList[i]}");

            Console.WriteLine("\nDemonstrating MyDictionary<string,int>:");
            var myDict = new MyDictionary<string, int>();
            myDict.Add("one", 1);
            myDict.Add("two", 2);
            Logger.Log("Added two items to MyDictionary");
            foreach (var kv in myDict)
                Console.WriteLine($"{kv.Key} -> {kv.Value}");

            Logger.Log("Program finished.");
        }

        static int ReadInt(string prompt, int defaultValue)
        {
            Console.Write(prompt);
            var s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s)) return defaultValue;
            if (int.TryParse(s.Trim(), out var v)) return v;
            return defaultValue;
        }
    }
}
