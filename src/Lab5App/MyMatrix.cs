using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab5App
{
    public class MyMatrix : IEnumerable<int>
    {
        private readonly Random rnd = new();
        private int[,] data;
        private int min, max;

        public int Rows => data.GetLength(0);
        public int Cols => data.GetLength(1);

        public MyMatrix(int rows, int cols, int min, int max)
        {
            if (rows <= 0) rows = 1;
            if (cols <= 0) cols = 1;
            this.min = min;
            this.max = max;
            data = new int[rows, cols];
            Fill();
        }

        public void Fill()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    data[i, j] = rnd.Next(min, max + 1);
            Logger.Log($"Matrix filled with random values in [{min},{max}].");
        }

        public void ChangeSize(int newRows, int newCols)
        {
            if (newRows <= 0) newRows = 1;
            if (newCols <= 0) newCols = 1;

            var newData = new int[newRows, newCols];
            int copyRows = Math.Min(newRows, Rows);
            int copyCols = Math.Min(newCols, Cols);

            for (int i = 0; i < copyRows; i++)
                for (int j = 0; j < copyCols; j++)
                    newData[i, j] = data[i, j];

            int oldRows = Rows;
            int oldCols = Cols;
            data = newData;

            // заполнить новые ячейки
            for (int i = 0; i < newRows; i++)
                for (int j = 0; j < newCols; j++)
                    if (i >= oldRows || j >= oldCols)
                        data[i, j] = rnd.Next(min, max + 1);

            Logger.Log($"Matrix resized to {newRows}x{newCols}.");
        }

        public void ShowPartialy(int rowFrom, int rowTo, int colFrom, int colTo)
        {
            rowFrom = Math.Max(0, rowFrom);
            colFrom = Math.Max(0, colFrom);
            rowTo = Math.Min(Rows - 1, Math.Max(rowFrom, rowTo));
            colTo = Math.Min(Cols - 1, Math.Max(colFrom, colTo));

            for (int i = rowFrom; i <= rowTo; i++)
            {
                for (int j = colFrom; j <= colTo; j++)
                    Console.Write($"{data[i, j],4}");
                Console.WriteLine();
            }
            Logger.Log($"ShowPartialy rows {rowFrom}-{rowTo} cols {colFrom}-{colTo}.");
        }

        public void Show()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                    Console.Write($"{data[i, j],4}");
                Console.WriteLine();
            }
            Logger.Log("Shown full matrix.");
        }

        public int this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= Rows || j < 0 || j >= Cols) throw new IndexOutOfRangeException();
                return data[i, j];
            }
            set
            {
                if (i < 0 || i >= Rows || j < 0 || j >= Cols) throw new IndexOutOfRangeException();
                data[i, j] = value;
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    yield return data[i, j];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
