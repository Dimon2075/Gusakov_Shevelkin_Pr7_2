using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr7_2
{
    public class MatrixCipher
    {
        /// <summary>
        /// Метод шифрования текста по матрице с перестановкой по столбцам.
        /// </summary>
        /// <param name="text">Исходный текст.</param>
        /// <param name="rows">Количество строк матрицы.</param>
        /// <param name="cols">Количество столбцов матрицы.</param>
        /// <returns>Зашифрованный текст.</returns>
        public static string MatrixEncrypt(string text, int rows, int cols)
        {
            int size = rows * cols;
            // Дополняем текст пробелами, чтобы он точно заполнил матрицу
            text = text.PadRight(size, ' ');

            char[,] matrix = new char[rows, cols];
            int index = 0;

            // Заполнение матрицы построчно
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    matrix[r, c] = text[index++];

            // Чтение по столбцам
            StringBuilder sb = new StringBuilder();
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    sb.Append(matrix[r, c]);

            return sb.ToString();
        }
    }
}
