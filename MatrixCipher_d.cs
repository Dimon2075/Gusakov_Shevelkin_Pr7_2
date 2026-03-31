using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pr7_2
{
    public class MatrixCipher_d
    {
        /// <summary>
        /// Метод дешифровки текста, восстановление исходного текста из зашифрованной строки.
        /// </summary>
        /// <param name="text">Зашифрованный текст.</param>
        /// <param name="rows">Количество строк в матрице.</param>
        /// <param name="cols">Количество столбцов в матрице.</param>
        /// <returns>Восстановленный исходный текст.</returns>
        public static string MatrixDecrypt(string text, int rows, int cols)
        {
            int size = rows * cols;
            // Дополняем текст до размера матрицы, если нужно
            if (text.Length < size)
                text = text.PadRight(size, ' ');

            char[,] matrix = new char[rows, cols];
            int index = 0;

            // Заполнение матрицы по столбцам (так мы читали при шифровании)
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    matrix[r, c] = text[index++];

            // Восстановление текста по строкам
            StringBuilder sb = new StringBuilder();
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    sb.Append(matrix[r, c]);

            // Удаление лишних пробелов в конце
            return sb.ToString().TrimEnd();
        }
    }
}
